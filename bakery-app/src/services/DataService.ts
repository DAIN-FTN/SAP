import { config } from "../config";

const baseUrl = `${config.httpProtocol}${config.serverAddress}${config.port}`;

export async function getData<T>(url: string): Promise<T> {
    const response = await fetch(`${baseUrl}${url}`);

    return await response.json() as T;
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