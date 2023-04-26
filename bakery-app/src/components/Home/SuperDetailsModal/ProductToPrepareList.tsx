import styled from 'styled-components';
import TableRow from '@mui/material/TableRow';
import { FC } from 'react';
import TableContainer from '@mui/material/TableContainer';
import Paper from '@mui/material/Paper';
import Table from '@mui/material/Table';
import TableHead from '@mui/material/TableHead';
import TableCell from '@mui/material/TableCell';
import TableBody from '@mui/material/TableBody';
import ProductToPrepareResponse from '../../../models/Responses/StartPreparing/ProductToPrepareResponse';

export interface ProductToPrepareListProps {
    products: ProductToPrepareResponse[];
}

const TableRowStyled = styled(TableRow)`
    cursor: pointer;

    &:last-child td + &:last-child th {
        border: 0;
    }

    &:hover {
        background-color: #f5f5f5;
    }
`;

const ProductToPrepareList: FC<ProductToPrepareListProps> = ({ products }) => {
    return (
        <TableContainer sx={{ padding: '8x' }} component={Paper}>
            <Table size='small' aria-label="simple table">
                <TableHead>
                    <TableRow>
                        <TableCell>Product name</TableCell>
                        <TableCell align="right">Number of products</TableCell>
                    </TableRow>
                </TableHead>
                <TableBody>
                    {products.map((product: ProductToPrepareResponse) => (
                        <TableRowStyled>
                            <TableCell>{product.name}</TableCell>
                            <TableCell align="right">{product.quantity}</TableCell>
                        </TableRowStyled>
                    ))}
                </TableBody>
            </Table>
        </TableContainer>
    );
}

export default ProductToPrepareList;