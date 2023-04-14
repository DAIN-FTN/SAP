import Paper from "@mui/material/Paper";
import Table from "@mui/material/Table";
import TableBody from "@mui/material/TableBody";
import TableCell from "@mui/material/TableCell";
import TableContainer from "@mui/material/TableContainer";
import TableHead from "@mui/material/TableHead";
import TableRow from "@mui/material/TableRow";
import { FC } from "react";
import styled from "styled-components";
import { BakingTimeSlot as BakingProgram } from "../../models/BakingTimeSlot";

export interface PrepareForOvenListProps {
    prepareForOven: BakingProgram[];
}

const TableRowStyled = styled(TableRow)`
    cursor: pointer;
    &:hover {
        background-color: #f5f5f5;
    }
`;

const ErrorMessage = styled.p`
    font-style: italic;
`;

const PrepareForOvenList: FC<{ props: PrepareForOvenListProps }> = ({props: { prepareForOven }}) => {
    if (prepareForOven.length === 0) {
        return <ErrorMessage>No baking programs to show.</ErrorMessage>;
    }
        
    return (
        <TableContainer component={Paper}>
            <Table sx={{ minWidth: 150 }} size="small" aria-label="simple table">
                <TableHead>
                    <TableRow>
                        <TableCell>Oven code</TableCell>
                        <TableCell align="right">Baking programmed at</TableCell>
                    </TableRow>
                </TableHead>
                <TableBody>
                    {prepareForOven.map((bakingProgram) => (
                        <TableRowStyled key={bakingProgram.id} sx={{ '&:last-child td, &:last-child th': { border: 0 } }} >
                            <TableCell component="th" scope="row">{bakingProgram.ovenCode}</TableCell>
                            <TableCell align="right">{bakingProgram.bakingProgrammedAt.toLocaleString("en-GB")}</TableCell>
                        </TableRowStyled>
                    ))}
                </TableBody>
            </Table>
        </TableContainer>
    );
};

export default PrepareForOvenList;
