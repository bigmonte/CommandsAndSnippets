<script>
export default {
  data () {
    return {
      searchText: '',
      tab: 'commands',
      currentRoute: 'commands' // default
    }
  },
  watch: {
    searchText: function (text) {
      this.setSearchText(text)
    }
  },
  methods: {
    setSearchText (text) {
      if ('name' in this.$route) {
        if (this.$route.name === 'commands') {
          this.$store.commit('commands/updateSearchText', text)
        }
        if (this.$route.name === 'snippets') {
          this.$store.commit('snippets/updateSearchText', text)
        }
      }
    }
  }
}
</script>

<template>
  <q-input
      dark
      borderless
      v-model="searchText"
      input-class="text-right"
      class="q-ml-md"
    >
    <template v-slot:append>
      <q-icon v-if="searchText === ''" name="search"></q-icon>
      <q-icon
        v-else
        name="clear"
        class="cursor-pointer"
        @click="setSearchText('')"
      ></q-icon>
    </template>
    <q-space />
  </q-input>
</template>
