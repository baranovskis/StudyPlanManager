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
                    <a href="#" @click="needsSavingStudyVariant" rel="noopener"
                       class="btn btn-neutral btn-icon">
                        <span class="btn-inner--icon">
                        <i class="fa fa-file-o mr-2"></i>
                        </span>
                        <span class="nav-link-inner--text">New</span>
                    </a>
                </li>
            </ul>
            <ul class="navbar-nav align-items-lg-center ml-lg-auto">
                <li class="nav-item d-none d-lg-block ml-lg-4">
                    <a href="#" @click="openStudyVariantList" rel="noopener"
                       class="btn btn-neutral btn-icon">
                        <span class="btn-inner--icon">
                        <i class="fa fa-folder-open-o mr-2"></i>
                        </span>
                        <span class="nav-link-inner--text">Open</span>
                    </a>
                </li>
            </ul>
            <ul class="navbar-nav align-items-lg-center ml-lg-auto">
                <li class="nav-item d-none d-lg-block ml-lg-4">
                    <a href="#" @click="saveStudyVariant" rel="noopener"
                       class="btn btn-neutral btn-icon">
                        <span class="btn-inner--icon">
                        <i class="fa fa-floppy-o mr-2"></i>
                        </span>
                        <span class="nav-link-inner--text">Save</span>
                    </a>
                </li>
            </ul>
            <ul class="navbar-nav align-items-lg-center ml-lg-auto">
                <li class="nav-item d-none d-lg-block ml-lg-4">
                    <a href="#/settings" rel="noopener"
                       class="btn btn-neutral btn-icon">
                        <span class="btn-inner--icon">
                        <i class="fa fa-cogs mr-2"></i>
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
                        <i class="fa fa-arrow-circle-o-down mr-2"></i>
                        </span>
                        <span class="nav-link-inner--text">Export</span>
                    </a>
                </li>
            </ul>
        </base-nav>
		<modal :show.sync="modals.newt"
			   gradient="danger"
			   modal-classes="modal-danger modal-dialog-centered">
			<h6 slot="header" class="modal-title" id="modal-title-notification">Your attention is required</h6>

			<div class="py-3 text-center">
				<i class="ni ni-bell-55 ni-3x"></i>
				<h4 class="heading mt-4">Unsaved changes</h4>
				<p>Would you like to save last changes?</p>
			</div>

			<template slot="footer">
				<base-button type="white" @click="needsNamingStudyVariant">Save</base-button>
				<base-button type="link"
							 text-color="white"
							 class="ml-auto"
							 @click="createNewStudyVariant">
					No
				</base-button>
			</template>
		</modal>
		<modal :show.sync="modals.savename"
                   body-classes="p-0"
                   modal-classes="modal-dialog-centered modal-sm">
			<card type="secondary" shadow
				  header-classes="bg-white pb-5"
				  body-classes="px-lg-5 py-lg-5"
				  class="border-0">
				<template>
					<div class="py-3 text-center">
						<h4 class="heading mt-4">Set name for variant</h4>
					</div>
					<form role="form">
						<base-input alternative
									v-model="new_variant_name"
									class="mb-3"
									placeholder="Name"
									addon-left-icon="ni ni-email-83">
						</base-input>
						<div class="text-center">
							<base-button 
								type="primary" 
								class="my-4"
								@click="saveStudyVariant"
							>
							Save
							</base-button>
							<base-button type="link"
								 class="ml-auto"
								 @click="modals.savename = false">
								Cancel
							</base-button>
						</div>
					</form>
				</template>
			</card>
		</modal>
    </header>
</template>
<script>
import BaseNav from "@/components/BaseNav";
import BaseDropdown from "@/components/BaseDropdown";
import CloseButton from "@/components/CloseButton";
import Modal from "@/components/Modal";
import Client from '../client';

export default {
  components: {
    BaseNav,
    CloseButton,
    BaseDropdown,
	Modal
  },
  data() {
    return {
      modals: {
		newt: false,
        savename: false,
      },
	  new_variant_name: '',
    };
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
	needsSavingStudyVariant: function () {
		this.modals.newt = true;
		
		Client
			.get('/NeedsSavingStudyVariant')
			.then(response => {
				console.log(response);
			}).catch(error => console.error(error));
	},
    createNewStudyVariant: function() {
		// TODO: Check if variant needs saving by get/NeedsSavingStudyVariant
		// TODO: If needs saving - use saveStudyVariant function
      Client
        .get('/NewStudyVariant', { responseType: 'blob' })
		.catch(error => console.error(error));
	  // TODO: reload grid
	  //this.doLoad();
    },
    openStudyVariantList: function() {
	  // TODO: Display modal with variants from get/StudyVariantList
	  // TODO: Open selected variant by passing id to put/StudyVariantList
    },
	needsNamingStudyVariant: function() {
		this.modals.newt = false;
		
		this.modals.savename = true;
	},
    saveStudyVariant: function() {
		this.modals.newt = false;
		this.modals.savename = false;
		
		alert("Variant name: " + this.new_variant_name);
	  // TODO: Check if variant needs name by get/NeedsNamingStudyVariant
	  // TODO: If needs naming - display modal with textbox - then pass string to get/NameStudyVariant
	  // TODO: Save variant by get/SaveStudyVariant
    }
  }
};
</script>
<style>
</style>
