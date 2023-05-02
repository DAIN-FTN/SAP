import { OrderStatus } from "../../Enums/OrderStatus";
import Customer from "../../Requests/Customer";

export default interface OrderResponse {
    id: string;
    shouldBeDoneAt: Date;
    status: OrderStatus;
    customer: Customer;
}
