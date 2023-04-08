import { FC } from "react";
import styled from "styled-components";
import NavMenuButton from "./NavMenuButton";

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
            <NavMenuButton buttonProps={{to: 'home', name: 'Home'}}></NavMenuButton>
            <NavMenuButton buttonProps={{to: 'order', name: 'Order'}}></NavMenuButton>
            <NavMenuButton buttonProps={{to: 'products', name: 'Products'}}></NavMenuButton>
        </Container>
    );
};

export default NavMenu;
