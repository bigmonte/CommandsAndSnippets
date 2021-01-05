<script>
import PlatformItem from '../components/shared/PlatformItem'
export default {
  components: { PlatformItem },
  async created () {
    await this.$store.dispatch('commands/fetchCommands')
    await this.$store.dispatch('snippets/fetchSnippets')
  },
  methods: {
    filterByPlatform (arr) {
      const platforms = []
      for (var i = 0; i < arr.length; i++) {
        const obj = arr[i]
        if (!platforms.includes(obj.platform)) {
          platforms.push(obj.platform)
        }
      }
      return platforms
    }
  },
  computed: {
    commands: {
      get () {
        return this.$store.state.commands.commandsData
      }
    },
    snippets: {
      get () {
        return this.$store.state.snippets.snippetsData
      }
    },
    hasCommands: {
      get () {
        return this.commands.length > 0
      }
    },
    hasSnippets: {
      get () {
        return this.snippets.length > 0
      }
    },
    getCommandsPlatform () {
      return this.filterByPlatform(this.commands)
    },
    getSnippetsPlatform () {
      return this.filterByPlatform(this.snippets)
    }
  }
}
</script>

<template>
  <div>
    <div
      class='text-h6 text-white text-center q-pa-xl'
      v-if='!hasCommands && !hasSnippets'
    >
      No content to show
    </div>
    <div class='q-pa-md'>
      <div class='row'>
        <div class='col q-pa-md' v-if='hasCommands'>
          <div class='text-h6 text-primary text-center text-primary q-pb-md'>
            CLI's
          </div>
          <q-list
            dense
            bordered
            padding
            class='rounded-borders bg-primary'
            v-for='platform in getCommandsPlatform'
            :key='platform'
          >
            <platform-item type='commands' :platform='platform' />
          </q-list>
        </div>
        <div class='col q-pa-md' v-if='hasSnippets'>
          <div class='text-h6 text-primary text-center text-primary q-pb-md'>
            Platforms / Languages
          </div>
          <q-list
            dense
            bordered
            padding
            class='rounded-borders bg-primary'
            v-for='platform in getSnippetsPlatform'
            :key='platform'
          >
            <platform-item type='snippets' :platform='platform' />
          </q-list>
        </div>
      </div>
    </div>
  </div>
</template>
