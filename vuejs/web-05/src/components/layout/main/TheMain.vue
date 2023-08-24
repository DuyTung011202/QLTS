<template>
    <div :class="isSidebarOpen ? 'w-100 h-80 body-pd' : 'main-container w-100 h-80'">
        <router-view></router-view>
    </div>
</template>
<script>

export default {
    name: "TheMain",
    components: {

    },
    props: ["isSidebarOpen"]
}
</script>

<style>
.body-pd,
.main-container {
    background-color: #f4f7ff !important;
}

.main-header {
    margin-bottom: 13px
}



.main-container {
    padding: 20px
}

.main-content {
    overflow-y: auto
}

.body-pd {
    padding: 20px 20px 20px 176px;
}

.h-80 {
    height: calc(100vh - 50px) !important;
}
</style>