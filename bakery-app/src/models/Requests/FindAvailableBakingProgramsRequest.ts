import OrderProductRequest from "./OrderProductRequest";

export default interface FindAvailableBakingProgramsRequest {
    shouldBeDoneAt: Date;
    orderProducts: OrderProductRequest[];
}
