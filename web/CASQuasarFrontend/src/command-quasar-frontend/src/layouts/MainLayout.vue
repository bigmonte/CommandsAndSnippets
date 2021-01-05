<template>
  <q-layout view="lHh Lpr lFf" class="bg-secondary">
    <q-header elevated>
      <q-toolbar class="bg-primary text-white">
        <q-toolbar-title>
          <router-link class="q-btn flat" :to="{ name: 'root' }">
            Commands and Snippets
          </router-link></q-toolbar-title >
        <menu-tabs/>
      </q-toolbar>
    </q-header>
    <q-page-container>
      <router-view/>
    </q-page-container>
    <c-footer/>
  </q-layout>
</template>

<script>
import MenuTabs from '../components/shared/MenuTabs'
import CFooter from '../components/shared/CFooter'

export default {
  name: 'MainLayout',
  components: { MenuTabs, CFooter }
}
</script>
