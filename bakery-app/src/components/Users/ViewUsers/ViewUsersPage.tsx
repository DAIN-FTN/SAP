import React, { FC } from 'react';
import styled from 'styled-components';
import UsersList from './UsersList';

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

const ViewUsersPage: FC<UsersProps> = () => (
    <Container>
        <Panel>
            <UsersList />
        </Panel>
        {/* <Panel>
            <OrderDetailsView orderId={selectedOrderId} />
        </Panel> */}
    </Container>
);

export default ViewUsersPage;
