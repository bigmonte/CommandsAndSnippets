import { Snippet } from '../../models'

export const updateSnippets = (state, toSet) => {
  state.snippetsData = []
  toSet.forEach(d => state.snippetsData.push(new Snippet(d.howTo, d.platform, d.codeSnippet, d.id)))
}

export const removeFromIndex = (state, index) => {
  state.snippetsData.splice(index, 1)
}

export const addSnippet = (state, snippetToSet) => {
  const snippet = new Snippet(snippetToSet.howTo, snippetToSet.platform, snippetToSet.codeSnippet, snippetToSet.id)
  state.snippetsData.push(snippet)
}

export const replaceSnippetAtIndex = (state, snippetToReplace) => {
  const index = state.snippetsData.findIndex(r => r.id === snippetToReplace.id)
  state.snippetsData[index] = new Snippet(snippetToReplace.howTo, snippetToReplace.platform, snippetToReplace.codeSnippet, snippetToReplace.id)
}

// Search snippets state

export const updateSearchText = (state, toSet) => {
  state.searchText = toSet
}

export const updateSearching = (state, toSet) => {
  state.searching = toSet
}

export const updateSearchData = (state, toSet) => {
  state.searchData = toSet
}
