import Paper from "@mui/material/Paper";
import Table from "@mui/material/Table";
import TableBody from "@mui/material/TableBody";
import TableCell from "@mui/material/TableCell";
import TableContainer from "@mui/material/TableContainer";
import TableHead from "@mui/material/TableHead";
import TableRow from "@mui/material/TableRow";
import { FC, useState } from "react";
import styled from "styled-components";
import { BakingTimeSlot as BakingProgram } from "../../models/BakingTimeSlot";
import { prepareBakingProgram } from "../../services/BakingProgramService";
import ErrorDialogue from "./ErrorDialogue";
import DetailsModal from "./DetailsModal";

export interface PrepareForOvenListProps {
    prepareForOven: BakingProgram[];
    refreshView: Function;
}

const TableRowStyled = styled(TableRow)`
    cursor: pointer;
    &:hover {
        background-color: #f5f5f5;
    }
`;

const PrepareButton = styled.div`
    cursor: pointer;
    background-color: #DC3F3F;
    border: none;
    color: #fff;
    box-sizing: border-box;
    transition: background-color 0.1s ease-in-out;
    width: fit-content;
    padding: 0px 14px;
    border-radius: 21px;

    &:hover {
        background-color: #cc2a2a;
    }
`;

const ErrorMessage = styled.p`
    font-style: italic;
`;

function getMeaningfulDate(date: Date) {
    if (isToday(date)) {
        return `Today, ${getTimeAsString(date)}`;
    }

    if (isTomorrow(date)) {
        return `Tomorrow, ${getTimeAsString(date)}`;
    }

    return `${formatDate(date)}, ${getTimeAsString(date)}`;

    function isToday(date: Date): boolean {
        const today = new Date();

        return date.getDate() === today.getDate() &&
            date.getMonth() === today.getMonth() &&
            date.getFullYear() === today.getFullYear();
    }

    function isTomorrow(date: Date): boolean {
        const tomorrow = new Date();
        tomorrow.setDate(tomorrow.getDate() + 1);

        return date.getDate() === tomorrow.getDate() &&
            date.getMonth() === tomorrow.getMonth() &&
            date.getFullYear() === tomorrow.getFullYear();
    }

    function formatDate(date: Date): string {
        const day = date.getDate().toString();
        const month = (date.getMonth() + 1).toString();
        const year = date.getFullYear().toString();

        return `${day}.${month}.${year}.`;
    }

    function getTimeAsString(date: Date): string {
        const hours = date.getHours().toString().padStart(2, '0');
        const minutes = date.getMinutes().toString().padStart(2, '0');

        return `${hours}:${minutes}`;
    }
}

const PrepareForOvenList: FC<{ props: PrepareForOvenListProps }> = ({ props: { prepareForOven, refreshView } }) => {
    const [open, setOpen] = useState(false);
    const [errorMessage, setErrorMessage] = useState("");
    const [showDetails, setShowDetails] = useState(false);
    const [selectedBakingProgram, setSelectedBakingProgram] = useState<BakingProgram | null>(null);

    async function prepareClickHandler(id: string, callback: Function) {
        try {
            await prepareBakingProgram(id);
        } catch (exception) {
            const error = exception as Error;
            setOpen(true);
            setErrorMessage(error.message);
        }
        callback();
    }

    function rowClickHandler(bakingProgram: BakingProgram) {
        setSelectedBakingProgram(bakingProgram);
        setShowDetails(true);
    }

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
                        <TableCell align="right" sx={{ width: '110px' }}>Available action</TableCell>
                    </TableRow>
                </TableHead>
                <TableBody>
                    {prepareForOven.map((bakingProgram) => (
                        <TableRowStyled key={bakingProgram.id} sx={{ '&:last-child td, &:last-child th': { border: 0 } }} >
                            <TableCell component="th" scope="row" onClick={() => rowClickHandler(bakingProgram)}>{bakingProgram.ovenCode}</TableCell>
                            <TableCell align="right" onClick={() => rowClickHandler(bakingProgram)}>{getMeaningfulDate(bakingProgram.bakingProgrammedAt)}</TableCell>
                            <TableCell align="right" sx={{ display: 'flex' }}>
                                <PrepareButton onClick={() => prepareClickHandler(bakingProgram.id, refreshView)}>Prepare</PrepareButton>
                            </TableCell>
                        </TableRowStyled>
                    ))}
                </TableBody>
            </Table>
            <ErrorDialogue open={open} errorMessage={errorMessage} onClose={() => setOpen(false)} />
            <DetailsModal isOpen={showDetails} onClose={() => setShowDetails(false)} bakingProgram={selectedBakingProgram} />
        </TableContainer>
    );
};

export default PrepareForOvenList;
