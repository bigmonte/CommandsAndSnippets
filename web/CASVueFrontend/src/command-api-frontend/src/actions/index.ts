import axios from 'axios';
interface Command {
  howTo: string;
  commandLine: string;
  platform: string;
  id: number;
}

export function fetchCommands () {
    return axios.get('/api/commands')
    .then (res => {
        return res.data
    })
}

export function searchCommands (searchTitle: string) {
  return axios.get(`/api/search/commands/${searchTitle}`)
  .then (res => {
      return res.data
  })
}


export function fetchCommandById (commandId: number) {
  return axios.get(`/api/commands/${commandId}`)
  .then (res => {
      return res.data
  })
}

export function updateCommandRequest (id: number, command: Command) {
  return axios
    .put(`/api/commands/${id}`, command)
    .then (res => res.data)
    .catch((error) =>  Promise.reject(error))
}

export function createCommandRequest (command: Command) {
  return axios
    .post('/api/commands', command)
    .then (res => res.data)
    .catch((error) =>  Promise.reject(error))
}

export function deleteCommandRequest (id: number) {
  return axios
    .delete(`/api/commands/${id}`)
    .then (res => res.data)
    .catch((error) =>  Promise.reject(error))
}