<script>
import LoggerMessage from './LoggerMessage'
export default {
  components: { LoggerMessage },
  computed: {
    messages: {
      get () {
        return this.$store.state.logger.messages
      }
    }
  }
}
</script>

<template>
<q-scroll-area style="height: 80px; max-width: 100%;">
  <q-list bordered v-for="message in messages" :key="message.id" >
      <logger-message
      :message="message.message"
      :isError="message.isError"/>
    </q-list>
</q-scroll-area>
</template>
