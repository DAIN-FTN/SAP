import Table from "@mui/material/Table";
import TableCell from "@mui/material/TableCell";
import TableHead from "@mui/material/TableHead";
import TableRow from "@mui/material/TableRow";
import { FC, useEffect, useState } from "react";
import styled from "styled-components";
import UserResponse from "../../../models/Responses/User/UserResponse";
import { getAll, update } from "../../../services/UserService";
import { CircularProgress, IconButton, Paper, TableBody, TableContainer, TextField } from "@mui/material";
import AddOutlinedIcon from '@mui/icons-material/AddOutlined';import { NavLink } from "react-router-dom";
import ToggleOffOutlinedIcon from '@mui/icons-material/ToggleOffOutlined';
import ToggleOnOutlinedIcon from '@mui/icons-material/ToggleOnOutlined';
import RegisterRequest from "../../../models/Requests/Users/RegisterRequest";
export interface UsersListProps {
    setSelectedUserId: (userId: string) => void;
    setIsCreateMode: (isCreateMode: boolean) => void;
    setEditedUser: (user: RegisterRequest | null) => void;
}

const filterUsers = (users: UserUiResponse[], filter: string) => {
    return users.filter(u => u.username.toLowerCase().includes(filter.toLowerCase()));
}

const Container = styled.div`
    padding: 8px;
    display: flex;
    flex-direction: column;
`;

const LabelIconWrapper = styled.div`
    display: flex;
    flex-direction: row;
    align-items: center;
`;

const Label = styled.p`
    font-size: 24px;
`;

const SearchWrapper = styled.div`
    display: flex;
    flex-direction: row;
`;


const ActionButton = styled.div`
display: flex;
flex-direction: column;
border: none;
padding: 15px 10px 10px 10px;
box-sizing: border-box;
align-items: center;
text-align: center;

&:hover {
    cursor: pointer;
}
`;

const TableRowStyled = styled(TableRow)`
    cursor: pointer;
    &:hover {
        background-color: #f5f5f5;
    }
`;

const AddIconCustom = styled(AddOutlinedIcon)`
    color: #DC3F3F;
    &:hover {
        background-color: #f5f5f5;
    }
`
export type UserUiResponse = UserResponse & { isLoading?: boolean };

const UsersList: FC<UsersListProps> = ({ setSelectedUserId, setIsCreateMode, setEditedUser}) => {
    const [userResults, setUserResults] = useState<UserUiResponse[]>([]);
    const [initialUserResults, setInitalUserResults] = useState<UserResponse[]>([]);

    function rowClickedHandler (userId: string) {
        setSelectedUserId(userId);
        setIsCreateMode(false);
        setEditedUser(null);
    }

    useEffect(() => {
        getAll().then((users) => {
            setUserResults(users);
            setInitalUserResults(users);
        });
    }, []);

    async function toggleUserActivity(event: any, userId: string) {
        event.stopPropagation();

        const user = userResults.find(u => u.id === userId);
        if (user) {
            user.isLoading = true;
            user.active = !user.active;
            setUserResults([...userResults]);

            //arbitrary delay to show loading
            await new Promise(resolve => setTimeout(resolve, 1000));
            await update(userId, {
                active: user.active,
                username: user.username,
                roleId: user.roleId
            });

            user.isLoading = false;
            setUserResults([...userResults]);
        }
    }

    function handleAddUserClick() {
        setIsCreateMode(true);
        setEditedUser(null);
    }


    return (
        <Container>
            <LabelIconWrapper>
                <Label>Users</Label>
                <ActionButton>
                    <AddIconCustom onClick={() => handleAddUserClick()}></AddIconCustom>
                </ActionButton>
            </LabelIconWrapper>
            <SearchWrapper>
                <TextField id="standard-basic" label="Name" variant="standard" fullWidth sx={{ paddingRight: '0px' }}
                    onChange={({target}) =>setUserResults(filterUsers(initialUserResults, target.value))} />
            </SearchWrapper>
            {userResults.length === 0 && <p>No users meet the search criteria</p>}
            {userResults.length > 0 && <TableContainer component={Paper}>
                <Table sx={{ minWidth: 150 }} size="small" aria-label="simple table">
                    <TableHead>
                        <TableRow>
                            <TableCell>Username</TableCell>
                            <TableCell>Role</TableCell>
                            <TableCell>Status</TableCell>
                            <TableCell>Toggle activity</TableCell>
                        </TableRow>
                    </TableHead>
                    <TableBody>
                        {userResults.map((user) => (
                            <TableRowStyled key={user.id} sx={{ '&:last-child td, &:last-child th': { border: 0 } }} onClick={() => rowClickedHandler(user.id)}>
                                <TableCell component="th" scope="row">{user.username}</TableCell>
                                <TableCell component="th" scope="row">{user.role}</TableCell>
                                {!user.isLoading && <TableCell component="th" scope="row">{user.active ? 'Active' : 'Inactive'}</TableCell>}
                                {user.isLoading && <TableCell component="th" scope="row"><CircularProgress /></TableCell>}
                                {user.active && <TableCell component="th" scope="row"><IconButton onClick={event => toggleUserActivity(event, user.id)}>
                                    <ToggleOffOutlinedIcon /> </IconButton></TableCell>}
                                {!user.active &&
                                    <TableCell component="th" scope="row"><IconButton onClick={event => toggleUserActivity(event, user.id)}>
                                        <ToggleOnOutlinedIcon /> </IconButton></TableCell>}
                            </TableRowStyled>
                        ))}
                    </TableBody>
                </Table>
            </TableContainer>}
        </Container>
    );
};

export default UsersList;
