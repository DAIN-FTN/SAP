import Paper from "@mui/material/Paper";
import Table from "@mui/material/Table";
import TableBody from "@mui/material/TableBody";
import TableCell from "@mui/material/TableCell";
import TableContainer from "@mui/material/TableContainer";
import TableHead from "@mui/material/TableHead";
import TableRow from "@mui/material/TableRow";
import { FC, useState } from "react";
import styled from "styled-components";
import { cancellBakingProgram, finishPreparingBakingProgram, startBakingBakingProgram } from "../../services/BakingProgramService";
import ErrorDialogue from "./ErrorDialogue";
import { DateUtils } from "../../services/Utils";
import SuperDetailsModal from "./SuperDetailsModal/SuperDetailsModal";
import StartPreparingResponse from "../../models/Responses/StartPreparing/StartPreparingResponse";
import BakingProgramResponse from "../../models/Responses/BakingProgramResponse";
import { BakingProgramStatus } from "../../models/Enums/BakingProgramStatus";

export interface PreparingListProps {
    preparingBakingPrograms: BakingProgramResponse[];
    preparingInProgress: StartPreparingResponse | null;
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
    const [showPreparingDetails, setShowPreparingDetails] = useState(false);
    const [selectedBakingProgram, setSelectedBakingProgram] = useState<BakingProgramResponse | null>(null);

    function rowClickHandler(bakingProgram: BakingProgramResponse) {
        setSelectedBakingProgram(bakingProgram);
        setShowDetails(true);
    }

    function finishClickHandler(bakingProgramId: string) {
        finishPreparingBakingProgram(bakingProgramId)
            .then(() => {
                refreshView();
            })
            .catch((error) => {
                console.warn('error')
                console.warn(error)
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

    function preparingClickHandler() {
        setShowPreparingDetails(true);

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
                    {preparingInProgress && <TableRowStyled onClick={() => setShowPreparingDetails(true)}>
                        <TableCell component="th" scope="row" sx={{ display: 'flex', flexDirection: "row" }}> {preparingInProgress.ovenCode}</TableCell>
                        <TableCell align="right">{DateUtils.getMeaningfulDate(preparingInProgress.bakingProgrammedAt)}</TableCell>
                        <AvailableActionsTableCell>
                            <PrepareButton onClick={() => cancelClickHandler(preparingInProgress.id)}>Cancel</PrepareButton>
                            <PrepareButton onClick={() => finishClickHandler(preparingInProgress.id)}>Finish</PrepareButton>
                        </AvailableActionsTableCell>
                    </TableRowStyled>}  
                    {preparingBakingPrograms
                        .filter(bakingProgram => bakingProgram.status == BakingProgramStatus.Prepared)
                        .map((bakingProgram) => (
                            <TableRowStyled key={bakingProgram.id}>
                                <TableCell component="th" scope="row" onClick={() => rowClickHandler(bakingProgram)}>{bakingProgram.ovenCode}</TableCell>
                                <TableCell align="right" onClick={() => rowClickHandler(bakingProgram)}>{DateUtils.getMeaningfulDate(bakingProgram.bakingProgrammedAt)}</TableCell>
                                <AvailableActionsTableCell>
                                   <PrepareButton onClick={() => startBakingClickHandler(bakingProgram.id)}>Start baking</PrepareButton>
                                </AvailableActionsTableCell>
                            </TableRowStyled>
                        ))}
                </TableBody>
            </Table>
            <ErrorDialogue open={isErrorDialogOpen} errorMessage={errorMessage} onClose={() => setIsErrorDialogOpen(false)} />
            <SuperDetailsModal isOpen={showPreparingDetails} onClose={() => setShowPreparingDetails(false)} bakingProgram={preparingInProgress} />
        </TableContainer>
    );
};

export default PreparingList;
