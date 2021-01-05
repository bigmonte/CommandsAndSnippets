<script>
import CommandForm from '../components/commands/CommandForm'
import notify from '../components/notify'

export default {
  components: { CommandForm },
  computed: {
    command: {
      get () { return this.$store.getters['commands/getCommandWithId'](this.$route.params.id) }
    }
  },
  mixins: [notify],
  data () {
    return {
      formTitle: 'Edit Command'
    }
  },
  beforeMount () { // Todo use beforeEnterRoute?
    if (this.command === null) {
      this.$router.push('/404')
    }
  },
  methods: {
    async onFormSubmit (command) {
      await this.$store.dispatch('commands/updateCommand', command)
        .then(() => this.showNotif('Command Updated!'))
        .catch((error) => this.showNotif(error))
    }
  }
}
</script>

<template>
<command-form
    :command="command"
    :formTitle="formTitle"
    @on-form-submit="onFormSubmit($event)"/>
</template>
