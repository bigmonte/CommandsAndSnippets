// Commands state
import { Command } from '../../models'

export const updateCommands = (state, commandsList) => {
  state.commandsData = []
  commandsList.forEach(d => state.commandsData.push(new Command(d.howTo, d.platform, d.commandLine, d.id)))
}

export const removeFromIndex = (state, index) => {
  state.commandsData.splice(index, 1)
}

export const addCommand = (state, commandToSet) => {
  const command = new Command(commandToSet.howTo, commandToSet.platform, commandToSet.commandLine, commandToSet.id)
  state.commandsData.push(command)
}

export const replaceCommandAtIndex = (state, commandToReplace) => {
  const index = state.commandsData.findIndex(r => r.id === commandToReplace.id)
  state.commandsData[index] = new Command(commandToReplace.howTo, commandToReplace.platform, commandToReplace.commandLine, commandToReplace.id)
}

// Search commands state

export const updateSearchText = (state, toSet) => {
  state.searchText = toSet
}

export const updateSearching = (state, toSet) => {
  state.searching = toSet
}

export const updateSearchData = (state, toSet) => {
  state.searchData = toSet
}
