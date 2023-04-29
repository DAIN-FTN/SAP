export interface BakingTimeSlot {
    id: string;
    code: string | null;
    createdAt: Date
    status: BakingProgramStatus | null;
    bakingTimeInMins: number | null;
    bakingTempInC: number | null;
    bakingProgrammedAt: Date
    canBePreparedAt: Date
    canBeBakedAt: Date
    bakingStartedAt: Date | null;
    ovenId: string | null;
    ovenCode: string | null;
}

export enum BakingProgramStatus {
    Pending,
    Created,
    Preparing,
    Prepared,
    Baking,
    Done,
    Cancelled,
    Finished
}