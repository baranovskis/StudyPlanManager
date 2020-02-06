import Vue from "vue";
import Router from "vue-router";
import AppHeader from "./layout/AppHeader";
import AppFooter from "./layout/AppFooter";
import Home from "./views/Home.vue";
import Settings from "./views/Settings.vue";
import Project from "./views/Project.vue";
import ProjectSettings from "./views/ProjectSettings.vue";

Vue.use(Router);

export default new Router({
  linkExactActiveClass: "active",
  routes: [
    {
      path: "/",
      name: "home",
      components: {
        header: AppHeader,
        default: Home,
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
    {
      path: "/project/:projectId",
      name: "project",
      components: {
        header: AppHeader,
        default: Project,
        footer: AppFooter
      }
    },
    {
      path: "/project/:projectId/settings",
      name: "project-settings",
      components: {
        header: AppHeader,
        default: ProjectSettings,
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
