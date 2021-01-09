<script>
import LoggerList from '../logger/LoggerList'
import { apiPostCall } from 'src/store/shared'
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
    },
    doLogout () {
      apiPostCall('auth/logout', {}).then(d => {
        this.loggedIn = false
        if (this.loggedIn) {
          this.$router.push('/')
          console.log(d)
        }
      })
    }
  },
  computed: {
    loggedIn: {
      get () {
        return this.$store.getters['account/loggedIn']
      },
      set (val) {
        this.$store.commit('account/setLoggedIn', val)
      }
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
        <template v-slot:action v-if="loggedIn">
          <q-btn flat color="white" label="Logout"
          @click="doLogout()"></q-btn>
        </template>
      </q-banner>
    </q-footer>
</template>
