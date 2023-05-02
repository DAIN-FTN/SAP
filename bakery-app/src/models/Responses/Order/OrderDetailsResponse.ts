import { OrderStatus } from "../../Enums/OrderStatus";
import Customer from "../../Requests/Customer";
import OrderBakingProgramResponse from "./OrderBakingProgramResponse";
import OrderProductResponse from "./OrderProductResponse";

export default interface OrderDetailsResponse {
    id: string;
    shouldBeDoneAt: Date
    status: OrderStatus;
    customer: Customer;
    products: OrderProductResponse[];
    bakingPrograms: OrderBakingProgramResponse[];
}
