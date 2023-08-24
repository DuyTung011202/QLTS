<template>
    <div class="default-combobox">
        <span :class="iconClass"></span>
        <input min="1" :class="customClass" :tabindex="tab" :type="customType" :id="customId" v-model="dataValue"
            :placeholder="customPlaceholder" @change="onInput" @input="onInput"
            @keypress="enableKeypress && inputNumber($event)" @keydown.up.prevent="increaseData"
            @keydown.down.prevent="decreaseData" />

        <div class="up-down">
            <div class="up">
                <div @click="increaseData" class="icon-up-bold"></div>
            </div>
            <div class="down">
                <div @click="decreaseData" class=" icon-down-bold"></div>
            </div>
        </div>


    </div>
</template>

<script>
import MISAFunction from "@/js/common/function";
export default {
    name: "MISAComboboxDropdown",
    props: {
        customType: {
            type: String
        },
        iconClass: {
            type: String
        },
        customId: {
            type: String
        },
        customClass: {
            type: String
        },
        customPlaceholder: {
            type: String
        },
        tab: {
            type: String
        },
        modelValue: {
            type: [String, Number]
        },
        depreciationValueChange: {
            type: Function
        },
        inputNumber: {
            type: Function
        },
        enableKeypress: {
            type: Boolean,
        },
        formatQuantity: {
            type: Boolean,
        },
    },
    created() {
        this.dataValue = this.modelValue
    },


    watch: {
        modelValue: function () {
            this.dataValue = this.modelValue
        },

    },

    methods: {
        /**
         * @description: Cập nhật giá trị input
         * @author: DDTung (03/07/2023)
         */
        onInput() {
            if (this.formatQuantity) {
                this.dataValue = this.formatMoney(this.dataValue)
            }
            this.$emit("update:modelValue", this.dataValue)
        },
        /**
        * @description: Tăng giá trị input
        * @author: DDTung (03/07/2023)
        */
        increaseData() {
            if (this.formatQuantity) {
                if (typeof (this.dataValue) == "number") {
                    this.dataValue = this.dataValue + 1;
                } else {
                    this.dataValue = this.convertMoneyToNum(this.dataValue)
                    this.dataValue = this.formatMoney(this.dataValue + 1);

                }
            } else {
                this.dataValue = parseFloat(this.dataValue) + 1;
            }
            this.$emit("update:modelValue", this.dataValue)

        },
        /**
        * @description: Giảm giá trị input
        * @author: DDTung (03/07/2023)
        */
        decreaseData() {
            if (this.formatQuantity) {
                if (typeof (this.dataValue) == "number") {
                    if (this.dataValue == 1) {
                        return
                    } else {
                        this.dataValue = this.dataValue - 1;
                    }
                }
                else {
                    this.dataValue = this.convertMoneyToNum(this.dataValue)
                    if (this.dataValue == 1) {
                        return
                    } else {

                        this.dataValue = this.formatMoney(this.dataValue - 1);
                    }
                }
            } else {
                if (this.dataValue < 1) {
                    return
                } else {
                    this.dataValue = parseFloat(this.dataValue) - 1;
                }
            }
            this.$emit("update:modelValue", this.dataValue)
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
        * @description: Chuyển tiền thành số
        * @param {*} data 
        * @author: DDTung (04/07/2023)
        */
        formatMoney(data) {
            return MISAFunction.formatMoney(data);
        },


    },



    data() {
        return {
            dataValue: ""
        }
    }
}
</script>

<style scoped>
.default-combobox {
    position: relative;

    input {
        margin-right: 11px;

        &::placeholder {
            font-size: 13px;
            font-weight: 400;
        }
    }
}

.combobox-input {
    background-color: #fff;
    border-radius: 2.5px;
    border: 1px solid #afafaf;
    width: 60px;
    height: 25px;
    overflow: hidden;
}

.icon-filter {
    background: var(--icon-url) no-repeat -240px -65px;
}

.up-down {
    position: absolute;
    top: 55%;
    transform: translateY(-50%);
    right: 2px;
}

.icon-up-bold {
    background: var(--icon-url) no-repeat -20px -320px;
    width: 24px;
    height: 24px;

    &:hover {
        cursor: pointer;
    }
}

.icon-down-bold {
    background: var(--icon-url) no-repeat -64px -332px;
    width: 24px;
    height: 24px;

    &:hover {
        cursor: pointer;
    }
}
</style>