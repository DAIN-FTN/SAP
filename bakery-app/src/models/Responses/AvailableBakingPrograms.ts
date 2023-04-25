import AvailableBakingProgramResponse from "./AvailableBakingProgramResponse";

export default interface AvailableProgramsResponse {
    bakingPrograms: AvailableBakingProgramResponse[];
    allProductsCanBeSuccessfullyArranged: boolean;
    isThereEnoughStockedProducts: boolean;
}