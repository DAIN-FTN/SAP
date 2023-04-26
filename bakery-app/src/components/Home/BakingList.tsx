import Paper from "@mui/material/Paper";
import Table from "@mui/material/Table";
import TableBody from "@mui/material/TableBody";
import TableCell from "@mui/material/TableCell";
import TableContainer from "@mui/material/TableContainer";
import TableHead from "@mui/material/TableHead";
import TableRow from "@mui/material/TableRow";
import { FC, useState } from "react";
import styled from "styled-components";
import ErrorDialogue from "./ErrorDialogue";
import DetailsModal from "./DetailsModal";
import BakingListItem from "./BakingListItem";
import BakingProgramResponse from "../../models/Responses/BakingProgramResponse";
export interface BakingListProps {
    bakingBakingPrograms: BakingProgramResponse[];
    refreshView: Function;
}

const ErrorMessage = styled.p`
    font-style: italic;
`;

const BakingList: FC<{ props: BakingListProps }> = ({ props: { bakingBakingPrograms, refreshView } }) => {
    const [open, setOpen] = useState(false);
    const [errorMessage, setErrorMessage] = useState("");
    const [showDetails, setShowDetails] = useState(false);
    const [selectedBakingProgram, setSelectedBakingProgram] = useState<BakingProgramResponse | null>(null);

    function rowClickHandler(bakingProgram: BakingProgramResponse) {
        setSelectedBakingProgram(bakingProgram);
        setShowDetails(true);
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
                        <TableCell align="right">Supposed end time</TableCell>
                        <TableCell align="right" sx={{ width: '110px' }}>Time left</TableCell>
                    </TableRow>
                </TableHead>
                <TableBody>
                    {bakingBakingPrograms.map((bakingBakingProgram) => (
                        <BakingListItem
                            key={bakingBakingProgram.id}
                            bakingBakingProgram={bakingBakingProgram} 
                            refreshView={refreshView} 
                            rowClickHandler={
                                () => rowClickHandler(bakingBakingProgram)
                            } />
                    ))}
                </TableBody>
            </Table>
            <ErrorDialogue open={open} errorMessage={errorMessage} onClose={() => setOpen(false)} />
            <DetailsModal isOpen={showDetails} onClose={() => setShowDetails(false)} bakingProgram={selectedBakingProgram} />
        </TableContainer>
    );
};

export default BakingList;
