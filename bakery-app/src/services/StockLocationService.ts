import { getData } from "./DataService";
import StockLocationResponse from "../models/Responses/StockLocation/StockLocationResponse";

export async function getAll(): Promise<StockLocationResponse[]> {
    return await getData<StockLocationResponse[]>(`/api/stock-locations`);
}