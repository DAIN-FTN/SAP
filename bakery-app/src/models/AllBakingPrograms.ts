import { BakingTimeSlot as BakingProgram } from "./BakingTimeSlot";

export interface AllBakingPrograms {
    prepareForOven: BakingProgram[];
    preparingAndPrepared: BakingProgram[];
    baking: BakingProgram[];
    done: BakingProgram[];
    preparingInProgress: BakingProgram;
}
