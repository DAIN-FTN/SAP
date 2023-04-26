import styled from 'styled-components';
import TableRow from '@mui/material/TableRow';
import { FC, useState } from 'react';
import TableCell from '@mui/material/TableCell';
import ProductToPrepareList from './ProductToPrepareList';
import LocationToPrepareFromResponse from '../../../models/Responses/StartPreparing/LocationToPrepareFromResponse';

export interface LocationsToPrepareFromListItemProps {
    location: LocationToPrepareFromResponse;
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
const LocationsToPrepareFromListItem: FC<LocationsToPrepareFromListItemProps> = ({ location }) => {
    const [showProductsToPrepare, setShowProductsToPrepare] = useState(false);

    return (
        <>
            <TableRowStyled key={location.locationId} onClick={() => setShowProductsToPrepare(!showProductsToPrepare)}>
                <TableCell component="th" scope="row" >{location.locationCode}</TableCell>
                <TableCell align="right">{location.products.length}</TableCell>
            </TableRowStyled>
            {showProductsToPrepare &&
                <TableRowStyled>
                    <TableCell colSpan={2} sx={{padding: '4px'}}>
                        <ProductToPrepareList key={location.locationId} products={location.products} />
                    </TableCell>
                </TableRowStyled>}
        </>
    );
}

export default LocationsToPrepareFromListItem;