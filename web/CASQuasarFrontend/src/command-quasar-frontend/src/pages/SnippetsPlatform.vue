<template>
  <snippet-list
    v-if="this.snippets.length > 0"
    :snippets="snippets">
  </snippet-list>
  <div
        class="text-h6 text-white text-center q-pa-xl"
        v-else>No snippets to show
  </div>
</template>

<script>
import SnippetList from '../components/snippets/SnippetList'

export default {
  components: { SnippetList },
  watch: {
    searchText: function (text) {
      this.handleSearch(text)
    }
  },
  methods: {
    async handleSearch (text) {
      if (this.canSearch) {
        this.$store.dispatch('snippets/fetchSearchData')
      }
    }
  },
  computed: {
    canSearch: {
      get () { return this.$store.getters['snippets/canSearch'] }
    },
    snippets: {
      get () { return this.$store.getters['snippets/snippetsWithPlatform'](this.$route.params.platform) }
    },
    searchText: {
      get () { return this.$store.state.snippets.searchText }
    }
  }
}
</script>
