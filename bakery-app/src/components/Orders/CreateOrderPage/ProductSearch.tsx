import Paper from "@mui/material/Paper";
import Table from "@mui/material/Table";
import TableBody from "@mui/material/TableBody";
import TableCell from "@mui/material/TableCell";
import TableContainer from "@mui/material/TableContainer";
import TableHead from "@mui/material/TableHead";
import TableRow from "@mui/material/TableRow";
import { FC, useState } from "react";
import AddCircleIcon from '@mui/icons-material/AddCircle';
import RemoveCircleIcon from '@mui/icons-material/RemoveCircle';
import IconButton from "@mui/material/IconButton";
import TextField from "@mui/material/TextField";
import ProductStockResponse from "../../../models/Responses/ProductStockResponse";
import styled from "styled-components";
import { getProductStock } from "../../../services/ProductService";

export interface ProductSearchProps {
    requestedQuantityChangeHandler: (productId: string, productName: string, availableQuantity: number, quantity: number) => void;
}

const Label = styled.p`
    font-size: 24px;
`;

const SearchWrapper = styled.div`
    display: flex;
    flex-direction: row;
`;

const ProductSearch: FC<ProductSearchProps> = ({ requestedQuantityChangeHandler }) => {
    const [productSearchResults, setProductSearchResults] = useState<ProductStockResponse[]>([]);

    function removeProductHandler(productId: string, productName: string, availableQuantity: number) {
        requestedQuantityChangeHandler(productId, productName, availableQuantity, -1);
    }

    function addProductHandler(productId: string, productName: string, availableQuantity: number) {
        requestedQuantityChangeHandler(productId, productName, availableQuantity, 1);
    }

    function productNameSearchChangeHandler(productName: string) {
        if (!productName) {
            setProductSearchResults([]);
        } else {
            getProductStock(productName).then((products) => {
                setProductSearchResults(products);
            });
        }
    };

    return (
        <>
            <Label>Search products in stock to add to order</Label>
            <SearchWrapper>
                <TextField id="standard-basic" label="Name" variant="standard" fullWidth sx={{ paddingRight: '0px' }}
                    onChange={(e) => productNameSearchChangeHandler(e.target.value)} />
            </SearchWrapper>
            {productSearchResults.length === 0 && <p>No products meet the search criteria</p>}
            {productSearchResults.length > 0 && <TableContainer component={Paper} sx={{ marginTop: '16px' }}>
                <Table sx={{ minWidth: 150 }} size="small" aria-label="simple table">
                    <TableHead>
                        <TableRow>
                            <TableCell>Product</TableCell>
                            <TableCell align="right">Available</TableCell>
                            <TableCell align="right">Increase/decrease</TableCell>
                        </TableRow>
                    </TableHead>
                    <TableBody>
                        {productSearchResults.map((product) => (
                            <TableRow key={product.id} sx={{ '&:last-child td, &:last-child th': { border: 0 } }}>
                                <TableCell component="th" scope="row">{product.name}</TableCell>
                                <TableCell align="right">{product.availableQuantity}</TableCell>
                                <TableCell align="right">
                                    <IconButton aria-label="delete" onClick={() => removeProductHandler(product.id, product.name, product.availableQuantity)}><RemoveCircleIcon /></IconButton>
                                    <IconButton aria-label="delete" onClick={() => addProductHandler(product.id, product.name, product.availableQuantity)}><AddCircleIcon /></IconButton>
                                </TableCell>
                            </TableRow>
                        ))}
                    </TableBody>
                </Table>
            </TableContainer>}
        </>
    );
};

export default ProductSearch;
