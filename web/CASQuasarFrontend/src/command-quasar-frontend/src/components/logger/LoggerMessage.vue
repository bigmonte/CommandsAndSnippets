<script>
export default {
  props: {
    isError: {
      type: Boolean,
      default: true
    },
    message: {
      type: String
    }
  }
}
</script>

<template>
  <q-item clickable v-ripple>
        <q-item-section avatar>
          <q-icon color="teal"
          :name="isError? 'mood_bad' : 'mood'"></q-icon>
        </q-item-section>
        <q-item-section>{{message}}</q-item-section>
  </q-item>
</template>
