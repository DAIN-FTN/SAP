import { getData, postData } from "./DataService";
import { BakingTimeSlot } from "../models/BakingTimeSlot";
import NewOrderRequest from "../models/NewOrderRequest";
import Order from "../models/Order";
import ProductBasicInfo from "../models/ProductBasicInfo";

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

    const temp = await postData<BakingTimeSlot[]>(`/api/baking-programs/available`, bakingTimeSlotRequest);

    return temp.map((bakingTimeSlot) => ({
        ...bakingTimeSlot,
        bakingStartedAt: new Date(bakingTimeSlot.bakingStartedAt),
        bakingProgrammedAt: new Date(bakingTimeSlot.bakingProgrammedAt),
        createdAt: new Date(bakingTimeSlot.createdAt)
    }));
}

export async function createNewOrder(name: string, newOrderRequest: NewOrderRequest): Promise<Order> {
    return await postData<Order>(`/api/products/stock?name=${name}`, newOrderRequest);
}