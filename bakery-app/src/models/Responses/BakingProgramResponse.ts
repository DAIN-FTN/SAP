import { BakingProgramStatus } from "../Enums/BakingProgramStatus";

export default interface BakingProgramResponse {
    id: string;
    code: string;
    createdAt: Date;
    status: BakingProgramStatus;
    bakingTimeInMins: number;
    bakingTempInC: number;
    bakingProgrammedAt: Date;
    canBePreparedAt: Date;
    canBeBakedAt: Date;
    bakingStartedAt: Date | null;
    ovenId: string;
    ovenCode: string;
}
