import { FC } from "react";
import { NavLink } from "react-router-dom";
import styled from "styled-components";

export interface ButtonProps {
    to: string;
    name: string;
    icon: React.ReactNode;
}

export const StyledButton = styled.div`
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

const NavMenuButton: FC<ButtonProps> = ({ name, to, icon }) => {
    
    return (
        <LinkWrapper to={to} >
            <StyledButton>
                {icon}
                {name}
            </StyledButton>
        </LinkWrapper>
    );
};

export default NavMenuButton;
