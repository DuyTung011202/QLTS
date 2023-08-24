<template>
    <div :class="isSidebarOpen ? 'l-navbar show' : 'l-navbar'" id="nav-bar">
        <nav class="nav">
            <div class="nav-header">
                <a href="#" class="nav__logo">
                    <div class="nav__logo-icon"></div>
                    <span class="nav__logo-name">MISA QLTS</span>
                </a>
                <div class="nav__list">
                    <router-link :to="link.path" v-for="link in links" :key="link.id"
                        :class="['nav__link', { 'active': link.active }]" @click="setActiveLink(link)">
                        <div :class="['nav__icon', 'nav__icon-' + link.id]"></div>
                        <span class="nav__name">{{ link.name }}</span>
                    </router-link>
                </div>
            </div>
            <hr>
            <div class="btn-toggle">
                <div :class="isSidebarOpen ? 'btn-toggle--close' : 'btn-toggle--open'" id="btn-toggle"
                    @click="toggleSidebar">
                </div>
            </div>
        </nav>
    </div>
</template>

<script>

export default {
    name: "TheSidebar",
    props: {
        isSidebarOpen: {
            type: String,
        }
    },

    created() {
        this.restoreStateFromLocalStorage();
    },

    data() {
        return {
            links: [
                { id: 1, name: 'Tổng quan', path: "/", active: true },
                { id: 2, name: 'Tài sản', path: "/products", active: false },
                { id: 3, name: 'Tài sản HT-ĐB', path: "/", active: false },
                { id: 4, name: 'Công cụ dụng cụ', path: "/tools", active: false },
                { id: 5, name: 'Danh mục', path: "/category", active: false },
                { id: 6, name: 'Tra cứu', path: "/search", active: false },
                { id: 7, name: 'Báo cáo', path: "/reports", active: false }
            ]
        };
    },
    methods: {
        /**
         * @description: ẩn hiện sidebar
         * @author: DDTung(03/07/2023)
         */
        toggleSidebar() {
            this.$emit('toggle-sidebar')
        },
        /**
       * @description: active navlink khi click và lưu vào localStorage
       * @author: DDTung(03/07/2023)
       */
        setActiveLink(clickedLink) {
            this.links.forEach(link => {
                link.active = link.id === clickedLink.id;
            });
            localStorage.setItem('activeLinkId', clickedLink.id);
        },
        /**
       * @description: active navlink khi load lại trang lấy từ localStorage
       * @author: DDTung(03/07/2023)
       */
        restoreStateFromLocalStorage() {
            const activeLinkId = localStorage.getItem('activeLinkId');
            if (activeLinkId) {
                this.links.forEach(link => {
                    link.active = link.id === Number(activeLinkId);
                });
            }
        }
    }
}
</script>

<style>
.l-navbar {
    position: fixed;
    top: 0;
    width: var(--nav-width);
    height: 100vh;
    background-color: var(--first-color);
    padding: 0 12px;
    transition: .2s;
    z-index: var(--z-fixed);
    left: 0
}

.nav {
    height: 100%;
    display: flex;
    flex-direction: column;
    justify-content: space-between;
    overflow: hidden
}

.nav-header {
    display: flex;
    flex-direction: column
}

.nav__link,
.nav__logo {
    display: grid;
    grid-template-columns: max-content max-content;
    align-items: center;
    column-gap: 1rem
}

.nav__logo {
    margin-bottom: 2rem;
    padding: 11px 15px 4px 4px;
    width: 100%
}

.nav__logo-icon {
    background: var(--icon-url) no-repeat -20px -680px;
    width: 36px;
    height: 36px
}

.nav__logo-name {
    color: var(--white-color);
    font-weight: 700
}

.nav__link {
    padding: 8px 10px;
    position: relative;
    color: var(--first-color-light);
    transition: .3s
}

.btn-toggle--close,
.btn-toggle--open {
    cursor: pointer;
    width: 24px;
    height: 24px;
    transition: .2s ease-in-out
}

.active,
.nav__link:hover {
    color: var(--white-color);
}

.active .nav__icon {
    opacity: 1;

}

.nav__icon {
    width: 24px;
    height: 24px;
    opacity: 0.2;
}

.nav__icon-1 {
    background: var(--icon-url) no-repeat -21px -153px
}

.nav__icon-2 {
    background: var(--icon-url) no-repeat -65px -153px
}

.nav__icon-3 {
    background: var(--icon-url) no-repeat -110px -153px
}

.nav__icon-4 {
    background: var(--icon-url) no-repeat -153px -153px
}

.nav__icon-5 {
    background: var(--icon-url) no-repeat -198px -155px
}

.nav__icon-6 {
    background: var(--icon-url) no-repeat -241px -153px
}

.nav__icon-7 {
    background: var(--icon-url) no-repeat -329px -153px
}

.btn-toggle {
    padding: 8px 10px;
}

.btn-toggle--open {
    background: var(--icon-url) no-repeat -329px -417px;
    float: left
}

.btn-toggle--close {
    background: var(--icon-url) no-repeat -285px -417px;
    float: right
}

.active::before {
    content: '';
    position: absolute;
    left: 0;
    width: 27%;
    height: 100%;
    background-color: #1aa4c8;
    z-index: -1;
    border-radius: 4px;
    transition: 0.5s ease-in-out;

}

.show {
    width: calc(var(--nav-width) + 156px)
}

.l-navbar.show .btn-toggle {
    border-top: 1px solid var(--border-color)
}

.l-navbar.show .active::before {
    width: 100%;
    transition: 0.5 s ease-in-out;
}
</style>
