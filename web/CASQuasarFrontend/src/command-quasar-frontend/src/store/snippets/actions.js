import { apiGetCall, apiDeleteCall, apiPostCall, apiPutCall } from '../shared'
import { LogMessage } from '../../models'
// Snippets API

// Fetch snippets data
export async function fetchSnippets (context) {
  await apiGetCall('snippets')
    .then((snippets) => {
      context.commit('updateSnippets', snippets)
      this.dispatch('logger/addMessage', new LogMessage('Snippets fetched from server', false))
    })
    .catch((error) => {
      this.dispatch('logger/addMessage', new LogMessage(`Fetch Snippets: ${error.message}`, true))
    })
}

// Remove snippet from API/DB
export async function removeSnippet (context, snippet) {
  await apiDeleteCall(`snippets/${snippet.id}`)
    .then((snp) => {
      const index = this.state.snippets.snippetsData.findIndex(r => r.id === snp.id)
      context.commit('removeFromIndex', index)
      this.dispatch('logger/addMessage', new LogMessage('Snippet removed from server', false))
    })
    .catch((error) => {
      this.dispatch('logger/addMessage', new LogMessage(`Remove snippet: ${error.message}`, true))
    })
}

// Create snippet
export async function createSnippet (context, snippet) {
  await apiPostCall('snippets', snippet)
    .then((snippet) => {
      this.dispatch('logger/addMessage', new LogMessage('Created Snippet', false))
      return snippet
    })
    .catch((error) => {
      this.dispatch('logger/addMessage', new LogMessage(`Create snippet: ${error.message}`, true))
    })
}

// Update snippet
export async function updateSnippet (context, snippet) {
  const id = snippet.id
  if ((context.getters.getSnippetWithId(id)).isEqual(snippet)) {
    return Promise.reject('The snippet had no changes!')
  }
  await apiPutCall(`snippets/${snippet.id}`, snippet)
    .then((updatedSnippet) => {
      context.commit('replaceSnippetAtIndex', updatedSnippet)
      this.dispatch('logger/addMessage', new LogMessage('Updated Snippet', false))
      return updatedSnippet
    })
    .catch((error) => {
      this.dispatch('logger/addMessage', new LogMessage(`Updating snippet: ${error.message}`, true))
    })
}

// Search API

export async function fetchSearchData (context) {
  const searchText = this.state.snippets.searchText
  await apiGetCall(`search/snippets/${searchText}`)
    .then((snippets) => {
      context.commit('updateSearchData', snippets)
      this.dispatch('logger/addMessage', new LogMessage(`Fetched snippet search data: ${searchText}`, false))
    })
    .catch((error) => {
      this.dispatch('logger/addMessage', new LogMessage(`Error fetching search snippet data: ${error.message}`, true))
    })
}
