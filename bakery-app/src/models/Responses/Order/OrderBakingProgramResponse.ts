import { BakingProgramStatus } from "../../BakingTimeSlot";
import OrderProductResponse from "./OrderProductResponse";

export default interface OrderBakingProgramResponse {
    code: string;
    bakingProgrammedAt: Date
    status: BakingProgramStatus;
    bakingStartedAt: Date | null;
    bakingTimeInMins: number;
    ovenCode: string;
    products: OrderProductResponse[];
}
