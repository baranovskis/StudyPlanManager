<template>
    <header class="header-global">
        <base-nav class="navbar-main" transparent type="" effect="light" expand>
            <router-link slot="brand" class="navbar-brand mr-lg-5" to="/">
                <img src="img/brand/white.png" alt="logo">
            </router-link>

            <div class="row" slot="content-header" slot-scope="{closeMenu}">
                <div class="col-6 collapse-brand">
                    <a href="https://demos.creative-tim.com/vue-argon-design-system/documentation/">
                        <img src="img/brand/blue.png">
                    </a>
                </div>
                <div class="col-6 collapse-close">
                    <close-button @click="closeMenu"></close-button>
                </div>
            </div>

            <ul class="navbar-nav navbar-nav-hover align-items-lg-center">
                <base-dropdown class="nav-item" menu-classes="dropdown-menu-xl">
                    <a slot="title" href="#" class="nav-link" data-toggle="dropdown" role="button">
                        <i class="ni ni-ui-04 d-lg-none"></i>
                        <span class="nav-link-inner--text">Components</span>
                    </a>
                    <div class="dropdown-menu-inner">
                        <a href="https://demos.creative-tim.com/vue-argon-design-system/documentation/"
                           class="media d-flex align-items-center">
                            <div class="icon icon-shape bg-gradient-primary rounded-circle text-white">
                                <i class="ni ni-spaceship"></i>
                            </div>
                            <div class="media-body ml-3">
                                <h6 class="heading text-primary mb-md-1">Getting started</h6>
                                <p class="description d-none d-md-inline-block mb-0">Get started with Bootstrap, the
                                    world's most popular framework for building responsive sites.</p>
                            </div>
                        </a>
                        <a href="https://demos.creative-tim.com/vue-argon-design-system/documentation/"
                           class="media d-flex align-items-center">
                            <div class="icon icon-shape bg-gradient-warning rounded-circle text-white">
                                <i class="ni ni-ui-04"></i>
                            </div>
                            <div class="media-body ml-3">
                                <h5 class="heading text-warning mb-md-1">Components</h5>
                                <p class="description d-none d-md-inline-block mb-0">Learn how to use Argon
                                    compiling Scss, change brand colors and more.</p>
                            </div>
                        </a>
                    </div>
                </base-dropdown>
            </ul>
            <ul class="navbar-nav align-items-lg-center ml-lg-auto">
                <li class="nav-item d-none d-lg-block ml-lg-4">
                    <a href="#" @click="createNewStudyVariant" rel="noopener"
                       class="btn btn-neutral btn-icon" download>
                        <span class="btn-inner--icon">
                        <i class="fa fa-cloud-download mr-2"></i>
                        </span>
                        <span class="nav-link-inner--text">New</span>
                    </a>
                </li>
            </ul>
            <ul class="navbar-nav align-items-lg-center ml-lg-auto">
                <li class="nav-item d-none d-lg-block ml-lg-4">
                    <a href="#" @click="exportFile" rel="noopener"
                       class="btn btn-neutral btn-icon" download>
                        <span class="btn-inner--icon">
                        <i class="fa fa-cloud-download mr-2"></i>
                        </span>
                        <span class="nav-link-inner--text">Open</span>
                    </a>
                </li>
            </ul>
            <ul class="navbar-nav align-items-lg-center ml-lg-auto">
                <li class="nav-item d-none d-lg-block ml-lg-4">
                    <a href="#/settings" rel="noopener"
                       class="btn btn-neutral btn-icon" download>
                        <span class="btn-inner--icon">
                        <i class="fa fa-cloud-download mr-2"></i>
                        </span>
                        <span class="nav-link-inner--text">Settings</span>
                    </a>
                </li>
            </ul>
            <ul class="navbar-nav align-items-lg-center ml-lg-auto">
                <li class="nav-item d-none d-lg-block ml-lg-4">
                    <a href="#" @click="exportFile" rel="noopener"
                       class="btn btn-neutral btn-icon" download>
                        <span class="btn-inner--icon">
                        <i class="fa fa-cloud-download mr-2"></i>
                        </span>
                        <span class="nav-link-inner--text">Export</span>
                    </a>
                </li>
            </ul>
        </base-nav>
    </header>
</template>
<script>
import BaseNav from "@/components/BaseNav";
import BaseDropdown from "@/components/BaseDropdown";
import CloseButton from "@/components/CloseButton";
import Client from '../client';

export default {
  components: {
    BaseNav,
    CloseButton,
    BaseDropdown
  },
  methods: {
    exportFile: function() {
      Client
        .get('/export', { responseType: 'blob' })
        .then(response => {
            const url = window.URL.createObjectURL(new Blob([response.data]));
            const link = document.createElement('a');
            link.href = url;
            link.setAttribute('download', 'export.xlsx');
            document.body.appendChild(link);
            link.click();
        }).catch(error => console.error(error));
    },
    createNewStudyVariant: function() {
      Client
        .get('/NewStudyVariant', { responseType: 'blob' })
		.catch(error => console.error(error));
	  // TODO: reload grid
	  //this.doLoad();
    }
  }
};
</script>
<style>
</style>
