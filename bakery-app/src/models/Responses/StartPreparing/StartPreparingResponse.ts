import LocationToPrepareFromResponse from "./LocationToPrepareFromResponse";

export default interface StartPreparingResponse {
    id: string;
    code: string;
    bakingTimeInMins: number;
    bakingTempInC: number;
    bakingProgrammedAt: Date
    ovenId: string;
    ovenCode: string;
    locations: LocationToPrepareFromResponse[];
}
