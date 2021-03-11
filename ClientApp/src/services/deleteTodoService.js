import httpClient from '../api/httpClient'
import { API_HOST } from '../helpers/contants'
const url = `${API_HOST}/api/todos`;

export default async function removeTodo(id) {
    var response = await httpClient.delete(`${url}/${id}`, id).catch(error => console.log(error));
    return response;
 }