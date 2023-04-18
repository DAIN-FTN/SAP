import Paper from "@mui/material/Paper";
import Table from "@mui/material/Table";
import TableBody from "@mui/material/TableBody";
import TableCell from "@mui/material/TableCell";
import TableContainer from "@mui/material/TableContainer";
import TableHead from "@mui/material/TableHead";
import TableRow from "@mui/material/TableRow";
import { FC, useState } from "react";
import styled from "styled-components";
import { BakingTimeSlot as BakingProgram, BakingProgramStatus } from "../../models/BakingTimeSlot";
import { cancellBakingProgram, startPreparingBakingProgram } from "../../services/BakingProgramService";
import ErrorDialogue from "./ErrorDialogue";
import DetailsModal from "./DetailsModal";
import { DateUtils } from "../../services/Utils";

export interface BakingListProps {
    bakingBakingPrograms: BakingProgram[];
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
    margin-right: 4px;

    &:hover {
        background-color: #cc2a2a;
    }
`;

const ErrorMessage = styled.p`
    font-style: italic;
`;

const AvailableActionsTableCell = styled(TableCell)`
    display: flex !important;
    flex-direction: row !important; 
    justify-content: flex-end !important;
`;

const BakingList: FC<{ props: BakingListProps }> = ({ props: { bakingBakingPrograms, refreshView } }) => {
    const [open, setOpen] = useState(false);
    const [errorMessage, setErrorMessage] = useState("");
    const [showDetails, setShowDetails] = useState(false);
    const [selectedBakingProgram, setSelectedBakingProgram] = useState<BakingProgram | null>(null);

    async function prepareClickHandler(id: string, callback: Function) {
        try {
            await startPreparingBakingProgram(id);
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

    function finishClickHandler(bakingProgramId: string) {
        cancellBakingProgram(bakingProgramId)
            .then(() => {
                refreshView();
            });
    }

    function cancelClickHandler(bakingProgramId: string) {
        cancellBakingProgram(bakingProgramId)
            .then(() => {
                refreshView();
            });
    }

    function startBakingClickHandler(bakingProgramId: string) {
        cancellBakingProgram(bakingProgramId)
            .then(() => {
                refreshView();
            });
    }

    if (bakingBakingPrograms.length === 0) {
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
                    {bakingBakingPrograms.map((bakingProgram) => (
                        <TableRowStyled key={bakingProgram.id} sx={{ '&:last-child td, &:last-child th': { border: 0 } }} >
                            <TableCell component="th" scope="row" onClick={() => rowClickHandler(bakingProgram)}>{bakingProgram.ovenCode}</TableCell>
                            <TableCell align="right" onClick={() => rowClickHandler(bakingProgram)}>{DateUtils.getMeaningfulDate(bakingProgram.bakingProgrammedAt)}</TableCell>
                            <AvailableActionsTableCell>
                                <PrepareButton onClick={() => finishClickHandler(bakingProgram.id)}>Finish</PrepareButton>
                                <PrepareButton onClick={() => cancelClickHandler(bakingProgram.id)}>Cancel</PrepareButton>
                                {(bakingProgram.status === BakingProgramStatus.Prepared)
                                    && <PrepareButton onClick={() => startBakingClickHandler(bakingProgram.id)}>Start baking</PrepareButton>}
                            </AvailableActionsTableCell>
                        </TableRowStyled>
                    ))}
                </TableBody>
            </Table>
            <ErrorDialogue open={open} errorMessage={errorMessage} onClose={() => setOpen(false)} />
            <DetailsModal isOpen={showDetails} onClose={() => setShowDetails(false)} bakingProgram={selectedBakingProgram} />
        </TableContainer>
    );
};

export default BakingList;
