import RoleResponse from "../models/Responses/Role/RoleResponse";
import { getData } from "./DataService";

export async function getAll(): Promise<RoleResponse[]> {
    const response = await getData<RoleResponse[]>(`/api/roles`);
    return response;
}