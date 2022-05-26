<template>

    <form @submit="onSubmit" class="search-form">
        <div class="form-control">
            <input v-model="inputWord" type="text" name="inputWord"
            placeholder="Search anagrams"/>
        </div>
        <Button type="submit" text="Search" class="button" />
    </form>
    <div>
        Found anagrams:
        <Words :words="anagrams" />
    </div>
</template>

<script>
import Words from './Words.vue';
import Button from './Button.vue';

export default {
    name: 'App-search-anagram',
    components: {
    Words,
    Button
},
    data() {
        return {
            inputWord: '',
            anagrams: []
        }
    },
    methods:{
        async fetchAnagrams(input){
            return await fetch(`https://localhost:7188/api/Anagram/${input}`)
                            .then(response => response.json())
                            .then(data => this.anagrams = data)
                            .catch(error => console.log(error.message));
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