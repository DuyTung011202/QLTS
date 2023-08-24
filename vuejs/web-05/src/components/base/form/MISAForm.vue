<template>
  <div id="form">
    <div class=" form">
      <div class="form-header">
        <h3>{{ this.formMode === 0 ? 'Thêm tài sản' : (this.formMode === 1 ? 'Sửa tài sản' : 'Nhân bản tài sản') }}</h3>
        <div v-tippy="tooltip.CloseButton" class="icon icon-close" @click="btnCloseOnClick"></div>
      </div>
      <div class="form-body">
        <div class="row-input ">
          <div class="row-input-left position-relative">
            <label for="product-code">{{ formInfo.fixed_asset_code }}
              <span>*</span>
            </label>
            <div class="content__input">
              <input v-model="product.fixed_asset_code" type="text" :placeholder="formPlaceholder.fixed_asset_code"
                id="product-code" tabindex="7" :class="inValidErrors.fixed_asset_code ? 'border-danger' : ''"
                @blur="validate('fixed_asset_code')" @focus="focusInput('fixed_asset_code')" />
            </div>
            <span v-if="inValidErrors.fixed_asset_code != ''" class="position-absolute top-100 start-0 text-danger">{{
              formInfo.fixed_asset_code
            }} {{
  inValidErrors.fixed_asset_code }}</span>
          </div>
          <div class="row-input-right position-relative">
            <label for="product-name">{{ formInfo.fixed_asset_name }}
              <span>*</span>
            </label>
            <div class="content__input">
              <input v-model="product.fixed_asset_name" type="text" :placeholder="formPlaceholder.fixed_asset_name"
                id="product-name" tabindex="8" :class="inValidErrors.fixed_asset_name ? 'border-danger' : ''"
                @blur="validate('fixed_asset_name')" @focus="focusInput('fixed_asset_name')" />
            </div>
            <span v-if="inValidErrors.fixed_asset_name != ''" class="position-absolute top-100 start-0 text-danger">{{
              formInfo.fixed_asset_name
            }} {{
  inValidErrors.fixed_asset_name }}</span>
          </div>
        </div>
        <div class="row-input ">
          <div class="row-input-left position-relative ">
            <MISACombobox :propApi="departmentURL" propKey="department_id" propValue="department_code"
              v-model:textInput="product.department_code" v-model="product.department_id"
              :customClass="inValidErrors.department_code ? 'border-danger' : ''"
              :placeholder="formPlaceholder.department_code" :label="formInfo.department_code" required tab="9">
              <template #item="item">
                {{ item.option['department_code'] }} - {{ item.option['department_name'] }}
              </template>
            </MISACombobox>
            <div class="error-wrapper">
              <span v-if="inValidErrors.department_code != ''" class="position-absolute top-100 start-0 text-danger">{{
                formInfo.department_code }} {{
    inValidErrors.department_code }}</span>
            </div>
          </div>
          <div class="row-input-right">
            <label for="department-name">{{ formInfo.department_name }}</label>
            <br />
            <div class="content__input">
              <input v-model="product.department_name" readonly type="text" id="department-name" />
            </div>
          </div>
        </div>
        <div class="row-input">
          <div class="row-input-left position-relative">
            <MISACombobox :propApi="categoryURL" propKey="fixed_asset_category_id" propValue="fixed_asset_category_code"
              v-model="product.fixed_asset_category_id" v-model:textInput="product.fixed_asset_category_code"
              :customClass="inValidErrors.fixed_asset_category_code ? 'border-danger' : ''"
              :placeholder="formPlaceholder.fixed_asset_category_code" :label="formInfo.fixed_asset_category_code"
              required tab="10">
              <template #item="item">
                {{ item.option['fixed_asset_category_code'] }} - {{ item.option['fixed_asset_category_name'] }}
              </template>
            </MISACombobox>
            <span v-if="inValidErrors.fixed_asset_category_code != ''"
              class="position-absolute top-100 start-0 text-danger">{{
                formInfo.fixed_asset_category_code
              }} {{
  inValidErrors.fixed_asset_category_code }}</span>
          </div>
          <div class="row-input-right">
            <label for="product-type-name">{{ formInfo.fixed_asset_category_name }}</label>
            <br />
            <div class="content__input">
              <input v-model="product.fixed_asset_category_name" readonly type="text" id="product-type-name" />
            </div>
          </div>
        </div>
        <div class="row-input-cl3">
          <div class="item-input-cl3 position-relative">
            <label for="product-quantity">{{ formInfo.quantity }} <span>*</span></label>
            <MISAComboboxUpdown v-model="product.quantity" tab="11" customType="text" :inputNumber="inputEnterNumber"
              enableKeypress customId="product-quantity" :formatQuantity="true"
              :customClass="inValidErrors.quantity ? 'border-danger no-spinners text-end' : 'no-spinners text-end'" />
            <span v-if="inValidErrors.quantity != ''" class="position-absolute top-100 start-0  text-danger">{{
              formInfo.quantity
            }} {{
  inValidErrors.quantity }}</span>
          </div>
          <div class="item-input-cl3 position-relative">
            <label for="product-price">{{ formInfo.cost }} <span>*</span></label>

            <div class="content__input">
              <input ref="cost" @focus="focusInput('cost')" v-model="product.cost" type="text" @input="formatCost"
                id="product-price" @keypress="inputEnterNumber($event)" tabindex="12"
                :class="inValidErrors.cost ? 'border-danger text-end' : 'text-end'" @blur="validate('cost')" />
            </div>
            <span v-if="inValidErrors.cost != ''" class="position-absolute top-100 start-0 text-danger">{{
              formInfo.cost
            }} {{
  inValidErrors.cost }}</span>
          </div>
          <div class="item-input-cl3 position-relative">
            <label for="life-time">{{ formInfo.life_time }} <span>*</span></label>
            <br />
            <div class="content__input">
              <input type="number" tabindex="13" v-model="product.life_time"
                :class="inValidErrors.life_time ? 'border-danger no-spinners item-input text-end' : 'no-spinners item-input text-end'"
                id="life-time" @blur="validate('life_time')" @focus="focusInput('life_time')" />
            </div>
            <span v-if="inValidErrors.life_time != ''" class="position-absolute top-100 start-0 text-danger"> {{
              formInfo.life_time
            }} {{
  inValidErrors.life_time }}</span>
          </div>
        </div>
        <div class="row-input-cl3 ">
          <div class="item-input-cl3 position-relative">
            <label for="product-depreciation-rate">{{ formInfo.depreciation_rate }} (%) <span>*</span></label>
            <MISAComboboxUpdown v-model="product.depreciation_rate"
              :customClass="inValidErrors.depreciation_rate ? 'border-danger no-spinners text-end' : 'no-spinners text-end'"
              customId="product-depreciation-rate" tab="14" customType="text" />
            <span v-if="inValidErrors.depreciation_rate != ''" class="position-absolute top-100 start-0 text-danger">{{
              formInfo.depreciation_rate }} {{
    inValidErrors.depreciation_rate }}</span>
          </div>
          <div class="item-input-cl3 position-relative">
            <label for="product-depreciation-value">{{ formInfo.depreciation_value }} <span>*</span></label>
            <br />
            <div class="content__input">
              <input readonly type="text" v-model="product.depreciation_value"
                :class="inValidErrors.depreciation_value ? 'border-danger text-end' : 'text-end'"
                id="product-depreciation-value" />
            </div>
            <span v-if="inValidErrors.depreciation_value != ''" class="position-absolute top-100 start-0 text-danger">{{
              formInfo.depreciation_value
            }} {{
  inValidErrors.depreciation_value }}</span>
          </div>
          <div class="item-input-cl3">
            <label for="year-tracking">{{ formInfo.production_year }}</label>
            <br />
            <div class="content__input">
              <input readonly class="text-end" id="year-tracking" v-model="product.production_year" />
            </div>
          </div>
        </div>
        <div class="row-input-end">
          <div class="item-input-cl3">
            <label for="purchase-date">{{ formInfo.purchase_date }}</label>
            <br />
            <ElDatePicker v-model="product.purchase_date" tabindex="15" type="date" :placeholder="dateConfig.Format"
              :format="dateConfig.Format"></ElDatePicker>
          </div>
          <div style="padding-right: 60px;" class="item-input-cl3 position-relative ">
            <label for="used-date">{{ formInfo.used_date }}</label>
            <br />
            <ElDatePicker v-model="product.used_date" tabindex="16" type="date" :placeholder="dateConfig.Format"
              @blur="validate('used_date')" :format="dateConfig.Format" @focus="focusInput('used_date')"
              :class="inValidErrors.used_date ? 'border-red' : ''"></ElDatePicker>
            <span v-if="inValidErrors.used_date != ''" class="position-absolute top-100 start-0 text-danger">{{
              formInfo.used_date
            }} {{
  inValidErrors.used_date }}</span>
          </div>
        </div>

      </div>
      <div class="form-footer">
        <MISAButton :text="formInfo.btnSave" class="btn-save btn--primary text-white" @click="btnSaveOnClick" tab="16">
        </MISAButton>
        <MISAButton :text="formInfo.btnCancel" class="btn--primary btn-cancel " @click="btnCloseOnClick" tab="17"
          @keydown.tab.prevent="setFocusFirstInput()">
        </MISAButton>
      </div>
      <MISALoading v-if="isLoading"></MISALoading>
    </div>
    <MISAPopup v-if="isShowPopup" @hidePopupFocus="hidePopupFocus" @hidePopup="hidePopup" @saveAndHideForm="saveAndHide"
      :btnLeft="btnLeftName" @hidePopupAndForm="hidePopupForm" :msg="msgError" :name="btnName" :close="closeStatus">
    </MISAPopup>
  </div>
</template>

<script>
import { Form, formPlaceholder } from "../../../js/common/form.js"
import { DateConfig } from "../../../js/common/config.js";
import MISAFunction from "../../../js/common/function";
import { ErrorMsg, btnPopup, NoticeMsg, Tooltip, Msg } from "../../../js/common/resource";
import { departmentURL } from "@/js/common/urlAsset";
import { categoryURL, productURL } from "@/js/common/urlAsset";
import { CloseST, ErrorCode } from "../../../js/common/enum";
import axios from "axios";
import { toast } from 'vue3-toastify';
import 'vue3-toastify/dist/index.css';
import { directive } from 'vue-tippy'

export default {
  name: "MISAForm",
  directives: {
    tippy: directive,
  },
  props: {
    productEdit: {
      type: Object
    },
    products: {
      type: Array
    },
    getPagging: {
      type: Function
    }
  },
  data() {
    return {
      toastMsg: Msg,
      tooltip: Tooltip,
      formInfo: Form,
      formPlaceholder: formPlaceholder,
      dateConfig: DateConfig,
      counter: 1,
      product: {},
      oldData: {},
      isShowPopup: false,
      closeStatus: 0,
      btnLeftName: "",
      msgError: "",
      btnName: "",
      isLoading: false,
      departmentURL: departmentURL,
      categoryURL: categoryURL,
      ErrorMsg: ErrorMsg,
      isValidAll: false,
      currentTabindex: 1,
      inValidErrors: {
        fixed_asset_code: "",
        fixed_asset_name: "",
        department_code: "",
        fixed_asset_category_code: "",
        quantity: "",
        cost: "",
        life_time: "",
        depreciation_rate: "",
        depreciation_value: "",
        used_date: "",
      },
      rules: {
        fixed_asset_code: { Required: true, Length: 20 },
        fixed_asset_name: { Required: true, Length: 255 },
        fixed_asset_category_code: { Required: true },

        used_date: { UseDay: true },
        department_code: { Required: true },
        cost: { Required: true, Length: 15 },
        depreciation_rate: { Major: 100 },
        life_time: { Required: true },
        depreciation_value: { Required: true },
        quantity: { Required: true },
      },

    }
  },
  async created() {
    this.oldData = { ...this.productEdit };
    if (this.formMode == this.$_MISAEnum.FORM_MODE.ADD) {
      this.oldData.fixed_asset_code = await this.getNewCode()
    }
    if (this.formMode == this.$_MISAEnum.FORM_MODE.DUPLICATE) {
      this.oldData.fixed_asset_code = await this.getNewCode()
    }
    this.oldData.quantity = this.formatMoney(this.productEdit.quantity)
    this.oldData.cost = this.formatMoney(this.productEdit.cost);
    this.oldData.depreciation_rate = this.productEdit.depreciation_rate.toFixed(2)
    this.oldData.depreciation_value = this.formatMoney(
      this.productEdit.depreciation_value
    );
    this.product = { ...this.productEdit };
    if (this.formMode == this.$_MISAEnum.FORM_MODE.ADD) {
      this.product.fixed_asset_code = await this.getNewCode()
    }
    if (this.formMode == this.$_MISAEnum.FORM_MODE.DUPLICATE) {
      this.product.fixed_asset_code = await this.getNewCode()
    }
    this.product.quantity = this.formatMoney(this.productEdit.quantity)
    this.product.depreciation_rate = this.productEdit.depreciation_rate.toFixed(2)
    this.product.cost = this.formatMoney(this.productEdit.cost);
    this.product.depreciation_value = this.formatMoney(
      this.productEdit.depreciation_value
    );
    this.setFocusFirstInput();
  },

  /**
   * @description: thêm sự kiện ấn esc khi mở form
   * @author: DDTung (08/08/2023)
   */
  mounted() {
    document.addEventListener("keydown", this.onDocumentKeydown);
  },
  /**
   * @description: xoá sự kiện ấn esc khi mở form
   * @author: DDTung (08/08/2023)
   */
  beforeUnmount() {
    document.removeEventListener("keydown", this.onDocumentKeydown);
  },
  watch: {
    /**
     * @description: Số năm sử dụng thay đổi thì tự động thay đổi tỷ lệ hao mòn
     * @param {*} newValue 
     * @author: DDTung (08/08/2023)
     */
    'product.life_time': function (newValue) {
      if (newValue != "") {
        this.inValidErrors.life_time = ""
      }
      this.product.depreciation_rate = (100 / this.product.life_time).toFixed(2)
    },
    /**
    * @description: Số lượng thay đổi thì check lỗi
    * @param {*} newValue 
    * @author: DDTung (08/08/2023)
    */
    'product.quantity': function (newValue) {
      if (newValue == 0) {
        this.inValidErrors.quantity = ErrorMsg.QuantityError
      } else {
        this.inValidErrors.quantity = ""
      }
    },
    /**
    * @description: Tỷ lệ hao mòn thay đổi thì tự động check lỗi
    * @param {*} newValue 
    * @author: DDTung (08/08/2023)
    */
    'product.depreciation_rate': function () {
      if (this.product.depreciation_rate == (100 / this.product.life_time)) {
        this.inValidErrors.depreciation_rate = ""
      }
      if (this.product.depreciation_rate == Infinity) {
        this.product.depreciation_rate = 0
      }
      this.formatCost();
    },
    /**
    * @description: Khi id của bộ phận sử dụng thay đổi thì tự động lấy tên bộ phận sử dụng
    * @param {*} newValue 
    * @author: DDTung (08/08/2023)
    */
    'product.department_id': function (newVal) {
      if (newVal !== null) {
        this.inValidErrors.department_code = "";
        this.getProductDepartmentName(newVal).then(department => {
          this.product.department_name = department;
        })
      } else {
        this.product.department_name = "";
      }
    },
    /**
    * @description: Khi id của mã tài sản thay đổi thì tự động lấy tên tài sản 
    * @param {*} newValue 
    * @author: DDTung (08/08/2023)
    */
    'product.fixed_asset_category_id': function (newVal) {
      if (newVal !== null) {
        this.inValidErrors.fixed_asset_category_code = "";
        this.getProductCategory(newVal).then(category => {
          this.product.fixed_asset_category_name = category.fixed_asset_category_name;
          this.product.life_time = category.life_time;
        })
      } else {
        this.product.fixed_asset_category_name = "";
        this.product.life_time = "";
      }
    },
  },


  computed: {
    formMode() {
      if (this.productEdit.editMode == 'duplicate') {
        return 2
      }
      else if (this.productEdit.editMode == 'edit') {
        return 1;
      }
      else {
        return 0;
      }
    },
  },

  methods: {
    /**
     * @description: Đóng form
     * @author: DDT(26/06/2023)
     */
    btnCloseOnClick() {
      let checkDataNoChange = JSON.stringify(this.oldData) == JSON.stringify(this.product)
      if (checkDataNoChange) {
        this.handleCloseForm()
      } else {
        this.showPopup();
        if (this.formMode == this.$_MISAEnum.FORM_MODE.ADD) {
          this.closeStatus = CloseST.AddClose;
          this.btnLeftName = btnPopup.No;
          this.msgError = NoticeMsg.ProopertyAdd;
          this.btnName = btnPopup.Cancel;
        } else {
          this.closeStatus = CloseST.EditClose;
          this.msgError = NoticeMsg.PropertyEdited;
          this.btnLeftName = btnPopup.Close;
          this.btnName = btnPopup.Save;
        }
      }

    },


    /**
    * @description: đóng form
    * @author: DDT(26/06/2023)
    */
    handleCloseForm() {
      this.$emit("close-form");
    },



    /**
   * @description: format tiền
   * @author: DDT(26/06/2023)
   */
    formatCost() {
      this.product.cost = this.formatMoney(this.product.cost);
      this.product.depreciation_value = this.formatMoney(parseInt((
        this.convertMoneyToNum(this.product.cost) *
        this.product.depreciation_rate
      ) / 100));
    },



    /**
    * @description: Chuyển tiền thành số
    * @param {*} data 
    * @author: DDTung (04/07/2023)
    */
    convertMoneyToNum(data) {
      return MISAFunction.convertMoneyToNum(data);
    },

    /**
    * @description: format tiền
    * @author: DDTung (03/07/2023)
    */
    formatMoney(money) {
      return MISAFunction.formatMoney(money);
    },

    /**
    * @description: hàm chỉ cho nhập số
    * @author: DDTung (03/07/2023)
    */
    inputEnterNumber(evt) {
      return MISAFunction.inputEnterNumber(evt);
    },


    /**
    * @description: hàm validate tổng
    * @author: DDTung (23/07/2023)
    */
    validateAll() {
      this.isValidAll = true; // biến check lỗi tổng
      for (const propName in this.rules) {
        let isValid = true; // biến check lỗi khi duyệt qua 1 trường dữ liệu
        for (const key in this.rules[propName]) {
          if (isValid) {
            let functionName = `validate${key}`;
            // kiểm tra nếu còn đúng thì validate tiếp
            if (isValid == true) {
              isValid = this[functionName](this.rules[propName][key], propName);
            }
          }
        }
        if (this.isValidAll) {
          this.isValidAll = isValid;
        }
      }
      if (!this.isValidAll) {
        this.focusToInputError();
      }
      return this.isValidAll;

    },

    /**
    *
    * @description: validate duyệt từng hàm validate chi tiết
    * @author: DDTung (23/07/2023)
    */
    validate(propName) {
      let isValid = true; //  biến lưu giá trị validate sau mỗi vòng for
      for (const key in this.rules[propName]) {
        let functionName = `validate${key}`;
        if (isValid == true) {
          isValid = this[functionName](this.rules[propName][key], propName);
        }
      }
    },
    /**
    * @description: validate bắt buộc nhập
    * @author: DDTung(23/07/2023)
    */
    validateRequired(value, propName) {
      // kiểm tra rỗng thì lưu lại lỗi và trả về false
      if (!this.product[propName]) {
        this.inValidErrors[propName] = ErrorMsg.Required;
        return false;
      } else {
        this.inValidErrors[propName] = "";
        return true;
      }
    },


    /**
    * @description: Validate độ dài của mã tài sản
    * @author: DDTung(23/07/2023)
    */
    validateLength(value, propName) {
      // kiểm tra độ dài của mã tài sản
      if (this.product[propName]) {
        if (this.product[propName].length > value) {
          this.inValidErrors[propName] = ErrorMsg.LengthError + value + " ký tự";
          return false;
        } else {
          return true;
        }
      }
    },


    /**
     * @description: validate tỉ lệ với năm
     * @author: DDTung(23/07/2023)
     */
    validateMajor(value, propName) {
      // số năm sử dụng thay đổi thì kiểm tra lại tỷ lệ hao mòn
      if (propName == "depreciation_rate") {
        if (this.product.depreciation_rate != parseFloat(
          (value / this.product.life_time).toFixed(2)
        )) {
          this.inValidErrors[propName] = ErrorMsg.AttritionRate;
          return false
        } else {
          return true;
        }
      }
    },

    /**
     * @description: validate ngày sử dụng k nhỏ hơn ngày mua
     * @author: DDTung(23/07/2023)
     */
    validateUseDay(value, propName) {
      var used = new Date(this.product.used_date);
      var purchase = new Date(this.product.purchase_date);
      if (propName == "used_date") {
        if (used.getFullYear() < purchase.getFullYear()) {
          this.inValidErrors[propName] = ErrorMsg.UsedYear;
          return false;
        } else if (used.getFullYear() == purchase.getFullYear()) {
          if (used.getMonth() + 1 < purchase.getMonth() + 1) {
            this.inValidErrors[propName] = ErrorMsg.UsedYear;
            return false;
          } else if (used.getMonth() + 1 == purchase.getMonth() + 1) {
            if (used.getDate() < purchase.getDate()) {
              this.inValidErrors[propName] = ErrorMsg.UsedYear;
              return false;
            } else {
              this.inValidErrors[propName] = "";
              return true;
            }
          } else {
            this.inValidErrors[propName] = "";
            return true;
          }
        } else {
          this.inValidErrors[propName] = "";
          return true;
        }
      }
    },

    /**
   *
   * @description: validate nguyên giá với giá trị hao mòn năm
   * @author: DDTung(23/07/2023)
   */
    validatePrice(value, propName) {
      var cost = this.convertMoneyToNum(this.product.cost);
      var rate = parseInt(this.product.depreciation_rate);
      if (propName == "cost") {
        this.product.depreciation_value = parseInt((cost * rate) / 100);
        this.product.depreciation_value = this.formatMoney(
          this.product.depreciation_value
        );
        return true;
      }
    },
    /**
    * @description: focus vào input bị lỗi
    * @author: DDTung(30/07/2023)
    */
    setFocusError(tabindex) {
      if (this.$el.querySelector(`input[tabindex='${tabindex}']`)) {
        this.$el.querySelector(`input[tabindex='${tabindex}']`).focus();
      }
    },

    /**
    * @description: focus vào input bị lỗi
    * @author: DDTung(30/07/2023)
    */
    focusToInputError() {
      try {
        let i = 7;
        for (const key in this.inValidErrors) {
          console.log(key);
          // kiểm tra xem trường nào có lỗi thì focus vào trường đó và break ngay
          if (this.inValidErrors[key] !== "") {
            this.currentTabindex = i;
            this.setFocusError(this.currentTabindex);
            break;
          }
          i++;
        }
      } catch (err) {
        console.log(err);
      }
    },

    /**
     * @description: focus thì bỏ thông báo lỗi trên input
     * @author: DDTung (06/08/2023)
     */
    focusInput(propName) {
      this.inValidErrors[propName] = "";
    },
    /**
    * @description: hàm sự kiện khi ấn lưu
    * @author: DDTung (10/07/2023)
    */
    btnSaveOnClick() {
      try {
        // validate dữ liệu:
        if (this.validateAll()) {
          // Dữ liệu đã hợp lệ thì gọi api thực hiện cất dữ liệu:
          if (this.formMode == this.$_MISAEnum.FORM_MODE.ADD || this.formMode == this.$_MISAEnum.FORM_MODE.DUPLICATE) {
            this.handleAdd()
          } else {
            this.handleEdit()
          }
        } else {
          this.isShowPopup = true;
          this.closeStatus = CloseST.ValiDate;
          this.btnName = btnPopup.Agree;
          this.msgError = ErrorMsg.ValidateBE;
        }
      } catch (error) {
        this.isShowPopup = true;
        this.closeStatus = CloseST.ValidateBE;
        this.btnName = btnPopup.Agree;
        this.msgError = ErrorMsg.ValidateBE
      }
    },
    /**
    * @description: focus vào ô đầu tiên
    * @author: DDTung (16/07/2023)
    */
    setFocusFirstInput() {
      this.$nextTick(function () {
        this.$el.querySelector('input[tabindex = "7"]').focus();
      });
    },
    /**
     * @description: Hàm thêm tài sản
     * @author: DDTung (16/07/2023)
     */
    handleAdd() {
      this.isLoading = true;
      const productData = { ...this.product };
      productData.quantity = this.convertMoneyToNum(productData.quantity);
      productData.cost = this.convertMoneyToNum(productData.cost);
      productData.depreciation_value = this.convertMoneyToNum(productData.depreciation_value);
      axios.post(`${productURL}`, productData)
        .then(() => {
          this.isLoading = false;
          this.handleCloseForm();
          this.showToast(this.toastMsg.AddSuccess);
          this.getPagging()
        })
        .catch(res => {
          console.log(res);
          if (res.response.data.ErrorCode == ErrorCode.DuplicateCode) {
            this.isShowPopup = true;
            this.closeStatus = CloseST.DuplicateCode;
            this.msgError =
              ErrorMsg.MsgDuplicateLeft +
              this.product.fixed_asset_code +
              ErrorMsg.MsgDuplicateRight;
            this.btnName = btnPopup.Agree;
          } else {
            this.isShowPopup = true;
            this.closeStatus = CloseST.ValidateBE;
            this.btnName = btnPopup.Agree;
            this.msgError = res.response.data.UserMessage
          }
        })

    },
    /**
    * @description: Hàm sửa tài sản
    * @author: DDTung (16/07/2023)
    */
    handleEdit() {
      this.isLoading = true;
      const productData = { ...this.product };
      productData.quantity = this.convertMoneyToNum(productData.quantity);
      productData.cost = this.convertMoneyToNum(productData.cost);
      productData.depreciation_value = this.convertMoneyToNum(productData.depreciation_value);
      axios.put(`${productURL}/${this.product.fixed_asset_id}`, productData)
        .then(() => {
          this.isLoading = false;
          this.handleCloseForm();
          this.showToast(this.toastMsg.EditSuccess);
          this.getPagging()
        }).catch(res => {
          console.log(res);
          if (res.response.data.ErrorCode == ErrorCode.DuplicateCode) {
            this.isShowPopup = true;
            this.closeStatus = CloseST.DuplicateCode;
            this.msgError =
              ErrorMsg.MsgDuplicateLeft +
              this.product.fixed_asset_code +
              ErrorMsg.MsgDuplicateRight;
            this.btnName = btnPopup.Agree;
          }
        })
    },

    /**
     * @description: Lấy tên phòng ban dựa vào mã bộ phận
     * @param {*} id 
     * @author: DDTung(20/07/2023)
     */
    async getProductDepartmentName(id) {
      try {
        const response = await axios.get(`${departmentURL}/${id}`);
        return response.data.department_name;
      } catch (error) {
        console.error(error);
      }
    },

    /**
     * @description: Lấy tên loại tài sản dựa vào mã loại tài sản
     * @param {*} id 
     * @author: DDTung(20/07/2023)
     */
    async getProductCategory(id) {
      try {
        const response = await axios.get(`${categoryURL}/${id}`);
        return response.data;
      } catch (error) {
        console.error(error);
      }
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
      this.isLoading = false;
      if (!this.isValidAll) {
        this.focusToInputError();
        console.log("Focus này");
      } else {
        this.setFocusFirstInput()
      }
    },
    /**
    * @description: đóng popup và focus vào input đầu tiên
    * @author: DDTung (22/07/2023)
    */
    hidePopupFocus(value) {
      this.isShowPopup = value;
      this.isLoading = false;
      this.setFocusFirstInput()
    },

    /**
     * @description: đóng popup
     * @author: DDTung (22/07/2023)
     */
    hidePopupForm(value) {
      this.showPopup(value);
      this.handleCloseForm();
    },

    /**
     * @description: đóng form và popup
     * @author: DDTung (22/07/2023)
     */
    saveAndHide(value) {
      this.showPopup(value);
      this.handleCloseForm();
      this.btnSaveOnClick();
    },

    /**
    * @description: Hiện toast message thành công
    * @author: DDTung (22/07/2023)
    */
    showToast(msg) {
      toast.success(`${msg}`, {
        position: toast.POSITION.BOTTOM_RIGHT,
      });
    },

    /**
    * @description: Lấy mã mới
    * @author: DDTung (22/07/2023)
    */
    async getNewCode() {
      const response = await axios.get(`${productURL}/code`)
      return response.data
    },


    /**
     * @description: Hàm chạy khi ấn esc
     * @param {*} event 
     * @author: DDTung (22/07/2023)
     */
    onDocumentKeydown(event) {
      if (event.key === "Escape") {
        this.btnCloseOnClick();
      }
    },


  }


}
</script>

<style lang="scss" >
.form {
  background-color: #fff;
  width: fit-content;
  height: fit-content;
  margin: 25px auto;
  border-radius: 5px;
  position: relative;

  .cancel {
    background-color: #fff;
    margin-right: 10px;
  }

  input {
    padding: 0 36px;
    margin-top: 8px;
    border-radius: 4px;
    border: 1px solid #afafaf;
    height: 36px;
    box-sizing: border-box;

    &::placeholder {
      font-style: italic;
    }
  }

  .icon-close {
    text-align: center;

    &:hover {
      opacity: .5;
    }
  }

  .v-combobox {
    margin-right: 0 !important;
  }

  .v-combobox__body {
    width: 260px !important;
    border-radius: 4px !important;
  }

  .v-combobox__selected {
    border-radius: 4px !important;
  }

  .v-combobox__focus {
    border: 1px solid #1aa4c8 !important;
  }

  .v-combobox__icon[data-v-fdaf08aa] {
    background: var(--icon-url) no-repeat -64px -320px;
  }

  .v-select__list {
    top: 100%;
    min-height: 200px;
  }

  .icon-down-bold {
    top: 55%;
    right: 2px !important;
  }

  .icon-updown-bold {
    background: var(--icon-url) no-repeat -196px -325px;
    top: 55%;
    right: 2px !important;
  }

  .btn--primary {
    display: flex;
    align-items: center;
    justify-content: center;
    padding: 0;
    width: 100px;
  }

  .lds-dual-ring[data-v-5f38b7f1] {
    width: 100%;
    height: 100%;
    background-color: #ffffff79 !important;
    top: 50%;
    left: 50%;
    transform: translate(-50%, -50%);
    display: flex;
    justify-content: center;
    align-items: center;

    &:after {
      border-color: #1aa4c8 transparent #1aa4c8 transparent;
    }
  }
}

#form {
  background-color: rgba(0, 0, 0, .2);
  z-index: 2000;
  width: 100%;
  height: 100%;
  margin: auto;
  position: fixed;
  display: flex;
  top: 0;
  left: 0;
}

.form-footer {
  display: flex;
  height: 52px;
  box-sizing: border-box;
  flex-direction: row-reverse;
  align-items: end;
  background-color: #edeaff;
  border-radius: 3px;
  padding: 16px 16px;
  align-items: center;

  button {
    height: 36px;
    min-width: 100px;
    border: none;
    border-radius: 3px;
    outline: 0;
  }
}

.form-header {
  display: flex;
  height: 52px;
  box-sizing: border-box;
  width: 100%;
  justify-content: space-between;
  align-items: center;
  padding: 16px 16px 0;
}

.form-body {
  padding: 0 16px 20px;
}

.top-80 {
  top: 80%;
}

label {
  span {
    color: red;
  }
}

.row-input {
  display: flex;
  justify-content: space-between;
  margin: 20px 0;
}

.item-input-cl3 {
  position: relative;
}

.row-input-left {
  position: relative;

  input {
    width: 260px;
    margin-right: 0 !important;
    padding: 0 14px !important;
  }

  .icon-down-bold {
    position: absolute;
    bottom: 14px;
    right: 16px;
  }
}

.row-input-end {
  .item-input-cl3 {
    margin-right: 16px;
  }

  input {
    padding: 0 14px !important;
    margin-right: 0 !important;
    width: 260px;
  }

  display: flex;

  label {
    margin-bottom: 8px;
  }
}

.row-input-right {
  input {
    width: 536px;
    padding: 0 14px !important;
    margin-right: 0 !important;
  }

  margin-left: 16px;
}

.row-input-cl3 {
  input {
    padding: 0 14px !important;
    margin-right: 0 !important;
    width: 260px;
  }

  display: flex;
  justify-content: space-between;
  margin: 20px 0;
}

#product-quantity {
  padding: 0 36px !important;
  margin-right: 0 !important;
  width: 260px;
}

#product-depreciation-rate {
  padding: 0 36px !important;
  margin-right: 0 !important;
  width: 260px;
}

input {
  outline-color: #1aa4c8;
  outline-offset: 1px;

  &:read-only {
    background-color: #edeaff;
    outline: 0;
  }

  &:hover {
    border-color: #1aa4c8;
    outline-color: #1aa4c8;
  }

  &:focus {
    outline-color: #1aa4c8;
    outline-style: hidden;
  }
}

.v-combobox__body {
  input {
    margin-top: 0px;
    padding: 0 !important;
  }
}

.icon-close {
  background: var(--icon-url) no-repeat -375px -287px;
}

.el-date-editor.el-input {
  width: 100% !important;
  height: 36px !important;
  font-size: 13px;

  .el-input__wrapper {
    &:hover {
      .el-input__suffix {
        display: none;
      }
    }
  }
}

.el-input__wrapper {
  border-radius: 3px !important;
  border: 1px solid #afafaf;
  height: 36px;
  width: 260px;
  overflow: hidden;
  box-shadow: none !important;
  flex-direction: row-reverse;
  justify-content: space-between;
  padding: 1px 0;
  box-sizing: border-box;

  &:hover {
    border-color: #1aa4c8;
    outline-color: #1aa4c8;
  }

  &:focus {
    outline: 1px solid #1aa4c8 !important;
    border-color: #1aa4c8;
  }

  svg {
    color: #646060;
    display: none;
  }
}

.el-input__inner {
  border: none !important;
  margin-bottom: 8px;
  color: #001031 !important;

  &:focus {
    outline: 1px !important;
  }
}

.item-input {
  &:hover {
    border-color: #1aa4c8;
    outline-color: #1aa4c8;
  }

  &:focus {
    outline-color: #1aa4c8;
    outline-style: hidden;
  }
}

.el-input__wrapper.is-focus {
  outline: 1px solid #1aa4c8 !important;
  border-color: #1aa4c8;
}

.border-red {
  .el-input__wrapper {
    border-color: #e03232 !important;
  }
}

input#el-id-7454-3 {
  &::placeholder {
    color: #646060;
    font-size: 13px;
    font-family: "Roboto", sans-serif;
  }
}

.el-input__icon {
  background: url("../../../assets/img/qlts-icon.svg") no-repeat -287px -67px !important;
  width: 18px !important;
  height: 18px !important;
}

.form-input {
  .el-input__inner {
    &::placeholder {
      font-family: "Roboto", sans-serif;
      color: #646060;
    }
  }

  .el-input.is-disabled {
    .el-input__wrapper {
      background-color: #f5f5f5;
    }
  }

  margin-top: 8px;
}

.el-icon {
  font-size: 15px !important;
}

.el-picker-panel__content {
  table {
    tr {
      border: none;
    }

    tbody {
      tr {
        &:hover {
          background-color: #fff;
        }
      }
    }

    th {
      text-align: center;

      &:first-child {
        padding: 5px;
      }

      &:last-child {
        padding: 5px;
      }
    }

    td {
      &:first-child {
        padding: 5px;
      }

      &:last-child {
        padding: 5px;
      }
    }
  }
}

.el-input {
  .el-input--prefix {
    .el-input--suffix {
      .el-date-editor {
        .el-date-editor--date {
          .el-tooltip__trigger {
            .el-tooltip__trigger {
              border: 0px solid transparent !important;
            }
          }
        }
      }
    }
  }
}

.btn-cancel {
  background-color: #fff !important;
  color: black;
  margin-right: 10px;

  &:hover {
    background-color: #dddddd !important;
  }
}
</style>