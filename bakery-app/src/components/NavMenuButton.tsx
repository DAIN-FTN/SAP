import React from "react";
import { FC } from "react";
import { Link } from "react-router-dom";
import styled from "styled-components";

const StyledButton = styled.button`
    background-color: #DC3F3F;
    border: none;
    width: 100%;
    padding: 8px;
    &:hover {
        cursor: pointer;
        background-color: #cc2a2a;
    }
`;

const Name = styled.span`
    font-size: 20px;
    text-align: center;
    max-width: 90%;
`;

const LinkWrapper = styled(Link)`
    display: flex;
    flex-direction: column;
    width: 100%;
    background-color: #E27272;
    align-items: center;
    text-decoration: none;
`;

export interface ButtonProps {
    to: string;
    name: string;
}

const NavMenuButton: FC<{ buttonProps: ButtonProps }> = ({ buttonProps }) => {
    return (
        <LinkWrapper to={buttonProps.to}>
            <StyledButton>
                {buttonProps.name}
            </StyledButton>
        </LinkWrapper>
    );
};

export default NavMenuButton;
