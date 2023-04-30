import { useState } from "react";
import { FC } from "react";
import styled from "styled-components";
import BasicProductInfo from "./Models/BasicProductInfo";
import BasicProductInfoForm from "./BasicProductInfoForm";
import CreateStockedProductRequest from "../../../models/Requests/StockedProducts/CreateStockedProductRequest";
import { create } from "../../../services/ProductService";
import CreateProductResponse from "../../../models/Responses/Product/CreateProductResponse";
import CreateProductRequest from "../../../models/Requests/Products/CreateProductRequest";
import Button from "@mui/material/Button";
import StockForm from "./StockForm/StockForm";

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

const CreateProductPage: FC = () => {
    const [basicProductInfo, setBasicProductInfo] = useState<BasicProductInfo | null>(null);
    const [stockInfo, setStockInfo] = useState<CreateStockedProductRequest[] | null>(null);

    function createProduct() {
        if (!basicProductInfo || !stockInfo) {
            return;
        }

        const request: CreateProductRequest = {
            name: basicProductInfo.name,
            bakingTimeInMins: basicProductInfo.bakingTimeInMins,
            bakingTempInC: basicProductInfo.bakingTempInC,
            size: basicProductInfo.size,
            stock: stockInfo
        };

        console.log("Request: ", request);

        // create(request).then((response: CreateProductResponse) => {
        //     console.log(response);
        // });

    }

    return (
        <Container>
            <Panel>
                <BasicProductInfoForm setBasicProductInfo={setBasicProductInfo} />
            </Panel>
            <Panel>
                {basicProductInfo && <StockForm setStock={setStockInfo} />}
                {stockInfo && <Button variant="contained" sx={{ marginTop: '24px' }} onClick={createProduct}>Create product</Button>}
            </Panel>
        </Container>
    );
};

export default CreateProductPage;
