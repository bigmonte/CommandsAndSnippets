<script>

export default {
}

</script>

<template>
    <div class="py-5 text-center">
        <img height="200" src="../assets/logo.png" />
        <p class="lead">Helper page for your useful commands and snippets</p>
        <router-link 
          :to="{name: 'commandHomePage'}"
          class="btn btn-outline-primary mr-2">Home</router-link>
        <router-link 
          :to="{name: 'commandNewPage'}"
          class="btn btn-outline-primary"
          >New Command</router-link>
    </div>
</template>

