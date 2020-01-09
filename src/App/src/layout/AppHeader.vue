<template>
    <header class="header-global">
        <base-nav class="navbar navbar-main navbar-expand-lg navbar-transparent navbar-light headroom" transparent type="" effect="light" expand>
            <router-link slot="brand" class="navbar-brand mr-lg-5" to="/">
                <img src="img/brand/white.png" alt="logo">
            </router-link>
        </base-nav>
    </header>
</template>
<script>
import BaseNav from "@/components/BaseNav";

export default {
  components: {
    BaseNav,
  },
};
</script>
<style>
</style>
