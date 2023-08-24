import { createApp } from "vue";
import App from "./App.vue";
import ElementPlus from "element-plus";
import "element-plus/dist/index.css";
import MISAComboboxUpdown from "./components/base/combobox/MISAComboboxUpdown.vue";
import MISAButton from "./components/base/button/MISAButton.vue";
import MISAForm from "./components/base/form/MISAForm.vue";
import MISAPopup from "./components/base/popup/MISAPopup.vue";
import MISALoading from "./components/base/loading/MISALoading.vue";
import router from "./router/router";
import VueTippy from "vue-tippy";
import { MISAEnum } from "./js/common/enum.js";
import MISACombobox from "./components/base/combobox/MISACombobox.vue";
import MISAPagination from "./components/base/paginantion/MISAPagination.vue";
import MISAContextMenu from "./components/base/contextmenu/MISAContextMenu.vue";
import Vue3Toasity from "vue3-toastify";
import "vue3-toastify/dist/index.css";
import dayjs from "dayjs";
import vi from "element-plus/dist/locale/vi.mjs";
import "tippy.js/dist/tippy.css";
const app = createApp(App);
app.component("MISAComboboxUpdown", MISAComboboxUpdown);
app.component("MISACombobox", MISACombobox);
app.component("MISAButton", MISAButton);
app.component("MISAForm", MISAForm);
app.component("MISALoading", MISALoading);
app.component("MISAPagination", MISAPagination);
app.component("MISAPopup", MISAPopup);
app.component("MISAContextMenu", MISAContextMenu);
app.config.globalProperties.$_MISAEnum = MISAEnum;

const toastOptions = {
  autoClose: 1000,
};

app.use(Vue3Toasity, toastOptions);
app.use(ElementPlus, {
  locale: vi,
});
app.use(router);
app.use(
  VueTippy,
  // optional
  {
    directive: "tippy", // => v-tippy
    component: "tippy", // => <tippy/>
    componentSingleton: "tippy-singleton", // => <tippy-singleton/>,
    defaultProps: {
      placement: "top",
      allowHTML: true,
    },
  }
);
app.mount("#app");
dayjs.Ls.en.weekStart = 1;
function handleKeyDown(event) {
  // Lấy mã phím từ sự kiện
  var keyCode = event.keyCode || event.which;
  if (keyCode === 38 || keyCode === 40) {
    event.preventDefault();
  }
}

// Đăng ký sự kiện keydown với cửa sổ trình duyệt
window.addEventListener("keydown", handleKeyDown);
