import { getData, postData } from "./DataService";
import { BakingTimeSlot } from "../models/BakingTimeSlot";
import NewOrderRequest from "../models/NewOrderRequest";
import Order from "../models/Order";
import ProductBasicInfo from "../models/ProductBasicInfo";
import ProductDetails from "../models/ProductDetails";

export async function fetchProductDetails(productId: string): Promise<null | ProductDetails> {
    try {
        return await getData<ProductDetails>(`/api/products/${productId}`);
    } catch (error) {
        return null as unknown as ProductDetails;
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
        bakingStartedAt: bakingTimeSlot.bakingStartedAt ? new Date(bakingTimeSlot.bakingStartedAt) : null,
        bakingProgrammedAt: new Date(bakingTimeSlot.bakingProgrammedAt),
        createdAt: new Date(bakingTimeSlot.createdAt)
    }));
}

export async function createNewOrder(name: string, newOrderRequest: NewOrderRequest): Promise<Order> {
    return await postData<Order>(`/api/products/stock?name=${name}`, newOrderRequest);
}