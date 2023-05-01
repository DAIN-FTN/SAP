import { useEffect, useContext } from "react";
import { User, useUser } from "./useUser";
import LoginRequest from "../models/Requests/Auth/LoginRequest";
import LoginResponse from "../models/Requests/Auth/LoginResponse";
import { getUserFromToken, postLogin } from "../services/AuthService";
import UserDetailsResponse from "../models/Responses/User/UserDetailsResponse";
import { AuthContext } from "../contexts/AuthContext";

export const useAuth = () => {
    const { addUserInfo, removeUserInfo } = useUser();
    const {user, setUser} = useContext(AuthContext)
  
    useEffect(() => {
      const user = localStorage.getItem('sap-bakery-user');
      console.log('useEffect in useAuth');
      console.log(user);
      if (user) {
        addUserInfo(JSON.parse(user));
      }
      console.log('user in useAuth:', user);
    }, []);
  
    const login = (loginRequest: LoginRequest) => {
        postLogin(loginRequest).then((response: LoginResponse | null) => {
            if(response){
                const token = response.token;
                getUserFromToken(token).then((user: UserDetailsResponse) => {
                    const loggedInUser: User = {
                         id: user.id,
                         roleId: user.roleId,
                         role: user.role,
                         token: token,
                         username: user.username
                    }
                    addUserInfo(loggedInUser);
                });
            }else{
                removeUserInfo();
            }
        });
    };
  
    const logout = () => {
        removeUserInfo();
    };
  
    return { user, setUser, login, logout };
  };