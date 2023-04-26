import { useState } from "react";
import { FC } from "react";
import styled from "styled-components";
import AvailableProductsList from "../CreateOrderPage/AvailableProductsList";
import Button from '@mui/material/Button';
import TextField from "@mui/material/TextField";
import BakingTimeSlotsList from "./BakingTimeSlotsList";
import NewOrderProductsList from "./NewOrderProductsList";
import { getAvailable } from "../../services/BakingProgramService";
import AvailableBakingProgramResponse from "../../models/Responses/AvailableBakingProgramResponse";
import OrderProductRequest from "../../models/Requests/OrderProductRequest";
import { getProductStock } from "../../services/ProductService";
import ProductStockResponse from "../../models/Responses/ProductStockResponse";

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
    flex-wrap: wrap;
`;

const Panel = styled.div`
    width: 50%;
    padding: 24px;
    display: flex;
    flex-direction: column;
    box-sizing: border-box;
`;

const Label = styled.p`
    font-size: 24px;
`;

const SearchWrapper = styled.div`
    display: flex;
    flex-direction: row;
`;

const CreateOrderPage: FC = () => {
    const [productsOnStock, setProductsOnStock] = useState<ProductStockResponse[]>([]);
    const [orderProducts, setOrderProducts] = useState<OrderProductRequest[]>([]);
    const [bakingTimeSlots, setBakingTimeSlots] = useState<AvailableBakingProgramResponse[]>([]);

    function setDateTimeHandler(e: any) {
        let dateTime = new Date(e.target.value);

        console.log("setDateTimeHandler", dateTime);

        getAvailable(dateTime, orderProducts)
            .then((availableProgramsResponse) => {
                console.log('fetch finished for getAvailable() in CreateOrderPage');
                setBakingTimeSlots(availableProgramsResponse.bakingPrograms);
            });
    };

    function productNameSearchChangeHandler(e: { target: { value: string; }; }) {
        const productName = e.target.value;

        console.log(productName);

        if (!productName) {
            setProductsOnStock([]);
        } else {
            getProductStock(productName).then((products) => {
                console.log('fetch finished for fetchProductsBasicInfo() in CreateOrderPage');
                setProductsOnStock(products);
            });
        }
    };

    async function createOrderHandler() {
        

        console.log("new order request");

        // await createNewOrder(newOrderRequest);
    };

    return (
        <Container>
            <Panel>
                <Label>Search products in stock to add to order</Label>
                <SearchWrapper>
                    <TextField id="standard-basic" label="Product name" variant="standard" fullWidth sx={{ paddingRight: '16px' }}
                        onChange={productNameSearchChangeHandler} />
                    <Button variant="contained">Search</Button>

                </SearchWrapper>
                <Label>Products in stock</Label>
                <AvailableProductsList props={{ availableProducts: productsOnStock, orderProducts, setOrderProducts }} />
                <Label>Products for the new order</Label>
                <NewOrderProductsList props={{ products: orderProducts }} />
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
                <BakingTimeSlotsList props={{ bakingTimeSlots }} />
                <Button variant="contained" onClick={createOrderHandler}>Create order</Button>
            </Panel>
        </Container>
    );
};

export default CreateOrderPage;
