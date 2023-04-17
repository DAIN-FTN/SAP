import { config } from "../config";

const baseUrl = `${config.httpProtocol}${config.serverAddress}${config.port}`;

export async function getData<T>(url: string): Promise<T> {
    const rawResponse = await fetch(`${baseUrl}${url}`);

    const jsonResponse = await rawResponse.json();

    if (jsonResponse.errors) {
        console.warn("Shit hit the fan... throwing error in component DataService.ts", jsonResponse.errors.ErrorToDisplay[0]);

        throw new Error(jsonResponse.errors.ErrorToDisplay[0]);
    }

    return jsonResponse as T;
}

export async function postData<T>(url: string, payload: any): Promise<T> {
    try {
        const response = await fetch(`${baseUrl}${url}`, {
            method: "POST",
            body: JSON.stringify(payload),
            headers: {
                'content-type': 'application/json'
            }
        });

        return await response.json() as T;
    } catch (error) {
        console.error(error);
        return [] as T;
    }
}