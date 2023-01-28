import { ProductBasicInfo } from "./ProductBasicInfo";

export interface BakingTimeSlotRequest {
    products: ProductBasicInfo[];
    deliveryTime: Date;
}