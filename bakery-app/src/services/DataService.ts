import { config } from "../config";

const baseUrl = `${config.httpProtocol}${config.serverAddress}${config.port}`;

export async function getData<T>(url: string): Promise<T> {
    const response = await fetch(`${baseUrl}${url}`);

    return await response.json() as T;
}

export async function postData<T>(url: string, payload: any): Promise<T> {
    try {

        var myHeaders = new Headers();
        myHeaders.append("Content-Type", "application/json");

        var raw = JSON.stringify({
            "ShouldBeDoneAt": "2023-01-21T14:39:00.000Z",
            "OrderProducts": [
                {
                    "ProductId": "5cd54cb6-0df4-420f-96fd-f6e2cf6e2000",
                    "Quantity": 2
                }
            ]
        });

        const response = await fetch(`${baseUrl}${url}`, {
            method: "POST",
            body: '{"ShouldBeDoneAt":"2023-01-21T14:39:00.000Z"}',
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