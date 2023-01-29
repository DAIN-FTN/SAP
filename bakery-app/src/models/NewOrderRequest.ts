import { ProductBasicInfo } from "./ProductBasicInfo";

export interface NewOrderRequest {
    products: ProductBasicInfo[];
    bakingProgramId: string | null;
}