<template>
  <header :class="isSidebarOpen ? 'header header-pd' : 'header'" id="header">
    <span class="header__title">Danh sách tài sản</span>
    <div class="r-header">
      <span>Sở tài chính</span>
      <div class="header-y">
        <div>
          <span style="margin: 0 16px 0 11px  ;">Năm</span>
          <span style="font-weight: 700;">2023</span>
        </div>
        <span class=" icon-updown-bold"></span>
      </div>
      <div class="header__icon header__icon-1"></div>
      <div class="header__icon header__icon-2"></div>
      <div class="header__icon header__icon-3"></div>
      <div class="header__icon header__icon-4"></div>
      <div class="header__icon header__icon-5"></div>
    </div>
  </header>
</template>

<script>
export default {
  name: "TheHeader",
  props: ["isSidebarOpen"]
}
</script>


<style scoped>
.header {
  width: 100%;
  height: var(--header-height);
  position: fixed;
  top: 0;
  left: 0;
  display: flex;
  align-items: center;
  justify-content: space-between;
  padding: 0 1rem;
  background-color: var(--white-color);
  z-index: var(--z-fixed);
  transition: .2s;
  padding: 0 20px 0 calc(var(--nav-width) + 20px)
}

.header__title {
  font-weight: 700;
  margin-right: 26px
}

.r-header {
  display: flex;
  align-items: center
}

.header-y {
  display: flex;
  align-items: center;
  justify-content: space-between;
  background-color: rgba(26, 164, 200, 0.2);
  border-radius: 3px;
  width: 112px;
  height: 30px;
  overflow: hidden;
  margin-right: 7px;
  margin-left: 16px;
}

.header__icon {
  width: 24px;
  height: 24px;
  margin-left: 20px;
  background-size: contain;
  cursor: pointer;
  background-position: center
}

.header__icon-1 {
  background: var(--icon-url) no-repeat -328px -108px;
  margin-left: 0
}

.header__icon-2 {
  background: var(--icon-url) no-repeat -195px -20px
}

.header__icon-3 {
  background: var(--icon-url) no-repeat -63px -63px
}

.header__icon-4 {
  background: var(--icon-url) no-repeat -18px -20px
}

.header__icon-5 {
  background: var(--icon-url) no-repeat -72px -338px;
  width: 8px;
  height: 5px;
  margin-left: 8px
}

.header-pd {
  padding: 0 20px 0 calc(var(--nav-width) + 156px + 20px)
}

.icon-updown-bold {
  background: url(../../../assets/img/qlts-icon.svg) no-repeat -196px -328px;

  width: 24px;
  height: 24px;
}
</style>