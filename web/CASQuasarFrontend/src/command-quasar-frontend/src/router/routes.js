
const routes = [
  {
    path: '/',
    component: () => import('layouts/MainLayout.vue'),
    children: [
      { path: '', name: 'root', component: () => import('pages/Login.vue') },
      { path: 'index', name: 'login', component: () => import('pages/Index.vue') }
    ]
  },
  {
    path: '/commands',
    component: () => import('layouts/MainLayout.vue'),
    children: [
      { path: '', name: 'commands', component: () => import('pages/Commands.vue') },
      { path: 'new', name: 'commandsNew', component: () => import('pages/CommandNew.vue') },
      { path: 'platform/:platform', name: 'commandsPlatform', component: () => import('pages/CommandsPlatform.vue') },
      { path: 'edit/:id', name: 'commandEdit', component: () => import('pages/CommandEdit.vue') }
    ]
  },
  {
    path: '/snippets',
    component: () => import('layouts/MainLayout.vue'),
    children: [
      { path: '', name: 'snippets', component: () => import('pages/Snippets.vue') },
      { path: 'new', name: 'snippetsNew', component: () => import('pages/SnippetNew.vue') },
      { path: 'platform/:platform', name: 'snippetsPlatform', component: () => import('pages/SnippetsPlatform.vue') },
      { path: 'edit/:id', name: 'snippetEdit', component: () => import('pages/SnippetEdit.vue') }
    ]
  },

  // Always leave this as last one,
  // but you can also remove it
  {
    path: '*',
    name: 'notFound',
    component: () => import('pages/Error404.vue')
  }
]

export default routes
