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
                  <a href="#" @click="modals.createNew = true" rel="noopener"
                       class="btn btn-primary btn-icon">
                        <span class="btn-inner--icon">
                        <i class="fa fa-file-o mr-2"></i>
                        </span>
                        <span class="nav-link-inner--text">Create New</span>
                    </a>
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
                                <th scope="col">Project</th>
                                <th scope="col">Creation Date</th>
                                <th scope="col">Last Update Date</th>
                                <th scope="col"></th>
                              </tr>
                            </thead>
                            <tbody>
                              <tr v-for="row in rows" v-bind:key="row.id">
                                <th scope="row">
                                  <router-link :to="{ name: 'project', params: { projectId: row.id }}">
                                    {{ row.name }}
                                  </router-link>
                                </th>
                                <td>{{ frontEndDateFormat(row.creationDate) }}</td>
                                <td>{{ frontEndDateFormat(row.lastUpdatedDate) }}</td>
                                <td>
                                  <base-dropdown>
                                    <base-button slot="title" type="primary" class="dropdown-toggle">
                                      Actions
                                    </base-button>
                                    <router-link class="dropdown-item" :to="{ name: 'project', params: { projectId: row.id }}">
                                      Edit
                                    </router-link>
                                    <a class="dropdown-item" href="#">Copy</a>
                                    <a class="dropdown-item" href="#" @click="deleteModal(row.id)">Delete</a>
                                    <div class="dropdown-divider"></div>
                                    <a class="dropdown-item" href="#" @click="exportFile(row.id)">Export</a>
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
      <modal :show.sync="modals.createNew">
        <template slot="header">
          <h5 class="modal-title" id="exampleModalLabel">Create new project</h5>
        </template>
        <div>
            <strong>Name:</strong>
            <input type="text" class="form-control" v-model="name">
        </div>
        <template slot="footer">
            <base-button type="secondary" @click="modals.createNew = false">Close</base-button>
            <base-button @click="createProject" type="primary">Create</base-button>
        </template>
      </modal>
      <modal :show.sync="modals.delete.show"
        gradient="danger"
        modal-classes="modal-danger modal-dialog-centered">

        <div class="py-3 text-center">
            <i class="ni ni-fat-remove ni-5x"></i>
            <h4 class="heading mt-4">Confirmation</h4>
            <p>Are you sure you want to delete this project?</p>
        </div>

        <template slot="footer">
            <base-button type="white" @click="deleteProject">YES</base-button>
            <base-button type="link" text-color="white" class="ml-auto" @click="modals.delete.show = false">
              Cancel
            </base-button>
        </template>
    </modal>
  </div>
</template>
<script>
import moment from "moment";
import BaseDropdown from "@/components/BaseDropdown";
import StudyRepository from "../repositories/StudyRepository";
import Modal from "@/components/Modal";
import Client from '../client';

export default {
  components: {
    BaseDropdown,
    Modal
  },
  data() {
    return {
      rows: [],
      name: null,
      modals: {
        createNew: false,
        delete: {
          show: false,
          id: null,
        },
      },
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
          name: this.name
        })
        .then(response => {
          this.$router.push({
            name: 'project',
            params: {
              projectId: response.data.id
            }
          });
        })
        .catch(error => {
          console.log(error);
        });

        this.modals.createNew = false;
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
      this.modals.delete.id = null;
    },

    exportFile: function(id) {
      Client.get("/export/" + id, { responseType: "blob" })
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
      return moment(date, "YYYY-MM-DD").format("DD/MM/YYYY");
    }
  }
};
</script>
