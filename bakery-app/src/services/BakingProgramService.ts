import { getData, putDataWithoutResponse } from "./DataService";
import AllBakingPrograms from "../models/Responses/AllBakingProgramsResponse";
import StartPreparingResponse from "../models/Responses/StartPreparing/StartPreparingResponse";
import AllBakingProgramsResponse from "../models/Responses/AllBakingProgramsResponse";
import BakingProgramResponse from "../models/Responses/BakingProgramResponse";

export async function fetchAllBakingPrograms(): Promise<AllBakingProgramsResponse> {
    const response = await getData<AllBakingProgramsResponse>(`/api/baking-programs`);
    return mapAllBakingPrograms(response);
}

export async function startPreparingBakingProgram(id: string): Promise<StartPreparingResponse> {
    return await getData<StartPreparingResponse>(`/api/baking-programs/start-preparing/${id}`);
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

    function mapPreparingInProgress(preparingInProgress: StartPreparingResponse | null): StartPreparingResponse | null{
        if (preparingInProgress === null) return null;    

        return {
            ...preparingInProgress,
            bakingProgrammedAt: new Date(preparingInProgress.bakingProgrammedAt)
        };
    }

    function mapBakingPrograms(bakingPrograms: BakingProgramResponse[]): BakingProgramResponse[] {
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