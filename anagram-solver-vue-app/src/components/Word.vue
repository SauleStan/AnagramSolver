<template>
    <h3>{{ word }}</h3>
</template>

<script>
export default {
    name: 'App-word',
    props: {
        word: String
    }
}
</script>