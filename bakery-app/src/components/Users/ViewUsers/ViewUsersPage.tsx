import React, { FC, useEffect, useState } from 'react';
import styled from 'styled-components';
import UsersList from './UsersList';
import UserDetailsView from './UserDetailsView';
import { useParams } from 'react-router-dom';
import CreateUser from '../CreateUser/CreateUser';
import RegisterRequest from '../../../models/Requests/Users/RegisterRequest';
import { Box, CircularProgress } from '@mui/material';

const Container = styled.div`
    width: 100%;
    padding: 36px;
    display: flex;
    flex-direction: row;
    -webkit-box-shadow: 0px 2px 14px 0px rgba(122,122,122,1);
    -moz-box-shadow: 0px 2px 14px 0px rgba(122,122,122,1);
    box-shadow: 0px 2px 14px 0px rgba(122,122,122,1);
    background-color: white;
    margin: 48px;
    flex-wrap: wrap;
`;

const Panel = styled.div`
    width: 50%;
    padding: 24px;
    display: flex;
    flex-direction: column;
    box-sizing: border-box;
`;

const LoadingPanel = styled.div`
    width: 100%;
    padding: 24px;
    display: flex;
    flex-direction: column;
    box-sizing: border-box;
    align-items: center;
    justify-content: center;
`;

interface UsersProps { }

const ViewUsersPage: FC<UsersProps> = () => {
    const {userId} = useParams() 
    const [selectedUserId, setUserId] = useState<string | null>(userId? userId : null);
    const [editedUser, setEditedUser] = useState<RegisterRequest | null>(null);
    const [isCreateMode, setIsCreatemode] = useState<boolean>(false);
    const [isLoading, setIsLoading] = useState<boolean>(false);

    return <Container>
        {isLoading && <LoadingPanel><Box><CircularProgress color="warning" /></Box></LoadingPanel>}
        {
            !isLoading &&
            <Panel>
                <UsersList setSelectedUserId={setUserId} setIsCreateMode={setIsCreatemode} setEditedUser={setEditedUser} />
            </Panel>
        }
        {
            !isLoading &&
            <Panel>
                {(editedUser !== null || isCreateMode ) && <CreateUser setIsLoading={setIsLoading} registerUserRequestData={editedUser} />}
                {editedUser === null && !isCreateMode && <UserDetailsView setEditedUser={setEditedUser} userId={selectedUserId} />}
            </Panel>
        }
    </Container>
};

export default ViewUsersPage;
