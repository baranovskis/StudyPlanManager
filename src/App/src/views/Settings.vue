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
          <div class="row pb-3">
            <div class="col-md-4">
              <base-button block type="success" icon="fa fa-floppy-o mr-2" @click="saveSettings">Save</base-button>
            </div>
          </div>
          <div class="row pb-3">
            <div class="col-md-4">
              <div class="row pb-3" v-for="(item, index) in availableNodes" :key="index">
                <div class="col-sm">
                  <card shadow>
                    <h6 class="text-primary text-uppercase">
                      {{item.name}}
                    </h6>
                    <p v-if="item.items.length == 0">This is shown when container is empty</p>
                    <nested-draggable :isChild="false" :data="item" />
                  </card>
                </div>
              </div>
            </div>
            <div class="col-md-8">
              <card shadow>
                <h6 class="text-primary text-uppercase">
                  {{projectTree.name}}
                </h6>
                <p v-if="projectTree.items.length == 0">This is shown when container is empty</p>
                <nested-draggable :isChild="false" :data="projectTree" />
              </card>
            </div>
          </div>
        </div>
      </card>
    </div>
  </section>
</div>
</template>

<script>
import nestedDraggable from "../components/Draggable/Nested";
import Modal from "@/components/Modal";
import ColorPicker from "vue-color-picker-wheel";
import SettingsRepository from "../repositories/SettingsRepository";

export default {
  components: {
    nestedDraggable,
    Modal,
    ColorPicker
  },
  data() {
    return {
      name: undefined,
      projectTree: {
        name: "Project Tree",
        type: "nodes",
        allowedTypes: ["course"],
        items: []
      },
      availableNodes: [{
          name: "Studies",
          type: "studies",
          allowedTypes: ["study"],
          items: []
        },
        {
          name: "Groups",
          type: "groups",
          allowedTypes: ["group"],
          items: []
        },
        {
          name: "Courses",
          type: "courses",
          allowedTypes: ["course"],
          items: []
        }
      ]
    };
  },
  mounted() {
    this.doLoad();
  },
  methods: {
    doLoad() {
      SettingsRepository.get(this.$route.params.projectId)
        .then(response => {
          this.name = response.data.name;

          // Available studies
          this.availableNodes[0].items = response.data.availableStudies;

          // Available groups
          this.availableNodes[1].items = response.data.availableGroups;

          // Available courses
          this.availableNodes[2].items = response.data.availableCourses;

          // Project tree
          this.projectTree.items = response.data.projectTree;
        })
        .catch(error => {
          console.log(error);
          //this.errored = true
        });
      //.finally(() => this.loading = false)
    },
    saveSettings() {
      SettingsRepository.save({
          Items: this.projectTree.items
        }, this.$route.params.projectId)
        .catch(error => {
          console.log(error);
        });
    },

  }
};
</script>

<style scoped>
</style>
