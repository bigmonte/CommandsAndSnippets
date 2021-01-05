<script>
export default {
  props: {
    command: {
      type: Object
    }
  },
  data () {
    return {
      uCommand: { ...this.command }
    }
  },
  methods: {
    onEditButtonClicked (selectedCommand) {
      this.$router.push({ name: 'commandEdit', params: { id: selectedCommand.id } })
    },
    async deleteCommand () {
      this.$store.dispatch('commands/removeCommand', this.uCommand)
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
          <div class="text-h6 text-white q-ml-sm">{{ command.howTo }}</div>
          <div class="text text-white q-ml-sm">{{ command.platform }}</div>
          <div class="text text-green q-ml-sm q-mt-md">
            <code>{{ command.commandLine }}</code>
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
                 @click="onEditButtonClicked(command)">
                  <q-item-section>Edit</q-item-section>
                </q-item>
                <q-item clickable v-close-popup
                @click="deleteCommand(command)">
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
      @click="onEditButtonClicked(command)">Edit command</q-btn>
  </q-card-actions> -->
  </q-card>
</template>
