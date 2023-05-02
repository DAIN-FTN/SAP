import BakingProgramResponse from "./BakingProgramResponse";
import StartPreparingResponse from "./StartPreparing/StartPreparingResponse";

export default interface AllBakingProgramsResponse {
    prepareForOven: BakingProgramResponse[];
    preparingAndPrepared: BakingProgramResponse[];
    baking: BakingProgramResponse[];
    done: BakingProgramResponse[];
    preparingInProgress: StartPreparingResponse | null;
}
