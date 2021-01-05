<script>
import { fetchCommandById } from '@/actions'
import CommandDetail from '@/components/CommandDetail'

export default {
  components: { CommandDetail },
  data () {
    return {
      command: null
    }
  },
  async created () {
    const commandId = this.$route.params.id
    this.command = await fetchCommandById(commandId)
  }
  
}
</script>

<template>
  <command-detail :command="command">
    <template #detailsOrGoBack>
      <button
        @click="$router.go(-1)"
        class="btn btn-outline-success"
        >Go Back</button>
    </template>
  </command-detail>
</template>