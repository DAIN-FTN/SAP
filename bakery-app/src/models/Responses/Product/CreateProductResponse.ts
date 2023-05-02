import CreateStockedProductResponse from "../StockedProduct/CreateStockedProductResponse";

export default interface CreateProductResponse {
    id: string;
    name: string;
    bakingTimeInMins: number;
    bakingTempInC: number;
    size: number;
    stock: CreateStockedProductResponse[];
}
