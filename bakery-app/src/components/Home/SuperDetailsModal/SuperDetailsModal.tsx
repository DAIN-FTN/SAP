import DialogTitle from '@mui/material/DialogTitle';
import Dialog from '@mui/material/Dialog';
import styled from 'styled-components';
import { DateUtils } from '../../../services/Utils';
import { FC } from 'react';
import LocationsToPrepareFromList from './LocationsToPrepareFromList';
import StartPreparingResponse from '../../../models/Responses/StartPreparing/StartPreparingResponse';

export interface SuperDetailsModalProps {
    isOpen: boolean;
    bakingProgram: StartPreparingResponse | null;
    onClose: Function;
}

const Container = styled.div`
    padding: 8px;
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
    width: 50%;
`;

const PanelLabel = styled.h4`
    
`;

const DialogStyled = styled(Dialog)`
    & .MuiPaper-elevation { // TODO: this "&" is not working and is being propagated to the child elements
        max-width: 900px;
        width: 900px;
    }
`;

const SuperDetailsModal: FC<SuperDetailsModalProps> = ({ isOpen, bakingProgram, onClose }) => {

    const handleClose = () => {
        onClose();
    };

    if (!isOpen) return null;

    if (!bakingProgram)
        return <ErrorMessage>Can not display details for the selected baking program because it's "null".</ErrorMessage>;

    return (
        <DialogStyled onClose={handleClose} open={isOpen}>
            <DetailsHeader>Preparing program details</DetailsHeader>
            <Container>
                <Panel>
                    <PanelLabel>Basic details</PanelLabel>
                    {/* <PropertyLabel>ID</PropertyLabel>
                    <PropertyValue>{bakingProgram.id}</PropertyValue> */}
                    <PropertyLabel>Oven code</PropertyLabel>
                    <PropertyValue>{bakingProgram.ovenCode}</PropertyValue>
                    <PropertyLabel>Baking programmed at</PropertyLabel>
                    <PropertyValue>{DateUtils.getMeaningfulDate(bakingProgram.bakingProgrammedAt)}</PropertyValue>
                    {/* <PropertyValue>{bakingProgram.bakingProgrammedAt.toString()}</PropertyValue> */}
                    <PropertyLabel>Baking temperature</PropertyLabel>
                    <PropertyValue>{bakingProgram.bakingTempInC}°C</PropertyValue>
                    <PropertyLabel>Baking time</PropertyLabel>
                    <PropertyValue>{bakingProgram.bakingTimeInMins} minutes</PropertyValue>
                </Panel>
                <Panel>
                    <LocationsToPrepareFromList locations={bakingProgram.locations}/>
                </Panel>
            </Container>
        </DialogStyled>
    );
}

export default SuperDetailsModal;