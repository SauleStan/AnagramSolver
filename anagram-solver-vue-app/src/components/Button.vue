<template>
    <button @click="$emit('buttonEvent')" :style="{background:color}">{{ text }}</button>
</template>

<script>
export default {
    name: 'App-button',
    props: {
        text: String,
        color: String
    }
}
</script>