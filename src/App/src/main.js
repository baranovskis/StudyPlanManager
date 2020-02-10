/*!

=========================================================
* Vue Argon Design System - v1.1.0
=========================================================

* Product Page: https://www.creative-tim.com/product/argon-design-system
* Copyright 2019 Creative Tim (https://www.creative-tim.com)
* Licensed under MIT (https://github.com/creativetimofficial/argon-design-system/blob/master/LICENSE.md)

* Coded by www.creative-tim.com

=========================================================

* The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.

*/
import Vue from "vue";
import App from "./App.vue";
import VueI18n from 'vue-i18n'
import router from "./router";
import Argon from "./plugins/argon-kit";
import './registerServiceWorker';
import VueCookie from 'vue-cookie'

import tRU from "./translates/ru.json";
import tEN from "./translates/en.json";
import tLV from "./translates/lv.json";


Vue.config.productionTip = false;
Vue.use(VueI18n);
Vue.use(Argon);
Vue.use(VueCookie);

const i18n = new VueI18n({
  locale: VueCookie.get('locale') || 'lv',
  messages: {
    ru: tRU,
    en: tEN,
    lv: tLV
  }
});

new Vue({
  router,
  i18n,
  render: h => h(App)
}).$mount("#app");
