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
            <div class="row pb-3">
              <div class="col-md-4">
                <card shadow>
                  <h6 class="text-primary text-uppercase">{{ $t(availableStudies.name) }}</h6>
                  <p v-if="availableStudies.items.length == 0">{{ $t('base.empty') }}</p>
                  <nested-draggable :isChild="false" :data="availableStudies" />
                </card>
              </div>
              <div class="col-md-8">
                <card shadow>
                  <h6 class="text-primary text-uppercase">{{$t (projectTree.name) }}</h6>
                  <p v-if="projectTree.items.length == 0">{{ $t('base.empty') }}</p>
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
import SettingsRepository from "../repositories/SettingsRepository";

export default {
  components: {
    nestedDraggable
  },
  data() {
    return {
      name: undefined,
      projectTree: {
        name: "project.projectTree",
        type: "nodes",
        allowedTypes: ["course"],
        items: []
      },
      availableStudies: {
        name: "project.studies",
        type: "studies",
        allowedTypes: ["study"],
        items: []
      }
    };
  },
  mounted() {
    this.doLoad();
  },
  methods: {
    doLoad() {
      SettingsRepository.getProject(this.$route.params.projectId)
        .then(response => {
          this.name = response.data.name;

          // Available studies
          this.availableStudies.items = response.data.availableStudies;

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
      SettingsRepository.saveProject(
        {
          Items: this.projectTree.items
        },
        this.$route.params.projectId
      ).catch(error => {
        console.log(error);
      });
      this.goBack();
    },
    goBack() {
      this.$router.go(-1);
    }
  }
};
</script>

<style scoped>
</style>
