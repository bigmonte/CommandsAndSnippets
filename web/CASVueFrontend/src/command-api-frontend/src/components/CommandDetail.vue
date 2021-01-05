<script>
export default {
    props: {
        command: {
            validator: prop => typeof prop === 'object' || prop === null,
            required: true
        }
    }
}
</script>

<template>
    <div v-if="!command?.id" class ="card">
        No Command is selected :(
    </div>
    <div v-else class="card">
        <div class="card-header">
            {{command.howTo}}
        </div>
        <div class="card-body">
            <blockquote class="blockquote mb-0">
            <p>{{command.platform}}</p>
            <footer class="text-muted mb-2">{{command.commandLine}}</footer>
            </blockquote>
            <a 
              :href="command.link"
              target="_blank"
              class="btn btn-outline-secondary mr-2">Command Link</a>
            <slot name ="detailsOrGoBack"></slot>
        </div>
    </div>
</template>
