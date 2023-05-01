import './App.css';
import Header from './components/Navigation/Header';
import styled from 'styled-components';
import NavMenu from './components/Navigation/NavMenu';
import { AdapterLuxon } from '@mui/x-date-pickers/AdapterLuxon';
import { LocalizationProvider } from '@mui/x-date-pickers/LocalizationProvider'
import { Outlet } from 'react-router-dom';
import { AuthContext } from './contexts/AuthContext';
import { useAuth } from './hooks/useAuth';
import { User } from './hooks/useUser';
import { useEffect } from 'react';
import { AuthProvider } from './AuthProvider';

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
  const { user, setUser } = useAuth();

  useEffect(() => {
    console.log('user in App useEffect: ',user);
  }, [user]);

  /*const userConst: User = {
    id:"00000000-0000-0000-0000-000000000002",
    roleId:"00000000-0000-0000-0000-000000000002",
    role:"Staff",
    token:"eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJqdGkiOiI3OTU0OTIzNS03ZTQ3LTRkNGEtOGQ5ZC01MjVjN2Y3NTM0MmEiLCJzdWIiOiIwMDAwMDAwMC0wMDAwLTAwMDAtMDAwMC0wMDAwMDAwMDAwMDIiLCJyb2xlIjoiU3RhZmYiLCJleHAiOjE2ODI5NDgzMTQsImlzcyI6Imh0dHA6Ly9sb2NhbGhvc3Q6NDQyMDAiLCJhdWQiOiJodHRwOi8vbG9jYWxob3N0OjQ0MjAwIn0.Q6bb_dKh4IrEifAht30AONOlTqyJn2eX8yQe4jMiol8",
    username:"AleksandarStaff"
  };*/

  return (
    <AuthProvider>
      <LocalizationProvider dateAdapter={AdapterLuxon}>
        <Container>
          <Header />
          <SecondRow>
            <NavMenu />
            <Outlet />
          </SecondRow>
        </Container>
      </LocalizationProvider>
    </AuthProvider>
  );
}

export default App;
