import LoginRequest from "../models/Requests/Auth/LoginRequest";
import LoginResponse from "../models/Requests/Auth/LoginResponse";
import UserDetailsResponse from "../models/Responses/User/UserDetailsResponse";
import { getData, postData } from "./DataService";

export async function postLogin(loginRequest: LoginRequest): Promise<LoginResponse> {
    try {
        const response = await postData<LoginResponse>(`/api/auth/login`, loginRequest);
        return response;
    } catch (error) {
        console.error("Unable to login", error);
        return null as unknown as LoginResponse;
    }
}

export async function getUserFromToken(token: string): Promise<UserDetailsResponse | null> {
    try {
        return await getData<UserDetailsResponse>(`/api/auth/Me`, token);
    } catch (error) {
        console.error("Unable to get user from token", error);
        return null as unknown as UserDetailsResponse;
    }
}