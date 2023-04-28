import { useState } from "react";
import { FC } from "react";
import styled from "styled-components";
import ProductSearch from "./ProductSearch";
import Button from '@mui/material/Button';
import OrderProductsList from "./OrderProductsList";
import AvailableBakingProgramResponse from "../../../models/Responses/AvailableBakingProgramResponse";
import OrderProductRequest from "../../../models/Requests/OrderProductRequest";
import ProductStockResponse from "../../../models/Responses/ProductStockResponse";

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

const CreateOrderPage: FC = () => {
    const [productsOnStock, setProductsOnStock] = useState<ProductStockResponse[]>([]);
    const [orderProducts, setOrderProducts] = useState<OrderProductRequest[]>([]);
    const [bakingTimeSlots, setBakingTimeSlots] = useState<AvailableBakingProgramResponse[]>([]);

    
    

    async function createOrderHandler() {
        

        console.log("new order request");

        // await createNewOrder(newOrderRequest);
    };

    return (
        <Container>
            <Panel>
                <ProductSearch requestedQuantityChangeHandler={() => console.log} />
                <OrderProductsList props={{ products: orderProducts }} />
            </Panel>
            <Panel>
                {/* <Label>Order delivery date and time</Label>
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
                <Label>Available baking time slots</Label> */}
                {/* <BakingTimeSlotsList props={{ bakingTimeSlots }} />  */}
                <Button variant="contained" onClick={createOrderHandler}>Create order</Button>
            </Panel>
        </Container>
    );
};

export default CreateOrderPage;
