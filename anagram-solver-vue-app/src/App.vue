<template>
  <Header title="Anagram Solver"/>
  <div>
    <SearchAnagram />
  </div>
</template>

<script>
import Header from './components/Header.vue'
import SearchAnagram from './components/SearchAnagram.vue';

export default {
  name: 'App',
  components: {
    Header,
    SearchAnagram
  }
}

</script>

<style>
#app {
  font-family: Avenir, Helvetica, Arial, sans-serif;
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
  text-align: center;
  color: #2c3e50;
  
}

button{
    background-color: #a7a9be;
    margin: 5px;
    border-radius: 10%;
    min-width: 100px;
    font-size: 18px;
    font-weight: bold;
    color: #271c19;
}

button:hover{
    background-color: #c2c4db;
}
</style>
