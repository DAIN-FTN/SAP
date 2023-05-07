import { FC, useEffect, useState } from "react";
import PreparedBakingProgramResponse from "../../../models/Responses/User/PreparedBakingProgramResponse";
import styled from "styled-components";
import { Paper, Table, TableBody, TableCell, TableContainer, TableHead, TableRow } from "@mui/material";
import { DateUtils } from "../../../services/Utils";

type UserBakingProgramsListProps = {
    bakingPrograms: PreparedBakingProgramResponse[];
};

const Container = styled.div`
`;

const TableRowStyled = styled(TableRow)`
    cursor: pointer;
    &:hover {
        background-color: #f5f5f5;
    }
`;

const Label = styled.p`
    margin-top: 5px;
    font-size: 24px;
`;


const UserBakingProgramsList: FC<UserBakingProgramsListProps> = ({ bakingPrograms }) => {
    const [componentHeight, setComponentHeight] = useState(0);
    
    useEffect(() => {
        const updateHeight = () => {
          const windowHeight = window.innerHeight;
          const desiredHeight = windowHeight * 0.15;
          setComponentHeight(desiredHeight);
        };
    
        window.addEventListener('resize', updateHeight);
        updateHeight();
    
        return () => {
          window.removeEventListener('resize', updateHeight);
        };
      }, []);

    if (bakingPrograms.length === 0) {
        return <p>No prepared baking programs to show</p>;
    }
        
    return (
        <Container>
            <Label>Prepared baking programs</Label>
            <TableContainer component={Paper} style={{ overflow: 'auto', maxHeight: componentHeight }}>
                <Table sx={{ minWidth: 150}} size="small">
                        <TableHead>
                            <TableRow>
                                <TableCell>Baking program code</TableCell>
                                <TableCell>Oven code</TableCell>
                                <TableCell align="right">Started preparing at</TableCell>
                            </TableRow>
                        </TableHead>
                        <TableBody>
                            {bakingPrograms.map((bakingProgram) => (
                                <TableRowStyled key={bakingProgram.id} sx={{ '&:last-child td, &:last-child th': { border: 0 } }}>
                                    <TableCell component="th" scope="row">{bakingProgram.code}</TableCell>
                                    <TableCell >{bakingProgram.ovenCode}</TableCell>
                                    <TableCell align="right">{DateUtils.getMeaningfulDate(new Date(bakingProgram.bakingStartedAt))}</TableCell>
                                </TableRowStyled>
                            ))}
                        </TableBody>
                    </Table>
            </TableContainer>

            
        </Container>
    );
};

export default UserBakingProgramsList;