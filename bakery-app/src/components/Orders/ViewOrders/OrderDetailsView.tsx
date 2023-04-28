import Paper from "@mui/material/Paper";
import TableContainer from "@mui/material/TableContainer";
import { FC } from "react";
import OrderDetailsResponse from "../../../models/Responses/Order/OrderDetailsResponse";

export interface OrderDetailsViewProps {
    order: OrderDetailsResponse | null;
}

const OrderDetailsView: FC<OrderDetailsViewProps> = ({ order }) => {
    if (order === null) {
        return <p>No order details to show</p>;
    }

    // function removeProductHandler(productId: string) {
    //     console.log("removeProductHandler", productId);

    // }
        
    return (
        <TableContainer component={Paper}>
            
        </TableContainer>
    );
};

export default OrderDetailsView;
