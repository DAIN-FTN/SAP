import { useState, useEffect, ReactNode } from 'react';
import { AuthContext, User } from './contexts/AuthContext';

interface AuthProviderProps {
    children: ReactNode;
}



export const AuthProvider = ({ children }: AuthProviderProps) => {
    const [user, setUser] = useState<User | null>(null);

    useEffect(() => {
        const user = localStorage.getItem('sap-bakery-user');
        if (user) {
            setUser(JSON.parse(user));
        }
    }, []);

    return (
        <AuthContext.Provider value={{ user, setUser }}>{children}</AuthContext.Provider>
    );
};