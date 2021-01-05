<script>
export default {
    emits: ['on-search']
}
</script>

<template>
  <form class="card p-2">
    <div class="input-group">
      <input
        @keyup="$emit('on-search', $event.target.value)"
        type="text" 
        class="form-control" 
        placeholder="Type to search" />
    </div>
    </form>
</template>
