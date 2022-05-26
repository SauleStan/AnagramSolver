<template>

    <form @submit="onSubmit" class="search-form">
        <div class="form-control">
            <input v-model="inputWord" type="text" name="inputWord"
            placeholder="Search anagrams"/>
        </div>
        <input type="submit" value="Search" class="button"/>
    </form>
    <div>
        Found anagrams:
        <Words :words="anagrams" />
    </div>
</template>

<script>
import Words from './Words.vue';

export default {
    name: 'App-search-anagram',
    components: {
        Words
    },
    data() {
        return {
            inputWord: '',
            anagrams: []
        }
    },
    methods:{
        async fetchAnagrams(input){
            const response = await fetch(`https://localhost:7188/api/Anagram/${input}`);
            const data = await response.json();
            return data;
        },
        async onSubmit(e){
            e.preventDefault();

            if(!this.inputWord) {
                alert('Please input a word')
                return
            }

            this.anagrams = await this.fetchAnagrams(this.inputWord);
        }
    }
}
</script>