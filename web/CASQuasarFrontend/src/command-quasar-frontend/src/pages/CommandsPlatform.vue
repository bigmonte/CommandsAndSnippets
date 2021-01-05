<script>
import CommandList from '../components/commands/CommandList'

export default {
  components: { CommandList },
  data () {
    return {
      isDetailView: true
    }
  },
  watch: {
    searchText: function (text) {
      this.handleSearch(text)
    }
  },
  methods: {
    async handleSearch (text) {
      if (this.canSearch) {
        this.$store.dispatch('commands/fetchSearchData')
      }
    }
  },
  computed: {
    canSearch: {
      get () {
        return this.$store.getters['commands/canSearch']
      }
    },
    commands: {
      get () {
        return this.$store.getters['commands/commandsWithPlatform'](this.$route.params.platform)
      }
    },
    searchText: {
      get () {
        return this.$store.state.commands.searchText
      }
    }
  }
}
</script>

<template>
  <command-list
    v-if="this.commands.length > 0"
    :commands="commands">
  </command-list>
  <div
        class="text-h6 text-white text-center q-pa-xl"
        v-else>No commands to show
  </div>
</template>
