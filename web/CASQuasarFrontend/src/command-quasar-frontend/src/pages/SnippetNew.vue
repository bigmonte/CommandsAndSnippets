<script>
import SnippetForm from '../components/snippets/SnippetForm'
import notify from '../components/notify'
export default {
  components: { SnippetForm },
  mixins: [notify],
  methods: {
    async submitCreateForm (snippetToCreate) {
      this.$store.dispatch('snippets/createSnippet', snippetToCreate)
        .then(() => this.showNotif('Snippet created!'))
    }
  }
}
</script>

<template>
  <snippet-form
    :formTitle="`${'Add snippet'}`"
    @on-form-submit="submitCreateForm($event) "/>
</template>
