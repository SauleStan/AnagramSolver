<template>
    <header>
        <router-link to="/" class="title">{{ title }}</router-link>
        <router-link to="/words">Words</router-link>
    </header>
</template>

<script>
export default {
    name: 'App-header',
    props: {
        title: String,
    },
}
</script>

<style scoped>
    header{
        display: flex;
        justify-content: left;
        align-items: center;
        margin-bottom: 20px;
        padding-left: 20px;
        background-color: lightblue;
    }
    .title {
        display: block;
        font-size: 2em;
        margin-block-start: 0.67em;
        margin-block-end: 0.67em;
        margin-inline-start: 0px;
        margin-inline-end: 0px;
        font-weight: bold;
    }
    a, a:visited{
        font-size: 20px;
        margin-left: 20px;
        color: inherit;
        text-decoration: none;
    }
    a:hover {
        color: #58697b;
    }
</style>