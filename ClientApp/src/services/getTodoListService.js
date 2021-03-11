import httpClient from '../api/httpClient'
import { API_HOST } from '../helpers/contants'
const url = `${API_HOST}/api/todos`;

export default async function getTodoList() {
    var response = await httpClient.get(url).catch(error => console.log(error));
    return response.data;
 }