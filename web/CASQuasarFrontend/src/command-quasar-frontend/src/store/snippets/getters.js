export const canSearch = (state) => {
  return (state.searchText.trim().length || state.searchText)
}

export const snippetsWithPlatform = (state) => (platform) => {
  return state.snippetsData.filter(c => c.platform === platform)
}

export const getSnippetWithId = (state) => (id) => {
  var result = state.snippetsData.filter(c => c.id === id)
  return result.length > 0 ? result[0] : null
}

export const getLastSnippets = (state) => (count) => {
  const arrLength = state.snippetsData.length

  if (arrLength === 0) return []

  if (arrLength >= count) {
    return state.snippetsData.slice(-count)
  }

  return state.snippetsData.slice(-arrLength)
}
