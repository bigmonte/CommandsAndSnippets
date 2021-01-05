import Vue from 'vue'

export async function apiGetCall (uriCall) {
  return await Vue.prototype.$axios.get(`/api/${uriCall}`)
    .then(res => { return res.data })
    .catch((error) => Promise.reject(error))
}

export async function apiDeleteCall (uriCall) {
  return await Vue.prototype.$axios
    .delete(`/api/${uriCall}`)
    .then(res => res.data)
    .catch((error) => Promise.reject(error))
}

export async function apiPostCall (uriCall, object) {
  return await Vue.prototype.$axios
    .post(`/api/${uriCall}`, object)
    .then(res => res.data)
    .catch((error) => Promise.reject(error))
}

export async function apiPutCall (uriCall, object) {
  return await Vue.prototype.$axios
    .put(`/api/${uriCall}`, object)
    .then(res => res.data)
    .catch((error) => Promise.reject(error))
}
