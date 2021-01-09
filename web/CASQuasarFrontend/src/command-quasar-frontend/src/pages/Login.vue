<template>
  <div class="q-pa-md" style="max-width: 400px">

    <q-form
      @submit="onSubmit"
      @reset="onReset"
      class="q-gutter-md"
    >
      <q-input
        filled
        v-model="email"
        label="Your Email *"
        hint="Email address"
        lazy-rules
        :rules="[ val => val && val.length > 0 || 'Please type your email']"
      />

      <q-input
        filled
        type="password"
        v-model="password"
        label="Your Password *"
        lazy-rules
        :rules="[
        val => val !== null && val !== '' || 'Please type your password'
      ]"
      />
      <div>
        <q-btn label="Submit" type="submit" color="primary"/>
        <q-btn label="Reset" type="reset" color="primary" flat class="q-ml-sm" />
      </div>
    </q-form>

  </div>
</template>

<script>
import { apiPostCall } from 'src/store/shared'

export default {
  name: 'Login',
  data () {
    return {
      email: null,
      password: null
    }
  },
  methods: {
    doLogin () {
      const ob = {
        email: this.email,
        password: this.password
      }
      apiPostCall('auth/login2', ob).then(d => {
        this.loggedIn = d
        if (this.loggedIn) {
          this.$router.push('/index')
          console.log(d)
        }
      })
    },
    onSubmit () {
      this.doLogin()
    },
    onReset () {
      console.log('submit')
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

<style scoped>

</style>
