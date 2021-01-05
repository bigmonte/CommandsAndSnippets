<script>
import CommandForm from '../components/commands/CommandForm'
import notify from '../components/notify'
export default {
  components: { CommandForm },
  mixins: [notify],
  methods: {
    async submitCreateForm (commandToCreate) {
      this.$store.dispatch('commands/createCommand', commandToCreate)
        .then(() => this.showNotif('Command created!'))
    }
  }
}
</script>

<template>
  <command-form
    :formTitle="`${'Add command'}`"
    @on-form-submit="submitCreateForm($event)"/>
</template>
