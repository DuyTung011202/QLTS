<template>
  <div class="context-menu-content " :style="{ top: posTop + 'px', left: posLeft + 'px' }"
    :class="showAbove ? 'showAbove' : ''">
    <div class="context-menu-item" @click="this.$emit(item.method)" v-for="(item, index) in items" :key="index">
      <div class="icon">
        <div :class="item.icon"></div>
      </div>
      <div class="text">{{ item.text }}</div>
    </div>
  </div>
</template>

<script>
import { TitlePopup } from "@/js/common/resource"
export default {
  name: "MISAContextMenu",
  props: {
    posTop: {
      type: Number,
      default: 10,
    },
    posLeft: {
      type: Number,
      default: 10,
    },

    initItem: { type: Array },
  },
  data() {
    return {
      heightOfContextMenu: 150,
      showAbove: false,
      items: [
        { icon: "icon-edit", text: TitlePopup.Edit, method: "clickEdit" },
        {
          icon: "icon-delete-black",
          text: TitlePopup.Delete,
          method: "clickDelete",
        },
        {
          icon: "icon-replicate",
          text: TitlePopup.Replication,
          method: "clickReplication",
        },
      ],
    };
  },


};
</script>

<style scoped>
#context-menu {
  position: fixed;
  top: 0;
  left: 0;
  right: 0;
  bottom: 0;
}

.context-menu-content {
  position: absolute;
  top: 0;
  left: 0;
  z-index: 3;
  width: 200px;
  height: fit-content;
  display: block;
  padding: 4px;
  border-radius: 4px;
  box-shadow: 0 2px 6px rgba(0, 0, 0, .16);
  background-color: #f9fafc;
  border: 1px solid #e0e0e0;

  .context-menu-item {
    display: flex;
    justify-content: start;
    gap: 10px;
    align-items: center;
    height: 35px;
    border-radius: 2.5px;
    border: 1px solid #f9fafc;
    cursor: pointer;

    .icon {
      margin-left: 10px;
    }

    &:hover {
      background-color: #1aa5c88e;
    }
  }

  .context-menu-item.active {
    background-color: #1aa5c88e;
  }
}

.context-menu-content.showAbove {
  transform: translateY(-100%);
}</style>
