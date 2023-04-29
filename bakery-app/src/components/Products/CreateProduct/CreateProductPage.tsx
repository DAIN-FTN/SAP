import { useState } from "react";
import { FC } from "react";
import styled from "styled-components";
import Button from '@mui/material/Button';
import TextField from "@mui/material/TextField";
import { getProductStock } from "../../../services/ProductService";
import ProductStockResponse from "../../../models/Responses/ProductStockResponse";
import BasicProductInfo from "./Models/BasicProductInfo";
import BasicProductInfoForm from "./BasicProductInfoForm";

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

const CreateProductPage: FC = () => {
    const [basicProductInfo, setBasicProductInfo] = useState<BasicProductInfo | null>(null);
    const [selectedProductId, setSelectedProductId] = useState<string | null>(null);

    return (
        <Container>
            <Panel>
                <BasicProductInfoForm />
            </Panel>
            <Panel>
                {/* <ProductDetailsView productId={selectedProductId} /> */}
            </Panel>
        </Container>
    );
};

export default CreateProductPage;
