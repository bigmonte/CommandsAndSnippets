export default {
  methods: {
    showNotif (text) {
      this.$q.notify({
        message: text,
        color: 'primary'
      })
    }
  }
}
