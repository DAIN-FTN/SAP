export default interface CreateStockedProductRequest {
    productId: string | null;
    locationId: string;
    quantity: number;
}
