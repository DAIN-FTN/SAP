import { useEffect, useState } from "react";
import { FC } from "react";
import styled from "styled-components";
import Button from '@mui/material/Button';
import TextField from "@mui/material/TextField";
import ProductsInStockList from "./ProductsInStockList";
import ProductDetailsView from "./ProductDetailsView";
import { getProductStock } from "../../../services/ProductService";
import ProductStockResponse from "../../../models/Responses/ProductStockResponse";
import { useParams } from "react-router-dom";

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
    const { productId } = useParams();
    const [productsOnStock, setProductsOnStock] = useState<ProductStockResponse[] | []>([]);
    const [selectedProductId, setSelectedProductId] = useState<string | null>(null);

    useEffect(() => {
        if (!productId) return;
        setSelectedProductId(productId);
    }, [productId]);

    const productNameSearchChangeHandler = (e: { target: { value: string; }; }) => {
        const productName = e.target.value;
        if (!productName) {
            setProductsOnStock([]);
            setSelectedProductId(null);
        } else {
            getProductStock(productName).then((products) => {
                if (!products) throw new Error('No products found');

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
                    <TextField id="standard-basic" label="Product name" variant="standard" fullWidth sx={{ paddingRight: '16px' }} onChange={productNameSearchChangeHandler}/>
                    <Button variant="contained">Search</Button>
                </SearchWrapper>
                <Label>Products in stock</Label>
                <ProductsInStockList props={{products: productsOnStock, setSelectedProductId}} />
            </Panel>
            <Panel>
                <ProductDetailsView productId={selectedProductId} />
            </Panel>
        </Container>
    );
};

export default ProductsPage;
