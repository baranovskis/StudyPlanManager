<template>
<draggable ghostClass="ghost" class="list-group" :list="data.items" :move="checkMove" :group="{ name: 'main', allowed: data.allowedTypes }">
  <li class="list-group-item py-2" style="border: 2px solid #ddd;" v-for="el in data.items" :key="el.treeId">
    {{ el.name }}
    <nested-draggable :data="el" />
  </li>
</draggable>
</template>

<script>
import draggable from "vuedraggable";

export default {
  props: {
    data: {
      required: true,
      type: Object
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
    }
  },
  name: "nested-draggable"
};
</script>

<style scoped>
.ghost {
  opacity: 0.5;
  background: #c8ebfb;
  padding: 0 15px;
}
</style>
