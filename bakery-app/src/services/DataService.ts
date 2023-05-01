import { config } from "../config";
import { ResponseError } from "../models/Errors/ResponseError";

const baseUrl = `${config.httpProtocol}${config.serverAddress}${config.port}`;

export async function getData<T>(url: string): Promise<T> {
    const rawResponse = await fetch(`${baseUrl}${url}`, {
        headers: {
            'authorization': getUserToken()
        }
    });

    const jsonResponse = await rawResponse.json();

    if (jsonResponse.errors) {
        console.warn("Shit hit the fan... throwing error in component DataService.ts", jsonResponse.errors.ErrorToDisplay[0]);

        throw new Error(jsonResponse.errors.ErrorToDisplay[0]);
    }

    return jsonResponse as T;
}

export async function postData<T>(url: string, payload: any): Promise<T> {

    const response = await fetch(`${baseUrl}${url}`, {
        method: "POST",
        body: JSON.stringify(payload),
        headers: {
            'content-type': 'application/json'
        }
    });

    if(response.status === 401)
        handleUnauthorizedAccess();

    
    const jsonResponse = await response.json()
    if(jsonResponse.errors){
        console.warn("Shit hit the fan... throwing error in component DataService.ts", jsonResponse.errors.ErrorToDisplay[0]);

        throw new Error(jsonResponse.errors.ErrorToDisplay[0]);
    }

    return jsonResponse as T;
    
}

function handleUnauthorizedAccess() {
    var message = 'Unauthorized access';
    console.warn(message);
    
    var error = new ResponseError(message);
    error.setStatus(401);
    
    throw error;
}

function getUserToken(): string {
    const token = localStorage.getItem('sap-bakery-token');
    return token != null? 'Bearer ' + token: '';
}

export async function putData<T>(url: string, payload: any = null): Promise<T> {
    const rawResponse = await fetch(`${baseUrl}${url}`, {
        method: "PUT",
        body: JSON.stringify(payload),
        headers: {
            'content-type': 'application/json'
        }
    });

    const jsonResponse = await rawResponse.json();

    if (jsonResponse.errors) {
        console.warn("Shit hit the fan... throwing error in component DataService.ts", jsonResponse.errors.ErrorToDisplay[0]);
        throw new Error(jsonResponse.errors.ErrorToDisplay[0]);
    }

    return jsonResponse as T;
}

export async function putDataWithoutResponse(url: string, payload: any = null) {
    const rawResponse = await fetch(`${baseUrl}${url}`, {
        method: "PUT",
        body: JSON.stringify(payload),
        headers: {
            'content-type': 'application/json'
        }
    });

    if (rawResponse.status === 200) return;

    const jsonResponse = await rawResponse.json();
    console.warn("Shit hit the fan... throwing error in component DataService.ts", jsonResponse.errors.ErrorToDisplay[0]);
    throw new Error(jsonResponse.errors.ErrorToDisplay[0]);
}