<template>
    <div class="q-ma-sm">
        <div v-for="snippet in snippets" :key="snippet.id" class="q-py-md">
          <snippet-list-item
            :snippet='snippet'/>
        </div>
  </div>
</template>

<script>
import SnippetListItem from './SnippetListItem'

export default {
  name: 'SnippetList',
  components: { SnippetListItem },
  props: {
    snippets: {
      type: Array
    }
  }
}
</script>
