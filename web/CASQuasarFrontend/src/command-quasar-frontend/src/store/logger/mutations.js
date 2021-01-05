export const updateCurrentMessage = (state, toSet) => {
  state.currentMessage = toSet
}

export const addMessage = (state, toSet) => {
  toSet.id = state.messages.length
  state.messages.unshift(toSet)
}
