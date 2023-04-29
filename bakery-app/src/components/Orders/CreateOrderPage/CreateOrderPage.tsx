import { useState } from "react";
import { FC } from "react";
import styled from "styled-components";
import ProductSearch from "./ProductSearch";
import Button from '@mui/material/Button';
import OrderProductsList from "./OrderProductsList";
import ProductRequestItem from "./Models/ProductRequestItem";
import CheckDeliveryTime from "./CheckDeliveryTime";
import { create } from "../../../services/OrderService";
import CreateOrderRequest from "../../../models/Requests/CreateOrderRequest";
import OrderProductRequest from "../../../models/Requests/OrderProductRequest";
import CreateOrderResponse from "../../../models/Responses/Order/CreateOrderResponse";
import { useNavigate } from "react-router-dom";

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

const CreateOrderPage: FC = () => {
    const [orderProducts, setOrderProducts] = useState<ProductRequestItem[]>([]);
    const [deliveryTime, setDeliveryTime] = useState<Date | null>(null);
    const [creationPossible, setCreationPossible] = useState<boolean>(false);
    const navigate = useNavigate();

    function changeRequestedQuantityHandler(productId: string, productName: string, availableQuantity: number, quantity: number) {
        if (orderProducts.length === 0 && quantity > 0) {
            setOrderProducts([{
                id: productId,
                name: productName,
                availableQuantity,
                requestedQuantity: quantity
            }]);
            return;
        }

        const productInOrderProducts = orderProducts.find((product) => product.id === productId) as ProductRequestItem;

        if (productInOrderProducts) {
            if (productInOrderProducts.requestedQuantity + quantity > availableQuantity) return;

            productInOrderProducts.requestedQuantity += quantity;

            if (productInOrderProducts.requestedQuantity === 0) {
                setOrderProducts(orderProducts.filter((product) => product.id !== productId));
            } else {
                setOrderProducts([...orderProducts]);
            }
        } else {
            if (quantity < 1) return;
            setOrderProducts([
                ...orderProducts,
                {
                    id: productId,
                    name: productName,
                    availableQuantity,
                    requestedQuantity: quantity
                }
            ]);
        }
    }

    async function createOrderHandler() {
        console.log("new order request");
        const orderRequest: CreateOrderRequest = {
            shouldBeDoneAt: deliveryTime as Date,
            customer: {
                fullName: "John Smith",
                email: "email@example.com",
                telephone: "+3811234567"
            },
            products: mapProductRequestItemsToProductRequest(orderProducts)
        };
        create(orderRequest).then((response: CreateOrderResponse) => {
            console.log(response);
            navigate(`/order/view/${response.id}`);
        });
    };

    function mapProductRequestItemsToProductRequest(productRequestItems: ProductRequestItem[]): OrderProductRequest[] {
        return productRequestItems.map((productRequestItem) => {
            return {
                productId: productRequestItem.id,
                quantity: productRequestItem.requestedQuantity
            } as OrderProductRequest;
        });
    }

    return (
        <Container>
            <Panel>
                <ProductSearch requestedQuantityChangeHandler={changeRequestedQuantityHandler} />
                <OrderProductsList requestedQuantityChangeHandler={changeRequestedQuantityHandler} products={orderProducts} />
            </Panel>
            <Panel>
                {orderProducts.length > 0 && <CheckDeliveryTime orderProducts={orderProducts} setCreationPossible={setCreationPossible} setDeliveryTime={setDeliveryTime} />}
                {creationPossible && <Button variant="contained" onClick={createOrderHandler}>Create order</Button>}
            </Panel>
        </Container>
    );
};

export default CreateOrderPage;
