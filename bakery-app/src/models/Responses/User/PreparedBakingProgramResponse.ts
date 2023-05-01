import { BakingProgramStatus } from "../../Enums/BakingProgramStatus";

export default interface PreparedBakingProgramResponse {
    id: string;
    code: string;
    createdAt: string;
    bakingProgramStatus: BakingProgramStatus;
    bakingTimeInMins: number;
    bakingTempInC: number;
    bakingProgrammedAt: string;
    bakingStartedAt: string;
    ovenId: string;
    ovenCode: string;
};