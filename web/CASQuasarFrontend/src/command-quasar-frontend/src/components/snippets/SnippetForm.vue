<script>
export default {
  props: {
    snippet: {
      type: Object,
      default: () => {}
    },
    formTitle: {
      type: String
    }
  },
  data () {
    return {
      uSnippet: { ...this.snippet }
    }
  },
  emit: ['on-form-submit'],
  methods: {
    onFormSubmit () {
      this.$emit('on-form-submit', this.uSnippet)
    }
  }
}
</script>
<template>
  <div>
    <div class="row q-pa-md">
      <q-btn
        type="back"
        color="primary"
        flat
        class="q-ml-sm"
        @click="$router.go(-1)"
      >
        <q-icon name="arrow_back" color="white"></q-icon>
      </q-btn>
    </div>
    <div class="row justify-center content-center items-center">
      <div class="col q-pa-sm" style="max-width: 700px">
        <q-form class="q-gutter-md" @submit.prevent="onFormSubmit">
          <q-input
            label-color="info"
            filled
            type="text"
            v-model="uSnippet.howTo"
            label="How To"
            hint="Short description of the snippet"
            :rules="[
              (val) => (val && val.length > 0) || 'Field cannot be empty',
            ]"
          ></q-input>
          <q-input
            label-color="info"
            filled
            type="text"
            v-model="uSnippet.platform"
            label="Platform"
            hint="What is the language/platform being used for this snippet?"
            :rules="[
              (val) => (val && val.length > 0) || 'Field cannot be empty',
            ]"
          >
          </q-input>
                    <q-input
            label-color="info"
            type="textarea"
            filled
            autogrow
            clearable
            v-model="uSnippet.codeSnippet"
            label="Code you want to store"
            hint="A useful snippet code"
            lazy-rules
            :rules="[
              (val) => (val && val.length > 0) || 'Field cannot be empty',
            ]"
          ></q-input>
          <q-btn type="submit" color="primary">{{ formTitle }}</q-btn>
        </q-form>
      </div>
    </div>
  </div>
</template>
