  <script>
      import CommandSearch from '@/components/CommandSearch'
      import CommandList from '@/components/CommandList'
      import CommandUpdate from '@/components/CommandUpdate'
      import CommandDetail from '@/components/CommandDetail'
      import CommandDelete from '@/components/CommandDelete'
      import { fetchCommands, searchCommands} from '@/actions'
      export default {
        components: {
          CommandSearch,
          CommandList,
          CommandUpdate,
          CommandDetail,
          CommandDelete
        },
        data () {
          return {
            isDetailView: true,
            selectedCommand: null,
            commands : []
          }
        },
        created () {
          // created is called once options are resolved (data, computed, methods, ...) and instance created
          this.getCommands()
          //console.log(res.data)

        },
        
        mounted () {
          // called after instance is mounted
          console.log("Instance mounted!")

        },
        computed: {
            commandsLength(){
              // revaluated each time reactive dependency change
              return this.commands.length
            },
            toggleBtnClass(){
              return this.isDetailView ? 'btn-warning' : 'btn-primary'
            },
            commandsAreNotEmpty() { return this.commandsLength > 0},
            activeCommand() {
              return this.selectedCommand || (this.commandsAreNotEmpty && this.commands[0]) || null
            }
        },
        methods: {
          async getCommands() {
            const commands = await fetchCommands();
            this.commands = commands
          },
          toggleView () {
            this.isDetailView = !this.isDetailView
          },
          selectCommand (selectedCommand) {
            // TODO: it's copied by reference!
            this.selectedCommand = selectedCommand
          },
          refreshDeletedCommandViewData (newCommand) {

            //const index = this.commands.findIndex( r => r.id === newCommand.id)
            const index = this.commands.findIndex( r => r.id === newCommand.id)
            this.commands.splice(index, 1)
            this.selectCommand(this.commands[0] || null)
          },
          refreshUpdatedCommandViewData (newCommand) {
            const index = this.commands.findIndex( r => r.id === newCommand.id)
            this.commands[index] = newCommand
            this.selectCommand(newCommand)
          },
          async handleSearch (text) {
            if (!text) { 
              this.getCommands() 
              return
              } 
            if (!text.trim().length) {
              return
            }
            this.commands = await searchCommands(text)
            this.selectedCommand = null
          }
        }
      }
  </script>

<template>
    <div class="row">
      <div class="col-md-6 order-md-2 mb-4">
        <h4 class="d-flex justify-content-between align-items-center mb-3">
          <span class="text-muted">Useful Commands</span>
          <span class="badge badge-secondary badge-pill">{{commandsLength}}</span>
        </h4>
        <command-search
          @on-search="handleSearch"
        />
        <command-list :commands="commands"
                      :activeId="activeCommand?.id"
                      @on-command-click="selectCommand"/>
      </div>
      <div class="col-md-6 order-md-1">
        <h4 class="mb-3">Command {{activeCommand?.id}} 
          <template v-if="commandsAreNotEmpty">
            <button 
              @click="toggleView"
              :class="`btn btn-sm btn-success ${toggleBtnClass} mr-2`">
              {{isDetailView? 'Edit Command' : 'Command details'}}
            </button> 
            <command-delete 
              @on-command-delete="refreshDeletedCommandViewData($event); !commandsAreNotEmpty ? this.isDetailView = true : null"
              :activeId="activeCommand?.id"/>
          </template>
        </h4>
          <command-detail v-if="isDetailView"
          :command="activeCommand">
            <template #detailsOrGoBack>
              <router-link
                  class="btn btn-outline-success"
                  :to="{name: 'commandDetailsPage', params: {id: activeCommand?.id}}"> Command details
              </router-link>
            </template>
          </command-detail>
          <command-update 
            v-else
            @on-command-update="refreshUpdatedCommandViewData($event)"
            :command="activeCommand"/>
      </div>
    </div>
  </template>



