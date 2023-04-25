import { getData, postData } from "./DataService";
import CreateOrderRequest from "../models/Requests/CreateOrderRequest";
import OrderResponse from "../models/Responses/Order/OrderResponse";
import OrderDetailsResponse from "../models/Responses/Order/OrderDetailsResponse";
import CreateOrderResponse from "../models/Responses/Order/CreateOrderResponse";

export async function getAll(): Promise<OrderResponse> {
    return await getData<OrderResponse>(`/api/orders`);
}

export async function getDetails(orderId: string): Promise<OrderDetailsResponse | null> {
    try {
        return await getData<OrderDetailsResponse>(`/api/orders/${orderId}`);
    } catch (error) {
        return null as unknown as OrderDetailsResponse;
    }
}

export async function create(createRequest: CreateOrderRequest): Promise<CreateOrderResponse> {
    return await postData<CreateOrderResponse>(`/api/orders`, createRequest);
}
