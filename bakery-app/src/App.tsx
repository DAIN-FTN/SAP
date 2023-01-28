import React from 'react';
import logo from './logo.svg';
import './App.css';
import Header from './components/Header';
import styled from 'styled-components';
import NavMenu from './components/NavMenu';
import CreateOrderPage from './components/CreateOrderPage';

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
    <Container>
      <Header/>
      <SecondRow>
        <NavMenu />
        <CreateOrderPage />
      </SecondRow>
    </Container>
  );
}

export default App;
