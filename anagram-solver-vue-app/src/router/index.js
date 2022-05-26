import { createRouter, createWebHistory } from 'vue-router'
import WordsView from '../views/WordsView.vue'
import HomeView from '../views/HomeView.vue'

const routes = [
    {
        path: '/',
        name: 'Home-view',
        component: HomeView
    },
    {
        path: '/words',
        name: 'Words-view',
        component: WordsView
    }
]

const router = createRouter({
    history: createWebHistory(process.env.BASE_URL), routes
})

export default router