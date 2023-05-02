import { config } from "../config";
import { ResponseError } from "../models/Errors/ResponseError";

const baseUrl = `${config.httpProtocol}${config.serverAddress}${config.port}`;

export async function getData<T>(url: string, token?: string): Promise<T> {
    let rawResponse: Response
    
    rawResponse = await fetch(`${baseUrl}${url}`, {
        headers: {
            'authorization': getUserToken(token)
        }
    });

    if (rawResponse.status === 401) {
        console.error("Unauthorized access. Token was not provided or is invalid.", getUserToken(token));
    
        window.dispatchEvent(new CustomEvent("unauthorized"));
    }

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

    if (response.status === 401)
        handleUnauthorizedAccess();


    const jsonResponse = await response.json()
    if (jsonResponse.errors) {
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

function getUserToken(token?: string): string {
    let result;

    if (token !== 'undefined' && token !== undefined) {
        console.debug("DataService.getUserToken(), token provided", token);
        result = 'Bearer ' + token;
        return result;
    }

    const rawUser = localStorage.getItem('sap-bakery-user')

    console.debug("DataService.getUserToken(), rawUser from LocalStorage", rawUser);

    if (!rawUser) {
        result = 'Bearer ';
        console.debug("DataService.getUserToken(), rawUser is bad", result);
        return result;
    }
    else {
        const tokenFromStorage = JSON.parse(rawUser).token;
        result = tokenFromStorage !== '' ? 'Bearer ' + tokenFromStorage : '';
        console.debug("DataService.getUserToken(), rawUser is good", result);
        return result;
    }
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