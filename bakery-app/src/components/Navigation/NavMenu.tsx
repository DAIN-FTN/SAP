import { FC, useEffect } from "react";
import styled from "styled-components";
import NavMenuButton from "./NavMenuButton";
import AccountCircleIcon from '@mui/icons-material/AccountCircle';
import HomeRoundedIcon from '@mui/icons-material/HomeRounded';
import ProductionQuantityLimitsRoundedIcon from '@mui/icons-material/ProductionQuantityLimitsRounded';
import Inventory2RoundedIcon from '@mui/icons-material/Inventory2Rounded';
import BakeryDiningRoundedIcon from '@mui/icons-material/BakeryDiningRounded';
import PeopleIcon from '@mui/icons-material/People';
import { ReceiptLongRounded } from "@mui/icons-material";
import { useAuthContext } from "../../hooks/useAuthContext";
import { useNavigate } from "react-router-dom";

const StyledButton = styled.div`
    display: flex;
    flex-direction: column;
    background-color: #DC3F3F;
    border: none;
    width: 100%;
    padding: 10px;
    color: #fff;
    box-sizing: border-box;
    align-items: center;
    transition: background-color 0.1s ease-in-out;
    text-align: center;

    &:hover {
        cursor: pointer;
        background-color: #ce3939;
    }
`;

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
        opacity: 1;
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
    opacity: 0;
    overflow: hidden;
    transition: all 0.2s ease-in-out;
`;

const NavMenu: FC = () => {
    const { user, logout } = useAuthContext();
    const navigate = useNavigate();

    useEffect(() => {
        console.log("useEffect in NavMenu, user: ", user);
        if(!user)
            navigate('/login');
    }, [user]);
     
    function handleLogout(){
        logout();
    }

    return (
        <Container>
            <NavLinks>
                <NavMenuButton to='home' name='Home' icon={<HomeRoundedIcon />} />
                <NavMenuButton to='order/view' name='View orders' icon={<ReceiptLongRounded />} />
                <NavMenuButton to='order/create' name='Create new order' icon={<ProductionQuantityLimitsRoundedIcon />} />
                <NavMenuButton to='products' name='Products' icon={<Inventory2RoundedIcon />} />
                { user?.role == "Admin" && <NavMenuButton to='products/create' name='Create new product' icon={<BakeryDiningRoundedIcon />} /> }
                { user?.role == "Admin" && <NavMenuButton to='users' name='Users' icon={<PeopleIcon />} /> }
            </NavLinks>
            <BottomNavigation>
                <AccountCircleIcon sx={{ color: '#fff', fontSize: '45px', position: 'absolute', top: '-23px', backgroundColor: '#DC3F3F', borderRadius: '50px' }} />
                <UsernameLabel>{user?.username}</UsernameLabel>
                <HiddenLogoutContent>
                    <StyledButton onClick={handleLogout}>Logout</StyledButton>
                </HiddenLogoutContent>
            </BottomNavigation>
        </Container>
    );
};

export default NavMenu;
