import { getData, postData } from "./DataService";
import CreateOrderRequest from "../models/Requests/CreateOrderRequest";
import OrderResponse from "../models/Responses/Order/OrderResponse";
import OrderDetailsResponse from "../models/Responses/Order/OrderDetailsResponse";
import CreateOrderResponse from "../models/Responses/Order/CreateOrderResponse";

export async function getAll(): Promise<OrderResponse[]> {
    const response = await getData<OrderResponse[]>(`/api/orders`);
    return mapOrderResponse(response);
}

export async function getDetails(orderId: string): Promise<OrderDetailsResponse | null> {
    try {
        const response = await getData<OrderDetailsResponse>(`/api/orders/${orderId}`);
        return mapOrderDetailsResponse(response);
    } catch (error) {
        return null as unknown as OrderDetailsResponse;
    }
}

export async function create(createRequest: CreateOrderRequest): Promise<CreateOrderResponse> {
    return await postData<CreateOrderResponse>(`/api/orders`, createRequest);
}

function mapOrderDetailsResponse(orderDetailsResponse: OrderDetailsResponse): OrderDetailsResponse {
    return {
        ...orderDetailsResponse,
        shouldBeDoneAt: new Date(orderDetailsResponse.shouldBeDoneAt),
        bakingPrograms: orderDetailsResponse.bakingPrograms.map(bakingProgram => ({
            ...bakingProgram,
            bakingProgrammedAt: new Date(bakingProgram.bakingProgrammedAt),
            bakingStartedAt: bakingProgram.bakingStartedAt ? new Date(bakingProgram.bakingStartedAt) : null
        }))
    };
}

function mapOrderResponse(response: OrderResponse[]): OrderResponse[] {
    return response.map(order => ({
        ...order,
        shouldBeDoneAt: new Date(order.shouldBeDoneAt)
    }));
}
