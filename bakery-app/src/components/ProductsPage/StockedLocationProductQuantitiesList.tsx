import Paper from "@mui/material/Paper";
import Table from "@mui/material/Table";
import TableBody from "@mui/material/TableBody";
import TableCell from "@mui/material/TableCell";
import TableContainer from "@mui/material/TableContainer";
import TableHead from "@mui/material/TableHead";
import TableRow from "@mui/material/TableRow";
import { FC } from "react";
import styled from "styled-components";
import StockOnLocationResponse from "../../models/Responses/StockOnLocationResponse";

export interface StockedLocationProductQuantitiesList {
    locationsWithStock: StockOnLocationResponse[];
}

const TableRowStyled = styled(TableRow)`
    &:hover {
        background-color: #f5f5f5;
    }
`;

const StockedLocationProductQuantitiesList: FC<{ props: StockedLocationProductQuantitiesList}> = ({props: { locationsWithStock}}) => {
    if (locationsWithStock != undefined && locationsWithStock.length === 0) {
        return <p>Not on stock</p>;
    }
        
    return (
        <TableContainer component={Paper}>
            <Table sx={{ minWidth: 150 }} size="small" aria-label="simple table">
                <TableHead>
                    <TableRow>
                        <TableCell>Code</TableCell>
                        <TableCell align="right">Quantity</TableCell>
                        <TableCell align="right">Reserved quantity</TableCell>
                    </TableRow>
                </TableHead>
                <TableBody>
                    {locationsWithStock.map((locationWithStock) => (
                        <TableRowStyled key={locationWithStock.id} sx={{ '&:last-child td, &:last-child th': { border: 0 } }}>
                            <TableCell component="th" scope="row">{locationWithStock.code}</TableCell>
                            <TableCell align="right">{locationWithStock.quantity}</TableCell>
                            <TableCell align="right">{locationWithStock.reservedQuantity}</TableCell>
                        </TableRowStyled>
                    ))}
                </TableBody>
            </Table>
        </TableContainer>
    );
};

export default StockedLocationProductQuantitiesList;