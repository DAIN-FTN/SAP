import Customer from "./Customer";
import OrderProductRequest from "./OrderProductRequest";

export default interface CreateOrderRequest {
    shouldBeDoneAt: Date;
    customer: Customer;
    products: OrderProductRequest[];
}
