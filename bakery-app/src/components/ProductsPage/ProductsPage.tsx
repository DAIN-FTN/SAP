import { useState } from "react";
import { FC } from "react";
import styled from "styled-components";
import { ProductBasicInfo } from "../../models/ProductBasicInfo";
import { fetchProductsBasicInfo } from "../../services/OrderService";
import AvailableProductsList from "../CreateOrderPage/AvailableProductsList";
import Button from '@mui/material/Button';
import TextField from "@mui/material/TextField";
import { NewOrderRequest } from "../../models/NewOrderRequest";
import { BakingTimeSlot } from "../../models/BakingTimeSlot";
import { ProductDetails } from "../../models/ProductDetails";

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

const ProductsPage: FC = () => {
    const [productsOnStock, setProductsOnStock] = useState<ProductBasicInfo[] | []>([]);
    const [orderProducts, setOrderProducts] = useState<ProductBasicInfo[] | []>([]);
    const [bakingTimeSlots, setBakingTimeSlots] = useState<BakingTimeSlot[] | []>([]);
    const [newOrderRequest, setNewOrderRequest] = useState<NewOrderRequest | null>(null);
    const [product, setProduct] = useState<ProductDetails | null>(null);


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

    return (
        <Container>
            <Panel>
                <Label>Search products</Label>
                <SearchWrapper>
                    <TextField id="standard-basic" label="Product name" variant="standard" fullWidth sx={{ paddingRight: '16px' }} 
                    onChange={productNameSearchChangeHandler}/>
                    <Button variant="contained">Search</Button>

                </SearchWrapper>
                <Label>Products in stock</Label>
                <AvailableProductsList props={{availableProducts: productsOnStock, orderProducts, setOrderProducts}} />
                {/* <NewOrderProductsList props={{products: orderProducts}} /> */}
            </Panel>
            <Panel>
                <Label>Product details</Label>
                {/* <ProductDetailsView product={product}> */}
            </Panel>
        </Container>
    );
};

export default ProductsPage;
