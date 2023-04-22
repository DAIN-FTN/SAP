import ProductBasicInfo from "./ProductBasicInfo";

export default interface BakingTimeSlotRequest {
    products: ProductBasicInfo[];
    deliveryTime: Date;
}