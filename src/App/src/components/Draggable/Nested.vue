<template>
  <draggable ghostClass="ghost" class="list-group" :list="data.items" :move="checkMove" :group="{ name: 'main', allowed: data.allowedTypes }">
    <li class="list-group-item" v-for="el in data.items" :key="el.treeId">
      {{ el.name }}
	  <base-button v-if="!isChild" type="info" icon="fa fa-pencil" @click="openModal(data, el)" />
	  <base-button v-if="!isChild" type="danger" icon="fa fa-trash" @click="deleteElement(data, el)" />
      <nested-draggable :isChild="true" :data="el" />
    </li>
  </draggable>
</template>
<script>
import draggable from "vuedraggable"
import { resolve } from 'url';
import { constants } from 'buffer';

export default {
  props: {
    data: {
      required: true,
      type: Object
    },
	isChild: {
	  required: false,
	  type: Boolean
	},
	openModalParent: {
		required: false,
		type: Function
	},
	deleteObjectParent: {
		required: false,
		type: Function
	}
  },
  components: {
    draggable
  },
  methods: {
    checkMove(arg) {
      let dragType = arg.draggedContext.element.type;
      let allowedTypes = arg.relatedContext.component.$attrs.group.allowed;
      return allowedTypes.includes(dragType);
    },
    openModal(tObj, itm){
      this.openModalParent(tObj, itm);
    },
	deleteElement(tObj, itm){
      this.deleteObjectParent(tObj, itm);
    }
  },
  name: "nested-draggable"
};
</script>

<style scoped>
.ghost {
  opacity: .5;
  background: #C8EBFB;
  padding: 0 15px;
}
</style>
