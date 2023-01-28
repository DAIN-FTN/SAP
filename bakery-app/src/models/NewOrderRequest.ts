import { ProductBasicInfo } from "./ProductBasicInfo";

export interface NewOrderRequest {
    products: ProductBasicInfo[];
    bakingProgram: string;
}