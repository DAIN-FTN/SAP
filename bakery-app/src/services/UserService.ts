import RegisterRequest from "../models/Requests/Users/RegisterRequest";
import UpdateUserRequest from "../models/Requests/Users/UpdateUserRequest";
import RegisterResponse from "../models/Responses/User/RegisterResponse";
import UpdateUserResponse from "../models/Responses/User/UpdateUserResponse";
import UserDetailsResponse from "../models/Responses/User/UserDetailsResponse";
import UserResponse from "../models/Responses/User/UserResponse";
import { getData, postData, putData } from "./DataService";

export async function getAll(): Promise<UserResponse[]> {
    const response = await getData<UserResponse[]>(`/api/users`);
    return response;
}
export async function getDetails(userId: string): Promise<UserDetailsResponse | null> {
    try {
        const response = await getData<UserDetailsResponse>(`/api/users/${userId}`);
        return response;
    } catch (error) {
        return null as unknown as UserDetailsResponse;
    }
}

export async function create(createRequest: RegisterRequest): Promise<RegisterResponse> {
    return await postData<RegisterResponse>(`/api/users`, createRequest);
}

export async function update(userId: string, updateRequest: UpdateUserRequest): Promise<UpdateUserResponse> {
    return await putData<UpdateUserResponse>(`/api/products/${userId}`, updateRequest);
}

