import { useContext, useState } from "react";
import { AuthContext } from "../contexts/AuthContext";

export interface User {
    id: string;
    username: string;
    roleId: string;
    role: string;
    token: string;
  }
  
  export const useUser = () => {
    const [ user, setUser ] = useState<User | null>(null)
  
    const addUserInfo = (user: User) => {
        console.log('user in useUser, addUserInfo:', user);
        setUser(user);
        localStorage.setItem('sap-bakery-user', JSON.stringify(user));
    };
  
    const removeUserInfo = () => {
        setUser(null);
        localStorage.setItem('sap-bakery-user', '');
    };
  
    return { user, setUser, addUserInfo, removeUserInfo };
  };