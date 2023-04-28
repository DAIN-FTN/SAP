import Paper from "@mui/material/Paper";
import TableContainer from "@mui/material/TableContainer";
import { FC } from "react";
import OrderProductRequest from "../../../models/Requests/OrderProductRequest";

export interface OrdersListProps {
    orderProducts: OrderProductRequest[];
    setSelectedOrderId: (orderId: string) => void;
}

const OrdersList: FC<OrdersListProps> = ({ orderProducts, setSelectedOrderId }) => {
    if (orderProducts.length === 0) {
        return <p>No products meet the seach criteria</p>;
    }

    function removeProductHandler(productId: string) {
        console.log("removeProductHandler", productId);

    }
        
    return (
        <TableContainer component={Paper}>
            
        </TableContainer>
    );
};

export default OrdersList;
