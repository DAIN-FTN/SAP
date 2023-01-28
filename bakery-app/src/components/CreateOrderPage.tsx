import React, { useEffect, useState } from "react";
import { FC } from "react";
import styled from "styled-components";
import { ProductBasicInfo } from "../models/ProductBasicInfo";
import { fetchProductsBasicInfo } from "../services/OrderService";
import AvailableProductsList, { AvailableProductsListProps } from "./AvailableProductsList";
import Button from "./Button";

const Container = styled.div`
    background-color: #e2e2e2;
    width: 100%;
    padding: 36px;
    display: flex;
    flex-direction: row;
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

const SearchField = styled.input`
    font-size: 24px;
    width: 80%;
`;

const CreateOrderPage: FC = () => {
    const products: AvailableProductsListProps = {
        products: [
            { id: "1", name: "Product 1", availableQuantity: 10 },
            { id: "2", name: "Product 2", availableQuantity: 20 },
            { id: "3", name: "Product 3", availableQuantity: 30 }
        ]
    };

    const [productsOnStock, setProductsOnStock] = useState<ProductBasicInfo[] | []>([
        { id: "1", name: "Product 1", availableQuantity: 10 },
        { id: "2", name: "Product 2", availableQuantity: 20 },
        { id: "3", name: "Product 3", availableQuantity: 30 }
    ]);

    useEffect(() => {
        fetchProductsBasicInfo("Croissant")
        .then((products) => {
            setProductsOnStock(products);
        });
    }, []);
    

    return (
        <Container>
            <Panel>
                <Label>Search products in stock to add to order</Label>
                <SearchWrapper>
                    <SearchField type="text" placeholder="Search products"></SearchField>
                    <Button buttonProps={{name: "Search"}}></Button>
                </SearchWrapper>
                <Label>List of products in stock</Label>
                <AvailableProductsList props={{products: productsOnStock}} />
                <Label>Products in order</Label>
                <AvailableProductsList props={products} />
            </Panel>
            <Panel>
                <Label>Order delivery date and time</Label>
                <Label>Available baking time slots</Label>
                <Button buttonProps={{name: "Create order"}}></Button>
            </Panel>
        </Container>
    );
};

export default CreateOrderPage;
