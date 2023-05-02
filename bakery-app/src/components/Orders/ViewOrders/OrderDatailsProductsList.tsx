import { FC } from "react";
import styled from "styled-components";
import OrderProductResponse from "../../../models/Responses/Order/OrderProductResponse";
import Table from "@mui/material/Table";
import TableHead from "@mui/material/TableHead/TableHead";
import TableRow from "@mui/material/TableRow";
import TableCell from "@mui/material/TableCell/TableCell";
import TableBody from "@mui/material/TableBody/TableBody";

export interface OrderDetailsProductsListProps {
    products: OrderProductResponse[];
}

const Container = styled.div`
`;

const TableRowStyled = styled(TableRow)`
    cursor: pointer;
    &:hover {
        background-color: #f5f5f5;
    }
`;

const Label = styled.p`
    font-size: 24px;
`;

const OrderDetailsProductsList: FC<OrderDetailsProductsListProps> = ({ products }) => {

    if (products.length === 0) {
        return <p>No order products to show</p>;
    }
        
    return (
        <Container>
            <Label>Products to bake</Label>

            <Table sx={{ minWidth: 150 }} size="small" aria-label="simple table">
                    <TableHead>
                        <TableRow>
                            <TableCell>Product name</TableCell>
                            <TableCell align="right">Quantity</TableCell>
                        </TableRow>
                    </TableHead>
                    <TableBody>
                        {products.map((product) => (
                            <TableRowStyled key={product.productId} sx={{ '&:last-child td, &:last-child th': { border: 0 } }}>
                                <TableCell component="th" scope="row">{product.productName}</TableCell>
                                <TableCell align="right">{product.quantityToBake}</TableCell>
                            </TableRowStyled>
                        ))}
                    </TableBody>
                </Table>

            
        </Container>
    );
};

export default OrderDetailsProductsList;
