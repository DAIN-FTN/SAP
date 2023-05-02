import StockOnLocationResponse from "../StockOnLocationResponse";

export default interface ProductDetailsResponse {
    id: string;
    name: string;
    bakingTimeInMins: number;
    bakingTempInC: number;
    size: number;
    locationsWithStock: StockOnLocationResponse[];
}
