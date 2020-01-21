<template>
    <header class="header-global">
        <base-nav class="navbar navbar-main navbar-expand-lg navbar-transparent navbar-light headroom" transparent type="" effect="light" expand>
            <router-link slot="brand" class="navbar-brand mr-lg-5" to="/">
                <img src="img/brand/white.png" alt="logo">
            </router-link>
			<div class="language-menu">
				<div v-bind:class="locale == 'ru' ? 'active' : ''" @click="locale = 'ru'">RU</div>
				<div v-bind:class="locale == 'lv' ? 'active' : ''" @click="locale = 'lv'">LV</div>
				<div v-bind:class="locale == 'en' ? 'active' : ''" @click="locale = 'en'">EN</div>
			</div>
        </base-nav>
    </header>
</template>
<script>
import BaseNav from "@/components/BaseNav";

export default {
  components: {
    BaseNav,
  },
  data() {
	  return {
		  locale: 'ru'
	  };
  },
  watch: {
    locale (val) {
      this.$i18n.locale = val
    }
  },
  methods: {
	  changeLanguage(lang) {
		  this.locale = lang;
	  }
  }
};
</script>
<style>
</style>
