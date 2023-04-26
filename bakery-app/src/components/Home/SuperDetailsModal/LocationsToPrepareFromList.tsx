import styled from 'styled-components';
import TableRow from '@mui/material/TableRow';
import { FC } from 'react';
import TableContainer from '@mui/material/TableContainer';
import Paper from '@mui/material/Paper';
import Table from '@mui/material/Table';
import TableHead from '@mui/material/TableHead';
import TableCell from '@mui/material/TableCell';
import TableBody from '@mui/material/TableBody';
import LocationsToPrepareFromListItem from './LocationsToPrepareFromListItem';
import LocationToPrepareFromResponse from '../../../models/Responses/StartPreparing/LocationToPrepareFromResponse';

export interface LocationsToPrepareFromListProps {
    locations: LocationToPrepareFromResponse[];
}

const Container = styled.div`
    display: flex;
    flex-direction: column;

    & .MuiPaper-elevation { // TODO: this "&" is not working and is being propagated to the child elements
        max-width: auto !important;
        width: auto !important;
    }
`;

const PanelLabel = styled.h4`
    
`;

const LocationsToPrepareFromList: FC<LocationsToPrepareFromListProps> = ({ locations }) => {
    return (
        <Container>
            <PanelLabel>Locations to prepare from</PanelLabel>
            <TableContainer component={Paper}>
                <Table sx={{ minWidth: 150 }} size="small" aria-label="simple table">
                    <TableHead>
                        <TableRow>
                            <TableCell>Location code</TableCell>
                            <TableCell align="right">Number of types of products</TableCell>
                        </TableRow>
                    </TableHead>
                    <TableBody>
                        {locations.map((location: LocationToPrepareFromResponse) => (
                            <LocationsToPrepareFromListItem key={location.locationId} location={location} />
                        ))}
                    </TableBody>
                </Table>
            </TableContainer>
        </Container>
    );
}

export default LocationsToPrepareFromList;