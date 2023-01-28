export interface ProductDetails {
    name: string;
    availableQuantity: number;
    stockedLocations: StockedLocationProductQuantity[];
}

export interface StockedLocationProductQuantity {
    id: number;
    availableQuantity: number;
}