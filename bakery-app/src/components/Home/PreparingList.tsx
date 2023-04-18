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
import { cancellBakingProgram, finishPreparingBakingProgram, startBakingBakingProgram } from "../../services/BakingProgramService";
import ErrorDialogue from "./ErrorDialogue";
import DetailsModal from "./DetailsModal";
import { DateUtils } from "../../services/Utils";
import { StartPreparing } from "../../models/Responses/StartPreparing";

export interface PreparingListProps {
    preparingBakingPrograms: BakingProgram[];
    preparingInProgress: StartPreparing | null;
    refreshView: Function;
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
    margin-left: 4px;

    &:hover {
        background-color: #cc2a2a;
    }
`;

const PreparingTag = styled.span`
    background-color: #DC3F3F;
    border: none;
    color: #fff;
    box-sizing: border-box;
    width: fit-content;
    padding: 0px 14px;
    border-radius: 21px;
    margin-right: 8px;
`;

const ErrorMessage = styled.p`
    font-style: italic;
`;

const AvailableActionsTableCell = styled(TableCell)`
    display: flex !important;
    flex-direction: row !important; 
    justify-content: flex-end !important;
`;

const PreparingList: FC<{ props: PreparingListProps }> = ({ props: { preparingBakingPrograms, preparingInProgress, refreshView } }) => {
    const [isErrorDialogOpen, setIsErrorDialogOpen] = useState(false);
    const [errorMessage, setErrorDialogMessage] = useState("");
    const [showDetails, setShowDetails] = useState(false);
    const [selectedBakingProgram, setSelectedBakingProgram] = useState<BakingProgram | null>(null);

    function rowClickHandler(bakingProgram: BakingProgram) {
        setSelectedBakingProgram(bakingProgram);
        setShowDetails(true);
    }

    function finishClickHandler(bakingProgramId: string) {
        finishPreparingBakingProgram(bakingProgramId)
            .then(() => {
                refreshView();
            })
            .catch((error) => {
                setIsErrorDialogOpen(true);
                setErrorDialogMessage(error.message);
            });
    }

    function cancelClickHandler(bakingProgramId: string) {
        cancellBakingProgram(bakingProgramId)
            .then(() => {
                refreshView();
            })
            .catch((error) => {
                setIsErrorDialogOpen(true);
                setErrorDialogMessage(error.message);
            });
    }

    function startBakingClickHandler(bakingProgramId: string) {
        startBakingBakingProgram(bakingProgramId)
            .then(() => {
                refreshView();
            })
            .catch((error) => {
                setIsErrorDialogOpen(true);
                setErrorDialogMessage(error.message);
            });
    }

    if (preparingBakingPrograms.length === 0) {
        return <ErrorMessage>No baking programs to show.</ErrorMessage>;
    }

    return (
        <TableContainer component={Paper}>
            <Table sx={{ minWidth: 150 }} size="small" aria-label="simple table">
                <TableHead>
                    <TableRow>
                        <TableCell>Oven code</TableCell>
                        <TableCell align="right">Baking programmed at</TableCell>
                        <TableCell align="right" sx={{ width: '180px' }}>Available action</TableCell>
                    </TableRow>
                </TableHead>
                <TableBody>
                    {preparingInProgress && <TableRowStyled>
                        <TableCell component="th" scope="row" sx={{ display: 'flex', flexDirection: "row" }}><PreparingTag>Preparing</PreparingTag> {preparingInProgress.ovenCode}</TableCell>
                        <TableCell align="right">123</TableCell>
                        <AvailableActionsTableCell>
                            <PrepareButton onClick={() => finishClickHandler(preparingInProgress.id)}>Finish</PrepareButton>
                        </AvailableActionsTableCell>
                    </TableRowStyled>}
                    {preparingBakingPrograms.map((bakingProgram) => (
                        <TableRowStyled key={bakingProgram.id}>
                            <TableCell component="th" scope="row" onClick={() => rowClickHandler(bakingProgram)}>{bakingProgram.ovenCode}</TableCell>
                            <TableCell align="right" onClick={() => rowClickHandler(bakingProgram)}>{DateUtils.getMeaningfulDate(bakingProgram.bakingProgrammedAt)}</TableCell>
                            <AvailableActionsTableCell>
                                {/* {(bakingProgram.status === BakingProgramStatus.Preparing)
                                    && <PrepareButton onClick={() => finishClickHandler(bakingProgram.id)}>Finish</PrepareButton>} */}
                                {(bakingProgram.status === BakingProgramStatus.Preparing)
                                    && <PrepareButton onClick={() => cancelClickHandler(bakingProgram.id)}>Cancel</PrepareButton>}
                                {(bakingProgram.status === BakingProgramStatus.Prepared)
                                    && <PrepareButton onClick={() => startBakingClickHandler(bakingProgram.id)}>Start baking</PrepareButton>}
                            </AvailableActionsTableCell>
                        </TableRowStyled>
                    ))}
                </TableBody>
            </Table>
            <ErrorDialogue open={isErrorDialogOpen} errorMessage={errorMessage} onClose={() => setIsErrorDialogOpen(false)} />
            <DetailsModal isOpen={showDetails} onClose={() => setShowDetails(false)} bakingProgram={selectedBakingProgram} />
        </TableContainer>
    );
};

export default PreparingList;
