<template>
    <div :key="word" v-for="word in words">
        <Word :word="word"/>
    </div>
</template>

<script>
import Word from '../Word.vue'
export default {
    name: "App-words",
    props: {
        words: Array,
    },
    components: { Word }
}
</script>