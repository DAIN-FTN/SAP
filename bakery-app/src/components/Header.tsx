import React from "react";
import { FC } from "react";
import styled from "styled-components";

const Container = styled.div`
    background-color: #DC3F3F;
    height: 50px;
    padding: 16px;
`;

const Title = styled.p`
    font-size: 24px;
`;

const Header: FC = () => {
    return (
        <Container>
            <Title>Bakery</Title>
        </Container>
    );
};

export default Header;
