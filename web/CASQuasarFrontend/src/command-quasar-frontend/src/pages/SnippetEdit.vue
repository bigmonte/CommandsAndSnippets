<script>
import SnippetForm from '../components/snippets/SnippetForm'
import notify from '../components/notify'
export default {
  components: { SnippetForm },
  data () {
    return {
      formTitle: 'Edit Snippet'
    }
  },
  mixins: [notify],
  beforeMount () { // Todo use beforeEnterRoute?
    if (this.snippet === null) {
      this.$router.push('/404')
    }
  },
  methods: {
    async onFormSubmit (snippet) {
      await this.$store.dispatch('snippets/updateSnippet', snippet)
        .then(() => this.showNotif('Updated snippet!'))
        .catch(error => this.showNotif(error))
    }
  },
  computed: {
    snippet: {
      get () { return this.$store.getters['snippets/getSnippetWithId'](this.$route.params.id) }
    }
  }
}

</script>

<template>
<snippet-form
    :snippet="snippet"
    :formTitle="formTitle"
    @on-form-submit="onFormSubmit($event)"/>
</template>
