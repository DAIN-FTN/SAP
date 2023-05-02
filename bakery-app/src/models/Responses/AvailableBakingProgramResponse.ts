import { BakingProgramStatus } from "../Enums/BakingProgramStatus";

export default interface AvailableBakingProgramResponse {
    id: string;
    code: string;
    createdAt: Date;
    status: BakingProgramStatus;
    bakingTimeInMins: number;
    bakingTempInC: number;
    bakingProgrammedAt: Date
    bakingStartedAt: Date | null;
    ovenCode: string;
}