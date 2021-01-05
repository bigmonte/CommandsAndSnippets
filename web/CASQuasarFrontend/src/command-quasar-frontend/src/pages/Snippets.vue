<script>
import SnippetList from '../components/snippets/SnippetList'

export default {
  components: { SnippetList },
  watch: {
    searchText: function (text) {
      this.handleSearch(text)
    }
  },
  async created () {
    this.$store.dispatch('snippets/fetchSnippets')
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
      get () {
        if (this.canSearch) return this.$store.state.snippets.searchData
        return this.$store.state.snippets.snippetsData
      }
    },
    searchText: {
      get () { return this.$store.state.snippets.searchText }
    }
  }
}
</script>

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
