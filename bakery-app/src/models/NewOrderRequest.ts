import ProductBasicInfo from "./ProductBasicInfo";

export default interface NewOrderRequest {
    products: ProductBasicInfo[];
    bakingProgramId: string | null;
}