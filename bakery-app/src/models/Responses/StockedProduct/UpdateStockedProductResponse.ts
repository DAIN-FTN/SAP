export default interface UpdateStockedProductResponse {
    locationId: string;
    locationCode: string;
    productId: string;
    productName: string;
    quantity: number;
    reservedQuantity: number;
}
