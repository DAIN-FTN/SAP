import { useState } from "react";
import { FC } from "react";
import styled from "styled-components";
import BasicProductInfo from "./Models/BasicProductInfo";

export interface BasicProductInfoProps {
}

const Container = styled.div`
    width: 100%;
    padding: 36px;
    display: flex;
    flex-direction: row;
    -webkit-box-shadow: 0px 2px 14px 0px rgba(122,122,122,1);
    -moz-box-shadow: 0px 2px 14px 0px rgba(122,122,122,1);
    box-shadow: 0px 2px 14px 0px rgba(122,122,122,1);
    background-color: white;
    margin: 48px;
`;

const Panel = styled.div`
    width: 50%;
    padding: 24px;
    display: flex;
    flex-direction: column;
    
`;

const Label = styled.p`
    font-size: 24px;
`;

const SearchWrapper = styled.div`
    display: flex;
    flex-direction: row;
`;

const BasicProductInfoForm: FC<BasicProductInfoProps> = ({}) => {
    const [basicProductInfo, setBasicProductInfo] = useState<BasicProductInfo | null>(null);
    const [selectedProductId, setSelectedProductId] = useState<string | null>(null);

    return (
        <Container>
            123
        </Container>
    );
};

export default BasicProductInfoForm;
