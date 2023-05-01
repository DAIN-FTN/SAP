import { useContext } from "react";
import { AuthContext } from "../contexts/AuthContext";

export const useAuthContext = () => {
    const user = useContext(AuthContext);

    if (user === undefined) {
        throw new Error("useAuthContext can only be used inside AuthProvider");
    }
    
    return user;
};