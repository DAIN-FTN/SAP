import { config } from "../config";
import { ProductBasicInfo } from "../models/ProductBasicInfo";

const baseUrl = `${config.httpProtocol}${config.serverAddress}${config.port}`;

async function fetchData<T>(url: string): Promise<T> {
    const response = await fetch(`${baseUrl}${url}`);

    return await response.json() as T;
}

export async function fetchProductsBasicInfo(name: string): Promise<ProductBasicInfo[]> {
    return await fetchData<ProductBasicInfo[]>(`/getProducts?name=${name}`);
}