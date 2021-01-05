<script>
import { deleteCommandRequest } from '@/actions'
export default {
  props: {
    activeId: Number
  },
  emits: ['on-command-delete'],
  methods: {
    async deleteCommand () {
      const deletedCommand = await deleteCommandRequest(this.activeId)
      this.$emit('on-command-delete', deletedCommand)
    }
  }
}
</script>

<template>
  <button
    @click="deleteCommand"
    class="btn btn-sm btn-danger">
    Delete
  </button>
</template>