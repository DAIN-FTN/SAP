export interface BakingTimeSlot {
    id: string;
    code: string | null;
    createdAt: Date | null;
    status: BakingProgramStatus | null;
    bakingTimeInMinutes: number | null;
    bakingTempInC: number | null;
    bakingProgrammedAt: Date
    bakingStartedAt: Date | null;
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