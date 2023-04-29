import { BakingTimeSlot as BakingProgram } from "./BakingTimeSlot";
import { StartPreparing } from "./Responses/StartPreparing";

export default interface AllBakingPrograms {
    prepareForOven: BakingProgram[];
    preparingAndPrepared: BakingProgram[];
    baking: BakingProgram[];
    done: BakingProgram[];
    preparingInProgress: StartPreparing;
}
