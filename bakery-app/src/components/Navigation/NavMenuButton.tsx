import { FC } from "react";
import { NavLink } from "react-router-dom";
import styled from "styled-components";
import HomeRoundedIcon from '@mui/icons-material/HomeRounded';
import ProductionQuantityLimitsRoundedIcon from '@mui/icons-material/ProductionQuantityLimitsRounded';
import Inventory2RoundedIcon from '@mui/icons-material/Inventory2Rounded';

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

    &:hover {
        cursor: pointer;
        background-color: #ce3939;
    }
`;

const LinkWrapper = styled(NavLink)`
    display: flex;
    flex-direction: column;
    width: 100%;
    background-color: #E27272;
    align-items: center;
    text-decoration: none;
    &.active{
        > div {
            background-color: #cc2a2a;
        }
    };
`;

export interface ButtonProps {
    to: string;
    name: string;
}

const NavMenuButton: FC<{ buttonProps: ButtonProps }> = ({ buttonProps }) => {
    let icon = null;
    switch (buttonProps.name) {
        case 'Home':
        icon = <HomeRoundedIcon />;
        break;
        case 'Order':
        icon = <ProductionQuantityLimitsRoundedIcon />;
        break;
        case 'Products':
        icon = <Inventory2RoundedIcon />;
        break;
        // Add other cases here for different icon types
        default:
        break;
    }

    return (
        <LinkWrapper to={buttonProps.to} >
            <StyledButton>
                {icon}
                {buttonProps.name}
            </StyledButton>
        </LinkWrapper>
    );
};

export default NavMenuButton;
