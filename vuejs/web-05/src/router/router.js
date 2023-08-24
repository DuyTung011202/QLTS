import { createRouter, createWebHistory } from "vue-router";
import ProductList from "../views/product/ProductList.vue";
import HomePage from "../views/home/HomePage.vue";
import ToolsPage from "../views/tools/ToolsPage.vue";
import CategoryPage from "../views/category/CategoryPage.vue";
import SearchPage from "../views/search/SearchPage.vue";
import ReportPage from "../views/report/ReportPage.vue";

const routers = [
  {
    path: "/",
    name: "HomeRouter",
    component: HomePage,
  },
  {
    path: "/products",
    name: "ProductRouter",
    component: ProductList,
  },
  {
    path: "/tools",
    name: "ToolsRouter",
    component: ToolsPage,
  },
  {
    path: "/category",
    name: "CategoryRouter",
    component: CategoryPage,
  },
  {
    path: "/search",
    name: "SearchRouter",
    component: SearchPage,
  },
  {
    path: "/reports",
    name: "ReportRouter",
    component: ReportPage,
  },
];

const router = createRouter({
  history: createWebHistory(),
  routes: routers,
});

export default router;
