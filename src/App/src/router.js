import Vue from "vue";
import Router from "vue-router";
import AppHeader from "./layout/AppHeader";
import AppFooter from "./layout/AppFooter";
import Table from "./views/Table.vue";
import Settings from "./views/Settings.vue";

Vue.use(Router);

export default new Router({
  linkExactActiveClass: "active",
  routes: [
    {
      path: "/",
      name: "table",
      components: {
        header: AppHeader,
        default: Table,
        footer: AppFooter
      }
    },
    {
      path: "/settings",
      name: "settings",
      components: {
        header: AppHeader,
        default: Settings,
        footer: AppFooter
      }
    },
  ],
  scrollBehavior: to => {
    if (to.hash) {
      return { selector: to.hash };
    } else {
      return { x: 0, y: 0 };
    }
  }
});
