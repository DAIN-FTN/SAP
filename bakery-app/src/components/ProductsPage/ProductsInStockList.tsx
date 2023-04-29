import Paper from "@mui/material/Paper";
import Table from "@mui/material/Table";
import TableBody from "@mui/material/TableBody";
import TableCell from "@mui/material/TableCell";
import TableContainer from "@mui/material/TableContainer";
import TableHead from "@mui/material/TableHead";
import TableRow from "@mui/material/TableRow";
import { FC } from "react";
import styled from "styled-components";
import ProductBasicInfo from "../../models/ProductBasicInfo";

export interface ProductsInStockListProps {
    products: ProductBasicInfo[];
    setSelectedProductId: Function;
}

const TableRowStyled = styled(TableRow)`
    cursor: pointer;
    &:hover {
        background-color: #f5f5f5;
    }
`;

const ProductsInStockList: FC<{ props: ProductsInStockListProps }> = ({props: { products: products, setSelectedProductId }}) => {
    if (products.length === 0) {
        return <p>Search products by name</p>;
    }
        
    return (
        <TableContainer component={Paper}>
            <Table sx={{ minWidth: 150 }} size="small" aria-label="simple table">
                <TableHead>
                    <TableRow>
                        <TableCell>Product name</TableCell>
                        <TableCell align="right">Available</TableCell>
                    </TableRow>
                </TableHead>
                <TableBody>
                    {products.map((product) => (
                        <TableRowStyled key={product.id} sx={{ '&:last-child td, &:last-child th': { border: 0 } }} onClick={() => setSelectedProductId(product.id)}>
                            <TableCell component="th" scope="row">{product.name}</TableCell>
                            <TableCell align="right">{product.quantity}</TableCell>
                        </TableRowStyled>
                    ))}
                </TableBody>
            </Table>
        </TableContainer>
    );
};

export default ProductsInStockList;
