import httpClient from '../api/httpClient'
import { API_HOST } from '../helpers/contants'
const url = `${API_HOST}/api/todos`;

export default async function updateTodo(payload) {
    var response = await httpClient.put(`${url}/${payload.id}`, payload).catch(error => console.log(error));
    return response;
 }