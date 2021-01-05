import { apiGetCall, apiDeleteCall, apiPostCall, apiPutCall } from '../shared'
import { LogMessage } from '../../models'
// Commands API

// Fetch commands data
export async function fetchCommands (context) {
  await apiGetCall('commands')
    .then((commands) => {
      context.commit('updateCommands', commands)
      this.dispatch('logger/addMessage', new LogMessage('Commands fetched from server', false))
    })
    .catch((error) => {
      this.dispatch('logger/addMessage', new LogMessage(`fetch commands: ${error.message}`, true))
    })
}

// Remove command from API/DB
export async function removeCommand (context, command) {
  await apiDeleteCall(`commands/${command.id}`)
    .then((cmd) => {
      const index = this.state.commands.commandsData.findIndex(r => r.id === cmd.id)
      context.commit('removeFromIndex', index)
      this.dispatch('logger/addMessage', new LogMessage('Command removed', false))
    })
    .catch((error) => {
      this.dispatch('logger/addMessage', new LogMessage(`remove commands ${error}`, true))
    })
}

// Create command
export async function createCommand (context, command) {
  await apiPostCall('commands', command)
    .then((cmd) => {
      this.dispatch('logger/addMessage', new LogMessage('Command created', false))
      return cmd
    })
    .catch((error) => {
      this.dispatch('logger/addMessage', new LogMessage(`Create command: ${error.message}`, true))
    })
}

// Update command
export async function updateCommand (context, command) {
  const id = command.id
  if ((context.getters.getCommandWithId(id)).isEqual(command)) {
    return Promise.reject('The command had no changes!')
  }
  await apiPutCall(`commands/${command.id}`, command)
    .then((updatedCommand) => {
      this.dispatch('logger/addMessage', new LogMessage('Updated command', false))
      context.commit('replaceCommandAtIndex', updatedCommand)
    })
    .catch((error) => {
      this.dispatch('logger/addMessage', new LogMessage(`Update command: ${error.message}`, true))
    })
}

// Search API
export async function fetchSearchData (context) {
  const searchText = this.state.commands.searchText
  await apiGetCall(`search/commands/${searchText}`)
    .then((results) => {
      context.commit('updateSearchData', results)
      this.dispatch('logger/addMessage', new LogMessage('Fetched command search data', false))
    })
    .catch((error) => {
      this.dispatch('logger/addMessage', new LogMessage(`Fetch search data: ${error.message}`, true))
    })
}
