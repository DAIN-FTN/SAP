import Paper from "@mui/material/Paper";
import Table from "@mui/material/Table";
import TableBody from "@mui/material/TableBody";
import TableCell from "@mui/material/TableCell";
import TableContainer from "@mui/material/TableContainer";
import TableHead from "@mui/material/TableHead";
import TableRow from "@mui/material/TableRow";
import { FC } from "react";
import AddCircleIcon from '@mui/icons-material/AddCircle';
import RemoveCircleIcon from '@mui/icons-material/RemoveCircle';
import IconButton from "@mui/material/IconButton";
import ProductRequestItem from "./Models/ProductRequestItem";
import styled from "styled-components";

export interface OrderProductsListProps {
    products: ProductRequestItem[];
    requestedQuantityChangeHandler: (productId: string, productName: string, availableQuantity: number, quantity: number) => void;
}

const Label = styled.p`
    font-size: 24px;
`;

const OrderProductsList: FC<OrderProductsListProps> = ({ products, requestedQuantityChangeHandler }) => {
    if (!products || products.length === 0) {
        return <p>No products added</p>;
    }

    return (
        <>
            <Label>Products for order</Label>
            <TableContainer component={Paper}>
                <Table sx={{ minWidth: 150 }} size="small" aria-label="simple table">
                    <TableHead>
                        <TableRow>
                            <TableCell>Product name</TableCell>
                            <TableCell align="right">Quantity for ordering</TableCell>
                            <TableCell align="right">Incerease/decrease</TableCell>
                        </TableRow>
                    </TableHead>
                    <TableBody>
                        {products.map((product) => (
                            <TableRow key={product.id} sx={{ '&:last-child td, &:last-child th': { border: 0 } }}>
                                <TableCell component="th" scope="row">{product.name}</TableCell>
                                <TableCell align="right">{product.requestedQuantity}</TableCell>
                                <TableCell align="right">
                                    <IconButton onClick={() => requestedQuantityChangeHandler(product.id, product.name, product.availableQuantity, -1)}><RemoveCircleIcon /></IconButton>
                                    <IconButton onClick={() => requestedQuantityChangeHandler(product.id, product.name, product.availableQuantity, 1)}><AddCircleIcon /></IconButton>
                                </TableCell>
                            </TableRow>
                        ))}
                    </TableBody>
                </Table>
            </TableContainer>
        </>
    );
};

export default OrderProductsList;
