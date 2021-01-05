<script>
export default {
  props: {
    command: Object,
    alert: Object
  },
  data() {
    return {
      uCommand: { ...this.command }
    }
  },
  emits: ['on-form-submit'],
  watch: { // watch change to the prop resource
    command (newCommand) {
      this.uCommand = {...newCommand}
    }
  },
  methods: {
    submitForm () {
      this.$emit('on-form-submit',  this.uCommand )
    }
  }
}
</script>
<template>
  <form @submit.prevent="submitForm">
    <div v-if="alert?.success" class= "alert alert-success">
      {{alert.success}}
    </div>
    <div v-if="alert?.error" class= "alert alert-danger">
      {{alert.error}}
    </div>
    <div class="mb-3">
      <label htmlFor="howTo">How To</label>
      <input 
        v-model="uCommand.howTo"
        type="text" 
        class="form-control" 
        id="howTo" 
        placeholder="Description of the command" />
    </div>
    <div class="mb-3">
      <label htmlFor="platform">Platform</label>
      <input 
        v-model="uCommand.platform"
        type="text" 
        class="form-control" 
        id="p" 
        placeholder="Platform of the command" />
    </div>
    <div class="mb-3">
      <label for="commandLine">Command Line</label>
      <textarea 
        v-model="uCommand.commandLine"
        class="form-control" 
        id="description" 
        placeholder="Command"></textarea>
    </div>
    <hr class="mb-4" />
    <button 
      class="btn btn-primary btn-lg btn-block" 
      type="submit">Submit</button>
  </form>
</template>
