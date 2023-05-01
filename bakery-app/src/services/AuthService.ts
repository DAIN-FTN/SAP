import LoginRequest from "../models/Requests/Auth/LoginRequest";
import LoginResponse from "../models/Requests/Auth/LoginResponse";
import UserDetailsResponse from "../models/Responses/User/UserDetailsResponse";
import { getData, postData } from "./DataService";

export async function login(loginRequest: LoginRequest): Promise<LoginResponse> {
    try {
        const response = await postData<LoginResponse>(`/api/auth/login`, loginRequest);
        return response;
    } catch (error) {
        return null as unknown as LoginResponse;
    }
}

export async function getUserFromToken(): Promise<UserDetailsResponse> {
    try {
        const response = await getData<UserDetailsResponse>(`/api/auth/Me`);
        return response;
    } catch (error) {
        return null as unknown as UserDetailsResponse;
    }
}