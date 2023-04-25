import AvailableBakingProgramResponse from "../Responses/AvailableBakingProgramResponse";

export default interface AvailableProgramsResponse {
    bakingPrograms: AvailableBakingProgramResponse[];
    allProductsCanBeSuccessfullyArranged: boolean;
    isThereEnoughStockedProducts: boolean;
}
