import { getData, postData } from "./DataService";
import { BakingTimeSlot } from "../models/BakingTimeSlot";
import { NewOrderRequest } from "../models/NewOrderRequest";
import { Order } from "../models/Order";
import { ProductBasicInfo } from "../models/ProductBasicInfo";

export async function fetchProductsBasicInfo(name: string): Promise<ProductBasicInfo[]> {
    return await getData<ProductBasicInfo[]>(`/api/products/stock?name=${name}`);
}

export async function fetchBakingTimeSlots(deliveryDateTime: Date, products: ProductBasicInfo[]): Promise<BakingTimeSlot[]> {
    const bakingTimeSlotRequest = {
        shouldBeDoneAt: deliveryDateTime,
        orderProducts: products.map((product) => ({
            productId: product.id,
            quantity: product.quantity,
        }))
    };

    return await postData<BakingTimeSlot[]>(`/api/baking-programs/available`, bakingTimeSlotRequest);
}

export async function createNewOrder(name: string, newOrderRequest: NewOrderRequest): Promise<Order> {
    return await postData<Order>(`/api/products/stock?name=${name}`, newOrderRequest);
}