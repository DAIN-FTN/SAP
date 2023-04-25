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
import TextField from "@mui/material/TextField";

export interface AvailableProductsListProps {
    availableProducts: ProductBasicInfo[];
    orderProducts: ProductBasicInfo[];
    setOrderProducts: Function;
}

const AvailableProductsList: FC<{ props: AvailableProductsListProps }> = ({ props }) => {
    if (props.availableProducts.length === 0) {
        return <p>No products meet the seach criteria</p>;
    }

    function removeProductHandler(productId: string) {
        console.log("removeProductHandler", productId);
        const productInOrderProducts = props.orderProducts.find((product) => product.id === productId) as ProductBasicInfo;


        if (!productInOrderProducts) return;
        
        productInOrderProducts.quantity -= 1;

        if (productInOrderProducts.quantity === 0) {
            props.setOrderProducts(props.orderProducts.filter((product) => product.id !== productId));
        } else {
            props.setOrderProducts([...props.orderProducts]);
        }
    }

    function addProductHandler(productId: string) {
        console.log("addProductHandler", productId);
        const productBasicInfo = props.availableProducts.find((product) => product.id === productId) as ProductBasicInfo;
        const productInOrderProducts = props.orderProducts.find((product) => product.id === productId)
        if (productInOrderProducts) {
            console.log("product already added");
            
            if (productInOrderProducts.quantity + 1 > productBasicInfo.quantity) {
                console.log("Can't add more products than available");
                return;
            }

            productInOrderProducts.quantity += 1;
            props.setOrderProducts([...props.orderProducts]);

        } else {
            console.log("product not added yet");
            props.setOrderProducts([
                ...props.orderProducts, 
                { id: productId, name: productBasicInfo.name, quantity: 1 }
            ]);
        }
    }
        
    return (
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
                    {props.availableProducts.map((product) => (
                        <TableRow
                            key={product.id}
                            sx={{ '&:last-child td, &:last-child th': { border: 0 } }}
                        >
                            <TableCell component="th" scope="row">
                                {product.name}
                            </TableCell>
                            <TableCell align="right">{product.quantity}</TableCell>
                            <TableCell align="right">
                                <IconButton aria-label="delete" onClick={() => removeProductHandler(product.id)}>
                                    <RemoveCircleIcon />
                                </IconButton>
                                <TextField id={`standard-basic-${product.id}`} label="Quantity" variant="standard" size="small" sx={{width: '70px'}}/>
                                <IconButton aria-label="delete" onClick={() => addProductHandler(product.id)}>
                                    <AddCircleIcon />
                                </IconButton>
                            </TableCell>
                        </TableRow>
                    ))}
                </TableBody>
            </Table>
        </TableContainer>
    );
};

export default AvailableProductsList;
