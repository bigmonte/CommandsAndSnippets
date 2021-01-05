<script>
import CommandForm from '@/components/CommandForm'
import { createCommandRequest } from '@/actions'
import alertMixin from '@/mixins/alert'

export default {
  components: { CommandForm },
  data () {
    return {
      commandForm : {howTo: '', commandLine: '', platform: ''}
    }
  },
  mixins: [alertMixin],
  beforeUnmount () {
    this.clearAlertTimeout()
  },
  methods: {
    async submitCreateForm (commandToCreate) {
      try{
          await createCommandRequest(commandToCreate)
          this.setAlert('success', 'Command created!')
          this.$router.push({name: 'commandHomePage'})
      } catch(error) {
          this.setAlert('error', error?.message)
      }
    }
  }
}
</script>

<template>
  <command-form
    :alert="alert"
    @on-form-submit="submitCreateForm" />
</template>