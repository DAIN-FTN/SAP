import React from "react";
import { FC } from "react";
import styled from "styled-components";
import Button from "./Button";

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
            <Button buttonProps={{name: "Home Page"}} />
            <Button buttonProps={{name: "Create New Order"}} />
        </Container>
    );
};

export default NavMenu;
