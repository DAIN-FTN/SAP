import React, { useEffect, useState } from "react";
import { FC } from "react";
import styled from "styled-components";
import { ProductBasicInfo } from "../models/ProductBasicInfo";
import { fetchBakingTimeSlots, fetchProductsBasicInfo } from "../services/OrderService";
import AvailableProductsList, { AvailableProductsListProps } from "./AvailableProductsList";
import Button from '@mui/material/Button';
import TextField from "@mui/material/TextField";
import { NewOrderRequest } from "../models/NewOrderRequest";
import { BakingTimeSlot } from "../models/BakingTimeSlot";
import BakingTimeSlotsList, { BakingTimeSlotsListProps } from "./BakingTimeSlotsList";
import { DateTimePicker } from "@mui/x-date-pickers/DateTimePicker";
import { stringify } from "querystring";
import NewOrderProductsList from "./NewOrderProductsList";

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
    const [productsOnStock, setProductsOnStock] = useState<ProductBasicInfo[] | []>([]);
    const [orderProducts, setOrderProducts] = useState<ProductBasicInfo[] | []>([]);
    const [bakingTimeSlots, setBakingTimeSlots] = useState<BakingTimeSlot[] | []>([]);
    const [newOrderRequest, setNewOrderRequest] = useState<NewOrderRequest | null>(null);

    let productsForNewOrderRequest: ProductBasicInfo[] = [];
    // let newOrderRequest: NewOrderRequest = {
    //     products: productsForNewOrderRequest,
    //     bakingProgramId: null,
    // };
    // let bakingTimeSlots: BakingTimeSlotsListProps = {
    //     bakingTimeSlots: [],
    // };

    // let productsOnStock: AvailableProductsListProps = {
    //     products: []
    // };

    // useEffect(() => {
    //     console.log('fetchProductsBasicInfo() in CreateOrderPage');
    //     fetchProductsBasicInfo("Croissant")
    //         .then((products) => {
    //             console.log('fetch finished for fetchProductsBasicInfo() in CreateOrderPage');
    //             // productsOnStock = { products: products };
    //             setProductsOnStock(products);
    //         });
    // }, []);

    const setDateTimeHandler = (e: any) => {
        let dateTime = new Date(e.target.value);
        console.log("setDateTimeHandler", dateTime);
        fetchBakingTimeSlots(dateTime, productsForNewOrderRequest).then((bakingTimeSlots) => {
            console.log('fetch finished for fetchBakingTimeSlots() in CreateOrderPage');
            setBakingTimeSlots(bakingTimeSlots);
        });
    };

    const productNameSearchChangeHandler = (e: { target: { value: string; }; }) => {
        const productName = e.target.value;
        console.log(productName);
        if (!productName) {
            setProductsOnStock([]);
        } else {
            fetchProductsBasicInfo(productName).then((products) => {
                console.log('fetch finished for fetchProductsBasicInfo() in CreateOrderPage');
                setProductsOnStock(products);
            });
        }
    };

    const createOrderHandler = () => {
        console.log(newOrderRequest);

    };

    console.log('rendered CreateOrderPage');

    return (
        <Container>
            <Panel>
                <Label>Search products in stock to add to order</Label>
                <SearchWrapper>
                    <TextField id="standard-basic" label="Product name" variant="standard" fullWidth sx={{ paddingRight: '16px' }} 
                    onChange={productNameSearchChangeHandler}/>
                    <Button variant="contained">Search</Button>

                </SearchWrapper>
                <Label>Products in stock</Label>
                <AvailableProductsList props={{availableProducts: productsOnStock, orderProducts, setOrderProducts}} />
                <Label>Products for the new order</Label>
                <NewOrderProductsList props={{products: orderProducts}} />
            </Panel>
            <Panel>
                <Label>Order delivery date and time</Label>
                <TextField
                    id="datetime-local"
                    type="datetime-local"
                    defaultValue={new Date().toISOString().slice(0, 16)}
                    sx={{ width: 250 }}
                    InputLabelProps={{
                        shrink: true,
                    }}
                    onChange={(e) => setDateTimeHandler(e)}
                />
                <Label>Available baking time slots</Label>
                <BakingTimeSlotsList props={{bakingTimeSlots}} />
                <Button variant="contained" onClick={createOrderHandler}>Create order</Button>
            </Panel>
        </Container>
    );
};

export default CreateOrderPage;
