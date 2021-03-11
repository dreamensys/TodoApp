import httpClient from '../api/httpClient'
import { API_HOST } from '../helpers/contants'
const url = `${API_HOST}/api/todos`;

export default async function createTodo(payload) {
    var response = await httpClient.post(url, payload).catch(error => console.log(error));
    return response.data;
 }