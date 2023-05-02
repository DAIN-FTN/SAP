import { useContext, useEffect } from "react";
import LoginRequest from "../models/Requests/Auth/LoginRequest";
import LoginResponse from "../models/Requests/Auth/LoginResponse";
import { getUserFromToken, postLogin } from "../services/AuthService";
import UserDetailsResponse from "../models/Responses/User/UserDetailsResponse";
import { AuthContext, User } from "../contexts/AuthContext";

export const useAuthContext = () => {
    const { user, setUser } = useContext(AuthContext);

    if (user === undefined) {
        throw new Error("useAuthContext can only be used inside AuthProvider");
    }

    useEffect(() => {
        const user = localStorage.getItem('sap-bakery-user');
        if (user) {
            setUser(JSON.parse(user));
        } else {
            setUser(null);
        }
    }, []);


    const login = async (loginRequest: LoginRequest): Promise<User | null> => {
        const response: LoginResponse = await postLogin(loginRequest);

        if (response) {
            const token = response.token;
            const userDetails = await getUserFromToken(token);

            if (!userDetails) throw Error('User details not found');

            const loggedInUser: User = {
                id: userDetails.id,
                roleId: userDetails.roleId,
                role: userDetails.role,
                token: token,
                username: userDetails.username
            }
            addUserInfo(loggedInUser);
            return loggedInUser;
        } else {
            removeUserInfo();
        }
        return user;
    };

    const logout = () => {
        removeUserInfo();
    };

    const addUserInfo = (user: User) => {
        console.log('user in useUser, addUserInfo:', user);
        setUser(user);
        localStorage.setItem('sap-bakery-user', JSON.stringify(user));
    };

    const removeUserInfo = () => {
        setUser(null);
        localStorage.removeItem('sap-bakery-user');
    };

    return { user, login, logout };
};