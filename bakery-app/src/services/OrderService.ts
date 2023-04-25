import { getData, postData } from "./DataService";
import { BakingTimeSlot } from "../models/BakingTimeSlot";
import NewOrderRequest from "../models/Requests/NewOrderRequest";
import Order from "../models/Order";
import ProductBasicInfo from "../models/ProductBasicInfo";
import CreateOrderResponse from "../models/Responses/CreateOrderResponse";

export async function fetchProductsBasicInfo(name: string): Promise<ProductBasicInfo[]> {
    try {
        const data = await getData<any[]>(`/api/products/stock?name=${name}`);
        return data.map((product) => ({
            id: product.id,
            name: product.name,
            quantity: product.availableQuantity,
        }));
    } catch (error) {
        return [] as ProductBasicInfo[];
    }
}

export async function fetchBakingTimeSlots(deliveryDateTime: Date, products: ProductBasicInfo[]): Promise<BakingTimeSlot[]> {
    const bakingTimeSlotRequest = {
        shouldBeDoneAt: deliveryDateTime.toISOString(),
        orderProducts: products.map((product) => ({
            productId: product.id,
            quantity: product.quantity,
        }))
    };
    console.log(bakingTimeSlotRequest);

    const temp = await postData<AvailableBakingPrograms.AvailableBakingPrograms>(`/api/baking-programs/available`, bakingTimeSlotRequest);

    return temp.bakingPrograms.map((bakingTimeSlot) => ({
        ...bakingTimeSlot,
        bakingStartedAt: bakingTimeSlot.bakingStartedAt ? new Date(bakingTimeSlot.bakingStartedAt) : null,
        bakingProgrammedAt: new Date(bakingTimeSlot.bakingProgrammedAt),
        createdAt: new Date(bakingTimeSlot.createdAt)
    }));
}

export async function createNewOrder(newOrderRequest: NewOrderRequest): Promise<CreateOrderResponse> {
    return await postData<CreateOrderResponse>(`/api/orders`, newOrderRequest);
}