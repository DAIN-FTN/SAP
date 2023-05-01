import PreparedBakingProgramResponse from "./PreparedBakingProgramResponse";

export default interface UserDetailsResponse {
    id: string;
    username: string;
    password: string;
    roleId: string;
    preparedPrograms: PreparedBakingProgramResponse[];
}
