import axios from 'axios' 


const post = (url, data, config = {}) => {
    return axios.post(url, data, config)
}

const get = (url) => {
    return axios.get(url)
}

const put = (url, data, config = {}) => {
    return axios.put(url, data, config)
}

//Cannot contain a delete method - Cause delete is a keyword.

const del = (url, config = {}) => {
    return axios.delete(url, config)
}

const httpClient = {
    post,
    get,
    put,
    delete: del
}

export default httpClient;