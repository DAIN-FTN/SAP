import { useState, useEffect, ReactNode, FC } from 'react';
import { AuthContext, User } from './contexts/AuthContext';

interface AuthProviderProps {
    children: ReactNode;
}

const AuthProvider: FC<AuthProviderProps> = ({ children }) => {
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

export default AuthProvider;