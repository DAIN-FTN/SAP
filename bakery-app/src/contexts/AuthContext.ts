import { createContext } from "react";

export interface User {
    id: string;
    username: string;
    roleId: string;
    role: string;
    token: string;
}

interface AuthContextFields {
    user: User | null;
    setUser: (user: User | null) => void;
}

export const AuthContext = createContext<AuthContextFields>({
    user: null,
    setUser: () => { },
});