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
                  <router-link type="button" class="btn btn-icon btn-info" :to="{ name: 'settings' }">
                    <span class="btn-inner--icon">
                      <i class="fa fa-cogs mr-2"></i>
                    </span>
                    <span class="btn-inner--text">GO BACK</span>
                  </router-link>
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
                  <div class="col-sm" v-for="(item, index) in data" :key="index">
                    <card shadow>
                      <h6 class="text-primary text-uppercase">
					    {{item.name}}
					    <base-button type="primary" icon="fa fa-plus" @click="openModal(item)" />
					  </h6>
                      <p v-if="item.items.length == 0">This is shown when container is empty</p>
                      <nested-draggable 
					    :isChild="false" 
						:data="item" 
						:openModalParent="openModal"
						:deleteObjectParent="deleteObject"
					  />
                    </card>
                  </div>
                </div>
                <pre>
                  {{data}}
                </pre>
              </div>
            </card>
          </div>
      </section>
	  <modal :show.sync="modals.manageStudies.show">
        <template slot="header">
          <h5 class="modal-title" id="exampleModalLabel">Manage study</h5>
        </template>
        <div>
          <div class="row">
            <div class="col-md-12">
              <label>Name</label>
              <base-input v-model="modals.manageStudies.name" placeholder="Name"></base-input>
            </div>
			<div class="col-md-12">
              <label>Credit Points</label>
              <base-input v-model="modals.manageStudies.CreditPoints" placeholder="Credit Points"></base-input>
            </div>
			<div class="col-md-12">
              <label>Credit Point Limit</label>
              <base-input v-model="modals.manageStudies.CreditPointLimit" placeholder="Credit Point Limit"></base-input>
            </div>
			<div class="col-md-12">
			  <base-checkbox
			    class="mb-3" 
				v-model="modals.manageStudies.IsObligatory"
				v-bind:checked="modals.manageStudies.IsObligatory ? 'checked' : ''"
			  >
				Is Obligatory
			  </base-checkbox>
            </div>
          </div>
        </div>
        <template slot="footer">
            <base-button type="secondary" @click="modals.manageStudies.show = false">Close</base-button>
            <base-button @click="createObject(modals.manageStudies)" type="primary">Create</base-button>
        </template>
      </modal>
	  <modal :show.sync="modals.manageGroups.show">
        <template slot="header">
          <h5 class="modal-title" id="exampleModalLabel">Manage group</h5>
        </template>
        <div>
          <div class="row">
            <div class="col-md-12">
              <label>Name</label>
              <base-input v-model="modals.manageGroups.name" placeholder="Name"></base-input>
            </div>
			<div class="col-md-12">
              <label>Minimal Study Count</label>
              <base-input v-model="modals.manageGroups.MinimalStudyCount" placeholder="Minimal Study Count"></base-input>
            </div>
          </div>
        </div>
        <template slot="footer">
            <base-button type="secondary" @click="modals.manageGroups.show = false">Close</base-button>
            <base-button @click="createObject(modals.manageGroups)" type="primary">Create</base-button>
        </template>
      </modal>
	  <modal :show.sync="modals.manageCourses.show">
        <template slot="header">
          <h5 class="modal-title" id="exampleModalLabel">Manage course</h5>
        </template>
        <div>
          <div class="row">
            <div class="col-md-12">
              <label>Name</label>
              <base-input v-model="modals.manageCourses.name" placeholder="Name"></base-input>
            </div>
			<div class="col-md-12">
              <label>Background Color Html</label>
			  <ColorPicker 
			    :width="150" 
				:height="150" 
				:disabled="false" 
				startColor="modals.manageCourses.BackgroundColorHtml" 
				v-model="modals.manageCourses.BackgroundColorHtml"
			  />
            </div>
          </div>
        </div>
        <template slot="footer">
            <base-button type="secondary" @click="modals.manageCourses.show = false">Close</base-button>
            <base-button @click="createObject(modals.manageCourses)" type="primary">Create</base-button>
        </template>
      </modal>
  </div>
</template>
<script>
import nestedDraggable from "../components/Draggable/Nested";
import Modal from "@/components/Modal";
import ColorPicker from 'vue-color-picker-wheel';

export default {
  name: "nested-example",
  display: "Nested",
  order: 15,
  components: {
    nestedDraggable,
	Modal,
	ColorPicker
  },
  data() {
    return {
      name: "Test",
	  modals: {
	    manageStudies: {
		  show: false,
		  name: "",
		  CreditPoints: 0,
		  CreditPointLimit: 0,
		  IsObligatory: false,
		  ParentTreeId: ""
	    },
		manageGroups: {
			show: false,
			name: "",
			MinimalStudyCount: 0
		},
		manageCourses: {
			show: false,
			name: "",
			BackgroundColorHtml: ""
		}
	  },
      data: [
        {
          name: "Available Studies",
          type: "studies",
		  modal: "manageStudies",
          allowedTypes: [
            "study"
          ],
          items: [
            {
              treeId: "61d604492d6c4cebb14305dab9d6ea26",
              name: "Latviesu valoda un literatura",
              type: "study",
              allowedTypes: [],
              items: []
            },
            {
              treeId: "88efd1eda72c4fa9b90138425e8acb32",
              name: "Anglu val. I",
              type: "study",
              allowedTypes: [],
              items: []
            },
            {
              treeId: "7995e0ad1c6f474492a6c9654f4078ca",
              name: "Vacu val. I",
              type: "study",
              allowedTypes: [],
              items: []
            }
          ]
        },
        {
          name: "Available Groups",
          type: "groups",
		  modal: "manageGroups",
          allowedTypes: [
            "group"
          ],
          items: [
            {
              treeId: "33a9dd7c5f5348b582c2ee6e3f151e4e",
              name: "Sociala un pilsoniska",
              type: "group",
              allowedTypes: [
                "study"
              ],
              items: [
                {
                  treeId: "7995e0a2d1c6f474492a6c9654f4078ca",
                  name: "Vacu val. I",
                  type: "study",
                  allowedTypes: [],
                  items: []
                }
              ]
            },
            {
              treeId: "e6ab741f907a4f869437c778bf93fd32",
              name: "Valodu",
              type: "group",
              allowedTypes: [
                "study"
              ],
              items: []
            },
            {
              treeId: "e7d94091b6fe41a283b8099c63c5de1f",
              name: "Dabaszinatnu",
              type: "group",
              allowedTypes: [
                "study"
              ],
              items: []
            }
          ]
        },
        {
          name: "Courses",
          type: "courses",
		  modal: "manageCourses",
          allowedTypes: [
            "course"
          ],
          items: [
            {
              treeId: "e75d940951b6fe41a283b8099c63c5de1f",
              name: "AAAA",
              type: "course",
              allowedTypes: [
                "group"
              ],
              items: []
            },
            {
              treeId: "e57d94091b6fe431a283b8099c63c5de1f",
              name: "BBBB",
              type: "course",
              allowedTypes: [
                "group"
              ],
              items: []
            }
          ]
        }
      ]
    };
  },
  methods: {
	  createObject(itm) {
		  console.log(itm);
	  },
	  deleteObject(tObj, itm) {
		  console.log(tObj);
		  console.log(itm);
	  },
	  openModal(tObj, itm = null) {
		  const modal = this.modals[tObj.modal];
		  modal.show = true;
		  
		  console.log(itm);
		  
		  Object.keys(modal).forEach(key => {
			  if (key != "show" && itm[key] !== undefined) {
				  modal[key] = itm[key];
			  }
			  // console.log(key);
		  });
	  }
  }
};
</script>

<style scoped>
</style>
