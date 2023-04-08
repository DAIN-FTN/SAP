import React from 'react';
import logo from './logo.svg';
import './App.css';
import Header from './components/Header';
import styled from 'styled-components';
import NavMenu from './components/NavMenu';
import CreateOrderPage from './components/CreateOrderPage/CreateOrderPage';
import { AdapterLuxon } from '@mui/x-date-pickers/AdapterLuxon';
import { LocalizationProvider } from '@mui/x-date-pickers/LocalizationProvider'
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
`;

function App() {
  return (
    <LocalizationProvider dateAdapter={AdapterLuxon}>
      <Container>
        <Header />
        <SecondRow>
          <NavMenu />
          <Outlet />
        </SecondRow>
      </Container>
    </LocalizationProvider>
  );
}

export default App;
