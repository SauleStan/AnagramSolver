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
        this.fetchData().then((data)=>
        {
            this.fetchedWords = data
        });
    },
    methods: {
        async fetchData(){
            const response =  await fetch(`https://localhost:7188/api/Words`);
            const data = await response.json();
            return data
        }
    }
}
</script>