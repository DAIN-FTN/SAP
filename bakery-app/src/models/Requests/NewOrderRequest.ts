export default interface NewOrderRequest {
    shouldBeDoneAt: Date;
    customer: Customer;
    products: OrderProductRequest[];
}

export interface Customer {
    fullName: string;
    email: string;
    telephone: string;
}

export interface OrderProductRequest {
    propuctId: string;
    quantity: number;
}