<script>
import LoggerList from '../logger/LoggerList'
export default {
  data () {
    return {
      showLogger: false
    }
  },
  name: 'CFooter',
  components: { LoggerList },
  methods: {
    toggleLogger () {
      this.showLogger = !this.showLogger
    }
  }
}
</script>

<template>
  <q-footer>
      <q-banner inline-actions class="text-white bg-primary">
        <logger-list
        v-if="showLogger"/>
        <template v-slot:action>
          <q-btn flat color="white" label="Toggle logger"
          @click="toggleLogger()"></q-btn>
        </template>
      </q-banner>
    </q-footer>
</template>
