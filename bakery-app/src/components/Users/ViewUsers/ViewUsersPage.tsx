import React, { FC, useEffect, useState } from 'react';
import styled from 'styled-components';
import UsersList from './UsersList';
import UserDetailsView from './UserDetailsView';
import { useParams } from 'react-router-dom';

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
interface UsersProps { }

const ViewUsersPage: FC<UsersProps> = () => {
    const {userId} = useParams() 
    const [selectedUserId, setUserId] = useState<string | null>(userId? userId : null);

    return <Container>
        <Panel>
            <UsersList  setSelectedUserId={setUserId} />
        </Panel>
        <Panel>
            <UserDetailsView userId={selectedUserId} />
        </Panel>
    </Container>
};

export default ViewUsersPage;
