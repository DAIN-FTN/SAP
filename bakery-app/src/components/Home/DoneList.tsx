import Paper from "@mui/material/Paper";
import Table from "@mui/material/Table";
import TableBody from "@mui/material/TableBody";
import TableCell from "@mui/material/TableCell";
import TableContainer from "@mui/material/TableContainer";
import TableHead from "@mui/material/TableHead";
import TableRow from "@mui/material/TableRow";
import { FC, useState } from "react";
import styled from "styled-components";
import { cancellBakingProgram, finishBakingProgram } from "../../services/BakingProgramService";
import ErrorDialogue from "./ErrorDialogue";
import DetailsModal from "./DetailsModal";
import BakingProgramResponse from "../../models/Responses/BakingProgramResponse";

export interface DoneListProps {
    doneBakingPrograms: BakingProgramResponse[];
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

const DoneList: FC<{ props: DoneListProps }> = ({ props: { doneBakingPrograms, refreshView } }) => {
    const [open, setOpen] = useState(false);
    const [errorMessage, setErrorMessage] = useState("");
    const [showDetails, setShowDetails] = useState(false);
    const [selectedBakingProgram, setSelectedBakingProgram] = useState<BakingProgramResponse | null>(null);

    function rowClickHandler(bakingProgram: BakingProgramResponse) {
        setSelectedBakingProgram(bakingProgram);
        setShowDetails(true);
    }

    function finishClickHandler(bakingProgramId: string) {
        finishBakingProgram(bakingProgramId)
            .then(() => {
                refreshView();
            });
    }

    if (doneBakingPrograms.length === 0) {
        return <ErrorMessage>No baking programs to show.</ErrorMessage>;
    }

    return (
        <TableContainer component={Paper}>
            <Table sx={{ minWidth: 150 }} size="small" aria-label="simple table">
                <TableHead>
                    <TableRow>
                        <TableCell>Oven code</TableCell>
                        <TableCell align="right" sx={{ width: '110px' }}>Available action</TableCell>
                    </TableRow>
                </TableHead>
                <TableBody>
                    {doneBakingPrograms.map((bakingProgram) => (
                        <TableRowStyled key={bakingProgram.id} sx={{ '&:last-child td, &:last-child th': { border: 0 } }} >
                            <TableCell component="th" scope="row" onClick={() => rowClickHandler(bakingProgram)}>{bakingProgram.ovenCode}</TableCell>
                            <AvailableActionsTableCell>
                                <PrepareButton onClick={() => finishClickHandler(bakingProgram.id)}>Finish</PrepareButton>
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

export default DoneList;
