import { getData, postData, putData } from "./DataService";
import ProductDetailsResponse from "../models/Responses/Product/ProductDetailsResponse";
import ProductResponse from "../models/Responses/Product/ProductResponse";
import ProductStockResponse from "../models/Responses/ProductStockResponse";
import CreateProductRequest from "../models/Requests/Products/CreateProductRequest";
import CreateProductResponse from "../models/Responses/Product/CreateProductResponse";
import UpdateProductRequest from "../models/Requests/Products/UpdateProductRequest";
import UpdateProductResponse from "../models/Responses/Product/UpdateProductResponse";

export async function getAll(): Promise<ProductResponse> {
    return await getData<ProductResponse>(`/api/products`);
}

export async function getDetails(productId: string): Promise<ProductDetailsResponse | null> {
    try {
        return await getData<ProductDetailsResponse>(`/api/products/${productId}`);
    } catch (error) {
        return null as unknown as ProductDetailsResponse;
    }
}

export async function getProductStock(name: string): Promise<ProductStockResponse[]> {
    try {
        return await getData<ProductStockResponse[]>(`/api/products/stock/${name}`);
    } catch (error) {
        return [];
    }
}

export async function create(createRequest: CreateProductRequest): Promise<CreateProductResponse> {
    return await postData<CreateProductResponse>(`/api/products`, createRequest);
}

export async function update(productId: string, updateRequest: UpdateProductRequest): Promise<UpdateProductResponse> {
    return await putData<UpdateProductResponse>(`/api/products/${productId}`, updateRequest);
}
