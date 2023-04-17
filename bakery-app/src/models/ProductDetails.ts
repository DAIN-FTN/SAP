export default interface ProductDetails {
    id: string;
    name: string;
    bakingTimeInMins: number;
    bakingTempInC: number;
    size: number;
    locationsWithStock: StockedLocationProductQuantity[];
}

export interface StockedLocationProductQuantity {
    id: string;
    code: string;
    quantity: number;
    reservedQuantity: number;
}