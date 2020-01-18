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
                      <h6 class="text-primary text-uppercase">{{item.name}}</h6>
                      <p v-if="item.items.length == 0">This is shown when container is empty</p>
                      <nested-draggable :data="item" />
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
  </div>
</template>
<script>
import nestedDraggable from "../components/Draggable/Nested";

export default {
  name: "nested-example",
  display: "Nested",
  order: 15,
  components: {
    nestedDraggable
  },
  data() {
    return {
      name: "Test",
      data: [
        {
          name: "Available Studies",
          type: "studies",
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
  }
};
</script>

<style scoped>
</style>
