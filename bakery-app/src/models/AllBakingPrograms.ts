import { BakingTimeSlot as BakingProgram } from "./BakingTimeSlot";

export default interface AllBakingPrograms {
    prepareForOven: BakingProgram[];
    preparingAndPrepared: BakingProgram[];
    baking: BakingProgram[];
    done: BakingProgram[];
    preparingInProgress: BakingProgram;
}
