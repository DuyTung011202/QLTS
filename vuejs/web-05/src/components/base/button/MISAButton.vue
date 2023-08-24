<template>
    <button :id="customId" :class="customClass" :tabindex="tab" :disabled="disabled">
        <div :class="iconClass"></div>
        {{ text }}
    </button>
</template>

<script>
export default {
    name: "MISAButton",
    props: {
        customId: { // custom id
            type: String
        },
        customClass: {  // custom class
            type: String
        },
        tab: {  // tab index
            type: String
        },
        iconClass: { // icon class
            type: String
        },
        text: { // button text
            type: String
        },
        disabled: { // disable
            type: Boolean
        },


    }
}
</script>


<style lang="scss">
:root {
    --icon-url: url("../../../assets/img/qlts-icon.svg");
}

.btn--delete {
    &:hover {
        cursor: pointer;
        opacity: .5;
    }

    &:active {
        cursor: pointer;
        opacity: .5;
    }

    width: 36px;
    height: 36px;
    border-radius: 3px;
    box-shadow: 0 2px 2px rgba(0, 0, 0, .16);
    background: var(--icon-url) no-repeat -454px -102px;
    background-color: #fff;
}

.btn--option {
    &:hover {
        cursor: pointer;
        opacity: .5;
    }

    &:active {
        cursor: pointer;
        opacity: .5;
    }

    width: 36px;
    height: 36px;
    border-radius: 3px;
    box-shadow: 0 2px 2px rgba(0, 0, 0, .16);
    background: var(--icon-url) no-repeat -278px -102px;
    background-color: #fff;
    margin: 0 10px;
}

.btn--primary {
    background-color: #1aa4c8;
    border-radius: 3px;
    width: 120px;
    height: 36px;
    overflow: hidden;
    color: #fff;
    outline: 0;
    border: none;
    padding-right: 20px;

    &:hover {
        background-color: #218aa4;
        cursor: pointer;
        outline: 2px solid #828c8f;
    }

    &:active {
        background-color: #27a0be;
        cursor: pointer;
        outline: 2px solid #828c8f;
    }

    &:focus {
        background-color: #3ca9c5;
        cursor: pointer;
        outline: 2px solid #828c8f;
    }
}

.btn--primary-icon {
    width: 16px;
    height: 16px;
    background: var(--icon-url) no-repeat -200px -420px;
    margin-left: 12px;
}
</style>