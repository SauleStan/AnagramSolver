<template>
    <Words :words="fetchedWords" />
</template>

<script>
import Words from './Words.vue'

export default {
    name: 'App-fetch-words',
    components: {
        Words
    },
    data () {
        return {
            fetchedWords: []
        }
    },
    mounted() {
        fetch(`https://localhost:7188/api/Words`)
            .then(response => response.json())
            .then(data => this.fetchedWords = data)
            .catch(error => console.log(error.message));
    }
    
}
</script>