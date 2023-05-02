import CreateStockedProductRequest from "../StockedProducts/CreateStockedProductRequest";

export default interface CreateProductRequest {
    name: string;
    bakingTimeInMins: number;
    bakingTempInC: number;
    size: number;
    stock: CreateStockedProductRequest[];
}
