export default interface CreateStockedProductResponse {
    locationId: string;
    locationCode: string;
    productId: string;
    productName: string;
    quantity: number;
    reservedQuantity: number;
}
