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
            <h1 class="display-3 text-white">{{ name }}</h1>
          </div>
          <div class="text-right w-100">
            <router-link
              type="button"
              class="btn btn-icon btn-info"
              :to="{ name: 'project-settings' }"
            >
              <span class="btn-inner--icon">
                <i class="fa fa-cogs mr-2"></i>
              </span>
              <span class="btn-inner--text">{{ $t('project.settings') }}</span>
            </router-link>
            <base-button
              type="primary"
              icon="fa fa-arrow-circle-o-down mr-2"
              @click="exportFile"
            >{{ $t('project.export') }}</base-button>
          </div>
        </div>
      </section>
      <!-- 1st Hero Variation -->
    </div>
    <section class="section section-lg pt-lg-0 mt--200">
      <div class="container">
        <card class="border-0" shadow body-classes="py-5">
          <div class="row">
            <div class="col-md-6">
              <base-input v-model="name" placeholder="Project name"></base-input>
            </div>
            <div class="col-md-3">
              <base-button
                block
                type="danger"
                icon="fa fa-undo mr-2"
                @click="modals.restore = true"
              >{{ $t('base.restore') }}</base-button>
            </div>
            <div class="col-md-3">
              <base-button
                block
                type="success"
                icon="fa fa-floppy-o mr-2"
                @click="saveProject"
              >{{ $t('base.save') }}</base-button>
            </div>
          </div>
          <div class="row" v-for="(item, index) in messages" v-bind:key="index">
            <div class="col-md-12">
              <base-alert type="info" v-if="item.severityLevel == 0 && item.message != null">
                <strong>{{ $t('errors.info') }}</strong>
                {{ $t(item.message) }}
              </base-alert>
              <base-alert type="warning" v-if="item.severityLevel == 1 && item.message != null">
                <strong>{{ $t('errors.warning') }}</strong>
                {{ $t(item.message) }}
              </base-alert>
              <base-alert type="danger" v-if="item.severityLevel == 2 && item.message != null">
                <strong>{{ $t('errors.error') }}</strong>
                {{ $t(item.message) }}
              </base-alert>
            </div>
          </div>
          <div class="row">
            <div class="col-lg-12">
              <slim-grid
                :height="-1"
                :autoHeight="true"
                :data="data"
                :editable="true"
                :autoEdit="true"
                :grouping="gridGrouping"
                :column-options="columnOptions"
                :row-formatter="rowFormatter"
                :show-pager="false"
                :showHeaderRow="false"
                :forceFitColumns="true"
                v-on:cell-change="doValidate"
              />
            </div>
          </div>
        </card>
      </div>
    </section>
    <modal
      :show.sync="modals.restore"
      gradient="danger"
      modal-classes="modal-danger modal-dialog-centered"
    >
      <div class="py-3 text-center">
        <i class="ni ni-fat-remove ni-5x"></i>
        <h4 class="heading mt-4">{{ $t('base.confirmation') }}</h4>
        <p>{{ $t('project.areYouSureRestore') }}</p>
        <p>{{ $t('project.allChangesWillBeLost') }}</p>
      </div>

      <template slot="footer">
        <base-button type="white" @click="restoreProject">{{ $t('base.yes') }}</base-button>
        <base-button
          type="link"
          text-color="white"
          class="ml-auto"
          @click="modals.restore = false"
        >{{ $t('base.cancel') }}</base-button>
      </template>
    </modal>
  </div>
</template>

<script>
import { Data, Editors } from "slickgrid-es6";
import SlimGrid from "vue-slimgrid";
import StudyRepository from "../repositories/StudyRepository";
import Client from "../client";
import Modal from "@/components/Modal";

export default {
  components: {
    SlimGrid,
    Modal
  },
  data() {
    return {
      name: undefined,
      data: [],
      messages: [],
      modals: {
        restore: false
      },
      gridGrouping: [
        {
          getter: "studyCourse",
          aggregators: [
            new Data.Aggregators.Sum("class_0"),
            new Data.Aggregators.Sum("class_1"),
            new Data.Aggregators.Sum("class_2")
          ],
          aggregateCollapsed: false,
          lazyTotalsCalculation: true,
          formatter(g) {
            console.log(g);
            return (
              "<div class='colorbox' style='cursor:pointer;background-color:" +
              g.rows[0].backgroundColor +
              ";top:0;position:absolute;left: 20px;padding:2px;'>" +
              g.value +
              "</div>"
            );
          }
        },
        {
          getter: "studyGroup",
          formatter(g) {
            return (
              g.value + ' <span style="color:green">(' + g.count + ")</span>"
            );
          },
          aggregators: [
            new Data.Aggregators.Sum("class_0"),
            new Data.Aggregators.Sum("class_1"),
            new Data.Aggregators.Sum("class_2")
          ],
          aggregateCollapsed: false,
          lazyTotalsCalculation: true
        }
      ],
      columnOptions: {
        "*": {
          headerFilter: false,
          sortable: false
        },
        severityLevel: {
          hidden: true
        },
        studyCourse: {
          hidden: true
        },
        studyGroup: {
          hidden: true
        },
        backgroundColor: {
          hidden: true
        },
        studyName: {
          name: this.$t("project.study"),
          minWidth: 150
        },
        class_0: {
          name: this.$t("project.class10"), //"10. klasse",
          editor: Editors.Integer,
          totalText: this.$t("project.total"),
          groupTotalsFormatter(totals, columnDef) {
            let val = totals.sum && totals.sum[columnDef.field];
            if (val != null) {
              return (
                this.totalText + ": " + Math.round(parseFloat(val) * 100) / 100
              );
            }
            return "";
          }
        },
        class_1: {
          name: this.$t("project.class11"), //"11. klasse",
          editor: Editors.Integer,
          totalText: this.$t("project.total"),
          groupTotalsFormatter(totals, columnDef) {
            let val = totals.sum && totals.sum[columnDef.field];
            if (val != null) {
              return (
                this.totalText + ": " + Math.round(parseFloat(val) * 100) / 100
              );
            }
            return "";
          }
        },
        class_2: {
          name: this.$t("project.class12"), //"12. klasse",
          editor: Editors.Integer,
          totalText: this.$t("project.total"),
          groupTotalsFormatter(totals, columnDef) {
            let val = totals.sum && totals.sum[columnDef.field];
            if (val != null) {
              return (
                this.totalText + ": " + Math.round(parseFloat(val) * 100) / 100
              );
            }
            return "";
          }
        }
      },
      rowFormatter(row) {
        console.log(row["severityLevel"]);
        
        return {
          cssClasses:
            row["severityLevel"] == 1
              ? "backWarning"
              : row["severityLevel"] == 2
              ? "backError"
              : null
        };
      }
    };
  },

  mounted() {
    this.doLoad();
  },

  methods: {
    doLoad() {
      this.data = [];

      StudyRepository.get(this.$route.params.projectId)
        .then(response => {
          this.name = response.data.project.name;
          this.messages = response.data.messages;

          for (let i = 0; i < response.data.project.courses.length; i++) {
            let data = response.data.project.courses[i];

            // If groups > 0
            if (data.groups.length > 0) {
              for (let y = 0; y < data.groups.length; y++) {
                let groupData = data.groups[y];

                // If studies > 0
                if (groupData.studies.length > 0) {
                  for (let n = 0; n < groupData.studies.length; n++) {
                    let studyData = groupData.studies[n];

                    let row = {
                      id: studyData.treeId,
                      studyCourse: data.courseName,
                      studyGroup: groupData.groupName,
                      studyName: studyData.studyName,
                      backgroundColor: data.backgroundColor,
                      severityLevel: 0
                    };

                    if (response.data.messages.length > 0) {
                      for (let i = 0; i < response.data.messages.length; i++) {
                        var message = response.data.messages[i];

                        if (message.treeId == studyData.treeId) {
                          row.severityLevel = message.severityLevel;
                        }
                      }
                    }

                    // Class 10 -> 12
                    for (let c = 0; c < studyData.creditPoints.length; c++) {
                      row["class_" + c] = studyData.creditPoints[c];
                    }

                    this.data.push(row);
                  }
                } else {
                  let row = {
                    id: groupData.treeId,
                    studyCourse: data.courseName,
                    studyGroup: groupData.groupName,
                    backgroundColor: data.backgroundColor,
                    severityLevel: 0
                  };

                  if (response.data.messages.length > 0) {
                    for (let i = 0; i < response.data.messages.length; i++) {
                      var message = response.data.messages[i];

                      if (message.treeId == groupData.treeId) {
                        row.severityLevel = message.severityLevel;
                      }
                    }
                  }

                  this.data.push(row);
                }
              }
            } else {
              let row = {
                id: data.treeId,
                studyCourse: data.courseName,
                backgroundColor: data.backgroundColor,
                severityLevel: 0
              };

              if (response.data.messages.length > 0) {
                for (let i = 0; i < response.data.messages.length; i++) {
                  var message = response.data.messages[i];

                  if (message.treeId == data.treeId) {
                    row.severityLevel = message.severityLevel;
                  }
                }
              }

              this.data.push(row);
            }
          }
        })
        .catch(error => {
          console.log(error);
          //this.errored = true
        });
      //.finally(() => this.loading = false)
    },

    doValidate(e, args) {
      StudyRepository.update(
        {
          treeId: args.slim.pk,
          column: args.slim.column,
          value: args.slim.value
        },
        this.$route.params.projectId
      )
        .then(response => {
          this.messages = response.data;
        })
        .catch(error => {
          console.log(error);
          //this.doLoad();
        });
    },

    saveProject() {
      StudyRepository.save(
        {
          name: this.name
        },
        this.$route.params.projectId
      ).catch(error => {
        console.log(error);
      });
    },

    restoreProject() {
      StudyRepository.restore(this.$route.params.projectId)
        .then(response => {
          this.doLoad();
        })
        .catch(error => {
          console.log(error);
        });

      this.modals.restore = false;
    },

    exportFile: function() {
      Client.get("/export/" + this.$route.params.projectId, {
        responseType: "blob"
      })
        .then(response => {
          const url = window.URL.createObjectURL(new Blob([response.data]));
          const link = document.createElement("a");
          link.href = url;
          link.setAttribute("download", "export.xlsx");
          document.body.appendChild(link);
          link.click();
        })
        .catch(error => console.error(error));
    }
  }
};
</script>

<style src="vue-slimgrid/dist/slimgrid.css"></style>
<style>
input.editor-text {
  position: absolute;
  width: 100%;
  height: 100%;
  border: 0;
  margin: 0;
  background: transparent;
  padding: 2px 3px 2px 3px;
  transform: translate(-3px, -2px);
}
</style>
<style>
.backError {
  background-color: lightpink;
}
.backWarning {
  background-color: lightsalmon;
}
</style>
