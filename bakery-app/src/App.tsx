import './App.css';
import Header from './components/Navigation/Header';
import styled from 'styled-components';
import NavMenu from './components/Navigation/NavMenu';
import { Outlet, useNavigate } from 'react-router-dom';
import { useAuthContext } from './hooks/useAuthContext';
import { useEffect } from 'react';

const Container = styled.div`
    display: flex;
    flex-direction: column;
    height: 100vh;
`;

const SecondRow = styled.div`
    display: flex;
    flex-direction: row;
    height: 100%;
    /* background-image: url('./bakery-pattern.jpg'); */
`;

function App() {
    const { logout } = useAuthContext();
    const navigate = useNavigate();

    useEffect(() => {
        window.addEventListener("unauthorized", () => {
            logout();
            navigate("/login");
        });
    }, []);

    return (
        <Container>
            <Header />
            <SecondRow>
                <NavMenu />
                <Outlet />
            </SecondRow>
        </Container>
    );
}

export default App;
