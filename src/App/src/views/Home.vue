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
            <base-button
              type="primary"
              icon="fa fa-file-o mr-2"
              @click="modals.createNew.show = true"
            >{{ $t('home.createNew') }}</base-button>
          </div>
          <div class="text-right w-100">
            <router-link
              type="button"
              class="btn btn-icon btn-secondary"
              :to="{ name: 'settings' }"
            >
              <span class="btn-inner--icon">
                <i class="fa fa-cogs mr-2"></i>
              </span>
              <span class="btn-inner--text">{{ $t('home.settings') }}</span>
            </router-link>
          </div>
        </div>
      </section>
      <!-- 1st Hero Variation -->
    </div>
    <section class="section section-lg pt-lg-0 mt--200">
      <div class="container">
        <div class="row justify-content-center">
          <div class="col-lg-12">
            <card class="border-0" shadow body-classes="py-5">
              <table class="table table-bordered table-hover">
                <thead>
                  <tr>
                    <th scope="col">{{ $t('home.project') }}</th>
                    <th scope="col">{{ $t('home.сreationDate') }}</th>
                    <th scope="col">{{ $t('home.lastUpdateDate') }}</th>
                    <th scope="col"></th>
                  </tr>
                </thead>
                <tbody>
                  <tr v-for="row in rows" v-bind:key="row.id">
                    <th scope="row">
                      <router-link
                        :to="{ name: 'project', params: { projectId: row.id }}"
                      >{{ row.name }}</router-link>
                    </th>
                    <td>{{ frontEndDateFormat(row.creationDate) }}</td>
                    <td>{{ frontEndDateFormat(row.lastUpdatedDate) }}</td>
                    <td>
                      <base-dropdown>
                        <base-button
                          slot="title"
                          type="primary"
                          class="dropdown-toggle"
                        >{{ $t('home.actions') }}</base-button>
                        <router-link
                          class="dropdown-item"
                          :to="{ name: 'project', params: { projectId: row.id }}"
                        >{{ $t('home.edit') }}</router-link>
                        <a
                          class="dropdown-item"
                          href="#"
                          @click="deleteModal(row.id)"
                        >{{ $t('home.delete') }}</a>
                        <div class="dropdown-divider"></div>
                        <a
                          class="dropdown-item"
                          href="#"
                          @click="exportFile(row.id)"
                        >{{ $t('home.export') }}</a>
                      </base-dropdown>
                    </td>
                  </tr>
                </tbody>
              </table>
            </card>
          </div>
        </div>
      </div>
    </section>
    <modal :show.sync="modals.createNew.show">
      <template slot="header">
        <h5 class="modal-title" id="exampleModalLabel">{{ $t('project.createNewProject') }}</h5>
      </template>
      <div>
        <div class="row">
          <div class="col-md-12">
            <label>{{ $t('project.name') }}</label>
            <base-input v-model="modals.createNew.name"></base-input>
          </div>
          <div class="col-md-12">
            <label>{{ $t('home.parentTemplate') }}</label>
            <select v-model="modals.createNew.parentId" class="form-control">
              <option :value="undefined" selected>{{ $t('project.default') }}</option>
              <option v-for="row in rows" v-bind:key="row.id" v-bind:value="row.id">{{ row.name }}</option>
            </select>
          </div>
        </div>
      </div>
      <template slot="footer">
        <base-button type="secondary" @click="modals.createNew.show = false">{{ $t('base.close') }}</base-button>
        <base-button @click="createProject" type="primary">{{ $t('base.create') }}</base-button>
      </template>
    </modal>
    <modal
      :show.sync="modals.delete.show"
      gradient="danger"
      modal-classes="modal-danger modal-dialog-centered"
    >
      <div class="py-3 text-center">
        <i class="ni ni-fat-remove ni-5x"></i>
        <h4 class="heading mt-4">{{ $t('base.confirmation') }}</h4>
        <p>{{ $t('home.areYouSureDeleteProject') }}</p>
      </div>

      <template slot="footer">
        <base-button type="white" @click="deleteProject">{{ $t('base.yes') }}</base-button>
        <base-button
          type="link"
          text-color="white"
          class="ml-auto"
          @click="modals.delete.show = false"
        >{{ $t('base.cancel') }}</base-button>
      </template>
    </modal>
  </div>
</template>

<script>
import moment from "moment";
import BaseDropdown from "@/components/BaseDropdown";
import StudyRepository from "../repositories/StudyRepository";
import Modal from "@/components/Modal";
import Client from "../client";

export default {
  components: {
    BaseDropdown,
    Modal
  },
  data() {
    return {
      rows: [],
      modals: {
        createNew: {
          show: false,
          name: undefined,
          parentId: undefined
        },
        delete: {
          show: false,
          id: undefined
        }
      }
    };
  },
  mounted() {
    this.doLoad();
  },
  methods: {
    doLoad() {
      StudyRepository.getAll()
        .then(response => {
          this.rows = response.data;
        })
        .catch(error => {
          console.log(error);
        });
    },

    createProject() {
      StudyRepository.create({
        name: this.modals.createNew.name,
        parentId: this.modals.createNew.parentId
      })
        .then(response => {
          this.$router.push({
            name: "project",
            params: {
              projectId: response.data.id
            }
          });
        })
        .catch(error => {
          console.log(error);
        });

      this.modals.createNew.show = false;
      this.modals.createNew.name = undefined;
      this.modals.createNew.parentId = undefined;
    },

    deleteModal(id) {
      this.modals.delete.show = true;
      this.modals.delete.id = id;
    },

    deleteProject() {
      StudyRepository.delete(this.modals.delete.id)
        .then(response => {
          this.doLoad();
        })
        .catch(error => {
          console.log(error);
        });

      this.modals.delete.show = false;
      this.modals.delete.id = undefined;
    },

    exportFile: function(id) {
      Client.get("/export/" + id, {
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
    },

    frontEndDateFormat: function(date) {
      return moment(date, "YYYY-MM-DDTHH:mm:ss").format("DD/MM/YYYY HH:mm:ss");
    }
  }
};
</script>
