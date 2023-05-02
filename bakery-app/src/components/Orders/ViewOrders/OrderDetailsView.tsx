import Paper from "@mui/material/Paper";
import TableContainer from "@mui/material/TableContainer";
import { FC, useEffect, useState } from "react";
import OrderDetailsResponse from "../../../models/Responses/Order/OrderDetailsResponse";
import { getDetails } from "../../../services/OrderService";
import styled from "styled-components";
import { DateUtils } from "../../../services/Utils";
import { OrderStatus } from "../../../models/Enums/OrderStatus";

export interface OrderDetailsViewProps {
    orderId: string | null;
}

const Container = styled.div`
    padding: 8px;
    display: flex;
    flex-direction: column;
`;

const PropertyLabel = styled.p`
    color: rgba(0, 0, 0, 0.6);
    font-family: "Roboto","Helvetica","Arial",sans-serif;
    font-weight: 400;
    font-size: 0.8rem;
    line-height: 1em;
    letter-spacing: 0.00938em;

    margin: 0px;
`;

const PropertyValue = styled.p`
    font-size: 22px;
    color: black;
    margin-top: 0px;
`;

const OrderDetailsView: FC<OrderDetailsViewProps> = ({ orderId }) => {
    const [orderDetails, setOrderDetails] = useState<OrderDetailsResponse | null>(null);

    useEffect(() => {
        if (!orderId) return;
        
        getDetails(orderId).then((response: OrderDetailsResponse | null) => {
            setOrderDetails(response);
        });

    }, [orderId]);

    if (orderId === null || orderDetails === null) {
        return <p>No order details to show</p>;
    }
        
    return (
        <Container>
            <PropertyLabel>Id</PropertyLabel>
            <PropertyValue>{orderDetails.id}</PropertyValue>
            <PropertyLabel>Should be done at</PropertyLabel>
            <PropertyValue>{DateUtils.getMeaningfulDate(orderDetails.shouldBeDoneAt)}</PropertyValue>
            <PropertyLabel>Status</PropertyLabel>
            <PropertyValue>{OrderStatus[orderDetails.status]}</PropertyValue>
        </Container>
    );
};

export default OrderDetailsView;
