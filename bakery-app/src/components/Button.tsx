import React from "react";
import { FC } from "react";
import styled from "styled-components";

const Container = styled.div`
    background-color: #DC3F3F;
    width: 100px;
    height: 100px;
    margin: 8px;
    padding: 8px;
    text-align: center;
    display: flex;
    align-items: center;
    justify-content: center;
`;

const Name = styled.span`
    font-size: 20px;
    text-align: center;
    max-width: 90%;
`;

export interface ButtonProps {
    name: string;
}

const Button: FC<{ buttonProps: ButtonProps }> = ({ buttonProps }) => {
    return (
        <Container>
            <Name>{buttonProps.name}</Name>
        </Container>
    );
};

export default Button;
