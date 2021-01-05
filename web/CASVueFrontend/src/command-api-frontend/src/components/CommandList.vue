<script>
export default {
    props: {
        commands: {
            type: Array,
            default: () => []
        },
        activeId: Number
    },
    emit: ['on-command-click'],
    computed: {
        activeCommandClass() {
            return command => command.id === this.activeId ? 'is-active' : ''
        }
    },
    methods: {
        onCommandClick(command){
            this.$emit('on-command-click', command)
        }
    }
    
}
</script>

<template>
    <ul class="list-group command-list mb-3">
        <li v-for="command in commands"
            :key="command.id"
            @click="onCommandClick(command)"
            :class="`${activeCommandClass(command)} list-group-item d-flex justify-content-between lh-condensed command-list-item`">
            <div>
            <h6 class="my-0">{{command.HowTo}}</h6>
            <small class="text-muted">{{command.platform}}</small>
            </div>
            <span class="text-muted">{{command.howTo}}</span>
        </li>
    </ul>
</template>

<style scoped lang="scss">
    .command-list {
        max-height: 300px;
        overflow-y: auto;
        &-item {
            &:hover {
                background-color: #f3f3f3;
            }
            cursor: pointer;
        }

    }
    .is-active {
         background-color: #f3f3f3;   
    }
</style>