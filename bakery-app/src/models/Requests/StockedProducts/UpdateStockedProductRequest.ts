export default interface UpdateStockedProductRequest {
    locationId: string;
    productId: string;
    quantity: number;
    reservedQuantity: number;
}
