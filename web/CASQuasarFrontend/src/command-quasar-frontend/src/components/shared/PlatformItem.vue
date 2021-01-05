<script>
export default {
  props: {
    platform: {
      type: String,
      required: true
    },
    type: {
      type: String,
      required: true
    }
  },
  methods: {
    onItemClicked () {
      this.$router.push({ name: `${this.type}Platform`, params: { platform: this.platform } })
    }
  }
}
</script>

<template>
  <q-item clickable v-ripple class="text-secondary"
    @click="onItemClicked()">
    <q-item-section>
      {{platform}}
    </q-item-section>
  </q-item>
</template>
