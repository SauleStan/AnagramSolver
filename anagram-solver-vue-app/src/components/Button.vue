<template>
    <button @click="onClick()" :style="{background:color}">{{ text }}</button>
</template>

<script>
export default {
    name: 'App-button',
    props: {
        text: String,
        color: String
    },
    methods:{
        onClick(){
            console.log(`${this.text} button clicked`);
        }
    }
}
</script>