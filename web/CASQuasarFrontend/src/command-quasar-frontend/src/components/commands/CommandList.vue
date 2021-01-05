<template>
    <div class="q-ma-sm">
        <div v-for="command in commands" :key="command.id" class="q-py-md">
          <command-list-item
            :command='command'/>
        </div>
  </div>
</template>

<script>
import CommandListItem from './CommandListItem'
export default {
  props: {
    commands: {
      type: Array
    }
  },
  name: 'CommandList',
  components: { CommandListItem }
}

</script>
