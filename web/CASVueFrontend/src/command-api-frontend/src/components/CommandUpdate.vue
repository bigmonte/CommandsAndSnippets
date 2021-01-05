<script>
import CommandForm from '@/components/CommandForm'
import { updateCommandRequest } from '@/actions'
import alertMixin from '@/mixins/alert'

export default {
  components: { CommandForm },
  props: {
    command: {
      type: Object
    }
  },
  mixins: [alertMixin],
  emits: ['on-command-update'],
  beforeUnmount () {
    this.clearAlertTimeout()
  },

  methods: {
    async submitUpdateForm (commandUpdated) {
      try{
          const updatedCommand = await updateCommandRequest(commandUpdated.id, commandUpdated)
          this.$emit('on-command-update', updatedCommand)
          this.setAlert('success', 'Command updated')
      } catch(error) {
          this.setAlert('error', error?.message)
          console.log(error?.message)
      }
    }
  }
}
</script>

<template>
  <command-form
    :alert="alert"
    :command="command"
    @on-form-submit="submitUpdateForm" />
</template>