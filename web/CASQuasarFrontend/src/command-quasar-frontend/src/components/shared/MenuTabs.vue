<script>
import Search from './Search'
export default {
  components: { Search },
  data () {
    return {
      currentRoute: 'commands'
    }
  },
  methods: {
    getAddRoute () {
      if ('name' in this.$route) {
        if (this.onCommandsRoute) this.currentRoute = this.$route.name
        if (this.onSnippetsRoute) this.currentRoute = this.$route.name
        return `${this.currentRoute}New`
      }
    }
  },
  computed: {
    onCommandsRoute: {
      get () {
        if ('name' in this.$route) {
          return this.$route.name === 'commands'
        }
        return false
      }
    },
    onSnippetsRoute: {
      get () {
        if ('name' in this.$route) {
          return this.$route.name === 'snippets'
        }
        return false
      }
    },
    canShowSearch: {
      get () {
        if ('name' in this.$route) {
          return (this.onCommandsRoute && this.$store.state.commands.commandsData.length > 0) || (this.onSnippetsRoute && this.$store.state.snippets.snippetsData.length > 0)
        }
        return false
      }
    },
    canShowAddButton: {
      get () {
        return this.onSnippetsRoute || this.onCommandsRoute
      }
    }
  }
}
</script>

<template>
  <q-tabs
    indicator-color="green">
    <search
      v-if="canShowSearch"/>
    <router-link
      v-if="canShowAddButton"
      :to="{ name: `${getAddRoute()}` }" class="q-btn flat round dense">
      <q-icon name="add" color="white"></q-icon>
    </router-link>
    <q-tabs>
      <q-route-tab
        label="Commands"
        to="/commands"
        exact/>
      <q-route-tab
        label="Snippets"
        to="/snippets"
        exact/>
    </q-tabs>
  </q-tabs>
</template>
