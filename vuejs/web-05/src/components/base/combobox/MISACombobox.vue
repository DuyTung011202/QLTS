<template>
    <div class="v-combobox">
        <div class="v-combobox__label position-relative" v-if="label">
            <label @click="showListData(); $refs.input.focus()">{{ label }} <span v-if="required">*</span>
            </label>
        </div>
        <div class="v-combobox__body">
            <div class="v-combobox__selected"
                :class="[{ error: error }, { 'v-combobox__focus': isShowListData }, customClass]" :style="style"
                :data-error="`${!isShowListData && error ? errorMess : ''}`"
                :tooltip="`${!isShowListData && error ? errorMess : ''}`">
                <span :class="iconClass"></span>
                <input ref="input" type="text" v-model="textInput" @click="inputClick" :placeholder="placeholder"
                    :disabled="selectBox" @focus="inputClick" @blur="hideListData" @keydown="selecItemUpDown"
                    :tabindex="tab" />

                <div class="v-combobox__icon" tabindex="-1" @click="showListData" @blur="hideListData"
                    @keydown="selecItemUpDown">
                    <div
                        :class="['ms-16 ms-icon', { 'ms-icon-arrow-down-black': !isShowListData, 'ms-icon-arrow-up-black': isShowListData }]">
                    </div>
                </div>
            </div>
            <transition name="slide-fade">
                <div class="v-select__list my-1" v-if="isShowListData" tabindex="-1" @keydown="selecItemUpDown"
                    :style="[position === 'top' ? { bottom: '100%' } : {}]" ref="combobox__data">
                    <div v-if="filterData.length === 0" class="v-select__items">
                        Không tìm thấy kết quả
                    </div>
                    <div v-else class="v-select__items text-start" v-for="( option, index ) of  filterData " :key="index"
                        @click="onHandleSelected(option, index)"
                        :class="[{ 'v-select__hover': index == indexItemFocus }, { 'v-select__focus': option[propKey] === keyItemSelected }]">
                        <slot name="item" :option="option" :index="index">
                            {{ option[propValue] }}
                        </slot>
                        <div v-if="!selectBox" :class="{ 'v-select__items--checked': option[propKey] === keyItemSelected }">
                        </div>
                    </div>
                </div>
            </transition>
        </div>
    </div>
</template>
<script>
import { removeVietnameseTones } from '../../../js/common/function.js'
import Validate from '../../../js/common/validate.js';
import axios from 'axios';
import Enum from '../../../js/common/enum.js';
const keycode = Enum.KEY_CODE;
export default {
    name: "MISACombobox",
    props: {
        data: { // Danh sách dữ liệu
            type: Array,
            required: false
        },
        placeholder: { // Placeholder của input
            type: String,
            required: false,
            default: "",
        },
        position: { // Vị trí hiển thị danh sách dữ liệu
            type: String,
            required: false,
            default: "top",
        },
        label: { // Label của combobox
            type: String,
            required: false,
            default: null,
        },
        required: { // Bắt buộc nhập
            type: Boolean,
            required: false,
            default: false,
        },
        selectBox: { // Nếu là select box thì không cho gõ text
            type: Boolean,
            required: false,
            default: false,
        },
        modelValue: { // Giá trị của combobox
            required: false,
            default: null,
        },
        propKey: { // Key của dữ liệu
            required: false,
            default: "key",
        },
        propValue: { // Value của dữ liệu
            required: false,
            default: "value",
        },
        styleProps: { // Style của combobox
            type: Array,
            required: false,
            default: () => [],
        },
        propApi: { // Nếu có api thì lấy dữ liệu từ api
            type: String,
            required: false,
            default: null,
        },
        errorLabel: { // Hiển thị lỗi
            required: false,
        },
        isSubmit: { // Kiểm tra khi submit form
            required: false,
            default: false,
        },
        iconClass: {
            type: String // Class icon nếu có
        },
        tab: {  // tabindex
            type: String
        },
        getPagging: {
            type: Function,
        },
        filterCombobox: {
            type: Boolean,
        },
        totalRecord: {
            type: Number
        },
        customClass: { // custom class
            type: String
        },
        hasValidate: {
            type: Boolean
        },
        onBlur: {
            type: Function,
        },
        onFocus: {
            type: Function,
        }
    },
    computed: {
        /**
         * @description: Tạo style cho combobox
         * Author: DDTung 11/07/2023
         */
        style() {
            let styleProps = {};
            if (this.styleProps.length > 0) {
                this.styleProps.forEach((item) => {
                    const key = item.split(":")[0];
                    const value = item.split(":")[1];
                    styleProps[key] = value;
                });
                return styleProps;
            } else {
                return {};
            }
        },
    },
    data() {
        return {
            isShowListData: false, // Hiển thị danh sách dữ liệu
            indexItemFocus: 0, // index của item được focus
            indexItemSelected: -1, // index của item được chọn
            keyItemSelected: this.modelValue,
            textInput: this.textInput,
            dataApi: [], // dữ liệu lấy từ api
            filterData: [], // dữ liệu lọc
            error: false, // Hiển thị lỗi
            errorMess: "", // Nội dung lỗi
        };
    },
    watch: {
        /**
         * @description: Khi thay đổi text thì sẽ tìm kiếm dữ liệu theo text
         * Author: DDTung 11/07/2023
         */
        textInput: {
            handler: function (value) {
                this.filterData = this.dataApi.filter((option) =>
                    removeVietnameseTones(option[this.propValue])
                        .toLowerCase()
                        .includes(removeVietnameseTones(value).toLowerCase())
                );
                this.indexItemFocus = -1;
            },
            deep: true,
        },
        /**
         * @description: Khi bên component cha gọi tới submit thì validate lại dữ liệu trước khi submit
         * Author: DDTung 11/07/2023
         */
        isSubmit: { // nếu isSubmit là true thì validate
            handler(isSubmitted) {
                if (isSubmitted) {
                    if (this.validate() && this.error) {
                        this.$emit("update:isSubmit", false);
                    }
                }
            },
            deep: true,
        },
        /**
         * @description: Nếu data bên component cha thay đổi thì sẽ lấy dữ liệu mới
         * Author: DDTung 11/07/2023
         */
        data: {
            handler(newData) {
                this.dataApi = newData;
                this.filterData = newData;
                if (this.indexItemSelected !== -1) {
                    this.textInput = newData[this.indexItemSelected][this.propValue];
                }
            },
            deep: true,
        },
        /**
         * @description: Nếu modelValue bên component cha thay đổi thì sẽ reset lại combobox
         * Author: DDTung 11/07/2023
         */
        modelValue(newVal) {
            if (newVal === null) { // Nếu modelValue bằng null thì set textInput = "" và keyItemSelected = ""
                this.$refs.input.blur();
                this.resetCombobox();
            }
        },
    },
    methods: {
        /**
        * @description: Hàm này dùng để hiển thị danh sách dữ liệu khi ấn vào input
        * Author: DDTung 11/07/2023
        */
        inputClick() {
            this.isShowListData = true
            this.filterData = this.dataApi; // Gán dữ liệu đã lọc bằng dữ liệu từ api
            if (this.indexItemSelected > -1) {
                this.$nextTick(() => { // Đợi DOM render xong thì mới scroll đến vị trí đã chọn
                    this.indexItemFocus = this.indexItemSelected;
                    const position = this.$refs['combobox__data'].children[this.indexItemSelected].offsetTop - this.$refs['combobox__data'].offsetTop;
                    this.$refs['combobox__data'].scrollTop = position;
                });
            }
        },
        /**
         * @description: Hàm này dùng để hiển thị danh sách dữ liệu
         * Author: DDTung 11/07/2023
         */
        showListData() {
            try {
                this.isShowListData = !this.isShowListData; // Chuyển trạng thái hiển thị danh sách dữ liệu
                if (this.isShowListData) {
                    this.filterData = this.dataApi; // Gán dữ liệu đã lọc bằng dữ liệu từ api
                    if (this.indexItemSelected > -1) {
                        this.$nextTick(() => { // Đợi DOM render xong thì mới scroll đến vị trí đã chọn
                            this.indexItemFocus = this.indexItemSelected;
                            const position = this.$refs['combobox__data'].children[this.indexItemSelected].offsetTop - this.$refs['combobox__data'].offsetTop;
                            this.$refs['combobox__data'].scrollTop = position;
                        });
                    }
                }
            } catch (error) {
                console.log(error);
            }
        },
        /**
         * @description: Hàm này dùng để lấy dữ liệu khi click vào item hoặc nhấn enter
         * @param {Object} item - Dữ liệu của item được chọn
         * @param {Number} index - Index của item được chọn
         * Author: DDTung 11/07/2023
         */
        onHandleSelected(item, index) {

            try {
                const value = item[this.propValue]; // Lấy giá trị của item được chọn
                this.textInput = value; // Gán giá trị cho textInput
                this.indexItemSelected = index; // Gán index của item được chọn
                this.indexItemFocus = index; // Gán index của item được focus
                this.keyItemSelected = item[this.propKey]; // Gán key của item được chọn
                this.$emit("update:modelValue", this.keyItemSelected); // Emit giá trị của item được chọn
                this.$emit("update:textInput", value); // Emit giá trị của text input
                this.isShowListData = false; // Hide list data
                // this.validate();
            } catch (error) {
                console.log(error);
            }
        },
        /**
         * @description: Hàm này dùng để ẩn đi danh sách dữ liệu
         * Author: DDTung 11/07/2023
         */
        hideListData() {
            if (!Validate.isNullOrEmpty(this.textInput)) { // Nếu textInput không rỗng thì emit giá trị của textInput là không có trong data
                this.$emit("update:modelValue", null);
                this.resetCombobox();
            }
            if (Validate.isNullOrEmpty(this.keyItemSelected)) { // Nếu keyItemSelected không rỗng thì emit giá trị của keyItemSelected là không có trong data
                this.onHandleSelected(this.dataApi[this.indexItemSelected], this.indexItemSelected);
            }
            this.isShowListData = false; // Hide list data
        },
        /**
         * @description: Hàm này dùng để chọn item khi nhấn phím mũi tên lên, xuống, enter, esc
         * Author: DDTung 11/07/2023
         */
        selecItemUpDown(event) {
            const keyCodePress = event.keyCode; // Lấy mã phím được nhấn
            if (!this.isShowListData && (keyCodePress == keycode.ARROW_DOWN || keyCodePress == keycode.ARROW_UP)) {
                this.showListData();
                return;
            }
            if (this.isShowListData) {
                try {
                    switch (keyCodePress) {
                        case keycode.ENTER:
                            if (this.filterData.length > 0 && this.indexItemFocus !== -1) {
                                // lấy ra vị trí index của item được chọn trong danh sách dữ liệu
                                const index = this.dataApi.findIndex((item) => item[this.propValue] == this.filterData[this.indexItemFocus][this.propValue]);
                                this.onHandleSelected(this.filterData[this.indexItemFocus], index);
                            } else {
                                this.hideListData();
                            }
                            break;
                        case keycode.ARROW_DOWN:
                            this.indexItemFocus = this.indexItemFocus === this.filterData.length - 1 ? 0 : this.indexItemFocus + 1; // Focus vào item tiếp theo
                            this.handleScroll(this.indexItemFocus);
                            break;
                        case keycode.ARROW_UP:
                            this.indexItemFocus = this.indexItemFocus === 0 ? this.filterData.length - 1 : this.indexItemFocus - 1; // Focus vào item trước đó
                            this.handleScroll(this.indexItemFocus);
                            break;
                        case keycode.ESC:
                            this.hideListData(); // Ẩn danh sách dữ liệu
                            break;
                        default:
                            break;
                    }
                } catch (error) {
                    console.log(error);
                }
            }
        },
        /**
         * @description: Hàm này dùng để xử lý thanh cuộn lên xuống khi focus vào item
         * Author: DDTung 11/07/2023
         */
        handleScroll(indexItemFocus) {
            try {
                const position = this.$refs['combobox__data'].children[indexItemFocus].offsetTop - this.$refs['combobox__data'].offsetTop;
                this.$refs['combobox__data'].scrollTop = position;
            } catch (error) {
                console.log(error);
            }
        },
        /**
         * @description: Hàm này dùng để reset lại combobox
         * Author: DDTung 11/07/2023
         */
        resetCombobox() {
            this.indexItemSelected = -1; // Reset lại index item được chọn
            this.indexItemFocus = 0; // Reset lại index item được focus
            this.keyItemSelected = null; // Reset lại key item được chọn
            this.textInput = ""; // Reset lại text input
            this.filterData = []; // Reset lại dữ liệu lọc
        },



    },
    /**
     * @description: Hook khởi tạo để gán giá trị vào combobox
     * Author: DDTung 11/07/2023
     */
    async created() {
        try {
            if (this.propApi) { // Nếu có prop api thì gọi api lấy dữ liệu
                const response = await axios.get(this.propApi);
                this.dataApi = response.data;
                this.filterData = response.data;
            }
            if (this.data) { // Nếu có dữ liệu truyền từ component cha thì gán dữ liệu
                this.dataApi = this.data;
                this.filterData = this.data;
            }
            if (this.modelValue) { // Nếu có giá trị mặc định truyền từ component cha
                const index = this.dataApi.findIndex((item) => item[this.propKey] === this.modelValue); // Tìm index của item có key trùng với giá trị mặc định
                if (index !== -1) { // Nếu tồn tại item có key trùng với giá trị mặc định
                    this.onHandleSelected(this.dataApi[index], index); // Lấy dữ liệu của item được focus
                }
            }
        } catch (error) {
            console.log(error);
        }
    },


}
</script>
<style lang="scss" scoped>
.slide-fade-enter-from,
.slide-fade-leave-to {
    transform: translateY(10px);
    opacity: 0;
}

.slide-fade-enter-active,
.slide-fade-leave-active {
    transition: all 0.3s;
}

.v-combobox__icon {
    background: var(--icon-url) no-repeat -62px -328px;
}

.v-select__list::-webkit-scrollbar {
    width: 5px;
}

.v-select__list::-webkit-scrollbar-thumb {
    background-color: #888;
    border-radius: 5px;
}

.v-select__list::-webkit-scrollbar-thumb:hover {
    background-color: #555;
}

.v-select__list::-webkit-scrollbar-track {
    background-color: #f1f1f1;
}

.v-select__hover {
    background-color: #1aa5c88e;

}



.v {
    &-combobox {
        position: relative;
        margin-right: 11px;
    }

    &-combobx_show {
        border: 1px solid #1aa4c8;
    }

    &-combobox__label {
        label {
            cursor: pointer;
        }

        margin-bottom: 8px;

        span {
            color: red;
        }
    }

    &-combobox__body {
        width: 60px;
        text-align: left;
        position: relative;
        outline: none;
        box-sizing: border-box;
        border-radius: 2.5px;
        background-color: #fff;
    }

    &-combobox__selected {
        user-select: none;
        padding: 0 20px 0 10px;
        outline: none;
        min-height: 25px;
        color: #000;
        box-sizing: border-box;
        border: 1px solid #afafaf;
        border-radius: 2.5px;
        background-color: #fff;
        display: flex;
        align-items: center;

        input {
            border: none;
            outline: none;
            padding: 0;
            background-color: transparent;
            overflow: hidden;
        }
    }

    &-combobox__focus {
        border: 1px solid #1aa4c8;
    }

    &-combobox__icon {
        top: 0;
        right: 0;
        position: absolute;
        min-width: 25px;
        cursor: pointer;
        bottom: 0;
        border-radius: 0 4px 4px 0;
        display: flex;
        align-items: center;
        justify-content: center;
    }

    &-select__list {
        border-radius: 4px;
        color: #000;
        position: absolute;
        background-color: #fff;
        padding: 8px;
        bottom: 100%;
        left: 0;
        right: 0;
        z-index: 9999;
        max-height: 200px;
        overflow-y: auto;
        box-shadow: 0 0 10px rgba(23, 27, 42, 0.24);
    }

    &-select__items {
        padding: 0 4px;
        cursor: pointer;
        user-select: none;
        border-radius: 4px;
        min-height: 36px;
        line-height: 36px;
        position: relative;
        text-align: center;

        &:hover {
            background-color: #1aa5c88e;
        }

        &.itemFocus {
            background-color: grey;
        }
    }

    &-select__focus {
        background-color: #1aa4c8 !important;
        color: #fff !important;
    }
}
</style>