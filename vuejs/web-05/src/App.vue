<template>
  <Header :isSidebarOpen="isSidebarOpen"></Header>
  <Sidebar :isSidebarOpen="isSidebarOpen" @toggle-sidebar="toggleSidebar"></Sidebar>
  <Main :isSidebarOpen="isSidebarOpen"></Main>
</template>

<script>
import Header from './components/layout/header/TheHeader.vue'
import Sidebar from './components/layout/sidebar/TheSidebar.vue'
import Main from './components/layout/main/TheMain.vue'
export default {
  name: 'App',
  components: {
    Header, Sidebar, Main
  },
  data() {
    return {
      isSidebarOpen: false,
    };
  },
  methods: {
    toggleSidebar() {
      this.isSidebarOpen = !this.isSidebarOpen;
    },
  }
}
</script>


<style>
@import url(./css/main.css);
@import 'bootstrap/dist/css/bootstrap.css';
</style>