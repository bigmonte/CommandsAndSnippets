<template>
    <div class="container">
      <Header/>
      <router-view></router-view>
    </div>
</template>

<script>
import Header from '@/components/Header'

export default { 
    components: { Header}
}
</script>

<style scoped>
@import '../node_modules/bootstrap/dist/css/bootstrap.min.css'
</style>
