import Table from "@mui/material/Table";
import TableCell from "@mui/material/TableCell";
import TableHead from "@mui/material/TableHead";
import TableRow from "@mui/material/TableRow";
import { FC, useEffect, useState } from "react";
import styled from "styled-components";
import OrderResponse from "../../../models/Responses/Order/OrderResponse";
import TableContainer from "@mui/material/TableContainer";
import Paper from "@mui/material/Paper";
import TableBody from "@mui/material/TableBody";
import { getAll } from "../../../services/OrderService";
import { DateUtils } from "../../../services/Utils";
import { OrderStatus } from "../../../models/Enums/OrderStatus";

export interface OrdersListProps {
    setSelectedOrderId: (orderId: string) => void;
}

const Container = styled.div`
    padding: 8px;
    display: flex;
    flex-direction: column;
`;

const Label = styled.p`
    font-size: 24px;
`;

// const SearchWrapper = styled.div`
//     display: flex;
//     flex-direction: row;
// `;

const TableRowStyled = styled(TableRow)`
    cursor: pointer;
    &:hover {
        background-color: #f5f5f5;
    }
`;

const OrdersList: FC<OrdersListProps> = ({ setSelectedOrderId }) => {
    const [orderResults, setOrderResults] = useState<OrderResponse[]>([]);

    useEffect(() => {
        getAll().then((orders) => {
            const sortedOrders = orders.sort((a, b) => DateUtils.compareDates(a.shouldBeDoneAt, b.shouldBeDoneAt));
            setOrderResults(sortedOrders);
        });
    }, []);

    // function productNameSearchChangeHandler(productName: string) {
    //     if (!productName) {
    //         setProductSearchResults([]);
    //     } else {
    //         getProductStock(productName).then((products) => {
    //             setProductSearchResults(products);
    //         });
    //     }
    // };

    return (
        <Container>
            <Label>Orders</Label>
            {/* <SearchWrapper>
                <TextField id="standard-basic" label="Name" variant="standard" fullWidth sx={{ paddingRight: '0px' }}
                    onChange={(e) => orderNameSearchChangeHandler(e.target.value)} />
            </SearchWrapper> */}
            {orderResults.length === 0 && <p>No products meet the search criteria</p>}
            {orderResults.length > 0 && <TableContainer component={Paper}>
                <Table sx={{ minWidth: 150 }} size="small" aria-label="simple table">
                    <TableHead>
                        <TableRow>
                            <TableCell>Order date</TableCell>
                            <TableCell align="right">Status</TableCell>
                        </TableRow>
                    </TableHead>
                    <TableBody>
                        {orderResults.map((order) => (
                            <TableRowStyled key={order.id} sx={{ '&:last-child td, &:last-child th': { border: 0 } }} onClick={() => setSelectedOrderId(order.id)}>
                                <TableCell component="th" scope="row">{DateUtils.getMeaningfulDate(order.shouldBeDoneAt)}</TableCell>
                                <TableCell align="right">{OrderStatus[order.status]}</TableCell>
                            </TableRowStyled>
                        ))}
                    </TableBody>
                </Table>
            </TableContainer>}
        </Container>
    );
};

export default OrdersList;
