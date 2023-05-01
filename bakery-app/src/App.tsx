import './App.css';
import Header from './components/Navigation/Header';
import styled from 'styled-components';
import NavMenu from './components/Navigation/NavMenu';
import { Outlet } from 'react-router-dom';

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
