import { getData, putDataWithoutResponse } from "./DataService";
import { BakingTimeSlot as BakingProgram } from "../models/BakingTimeSlot";
import AllBakingPrograms from "../models/AllBakingPrograms";
import { StartPreparing } from "../models/Responses/StartPreparing";

export async function fetchAllBakingPrograms(): Promise<AllBakingPrograms> {
    const response = await getData<AllBakingPrograms>(`/api/baking-programs`);
    return mapAllBakingPrograms(response);
}

export async function startPreparingBakingProgram(id: string): Promise<BakingProgram> {
    return await getData<BakingProgram>(`/api/baking-programs/start-preparing/${id}`);
}

export async function cancellBakingProgram(id: string) {
    return await putDataWithoutResponse(`/api/baking-programs/cancell-preparing/${id}`);
}

export async function finishPreparingBakingProgram(id: string) {
    return await putDataWithoutResponse(`/api/baking-programs/finish-preparing/${id}`);
}

export async function startBakingBakingProgram(id: string) {
    return await putDataWithoutResponse(`/api/baking-programs/start-baking/${id}`);
}

function mapAllBakingPrograms(allBakingPrograms: AllBakingPrograms): AllBakingPrograms {
    return {
        preparingInProgress: mapPreparingInProgress(allBakingPrograms.preparingInProgress),
        prepareForOven: mapBakingPrograms(allBakingPrograms.prepareForOven),
        preparingAndPrepared: mapBakingPrograms(allBakingPrograms.preparingAndPrepared),
        baking: mapBakingPrograms(allBakingPrograms.baking),
        done: mapBakingPrograms(allBakingPrograms.done),
    }

    function mapPreparingInProgress(preparingInProgress: StartPreparing): StartPreparing {
        return {
            ...preparingInProgress,
            bakingProgrammedAt: new Date(preparingInProgress.bakingProgrammedAt)
        };
    }

    function mapBakingPrograms(bakingPrograms: BakingProgram[]): BakingProgram[] {
        return bakingPrograms.map((bakingProgram) => ({
            ...bakingProgram,
            createdAt: new Date(bakingProgram.createdAt),
            bakingProgrammedAt: new Date(bakingProgram.bakingProgrammedAt),
            canBePreparedAt: new Date(bakingProgram.canBePreparedAt),
            canBeBakedAt: new Date(bakingProgram.canBeBakedAt),
            bakingStartedAt: bakingProgram.bakingStartedAt ? new Date(bakingProgram.bakingStartedAt) : null,
        }))
    }
}