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
import OrderProductRequest from "../../../models/Requests/OrderProductRequest";
import styled from "styled-components";
import Button from "@mui/material/Button";

export interface ProductSearchProps {
    requestedQuantityChangeHandler: (productId: string, quantity: number) => void;
}

const Panel = styled.div`
    width: 50%;
    padding: 24px;
    display: flex;
    flex-direction: column;
    box-sizing: border-box;
`;

const Label = styled.p`
    font-size: 24px;
`;

const SearchWrapper = styled.div`
    display: flex;
    flex-direction: row;
`;

const ProductSearch: FC<ProductSearchProps> = ({ requestedQuantityChangeHandler }) => {
    const [productsThatMatchSearchCriteria, setProductsThatMatchSearchCriteria] = useState<ProductStockResponse[]>([]);

    // if (props.availableProducts.length === 0) {
    //     return <p>No products meet the seach criteria</p>;
    // }

    function removeProductHandler(productId: string) {
        // console.log("removeProductHandler", productId);
        // const productInOrderProducts = props.orderProducts.find((product) => product.productId === productId) as OrderProductRequest;


        // if (!productInOrderProducts) return;

        // productInOrderProducts.quantity -= 1;

        // if (productInOrderProducts.quantity === 0) {
        //     props.setOrderProducts(props.orderProducts.filter((product) => product.productId !== productId));
        // } else {
        //     props.setOrderProducts([...props.orderProducts]);
        // }
    }

    function addProductHandler(productId: string) {
        // console.log("addProductHandler", productId);
        // const productBasicInfo = props.availableProducts.find((product) => product.id === productId) as ProductStockResponse;
        // const productInOrderProducts = props.orderProducts.find((product) => product.productId === productId)
        // if (productInOrderProducts) {
        //     console.log("product already added");

        //     if (productInOrderProducts.quantity + 1 > productBasicInfo.availableQuantity) {
        //         console.log("Can't add more products than available");
        //         return;
        //     }

        //     productInOrderProducts.quantity += 1;
        //     props.setOrderProducts([...props.orderProducts]);

        // } else {
        //     console.log("product not added yet");
        //     props.setOrderProducts([
        //         ...props.orderProducts,
        //         { id: productId, name: productBasicInfo.name, quantity: 1 }
        //     ]);
        // }
    }

    function productNameSearchChangeHandler(productName: string) {
        console.log(productName);

        // if (!productName) {
        //     setProductsOnStock([]);
        // } else {
        //     getProductStock(productName).then((products) => {
        //         console.log('fetch finished for fetchProductsBasicInfo() in CreateOrderPage');
        //         setProductsOnStock(products);
        //     });
        // }
    };

    return (
        <>
            <Label>Search products in stock to add to order</Label>
            <SearchWrapper>
                <TextField id="standard-basic" label="Product name" variant="standard" fullWidth sx={{ paddingRight: '16px' }}
                    onChange={(e) => productNameSearchChangeHandler(e.target.value)} />
            </SearchWrapper>
            <Label>Products in stock</Label>
            <TableContainer component={Paper}>
                <Table sx={{ minWidth: 150 }} size="small" aria-label="simple table">
                    <TableHead>
                        <TableRow>
                            <TableCell>Product name</TableCell>
                            <TableCell align="right">Available</TableCell>
                            <TableCell align="right">Quantity for ordering</TableCell>
                        </TableRow>
                    </TableHead>
                    <TableBody>
                        {productsThatMatchSearchCriteria.map((product) => (
                            <TableRow
                                key={product.id}
                                sx={{ '&:last-child td, &:last-child th': { border: 0 } }}
                            >
                                <TableCell component="th" scope="row">
                                    {product.name}
                                </TableCell>
                                <TableCell align="right">{product.availableQuantity}</TableCell>
                                <TableCell align="right">
                                    <IconButton aria-label="delete" onClick={() => removeProductHandler(product.id)}>
                                        <RemoveCircleIcon />
                                    </IconButton>
                                    <TextField id={`standard-basic-${product.id}`} label="Quantity" variant="standard" size="small" sx={{ width: '70px' }} />
                                    <IconButton aria-label="delete" onClick={() => addProductHandler(product.id)}>
                                        <AddCircleIcon />
                                    </IconButton>
                                </TableCell>
                            </TableRow>
                        ))}
                    </TableBody>
                </Table>
            </TableContainer>
        </>

    );
};

export default ProductSearch;
