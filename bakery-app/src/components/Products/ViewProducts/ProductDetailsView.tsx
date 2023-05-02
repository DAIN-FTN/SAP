import { FC, useEffect, useState } from "react";
import styled from "styled-components";
import { getDetails } from "../../../services/ProductService";
import StockedLocationProductQuantitiesList from "./StockedLocationProductQuantitiesList";
import ProductDetailsResponse from "../../../models/Responses/Product/ProductDetailsResponse";

export interface ProductDetailsViewProps {
    productId: string | null;
}

const Container = styled.div`
    width: 100%;
    height: 100%;
    display: flex;
    flex-direction: column;
`;

const DetailsPanelLabel = styled.p`
    font-size: 24px;
`;

const ProductPropertyLabel = styled.p`
    color: rgba(0, 0, 0, 0.6);
    font-family: "Roboto","Helvetica","Arial",sans-serif;
    font-weight: 400;
    font-size: 0.8rem;
    line-height: 1.4375em;
    letter-spacing: 0.00938em;

    margin: 0px;
`;

const ProductPropertyValue = styled.p`
    font-size: 22px;
    color: black;
    margin-top: 0px;
`;

const ProductDetailsView: FC<ProductDetailsViewProps> = ({ productId }) => {
    const [product, setProduct] = useState<ProductDetailsResponse | null>(null);

    useEffect(() => {
        const fetchProduct = async () => {
            if (productId) {
                const product = await getDetails(productId);
                setProduct(product);
            }
        };

        fetchProduct();
    }, [productId]);

    if (!productId) {
        return (<div>Select a product to view details</div>);
    } else if (!product) {
        return (<div>Loading...</div>);
    } else {
        return (
            <Container>
                <DetailsPanelLabel>Product details</DetailsPanelLabel>
                <ProductPropertyLabel>Name</ProductPropertyLabel>
                <ProductPropertyValue>{product.name}</ProductPropertyValue>
                <ProductPropertyLabel>Baking time</ProductPropertyLabel>
                <ProductPropertyValue>{product.bakingTimeInMins} minutes</ProductPropertyValue>
                <ProductPropertyLabel>Baking temperature</ProductPropertyLabel>
                <ProductPropertyValue>{product.bakingTempInC}°C</ProductPropertyValue>
                <ProductPropertyLabel>Size</ProductPropertyLabel>
                <ProductPropertyValue>{product.size}</ProductPropertyValue>
                <ProductPropertyLabel>Locations with stock</ProductPropertyLabel>
                <StockedLocationProductQuantitiesList props={{locationsWithStock: product.locationsWithStock}} />
            </Container>
        );
    }
}

export default ProductDetailsView;