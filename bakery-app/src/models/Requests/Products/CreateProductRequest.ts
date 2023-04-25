import CreateStockedProductRequest from "../StockedProducts/CreateStockedProductRequest";

export default interface CreateProductRequest {
    name: string;
    bakingTimeinMins: number;
    bakingTempInC: number;
    size: number;
    stock: CreateStockedProductRequest[];
}
