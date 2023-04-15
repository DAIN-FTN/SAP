import { FC } from "react";
import styled from "styled-components";
import NavMenuButton from "./NavMenuButton";
import AccountCircleIcon from '@mui/icons-material/AccountCircle';

const Container = styled.div`
    display: flex;
    justify-content: space-between;
    flex-direction: column;
    height: 100%;
    background-color: #E27272;
    align-items: center;
    width: 150px;
`;

const NavLinks = styled.div`
    width: 100%;
    display: flex;
    flex-direction: column;
`;

const BottomNavigation = styled.div`
    width: 100%;
    display: flex;
    flex-direction: column;
    align-items: center;
    padding-bottom: 30px;
    background-color: #DC3F3F;
    position: relative;

    &:hover div {
        height: 40px;
        max-height: fit-content;
    }
`;

const UsernameLabel = styled.p`
    margin-top: 30px;
    color: #fff;
    text-align: center;
`;

const HiddenLogoutContent = styled.div`
    width: 150px;
    flex-direction: column;
    align-items: center;
    height: 0px;
    overflow: hidden;
    transition: height 0.2s ease-in-out;
`;

const NavMenu: FC = () => {
    return (
        <Container>
            <NavLinks>
                <NavMenuButton buttonProps={{to: 'home', name: 'Home'}}></NavMenuButton>
                <NavMenuButton buttonProps={{to: 'order', name: 'Order'}}></NavMenuButton>
                <NavMenuButton buttonProps={{to: 'products', name: 'Products'}}></NavMenuButton>
            </NavLinks>
            <BottomNavigation>
                <AccountCircleIcon sx={{color: '#fff', fontSize: '45px', position: 'absolute', top: '-23px', backgroundColor: '#DC3F3F', borderRadius: '50px'}} />
                <UsernameLabel>John Smith</UsernameLabel>
                <HiddenLogoutContent>
                    <NavMenuButton buttonProps={{to: 'logout', name: 'Logout'}}></NavMenuButton>
                </HiddenLogoutContent>
            </BottomNavigation>
        </Container>
    );
};

export default NavMenu;
