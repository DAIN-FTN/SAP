import Table from "@mui/material/Table";
import TableCell from "@mui/material/TableCell";
import TableHead from "@mui/material/TableHead";
import TableRow from "@mui/material/TableRow";
import { FC, useEffect, useState } from "react";
import styled from "styled-components";
import UserResponse from "../../../models/Responses/User/UserResponse";
import { getAll } from "../../../services/UserService";
import { Paper, TableBody, TableContainer, TextField } from "@mui/material";
import { DateUtils } from "../../../services/Utils";

export interface UsersListProps {
    setSelectedUserId?: (userId: string) => void;
}

const Container = styled.div`
    padding: 8px;
    display: flex;
    flex-direction: column;
`;

const Label = styled.p`
    font-size: 24px;
`;

const SearchWrapper = styled.div`
    display: flex;
    flex-direction: row;
`;

const TableRowStyled = styled(TableRow)`
    cursor: pointer;
    &:hover {
        background-color: #f5f5f5;
    }
`;

const UsersList: FC<UsersListProps> = ({ setSelectedUserId }) => {
    const [userResults, setUserResults] = useState<UserResponse[]>([]);

    useEffect(() => {
        getAll().then((users) => {
            setUserResults(users);
        });
    }, []);


    return (
        <Container>
            <Label>Users</Label>
            <SearchWrapper>
                <TextField id="standard-basic" label="Name" variant="standard" fullWidth sx={{ paddingRight: '0px' }}
                    onChange={(e) => console.log('changed)')} />
            </SearchWrapper>
            {userResults.length === 0 && <p>No users meet the search criteria</p>}
            {userResults.length > 0 && <TableContainer component={Paper}>
                <Table sx={{ minWidth: 150 }} size="small" aria-label="simple table">
                    <TableHead>
                        <TableRow>
                            <TableCell>Username</TableCell>
                            <TableCell>Role</TableCell>
                            <TableCell>Status</TableCell>
                        </TableRow>
                    </TableHead>
                    <TableBody>
                        {userResults.map((user) => (
                            <TableRowStyled key={user.id} sx={{ '&:last-child td, &:last-child th': { border: 0 } }} onClick={() => setSelectedUserId!(user.id)}>
                                <TableCell component="th" scope="row">{user.username}</TableCell>
                                <TableCell component="th" scope="row">{user.role}</TableCell>
                                <TableCell component="th"  scope="row">{user.active?'Active': 'Inactive'}</TableCell>
                            </TableRowStyled>
                        ))}
                    </TableBody>
                </Table>
            </TableContainer>}
        </Container>
    );
};

export default UsersList;
