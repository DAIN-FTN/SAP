export interface BakingTimeSlot {
    id: string;
    code: string | null;
    createdAt: Date
    status: BakingProgramStatus | null;
    bakingTimeInMins: number | null;
    bakingTempInC: number | null;
    bakingProgrammedAt: Date
    bakingStartedAt: Date
    ovenCode: string | null;
}

export enum BakingProgramStatus {
    Created,
    Preparing,
    Prepared,
    Baking,
    Done,
    Cancelled
}