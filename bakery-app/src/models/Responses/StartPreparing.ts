export interface StartPreparing {
    id: string;
    code: string;
    bakingTimeInMins: number;
    bakingTempInC: number;
    bakingProgrammedAt: Date
    ovenId: string;
    ovenCode: string;
    locations: LocationToPrepareFrom[];
}

export interface LocationToPrepareFrom {
    locationId: string;
    locationCode: string;
    products: ProductToPrepare[];
}

export interface ProductToPrepare {
    productId: string;
    name: string;
    quantity: number;
    orderId: string;
}