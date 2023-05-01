import { createContext } from "react";
import { User } from "../hooks/useUser";

interface AuthContextFields {
    user: User | null;
    setUser: (user: User | null) => void;
  }
  
  export const AuthContext = createContext<AuthContextFields>({
    user: null,
    setUser: (x: any) => {
        console.log("setttting user", x)
    },
  });