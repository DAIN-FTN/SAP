import React from "react";
import { FC } from "react";
import styled from "styled-components";
import Button from '@mui/material/Button';

const Container = styled.div`
    display: flex;
    flex-direction: column;
    width: 150px;
    height: 100%;
    background-color: #E27272;
    align-items: center;
`;

const NavMenu: FC = () => {
    return (
        <Container>
            <Button variant="contained" sx={{ backgroundColor: '#DC3F3F', margin: '8px' }}>Home Page</Button>
            <Button variant="contained" sx={{ backgroundColor: '#DC3F3F', margin: '8px' }}>Create new order</Button>
            
        </Container>
    );
};

export default NavMenu;
