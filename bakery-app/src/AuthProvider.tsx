import { useState, useEffect, ReactNode } from 'react';
// import { getUser } from './auth.js'

import { User } from './hooks/useUser';
import { AuthContext } from './contexts/AuthContext';

interface AuthProviderProps {
    children: ReactNode;
}

export const AuthProvider = ({ children }: AuthProviderProps) => {
    const [user, setUser] = useState<User | null>(null);

    const userConst: User = {
        id: "00000000-0000-0000-0000-000000000002",
        roleId: "00000000-0000-0000-0000-000000000002",
        role: "Staff",
        token: "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJqdGkiOiI3OTU0OTIzNS03ZTQ3LTRkNGEtOGQ5ZC01MjVjN2Y3NTM0MmEiLCJzdWIiOiIwMDAwMDAwMC0wMDAwLTAwMDAtMDAwMC0wMDAwMDAwMDAwMDIiLCJyb2xlIjoiU3RhZmYiLCJleHAiOjE2ODI5NDgzMTQsImlzcyI6Imh0dHA6Ly9sb2NhbGhvc3Q6NDQyMDAiLCJhdWQiOiJodHRwOi8vbG9jYWxob3N0OjQ0MjAwIn0.Q6bb_dKh4IrEifAht30AONOlTqyJn2eX8yQe4jMiol8",
        username: "AleksandarStaff"
    };

    useEffect(() => {
        const currentUser = userConst;
        setUser(currentUser)
    }, []);

    return (
        <AuthContext.Provider value={{ user, setUser }}>{children}</AuthContext.Provider>
    );
};