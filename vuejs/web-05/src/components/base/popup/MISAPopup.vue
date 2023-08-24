<template>
  <div id="popup">
    <div class="popup" :style="closeStatus == 9 ? { width: 390 + 'px', minHeight: 170 + 'px' } : {}">
      <div class="popup-body">
        <div class="icon-popup">
          <div class="icon icon-warning"></div>
        </div>

        <div class="content-popup">
          <span>
            <strong v-if="closeStatus == 3 || closeStatus == 4">{{
              item1
            }}</strong>
          </span>
          <span v-html="msg1"></span>
        </div>

      </div>
      <div class="popup-footer">
        <MISAButton ref="nobutton" v-show="closeStatus == 1 ||
          closeStatus == 3 ||
          closeStatus == 4 ||
          closeStatus == 6
          " customClass="no btn-hover-outline" @click="onClickBtnNo" :text="btnLeft1" tabindex="18">
        </MISAButton>
        <MISAButton v-if="closeStatus == 6" customClass="no-save" @click="onclickNoSave" text="Không lưu" tabindex="19">
        </MISAButton>
        <MISAButton customClass="cancel btn-hover-blue" @click="onClickCancel" text="Đồng ý" tabindex="20"
          @keydown.tab.prevent="setFocusFirstButton()">

        </MISAButton>
      </div>
    </div>
  </div>
</template>
<script>
import { CloseST } from "../../../js/common/enum";
export default {
  data() {
    return {
      closeStatus: 0,
      msg1: '',
      btnLeft1: '',
      name1: '',
      item1: '',

    };
  },
  mounted() {
    this.setFocus();
  },
  watch: {
    close: function (value) {
      this.closeStatus = value;
    },
  },
  created() {
    this.msg1 = this.msg
    this.name1 = this.name
    this.closeStatus = this.close
    this.item1 = this.item
    this.btnLeft1 = this.btnLeft
  },
  props: ["msg", "name", "close", "item", "btnLeft"],
  methods: {
    /**
    * @description: khi hiện popup tự động focus vào nut đồng ý
    * @author: DDTung (16/07/2023)
    */
    setFocus() {
      this.$nextTick(function () {
        this.$el.querySelector('button[tabindex = "20"]').focus();
      });
    },
    /**
    * @description: focus vào nút đầu tiên
    * @author: DDTung (16/07/2023)
    */
    setFocusFirstButton() {
      this.$nextTick(function () {
        this.$el.querySelector('button[tabindex = "18"]').focus();
      });
    },

    // tắt popup
    onClickBtnNo() {
      this.$emit("hidePopup", false);
    },
    onclickNoSave() {
      this.$emit("hidePopupAndForm", false);
    },
    //tắt popup, form
    onClickCancel() {
      console.log(this.closeStatus);
      if (this.closeStatus == CloseST.DeleteCloseNotChoose || this.closeStatus == CloseST.ValiDate || this.closeStatus == 9) {
        this.$emit("hidePopup", false);
      }
      else if (this.closeStatus == CloseST.DuplicateCode) {
        this.$emit("hidePopupFocus", false);
      } else if (this.closeStatus == CloseST.DeleteMulti || this.closeStatus == CloseST.DeleteOne) {
        this.$emit("isDelete", false);
      } else if (this.closeStatus == CloseST.EditClose) {
        this.$emit("saveAndHideForm", false);
      }
      else {
        this.$emit("hidePopupAndForm", false);
      }
    },
  }
};
</script>
<style lang="scss">
.no-save {
  color: #1aa4c8;
  background-color: #fff;
  border: 1px solid #1aa4c8;
  margin-left: 12px;

  &:focus {
    background-color: #abe9f8;
  }

  &:hover {
    background-color: #abe9f8;
  }
}

#popup {
  background-color: rgba(0, 0, 0, 0.2);
  z-index: 100;
  width: 100%;
  height: 100%;
  margin: auto;
  position: fixed;
  display: flex;
  top: 0;
  left: 0;
  flex-direction: column;
}

.popup {
  font-size: 13px;
  width: 450px;
  min-height: 180px;
  height: fit-content;
  background-color: #fff;
  margin: auto;
  box-sizing: border-box;
  border-radius: 5px;
  box-shadow: 0 3px 10px rgba(0, 0, 0, 0.2);

  .no {
    background-color: #fff;
    border: 1px solid #aca8a8;
  }

  .cancel {
    background-color: #1aa4c8;
    color: #fff;
    border: none;
    margin-left: 12px;
  }

  .icon-warning {
    background-repeat: no-repeat;
    width: 50px;
  }
}

.popup-body {
  align-items: center;
  display: flex;
  margin: 24px;
  height: calc(100% - 101px);
}

#content {
  margin-left: 20px;
  margin-top: 4px;
  text-align: start;
  width: calc(100% - 80px);
}

.content-popup {
  justify-content: flex-start;
  margin-left: 15px;
}

.icon-popup {
  margin-top: -8px;
}

.popup-footer {
  display: flex;
  justify-content: flex-end;
  margin-top: 0px;
  background-color: #dddddd5e;
  padding: 8px 10px;

  button {
    min-width: 110px;
    height: 36px;
    outline: none;
    border-radius: 3px;
  }
}

.btn-hover-blue {
  &:focus {
    background-color: #0062cc;
  }

  &:hover {
    background-color: #0062cc;
  }
}

.btn-hover-outline {
  &:focus {
    background-color: #71dffa;
    color: #fff;
    border: none;
  }

  &:hover {
    background-color: #1aa4c8;
    color: #fff;
    border: none;
  }
}

.icon-warning {
  background: url("../../../assets/icon/icons8-warning-50.png");
  width: 40px;
  height: 50px;
}
</style>
