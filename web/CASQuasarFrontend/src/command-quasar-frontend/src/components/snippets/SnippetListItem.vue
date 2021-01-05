<script>
export default {
  props: {
    snippet: {
      type: Object
    }
  },
  data () {
    return {
      uSnippet: { ...this.snippet }
    }
  },
  methods: {
    onEditButtonClicked (selectedSnippet) {
      this.$router.push({ name: 'snippetEdit', params: { id: selectedSnippet.id } })
    },
    async deleteSnippet () {
      this.$store.dispatch('snippets/removeSnippet', this.uSnippet)
    }
  }
}
</script>

<template>
  <q-card
    class="text-white"
    style="background: linear-gradient(45deg, #486678, #343434)"
  >
    <q-card-section>
      <div class="row">
        <div class="col">
          <div class="text-h6 text-white q-ml-sm">{{ snippet.howTo }}</div>
          <div class="text text-white q-ml-sm">{{ snippet.platform }}</div>
          <div class="text text-green q-ml-sm q-mt-md">
            <p>{{ snippet.codeSnippet }}</p> <!-- TODO use markdown-it or similar like Quasar docs uses -->
          </div>
        </div>
        <div class="col-xl">
          <q-btn flat round dense>
            <q-icon name="more_vert"></q-icon>
            <q-menu
            content-class="bg-primary text-white"
          :offset="[-39, -33]"
        >
              <q-list style="min-width: 100px">
                <q-item clickable v-close-popup
                 @click="onEditButtonClicked(snippet)">
                  <q-item-section>Edit</q-item-section>
                </q-item>
                <q-item clickable v-close-popup
                @click="deleteSnippet(snippet)">
                  <q-item-section>Remove</q-item-section>
                </q-item>
              </q-list>
            </q-menu>
          </q-btn>
        </div>
      </div>
    </q-card-section>
    <!-- <q-card-actions>
    <q-btn type="update" color="primary"
      @click="onEditButtonClicked(snippet)">Edit snippet</q-btn>
  </q-card-actions> -->
  </q-card>
</template>
