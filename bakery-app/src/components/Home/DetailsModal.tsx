import DialogTitle from '@mui/material/DialogTitle';
import Dialog from '@mui/material/Dialog';
import styled from 'styled-components';
import { BakingTimeSlot as BakingProgram } from '../../models/BakingTimeSlot';
import { DateUtils } from '../../services/Utils';

export interface DetailsModalProps {
    isOpen: boolean;
    bakingProgram: BakingProgram | null;
    onClose: Function;
}

const Container = styled.div`
    padding: 24px;
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
    line-height: 1.4375em;
    letter-spacing: 0.00938em;

    margin: 0px;
`;

const PropertyValue = styled.p`
    font-size: 22px;
    color: black;
    margin-top: 0px;
`;

export default function DetailsModal(props: DetailsModalProps) {
    const { onClose, bakingProgram, isOpen: open } = props;

    const handleClose = () => {
        onClose(bakingProgram);
    };

    return (
        bakingProgram && <Dialog onClose={handleClose} open={open}>
            <DetailsHeader>Baking program details</DetailsHeader>
            <Container>
                <PropertyLabel>ID</PropertyLabel>
                <PropertyValue>{bakingProgram.id}</PropertyValue>
                <PropertyLabel>Oven code</PropertyLabel>
                <PropertyValue>{bakingProgram.ovenCode}</PropertyValue>
                <PropertyLabel>Baking programmed at</PropertyLabel>
                <PropertyValue>{DateUtils.isToday(bakingProgram.bakingProgrammedAt)}</PropertyValue>
                <PropertyLabel>Baking started at</PropertyLabel>
                <PropertyValue>{DateUtils.isToday(bakingProgram.bakingStartedAt)}</PropertyValue>
                <PropertyLabel>Created at</PropertyLabel>
                <PropertyValue>{DateUtils.isToday(bakingProgram.createdAt)}</PropertyValue>
                <PropertyLabel>Status</PropertyLabel>
                <PropertyValue>{bakingProgram.status}</PropertyValue>
                <PropertyLabel>Baking temperature</PropertyLabel>
                <PropertyValue>{bakingProgram.bakingTempInC}°C</PropertyValue>
                <PropertyLabel>Baking time</PropertyLabel>
                <PropertyValue>{bakingProgram.bakingTimeInMins} minutes</PropertyValue>
            </Container>
        </Dialog>
    );
}
