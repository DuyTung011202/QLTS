<template>
  <div class="main-header d-flex justify-content-between ">
    <div class="l-main d-flex align-items-center">

      <div class="content__input">
        <span class="input-icon icon-search"></span>
        <input ref="textSearch" v-tippy="tooltip.TextSearch" v-model="textSearch" class="default-input"
          placeholder="Tìm kiếm tài sản" @keypress.enter="onEnterSearch" tabindex="1" />
      </div>
      <MISACombobox :propApi="categoryURL" propKey="fixed_asset_category_id" propValue="fixed_asset_category_name"
        :getPagging="getPagging" v-model="productForFilter.fixed_asset_category_id" filterCombobox
        v-model:textInput="productForFilter.fixed_asset_category_code" iconClass="input-icon icon-filter"
        placeholder="Loại tài sản" tab="2">
      </MISACombobox>
      <MISACombobox :propApi="departmentURL" propKey="department_id" propValue="department_name" :getPagging="getPagging"
        v-model="productForFilter.department_id" v-model:textInput="productForFilter.department_code" filterCombobox
        iconClass="input-icon icon-filter" placeholder="Bộ phận sử dụng" tab="3">
      </MISACombobox>
    </div>
    <div class="r-main d-flex align-items-center">
      <MISAButton customId="btn-add" customClass="btn--primary d-flex align-items-center" iconClass="btn--primary-icon"
        text="Thêm tài sản" @click="handleAddForm" tab="4">
      </MISAButton>
      <MISAButton v-tippy="tooltip.ExportExcel" customClass="btn--option" @click="exportExcel" tab="5"></MISAButton>
      <MISAButton v-tippy="tooltip.DeleteButton" customClass="btn--delete" @click="deleteSelected"
        @keydown.tab.prevent="setFocusFirstInput()" tab="6"></MISAButton>
    </div>
  </div>
  <div class="context-menu-overlay" v-if="isShowContextMenu" @click="closeContextMenu"></div>
  <div class="table" :style="products.length < 13 ? 'height:fit-content' : 'height:90%'">
    <table>
      <thead>
        <tr>
          <th class="text-center width-fit table-checkbox">
            <input type="checkbox" @click="selectedAllItem"
              :checked="listProduct.length == products.length && listProduct.length != 0" ref="checkall" />
          </th>
          <th v-tippy="tooltip.NumericTooltip" class=" text-center">{{ tableInfo.numeric }}
          </th>
          <th>{{ tableInfo.fixed_asset_code }}</th>
          <th>{{ tableInfo.fixed_asset_name }}</th>
          <th>{{ tableInfo.fixed_asset_category_name }}</th>
          <th>{{ tableInfo.department_name }}</th>
          <th class="text-right">{{ tableInfo.quantity }}</th>
          <th class="text-right">{{ tableInfo.cost }}</th>
          <th v-tippy="tooltip.DepreciationTooltip" class="text-right ">{{ tableInfo.depreciation }}
          </th>
          <th class="text-right">{{ tableInfo.residualValue }}</th>
          <th class="text-center">{{ tableInfo.function }}</th>
        </tr>
      </thead>
      <tbody>
        <tr class="empty-data" v-if="products.length === 0">

          <td colspan="12" class="w-100">
            <div class="no-data">
              <div class="icon-noData"></div>
              <h3>Không có dữ liệu</h3>
            </div>
          </td>
        </tr>
        <tr v-for="(product, index) in products" :key="product.fixed_asset_id"
          :class="listProduct.includes(product) ? 'tr-active' : ''" @click="selectItemToList(product)"
          @contextmenu="onClickContextMenu(product, $event)" @dblclick="onUpdate({ ...product, editMode: 'edit' })"
          @mouseup.prevent.shift="mouseUp(product)" @mousedown.prevent.shift="mouseDown(product)">
          <td style="min-width: 45px; max-width: 45px" class="text-center table-checkbox">
            <input type="checkbox" :checked="listProduct.includes(product)" @click.stop="selectItemToList(product)"
              @dblclick.stop />
          </td>
          <td style="min-width: 45px; max-width: 45px" class="text-center">{{ index + 1 + (pageNumber - 1) * pageSize }}
          </td>
          <td style="min-width: 80px; max-width: 80px">{{ product.fixed_asset_code }}</td>
          <td style="min-width: 130px; max-width: 130px">{{ product.fixed_asset_name }}</td>
          <td style="
                  max-width: 150px; 
                  min-width: 150px">{{ product.fixed_asset_category_name }}
          </td>
          <td style="max-width: 150px !important;
                  min-width: 150px !important">{{ product.department_name }}</td>
          <td style="min-width: 80px; max-width: 80px" class="text-right">{{
            formatMoney(product.quantity) }}</td>
          <td style="min-width: 105px; max-width: 105px" class="text-right">{{
            formatMoney(product.cost) }}</td>
          <td style="min-width: 105px; max-width: 105px" class="text-right">{{
            formatMoney(product.depreciation_value) }}</td>
          <td style="min-width: 105px; max-width: 105px" class="text-right">{{
            formatMoney(product.cost - product.depreciation_value) }}</td>
          <td style="min-width: 90px; max-width: 90px" class="text-center" @click.stop @dblclick.stop>
            <div class="function-table">
              <div v-tippy="tooltip.EditButton" class="icon icon-edit"
                @click="onUpdate({ ...product, editMode: 'edit' })"></div>
              <div v-tippy="tooltip.DuplicateButton" class="icon icon-duplicate"
                @click="onDuplicate({ ...product, editMode: 'duplicate' })">
              </div>
            </div>
          </td>
        </tr>
      </tbody>
      <tfoot>
        <tr>
          <td colspan="3">Tổng số: <strong>{{ totalRecord }}</strong> bản ghi</td>
          <td colspan="3">
            <div class="d-flex align-items-center">
              <MISAPagination v-model:pageNumber="pageNumber" v-model:pageSize="pageSize" :totalRecord="totalRecord">
              </MISAPagination>
            </div>
          </td>
          <td class="text-end">
            <strong>{{ formatMoney(totalQuantity) }}</strong>
          </td>
          <td class="text-end">
            <strong>{{ formatMoney(totalPrice) }}</strong>
          </td>
          <td class="text-end">
            <strong>{{ formatMoney(totalDepreciation) }}</strong>
          </td>
          <td class="text-end">
            <strong>{{ formatMoney(totalResidual) }}</strong>
          </td>
          <td></td>
        </tr>
      </tfoot>
      <MISALoading v-if="isLoading"></MISALoading>
    </table>
  </div>

  <MISAForm :getPagging="getPagging" :products="products" v-if="isFormVisible" :isFormVisible="isFormVisible"
    @close-form="closeForm" :productEdit="productForUpdate">
  </MISAForm>

  <MISAContextMenu :posTop="posTop" :posLeft="posLeft" v-if="isShowContextMenu" @clickEdit="onClickEditContextMenu"
    @clickDelete="onClickDeleteContextMenu" @closeContextMenu="closeContextMenu"
    @clickReplication="onClickReplicationContextMenu" @mouseleave="closeContextMenu" />


  <MISAPopup v-if="isShowPopup" @hidePopup="hidePopup" :msg="msgDelete" @loadData="loadData" :name="btnName"
    :btnLeft="btnLeftName" :close="closeStatus" @isDelete="deleted" :item="itemDelete"></MISAPopup>
</template>
 
<script>
import { directive } from 'vue-tippy'
import axios from 'axios';
import { productURL, departmentURL, categoryURL, filtersURL } from "@/js/common/urlAsset";
import { toast } from 'vue3-toastify';
import 'vue3-toastify/dist/index.css';
import { btnPopup, NoticeMsg, ErrorMsg, Tooltip, Msg } from "../../js/common/resource";
import { CloseST } from "../../js/common/enum";
import { Table } from "../../js/common/table"
import MISAFunction from "../../js/common/function"
export default {
  name: "ProductList",
  directives: {
    tippy: directive,
  },
  data() {
    return {
      isShowContextMenu: false,
      posTop: 10,
      posLeft: 10,
      productURL: productURL,
      departmentURL: departmentURL,
      categoryURL: categoryURL,
      filtersUrl: filtersURL,
      totalRecord: 0,
      products: [],
      selectedProducts: [],
      ids: [],
      isFormVisible: false,
      comboboxUrl: departmentURL,
      isLoading: false,
      isShowPopup: false,
      msgDelete: "",
      btnNames: "",
      closeStatus: "",
      btnLeftName: "",
      productForUpdate: {},
      productForFilter: {
        department_id: "",
        department_code: "",
        fixed_asset_category_id: "",
        fixed_asset_category_code: "",
      },
      itemDelete: "",
      productForAdd: {
        fixed_asset_name: "",
        department_id: "",
        department_code: "",
        fixed_asset_category_id: "",
        fixed_asset_category_code: "",
        purchase_date: new Date(Date.now()),
        used_date: new Date(Date.now()),
        cost: 0,
        quantity: 1,
        depreciation_rate: 0,
        depreciation_value: 0,
        production_year: 2023,
      },
      totalQuantity: 0,
      totalPrice: 0,
      totalDepreciation: 0,
      totalResidual: 0,
      pageSize: 100,
      pageNumber: 1,
      textSearch: "",
      modifiedProducts: [],
      tableInfo: Table,
      tooltip: Tooltip,
      listProduct: [],
      currentProduct: {},
      listOnMouseDown: {},
      listOnMouseUp: {},
      toastMsg: Msg,
    };
  },
  /**
   * @author: DDTung (27/06/2023)
   * @description: Lấy api sản phẩm 
   */
  async created() {
    this.isLoading = true;
    await this.loadData();
    this.setFocusFirstInput();
    this.handleCaculate()
    this.isLoading = false;
  },

  watch: {

    pageNumber: function () {
      this.getPagging()
    },

    pageSize: function () {
      this.getPagging()
    },


    textSearch: function () {
      if (this.textSearch == "") {
        this.getPagging()
      }
    },

    'productForFilter.fixed_asset_category_id': function (newValue, oldValue) {
      if (newValue != null && newValue != oldValue && newValue != "") {
        this.getPagging();
      } else if (this.productForFilter.department_id != "") {
        this.getPagging();
      }
      else {
        this.loadData()
      }
    },

    'productForFilter.department_id': async function (newValue, oldValue) {
      if (newValue != null && newValue != oldValue && newValue != "") {
        this.getPagging();
      } else {
        if (this.productForFilter.fixed_asset_category_id != "") {
          this.getPagging();
        }
        else {
          this.loadData()
        }
      }
    }
  },

  methods: {

    /**
     *  @description: Lấy dữ liệu khi khởi tạo
     *  @author: DDTung(24/07/2023)
     */
    async loadData() {
      try {
        await this.getPagging();
      } catch (error) {
        console.error(error);
      }
    },

    /**
    * @description: focus vào ô đầu tiên
    * @author: DDTung (16/07/2023)
    */
    setFocusFirstInput() {
      this.$refs.textSearch.focus()
    },

    /**
    * @returns: Hàm chạy khi ấn thêm mới
    * @author: DDTung (27/06/2023)
    */
    handleAddForm() {
      this.productForUpdate = this.productForAdd
      this.isFormVisible = true;
    },

    /**
    * @returns: Hàm chạy khi ấn đóng form
    * @author: DDTung (27/06/2023)
    */
    closeForm() {
      this.productForUpdate = {};
      this.isFormVisible = false;
      this.setFocusFirstInput()
    },

    /**
    * @returns: Hàm chạy khi ấn sửa
    * @author: DDTung (27/06/2023)
    */
    onUpdate(product) {
      this.productForUpdate = product
      this.isFormVisible = true;
    },

    /**
    * @returns: Hàm chạy khi ấn nhân bản
    * @author: DDTung (27/06/2023)
    */
    onDuplicate(product) {
      this.productForUpdate = product
      this.isFormVisible = true;
    },
    /**
     * @returns: Hàm hiện thông báo để xoá 
     * @author: DDTung (27/06/2023)
     */
    deleteSelected() {
      if (this.listProduct.length == 0) {
        this.isShowPopup = true;
        this.msgDelete = ErrorMsg.NotChooseProperty;
        this.closeStatus = CloseST.DeleteCloseNotChoose;
        this.btnName = btnPopup.ClosePop;
      }
      else if (this.listProduct.length === 1) {
        this.isShowPopup = true;
        this.itemDelete = this.listProduct[0].fixed_asset_code
        this.closeStatus = CloseST.DeleteOne;
        this.btnLeftName = btnPopup.No;
        this.msgDelete = NoticeMsg.ConfirmDelete;
        this.btnName = btnPopup.Delete;
      } else {
        this.isShowPopup = true;
        this.itemDelete = this.ids.length + " tài sản";
        this.closeStatus = CloseST.DeleteMulti;
        this.btnLeftName = btnPopup.No;
        this.msgDelete = NoticeMsg.ConfirmDelete;
        this.btnName = btnPopup.Delete;
      }
    },


    /**
     * @description: Xoá theo id
     * @param {*} productId 
     * @author: DDTung (23/07/2023)
     */
    async deleteSingleProduct(productId) {
      try {
        await axios.delete(`${productURL}/${productId}`);
        this.showToastSuccess(this.toastMsg.DeleteSuccess)
        this.getPagging()
        this.listProduct = [];
      } catch (error) {
        console.error(error);
      }
    },

    /**
    * @description: Xoá theo list
    * @param {*} productId 
    * @author: DDTung (23/07/2023)
    */
    async deleteMultipleProducts() {
      try {
        this.isLoading = true;
        let listProductId = [];
        this.ids.filter((asset) => {
          listProductId.push(asset);
        });
        await axios.delete(`${productURL}`, {
          headers: {
            'Content-Type': 'application/json'
          },
          data: listProductId
        });
        this.ids = []
        this.getPagging()
        this.showToastSuccess(this.toastMsg.DeleteSuccess)
        this.isLoading = false;
      } catch (err) {
        console.log(err);
      }
    },
    /**
    * @returns: Hàm tính tổng
    * @author: DDTung (27/06/2023)
    */
    handleCaculate() {
      this.caculateTotalQuantity(),
        this.caculateTotalPrice(),
        this.caculateTotalDepreciation(),
        this.caculateTotalResidual()
    },

    /**
   * @returns: Tính tổng số lượng
   * @author: DDTung (27/06/2023)
   */
    caculateTotalQuantity() {
      this.totalQuantity = this.products.reduce((total, product) => total + product.quantity, 0);
    },
    /**
    * @returns: Tính tổng nguyên giá
    * @author: DDTung (27/06/2023)
    */
    caculateTotalPrice() {
      this.totalPrice = this.products.reduce((total, product) => total + product.cost, 0);
    },
    /**
    * @returns: Tính tổng khấu hao
    * @author: DDTung (27/06/2023)
    */
    caculateTotalDepreciation() {
      this.totalDepreciation = this.products.reduce((total, product) => total + product.depreciation_value, 0);
    },

    /**
    * @returns: Tính tổng số tiền còn lại
    * @author: DDTung (27/06/2023)
    */
    caculateTotalResidual() {
      this.totalResidual = this.totalPrice - this.totalDepreciation;
    },

    /**
    * @description: hiển thị popup
    * @author: DDTung (22/07/2023)
    */
    showPopup() {
      this.isShowPopup = !this.isShowPopup;
    },

    /**
     * @description: đóng popup
     * @author: DDTung (22/07/2023)
     */
    hidePopup(value) {
      this.isShowPopup = value;
      this.setFocusFirstInput()
    },

    /**
     * @description: hàm xoá các item
     * @author: DDTung (22/07/2023)
     */
    deleted(value) {
      this.isShowPopup = value;
      if (this.closeStatus == CloseST.DeleteOne) {
        this.deleteSingleProduct(this.listProduct[0].fixed_asset_id)
      } else {
        this.deleteMultipleProducts();
      }
    },

    /**
    * @description: hiển thị toast xoá thành công
    * @author: DDTung (22/07/2023)
    */
    showToastSuccess(msg) {
      toast.success(`${msg}`, {
        position: toast.POSITION.BOTTOM_RIGHT,
        transition: toast.TRANSITIONS.BOUNCE,
      });
    },
    /**
    * @description: hiển thị toast xoá thất bại
    * @author: DDTung (22/07/2023)
    */
    async exportExcel() {
      try {
        const response = await axios.get("https://localhost:7072/api/v1/Products/export", {
          responseType: 'blob', // Đảm bảo dữ liệu trả về là dạng blob (binary large object)
        });
        if (response.status === 200) {
          // Nếu thành công, tiến hành tạo URL tạm thời và tải xuống file Excel
          const url = window.URL.createObjectURL(new Blob([response.data]));
          const link = document.createElement('a');
          link.href = url;
          link.setAttribute('download', 'QLTS.xlsx'); // Đặt tên file Excel cần tải xuống
          document.body.appendChild(link);
          link.click();
          window.URL.revokeObjectURL(url);


        } else {
          toast.error("Xuất excel thất bại!", {
            position: toast.POSITION.BOTTOM_RIGHT,
            transition: toast.TRANSITIONS.BOUNCE,
          });
        }
      } catch (error) {
        toast.error("Lỗi server!", {
          position: toast.POSITION.BOTTOM_RIGHT,
          transition: toast.TRANSITIONS.BOUNCE,
        });
      }
    },
    /**
     * @description: Lấy dữ liệu phân trang
     * @author: DDTung (26/07/2023)
     */
    async getPagging() {
      try {
        let url = `${filtersURL}?pageSize=${this.pageSize}&pageNumber=${this.pageNumber}&searchText=${this.textSearch}`;
        if (this.productForFilter.fixed_asset_category_id != null && this.productForFilter.fixed_asset_category_id != "") {
          url += `&fixed_asset_category_id=${this.productForFilter.fixed_asset_category_id}`
        }
        if (this.productForFilter.department_id != null && this.productForFilter.department_id != "") {
          url += `&department_id=${this.productForFilter.department_id}`
        }
        const response = await axios.get(url);
        this.products = response.data.data
        // this.seletedIds = this.ids.filter((id) => this.products.some((product) => product.fixed_asset_id === id));
        this.listProduct = this.products.filter((product) =>
          this.ids.includes(product.fixed_asset_id)
        );
        // console.log(this.listProduct);
        this.totalRecord = response.data.total_record;
        this.handleCaculate()
      } catch (error) {
        console.log(error);
      }
    },
    /**
     * @description: Ấn enter thì sẽ lọc và phân trang
     * @author: DDTung (26/07/2023)
     */
    onEnterSearch() {
      this.getPagging();
    },

    /**
   * @description: format tiền
   * @author: DDTung (06/08/2023)
   */
    formatMoney(money) {
      return MISAFunction.formatMoney(money);
    },


    /**
    * @description: thêm phần tử xóa và bỏ khi đã được chọn
    * @author: DDTung (07/08/2023)
    */
    selectItemToList(product) {
      try {
        const productId = product.fixed_asset_id
        this.currentProduct = product;

        if (!this.listProduct.includes(product) && !this.ids.includes(productId)) {
          //thực hiện chọn
          this.listProduct.push(product);
          this.ids.push(productId);
        } else {
          //thực hiện bỏ chọn
          this.listProduct = this.listProduct.filter((a) => {
            return a !== product;
          });
          this.ids = this.ids.filter((id) => id !== productId);
        }

        this.currentProduct =
          this.listProduct[this.listProduct.length - 1];
      }
      catch (err) {
        console.log(err);
      }

    },


    /**
    * @description: hàm chọn các item
    * @author: DDTung (07/08/2023)
    */
    selectItem(product) {
      this.listProduct = [];
      this.listProduct.push(product);
      this.currentProduct = product;
    },



    /**
    * @description: hàm chọn tất cả item
    * @author: DDTung (07/08/2023)
    */
    selectedAllItem() {
      try {
        if (this.listProduct.length < this.products.length) {
          this.listProduct = this.products;
          this.ids = this.products.map(product => product.fixed_asset_id)
        } else {
          this.listProduct = [];
          this.ids = [];
        }
      } catch (err) {
        console.log(err);
      }
    },

    /**
   * @description: up shift thả chuột để lấy vị trí cuối cùng chọn
   * @author:  DDTung (07/08/2023)
   */
    mouseUp(asset) {
      try {
        this.listOnMouseUp = asset;
        this.selectMultipleItem(this.listOnMouseDown, this.listOnMouseUp);
        this.listOnMouseDown = {};
        this.listOnMouseUp = {};
      } catch (err) {
        console.log(err);
      }
    },
    /**
     *
     * @description:ấn shift
     * @author:  DDTung (07/08/2023)
     */
    mouseDown(asset) {
      try {
        // lưu tài sản khi mousedown
        this.listOnMouseDown = asset;
      } catch (err) {
        console.log(err);
      }
    },

    selectMultipleItem(item1, item2) {
      //đoạn này là lấy những tài sản khi kéo giữ chuột
      if (
        this.products.includes(item1) &&
        this.products.includes(item2)
      ) {
        // lấy vị trí của 2 item trong mảng fixedAsset
        //start là vị trí đầu khi down ctrl
        let startIndex = this.products.indexOf(item2);
        //end là vị trí khi up ctrl
        let endIndex = this.products.indexOf(item1);
        // kiểm tra vị trí bắt đầu và kết thúc nếu bắt đầu lớn hơn kết thúc thì đổi lại
        if (startIndex > endIndex) {
          let tmp = startIndex;
          startIndex = endIndex;
          endIndex = tmp;
        }

        // thêm các item chưa có trong mảng xóa từ vị trí bắt đầu đến kết thúc
        for (let i = startIndex; i <= endIndex; i++) {
          //nếu trong danh sách chưa gồm tài sản đó thì push vào danh sách
          if (!this.listProduct.includes(this.products[i]) && !this.ids.includes(this.products[i].fixed_asset_id)) {
            this.listProduct.push(this.products[i]);
            this.ids.push(this.products[i].fixed_asset_id);
          }
        }
      }
    },

    /**
    * @description: click lấy vị trí context menu
    * @author:  DDTung (07/08/2023)
    */
    onClickContextMenu(product, e) {
      e.preventDefault();
      this.selectItem(product);
      this.posTop = e.clientY - 50;
      this.posLeft = e.clientX;
      this.isShowContextMenu = true;
    },



    /**
   * @description click vào sửa trong  chức năng context menu
   * @author:  DDTung (07/08/2023)
   */
    onClickEditContextMenu() {
      this.isShowContextMenu = false;
      this.onUpdate({ ...this.currentProduct, editMode: 'edit' });
    },

    /**
    * @description click vào xoá trong  chức năng context menu
    * @author:  DDTung (07/08/2023)
    */
    onClickDeleteContextMenu() {
      this.isShowContextMenu = false;
      this.deleteSelected()
    },
    /**
    * @description click vào nhân bản trong  chức năng context menu
    * @author:  DDTung (07/08/2023)
    */
    onClickReplicationContextMenu() {
      this.isShowContextMenu = false;
      this.onDuplicate({ ...this.currentProduct, editMode: 'duplicate' });
    },
    /**
    * @description hàm đóng context menu khi di chuột ra ngoài
    * @author:  DDTung (07/08/2023)
    */
    closeContextMenu() {
      this.isShowContextMenu = false;
    },



  }
}


</script>

<style lang="scss">
/* CSS for the checkbox */
@keyframes fadeIn {
  from {
    opacity: 0;
  }

  to {
    opacity: 1;
  }
}

.default-input {
  background-color: #fff;
  border-radius: 2.5px;
  border: 1px solid #afafaf;
  width: 180px;
  height: 35px;
  overflow: hidden;
  padding-left: 38px;

  &:focus {
    border-color: var(--primary-color);
  }
}

.content__input {
  input {
    &::placeholder {
      font-size: 13px;
      font-style: italic;
    }

    padding: 0 36px;
    margin-right: 11px;
  }

  position: relative;
}

.input-icon {
  position: absolute;
  width: 24px;
  height: 24px;
  top: 50%;
  transform: translateY(-50%);
  left: 8px;
}

.icon-search {
  background: var(--icon-url) no-repeat -240px -20px;
}

.l-main {
  .v-combobox__body {
    width: 180px !important;
    border-radius: 2.5px;
  }

  .v-combobox__selected {
    height: 35px;
    padding: 0 35px 0 40px !important;
    border: 1px solid #afafaf !important;
    border-radius: 2.5px !important;

    &:hover {
      border: 1px solid #1aa4c8 !important;
    }
  }

  .v-combobox__focus {
    border: 1px solid #1aa4c8 !important;
  }

  .v-combobox__icon[data-v-fdaf08aa] {
    background: var(--icon-url) no-repeat -62px -322px !important;
  }

  .icon-filter {
    background: var(--icon-url) no-repeat -240px -65px;
  }

  .v-select__list {
    top: 100%;
    min-height: 160px;
  }
}

.r-main {
  button {
    padding: 0 !important;
    border: unset !important;
  }
}

table {
  width: 100%;
  height: 100%;
  border-collapse: collapse;

  tr {
    text-align: left;
    height: 40px;
    white-space: nowrap;
  }

  &:focus {
    border: none;
    outline: 0;
  }

  thead {
    tr {
      height: 38px;
    }
  }

  td {
    padding: 0 10px;
    max-width: 100px;
    min-width: 50px;
    overflow-x: hidden;
    white-space: nowrap;
    text-overflow: ellipsis;

    &:first-child {
      padding: 5px 10px 0 16px;
    }
  }

  th {
    padding: 0 10px;
    max-width: 100px;
    min-width: 50px;
    overflow-x: hidden;
    white-space: nowrap;
    text-overflow: ellipsis;

    &:first-child {
      padding: 5px 10px 0 16px;
    }
  }

  tbody {
    tr {
      max-height: 36px !important;
      border-bottom: 1px solid #e2e2e2;
      cursor: pointer;

      &:hover {
        background-color: rgba(26, 164, 200, .2);

        .function-table {
          display: flex;
        }
      }
    }
  }

  .v-combobox__icon {
    background: var(--icon-url) no-repeat -62px -328px !important;
  }

  .icon {
    width: 20px;
    height: 20px;
    display: flex;
    justify-content: center;
    align-items: center;
    cursor: pointer;
  }

  .v-select__items {
    text-align: center !important;
  }
}

.table {
  background-color: #fff;
  overflow: auto;
  margin: 0;
  position: relative;
  border-radius: 3.5px;
  border: 1px solid #afafaf;
  box-shadow: 0 3px 10px rgba(0, 0, 0, .16);
  width: 100%;
  position: relative;
  height: 90%;

  .lds-dual-ring[data-v-5f38b7f1] {
    width: 100%;
    height: 100%;
    top: 50%;
    left: 50%;
    transform: translate(-50%, -50%);
    display: flex;
    align-items: center;
    justify-content: center;
    background-color: rgb(255, 255, 255);

    &:after {
      border-color: #1aa4c8 transparent #1aa4c8 transparent;
    }
  }

  &::-webkit-scrollbar {
    width: 2px;
    height: 5px;
    border-top: 1px solid #e2e2e2;
  }

  &::-webkit-scrollbar-thumb {
    border-radius: 2px;
    background-color: #ccc;
  }
}

td.text {
  text-align: left;
}

th.text {
  text-align: left;
}

tfoot {
  position: sticky;
  left: 0;
  background-color: #f5f5f5;
  border-color: inherit;
  border-style: unset;
  border-width: 0;
  bottom: 0px;
  z-index: 1;
  border-top: 1px solid #e2e2e2;

  tr {
    background-color: #ebebeb !important;
  }

  td {
    border-top: 1px solid #e2e2e2;
    overflow: unset;
  }
}

thead {
  position: sticky;
  left: 0;
  background-color: #f5f5f5;
  border-color: inherit;
  border-style: unset;
  border-width: 0;
  top: 0;
  z-index: 1;
  border-bottom: 1px solid #e2e2e2;

  tr {
    background-color: #ebebeb !important;
  }

  th {
    border-bottom: 1px solid #e2e2e2;
    overflow: unset;
  }
}

tbody {
  border-color: inherit;
  border-style: unset;
  border-width: 0;
}

tr {
  border-color: inherit;
  border-style: unset;
  border-width: 0;
}

td {
  border-color: inherit;
  border-style: unset;
  border-width: 0;
}

th {
  border-color: inherit;
  border-style: unset;
  border-width: 0;
}

td.number {
  text-align: right;
}

th.number {
  text-align: right;
}

.tr-active {
  background-color: rgba(26, 164, 200, .2) !important;
}

.text-center {
  text-align: center;
}

.text-right {
  text-align: end;
}

.function-table {
  display: none;
  justify-content: center;
}

.icon-duplicate {
  background: var(--icon-url) no-repeat -240px -108px;
}

.icon-edit {
  background: var(--icon-url) no-repeat -152px -64px;
  margin-right: 20px;
}

.icon-prev {
  background: var(--icon-url) no-repeat -199px -242px;
}

.icon-next {
  background: var(--icon-url) no-repeat -242px -242px;
}

.icon-noData {
  background: url("../../assets/img/bg_report_nodata.76e50bd8.svg") no-repeat 0 0;
  width: 132px;
  height: 76px;
}

.no-data {
  display: flex;
  align-items: center;
  justify-content: center;
  flex-direction: column;
}

.empty-data {
  height: 500px !important;

  &:active {
    background-color: unset !important;
  }

  &:hover {
    background-color: unset;
    cursor: unset;
  }
}

input[type="checkbox"] {
  appearance: none;
  -webkit-appearance: none;
  -moz-appearance: none;
  outline: none;
  cursor: pointer;
  position: relative;
  width: 15px;
  height: 15px;
  border: 2px solid #ccc;
  background-color: #fff;
  border-radius: 4px;
  transition: transform 0.3s ease-in-out;

  &:checked {
    background-color: #1aa4c8;
    border-color: #1aa4c8;
    animation: fadeIn 0.3s ease-in-out;
    transform: rotate(360deg);

    &:disabled {
      background-color: #999;
      border-color: #999;
    }

    &::before {
      visibility: visible;
    }
  }

  &:not(:checked) {
    &:disabled {
      background-color: #ddd;
      border-color: #ddd;
    }
  }

  &::before {
    content: "\2713";
    position: absolute;
    top: 50%;
    left: 50%;
    transform: translate(-50%, -50%);
    font-size: 12px;
    color: #fff;
    visibility: hidden;
  }
}

.context-menu-overlay {
  position: fixed;
  top: 0;
  left: 0;
  width: 100%;
  height: 100%;
  background-color: rgba(255, 255, 255, 0);
  z-index: 2;
}
</style>