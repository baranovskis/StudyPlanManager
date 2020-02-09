<template>
  <div>
    <div class="position-relative">
      <!-- shape Hero -->
      <section class="section-shaped my-0">
        <div class="shape shape-style-1 shape-primary shape-skew">
          <span></span>
          <span></span>
          <span></span>
          <span></span>
          <span></span>
          <span></span>
          <span></span>
          <span></span>
          <span></span>
        </div>
        <div class="container shape-container d-flex">
          <div class="text-left w-100">
            <h1 class="display-3 text-white">{{ $t('home.settings') }}</h1>
          </div>
        </div>
      </section>
      <!-- 1st Hero Variation -->
    </div>
    <section class="section section-lg pt-lg-0 mt--200">
      <div class="container">
        <card class="border-0" shadow body-classes="py-5">
          <div class="container">
            <div class="row">
              <div class="col-md-4">
                <base-button
                  block
                  type="warning"
                  icon="fa fa-undo mr-2"
                  @click="goBack"
                >{{ $t('base.cancel') }}</base-button>
              </div>
              <div class="col-md-8">
                <base-button
                  block
                  type="success"
                  icon="fa fa-floppy-o mr-2"
                  @click="saveSettings"
                >{{ $t('base.save') }}</base-button>
              </div>
            </div>
            <div class="row py-3">
              <div class="col-lg-12">
                <slim-grid
                  :height="-1"
                  :autoHeight="true"
                  :data="studies"
                  :editable="true"
                  :autoEdit="true"
                  :column-options="studiesOptions"
                  :show-pager="false"
                  :showHeaderRow="false"
                  :forceFitColumns="true"
                  v-on:grid-dbl-click="onGridDoubleClick"
                  v-on:grid-click="onGridClick"
                />
              </div>
            </div>
            <div class="row py-3">
              <div class="col-lg-12">
                <slim-grid
                  :height="-1"
                  :autoHeight="true"
                  :data="groups"
                  :editable="true"
                  :autoEdit="true"
                  :column-options="groupsOptions"
                  :show-pager="false"
                  :showHeaderRow="false"
                  :forceFitColumns="true"
                  v-on:grid-click="onGridClick"
                />
              </div>
            </div>
            <div class="row py-3">
              <div class="col-lg-12">
                <slim-grid
                  :height="-1"
                  :autoHeight="true"
                  :data="courses"
                  :editable="true"
                  :autoEdit="true"
                  :column-options="coursesOptions"
                  :show-pager="false"
                  :showHeaderRow="false"
                  :forceFitColumns="true"
                  v-on:grid-click="onGridClick"
                />
              </div>
            </div>
          </div>
        </card>
      </div>
    </section>
  </div>
</template>

<script>
import { Data, Editors, Formatters } from "slickgrid-es6";
import SlimGrid from "vue-slimgrid";
import ColorPicker from "vue-color-picker-wheel";
import SettingsRepository from "../repositories/SettingsRepository";
import $ from "jquery";
import spectrum from "spectrum-colorpicker";
import { constants } from "buffer";

export default {
  components: {
    SlimGrid,
    ColorPicker
  },
  data() {
    return {
      studies: [],
      groups: [],
      courses: [],

      studiesOptions: {
        "*": {
          headerFilter: false,
          sortable: false
        },

        studyName: {
          name: this.$t("settings.studyName"),
          minWidth: 150,
          editor: Editors.Text
        },

        creditPointLimit: {
          name: this.$t("settings.creditPointLimit"),
          editor: Editors.Integer
        },

        isObligatory: {
          name: this.$t("settings.isObligatory"),
          formatter: Formatters.Checkmark
        },

        action: {
          name: this.$t("settings.actions"),
          formatter: this.studyButtonsFormatter
        }
      },

      groupsOptions: {
        "*": {
          headerFilter: false,
          sortable: false
        },

        groupName: {
          name: this.$t("settings.groupName"),
          minWidth: 150,
          editor: Editors.Text
        },

        minimalStudyCount: {
          name: this.$t("settings.minimalStudyCount"),
          editor: Editors.Integer
        },

        action: {
          name: this.$t("settings.actions"),
          formatter: this.groupButtonsFormatter
        }
      },

      coursesOptions: {
        "*": {
          headerFilter: false,
          sortable: false
        },

        courseName: {
          name: this.$t("settings.courseName"),
          minWidth: 150,
          editor: Editors.Text
        },

        backgroundColor: {
          name: this.$t("settings.backgroundColor"),
          formatter: this.customColorFormatter,
          editor: this.customColorEditor
        },

        action: {
          name: this.$t("settings.actions"),
          formatter: this.courseButtonsFormatter
        }
      }
    };
  },
  mounted() {
    this.doLoad();
  },
  methods: {
    doLoad() {
      SettingsRepository.get()
        .then(response => {
          // Studies
          for (let i = 0; i < response.data.studies.length; i++) {
            var data = response.data.studies[i];

            let row = {
              id: data.treeId,
              studyName: data.studyName,
              creditPointLimit: data.creditPointLimit,
              isObligatory: data.isObligatory,
              action: undefined
            };

            this.studies.push(row);
          }

          // Groups
          for (let i = 0; i < response.data.groups.length; i++) {
            var data = response.data.groups[i];

            let row = {
              id: data.treeId,
              groupName: data.groupName,
              minimalStudyCount: data.minimalStudyCount,
              action: undefined
            };

            this.groups.push(row);
          }

          // Courses
          for (let i = 0; i < response.data.courses.length; i++) {
            var data = response.data.courses[i];

            let row = {
              id: data.treeId,
              courseName: data.courseName,
              backgroundColor: data.backgroundColor,
              action: undefined
            };

            this.courses.push(row);
          }
        })
        .catch(error => {
          console.log(error);
          //this.errored = true
        });
      //.finally(() => this.loading = false)
    },

    saveSettings() {
      let studies = [...this.studies];
      let groups = [...this.groups];
      let courses = [...this.courses];

      studies.forEach(function(obj) {
        obj.treeId = obj.id;
        delete obj.id;
      });

      groups.forEach(function(obj) {
        obj.treeId = obj.id;
        delete obj.id;
      });

      courses.forEach(function(obj) {
        obj.treeId = obj.id;
        delete obj.id;
      });

      SettingsRepository.save({
        studies: studies,
        groups: groups,
        courses: courses
      }).catch(error => {
        console.log(error);
      });

      this.goBack();
    },

    goBack() {
      this.$router.go(-1);
    },

    onGridClick(e, args) {
      if ($(e.target).hasClass("delStudy")) {
        if (!args.grid.getEditorLock().commitCurrentEdit()) {
          return;
        }

        this.studies.splice(args.row, 1);
        args.grid.updateRowCount();
        args.grid.render();
      } else if ($(e.target).hasClass("delGroup")) {
        if (!args.grid.getEditorLock().commitCurrentEdit()) {
          return;
        }

        this.groups.splice(args.row, 1);
        args.grid.updateRowCount();
        args.grid.render();
      } else if ($(e.target).hasClass("delCourse")) {
        if (!args.grid.getEditorLock().commitCurrentEdit()) {
          return;
        }

        this.courses.splice(args.row, 1);
        args.grid.updateRowCount();
        args.grid.render();
      } else if ($(e.target).hasClass("newStudy")) {
        if (!args.grid.getEditorLock().commitCurrentEdit()) {
          return;
        }

        let row = {
          id: this.getNewId(),
          studyName: "",
          creditPointLimit: 0,
          isObligatory: false,
          action: undefined
        };

        this.studies.push(row);

        args.grid.updateRowCount();
        args.grid.render();
      } else if ($(e.target).hasClass("newGroup")) {
        if (!args.grid.getEditorLock().commitCurrentEdit()) {
          return;
        }

        let row = {
          id: this.getNewId(),
          groupName: "",
          minimalStudyCount: 0,
          action: undefined
        };

        this.groups.push(row);

        args.grid.updateRowCount();
        args.grid.render();
      } else if ($(e.target).hasClass("newCourse")) {
        if (!args.grid.getEditorLock().commitCurrentEdit()) {
          return;
        }

        let row = {
          id: this.getNewId(),
          courseName: "",
          backgroundColor: "#fff",
          action: undefined
        };

        this.courses.push(row);

        args.grid.updateRowCount();
        args.grid.render();
      }
    },

    getNewId() {
      var dt = new Date().getTime();
      var uuid = "xxxxxxxxxxxx4xxxyxxxxxxxxxxxxxxx".replace(/[xy]/g, function(
        c
      ) {
        var r = (dt + Math.random() * 16) % 16 | 0;
        dt = Math.floor(dt / 16);
        return (c == "x" ? r : (r & 0x3) | 0x8).toString(16);
      });

      return uuid;
    },

    onGridDoubleClick(e, args) {
      let column = args.grid.getColumns()[args.cell].id;

      if (column == "isObligatory") {
        if (!args.grid.getEditorLock().commitCurrentEdit()) {
          return;
        }

        this.studies[args.row].isObligatory = !this.studies[args.row]
          .isObligatory;
        args.grid.updateRow(args.row);
        e.stopPropagation();
      }
    },

    customColorEditor(args) {
      var $input = $("<div class='colorbox'></div>");
      var defaultValue;
      var scope = this;

      this.init = function() {
        // make the background look exactly the same when the color is being
        // edited
        $(args.container).css("background-color", "#fff");

        $input.appendTo($(args.container));
        $input.css("background-color", args.item.value);

        // initialize spectrum
        $input.spectrum({
          color: args.item.value,
          showInput: true,
          allowEmpty: false,
          showPalette: false,
          showInitial: true,
          clickoutFiresChange: true,
          className: "full-spectrum",
          preferredFormat: "hex6",

          /* On change callback */
          change: function(color) {
            $input.css("background-color", color.toHexString());

            // commit the changes as soon as a new shape is selected
            // https://stackoverflow.com/a/35768360/379593
            args.grid.getEditorLock().commitCurrentEdit();
            args.grid.resetActiveCell();
          }
        });

        $input
          .spectrum("container")
          .find(".sp-input")
          .on("keydown keypress", function(e) {
            e.stopPropagation();
          });
      };

      this.destroy = function() {
        $input.spectrum("hide");
        $input.spectrum("destroy");
        $input.remove();
      };

      this.focus = function() {
        $input.focus();
        $input.spectrum("show");
      };

      this.isValueChanged = function() {
        return $input.spectrum("get").toHexString() !== defaultValue;
      };

      this.serializeValue = function() {
        return $input.spectrum("get").toHexString();
      };

      this.loadValue = function(item) {
        defaultValue = item[args.column.field];
        $input.spectrum("set", defaultValue);
      };

      this.applyValue = function(item, state) {
        item[args.column.field] = state;
      };

      this.validate = function() {
        return {
          valid: true,
          msg: null
        };
      };

      this.hide = function() {
        $input.spectrum("hide");
      };

      this.show = function() {
        setTimeout(function() {
          $input.spectrum("show");
        }, 100);
      };

      this.position = function(cellBox) {
        $input.spectrum("reflow");
      };

      this.init();
    },

    customColorFormatter(row, cell, value, columnDef, dataContext) {
      return (
        "<div class='colorbox' style='cursor:pointer;background-color:" +
        value +
        ";'></div>"
      );
    },

    studyButtonsFormatter(row, cell, value, columnDef, dataContext) {
      let addText = this.$t("settings.addNew");
      let deleteText = this.$t("settings.delete");

      return (
        '<a class="newStudy text-primary"><i class="fa fa-plus-circle"></i> ' +
        addText +
        '</a> | <a class="delStudy text-danger"><i class="fa fa-minus-circle"></i> ' +
        deleteText +
        "</a>"
      );
    },

    groupButtonsFormatter(row, cell, value, columnDef, dataContext) {
      let addText = this.$t("settings.addNew");
      let deleteText = this.$t("settings.delete");

      return (
        '<a class="newGroup text-primary"><i class="fa fa-plus-circle"></i> ' +
        addText +
        '</a> | <a class="delGroup text-danger"><i class="fa fa-minus-circle"></i> ' +
        deleteText +
        "</a>"
      );
    },

    courseButtonsFormatter(row, cell, value, columnDef, dataContext) {
      let addText = this.$t("settings.addNew");
      let deleteText = this.$t("settings.delete");

      return (
        '<a class="newCourse text-primary"><i class="fa fa-plus-circle"></i> ' +
        addText +
        '</a> | <a class="delCourse text-danger"><i class="fa fa-minus-circle"></i> ' +
        deleteText +
        "</a>"
      );
    }
  }
};
</script>

<style scoped>
</style>
