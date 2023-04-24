import { BakingTimeSlot } from "../BakingTimeSlot";

export interface AvailableBakingPrograms {
    allProductsCanBeSuccessfullyArranged: boolean;
    isThereEnoughStockedProducts: boolean;
    bakingPrograms: BakingTimeSlot[];
}