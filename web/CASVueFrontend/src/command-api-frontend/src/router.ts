import { 
  RouteRecordRaw, 
  createRouter, 
  createWebHistory 
} from 'vue-router'

import CommandHome from '@/pages/CommandHome.vue'
import CommandNew from '@/pages/CommandNew.vue'
import CommandsRoutes from '@/pages/CommandsRoutes.vue'
import CommandDetails from '@/pages/CommandDetails.vue'

export enum routesId {
    commandRoutes = 'commandsRoutes',
    commandHomePage = 'commandHomePage',
    commandNewPage = 'commandNewPage',
    commandDetailsPage = 'commandDetailsPage'
  }

const routes: RouteRecordRaw[]  = [
    { path: '/' , redirect: {name: 'commandHomePage'}},
    { 
        path: '/commands', 
        name: routesId.commandRoutes, 
        component: CommandsRoutes, 
        children :[
            { path: '', name: routesId.commandHomePage, component: CommandHome },
            { path: 'new', name: routesId.commandNewPage, component: CommandNew },
            { path: ':id', name: routesId.commandDetailsPage, component: CommandDetails }
        ]},
]

const router = createRouter({
    history: createWebHistory(),
    linkExactActiveClass: 'active',
    routes
})

export default router