import DialogTitle from '@mui/material/DialogTitle';
import Dialog from '@mui/material/Dialog';
import styled from 'styled-components';
import { BakingTimeSlot as BakingProgram, BakingProgramStatus } from '../../models/BakingTimeSlot';
import { DateUtils } from '../../services/Utils';
import TableContainer from '@mui/material/TableContainer';
import Paper from '@mui/material/Paper';
import Table from '@mui/material/Table';
import TableHead from '@mui/material/TableHead';
import TableRow from '@mui/material/TableRow';
import TableCell from '@mui/material/TableCell';
import TableBody from '@mui/material/TableBody';

export interface DetailsModalProps {
    isOpen: boolean;
    bakingProgram: BakingProgram | null;
    onClose: Function;
}

const Container = styled.div`
    padding: 24px;
    display: flex;
    flex-direction: row;
`;

const DetailsHeader = styled(DialogTitle)`
    background-color: #dc3f3f;
    color: #fff;
`;

const ErrorMessage = styled.p`
    padding: 0px 24px;
`;

const PropertyLabel = styled.p`
    color: rgba(0, 0, 0, 0.6);
    font-family: "Roboto","Helvetica","Arial",sans-serif;
    font-weight: 400;
    font-size: 0.8rem;
    line-height: 1em;
    letter-spacing: 0.00938em;

    margin: 0px;
`;

const PropertyValue = styled.p`
    font-size: 22px;
    color: black;
    margin-top: 0px;
`;

const Panel = styled.div`
    padding: 16px;
`;

const PanelLabel = styled.h4`
    
`;

const DialogStyled = styled(Dialog)`
    /* .MuiPaper-elevation {
        width: 900px;
        max-width: 900px;
    } */
`;

const TableRowStyled = styled(TableRow)`
    cursor: pointer;

    &:last-child td + &:last-child th {
        border: 0;
    }

    &:hover {
        background-color: #f5f5f5;
    }
`;

export default function DetailsModal(props: DetailsModalProps) {
    const { onClose, bakingProgram, isOpen } = props;

    const handleClose = () => {
        onClose(bakingProgram);
    };

    if (!isOpen) return null;

    if (!bakingProgram)
        return <ErrorMessage>Can not display details for the selected baking program because it's "null".</ErrorMessage>;

    return (
        <DialogStyled onClose={handleClose} open={isOpen}>
            <DetailsHeader>Baking program details</DetailsHeader>
            <Container>
                <Panel>
                    <PanelLabel>Basic details</PanelLabel>
                    <PropertyLabel>ID</PropertyLabel>
                    <PropertyValue>{bakingProgram.id}</PropertyValue>
                    <PropertyLabel>Oven code</PropertyLabel>
                    <PropertyValue>{bakingProgram.ovenCode}</PropertyValue>
                    <PropertyLabel>Created at</PropertyLabel>
                    <PropertyValue>{DateUtils.getMeaningfulDate(bakingProgram.createdAt)}</PropertyValue>
                    <PropertyLabel>Can be prepared at</PropertyLabel>
                    <PropertyValue>{DateUtils.getMeaningfulDate(bakingProgram.canBePreparedAt)}</PropertyValue>
                    <PropertyLabel>Can be baked at</PropertyLabel>
                    <PropertyValue>{DateUtils.getMeaningfulDate(bakingProgram.canBeBakedAt)}</PropertyValue>
                    <PropertyLabel>Baking programmed at</PropertyLabel>
                    <PropertyValue>{DateUtils.getMeaningfulDate(bakingProgram.bakingProgrammedAt)}</PropertyValue>
                    <PropertyLabel>Baking started at</PropertyLabel>
                    <PropertyValue>{DateUtils.getMeaningfulDate(bakingProgram.bakingStartedAt!)}</PropertyValue>
                    <PropertyLabel>Status</PropertyLabel>
                    <PropertyValue>{BakingProgramStatus[bakingProgram.status!]}</PropertyValue>
                    <PropertyLabel>Baking temperature</PropertyLabel>
                    <PropertyValue>{bakingProgram.bakingTempInC}°C</PropertyValue>
                    <PropertyLabel>Baking time</PropertyLabel>
                    <PropertyValue>{bakingProgram.bakingTimeInMins} minutes</PropertyValue>
                </Panel>
                {/* <Panel>
                    <PanelLabel>Products</PanelLabel>
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
                                {preparingBakingPrograms.map((bakingProgram) => (
                                    <TableRowStyled key={bakingProgram.id}>
                                        <TableCell component="th" scope="row" onClick={() => rowClickHandler(bakingProgram)}>{bakingProgram.ovenCode}</TableCell>
                                        <TableCell align="right" onClick={() => rowClickHandler(bakingProgram)}>{DateUtils.getMeaningfulDate(bakingProgram.bakingProgrammedAt)}</TableCell>
                                        <AvailableActionsTableCell>
                                            {(bakingProgram.status === BakingProgramStatus.Preparing)
                                                && <PrepareButton onClick={() => finishClickHandler(bakingProgram.id)}>Finish</PrepareButton>}
                                            {(bakingProgram.status === BakingProgramStatus.Preparing)
                                                && <PrepareButton onClick={() => cancelClickHandler(bakingProgram.id)}>Cancel</PrepareButton>}
                                            {(bakingProgram.status === BakingProgramStatus.Prepared)
                                                && <PrepareButton onClick={() => startBakingClickHandler(bakingProgram.id)}>Start baking</PrepareButton>}
                                        </AvailableActionsTableCell>
                                    </TableRowStyled>
                                ))}
                            </TableBody>
                        </Table>
                    </TableContainer>
                </Panel> */}
            </Container>
        </DialogStyled>
    );
}
