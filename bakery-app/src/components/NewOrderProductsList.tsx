import Paper from "@mui/material/Paper";
import Table from "@mui/material/Table";
import TableBody from "@mui/material/TableBody";
import TableCell from "@mui/material/TableCell";
import TableContainer from "@mui/material/TableContainer";
import TableHead from "@mui/material/TableHead";
import TableRow from "@mui/material/TableRow";
import React, { useState } from "react";
import { FC } from "react";
import styled from "styled-components";
import { ProductBasicInfo } from "../models/ProductBasicInfo";
// import Table from 'react-bootstrap/Table';
// import 'bootstrap/dist/css/bootstrap.min.css';
import AddCircleIcon from '@mui/icons-material/AddCircle';
import RemoveCircleIcon from '@mui/icons-material/RemoveCircle';
import IconButton from "@mui/material/IconButton";
import TextField from "@mui/material/TextField";

export interface NewOrderProductsListProps {
    products: ProductBasicInfo[];
}

const NewOrderProductsList: FC<{ props: NewOrderProductsListProps }> = ({ props }) => {
    if (props.products.length === 0) {
        return <p>No products added</p>;
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
                    {props.products.map((product) => (
                        <TableRow
                            key={product.id}
                            sx={{ '&:last-child td, &:last-child th': { border: 0 } }}
                        >
                            <TableCell component="th" scope="row">
                                {product.name}
                            </TableCell>
                            <TableCell align="right">{product.quantity}</TableCell>
                            <TableCell align="right">
                                <IconButton aria-label="delete">
                                    <RemoveCircleIcon />
                                </IconButton>
                                <TextField id={`standard-basic-${product.id}`} label="Quantity" variant="standard" size="small" sx={{width: '70px'}} />
                                <IconButton aria-label="delete">
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

export default NewOrderProductsList;
