import { FC, useEffect, useState } from "react";
import styled from "styled-components";
import UserDetailsResponse from "../../../models/Responses/User/UserDetailsResponse";
import { getDetails } from "../../../services/UserService";
import UserBakingProgramsList from "./UserBakingProgramsList";

type UserDetailsViewProps = {
    userId: string | null;
};

const Container = styled.div`
    padding: 8px;
    display: flex;
    flex-direction: column;
`;

const Label = styled.p`
    font-size: 24px;
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
const UserDetailsView : FC<UserDetailsViewProps> = ({ userId }) => {
    const [user, setUserDetails] = useState<UserDetailsResponse | null>(null);

    useEffect(() => {
        if (!userId) return;

        getDetails(userId).then((response: UserDetailsResponse | null) => {
            setUserDetails(response);
        });

    }, [userId]);


    if (userId === null || user === null) {
        return <p>Nothing to show</p>;
    }

    return (
        <Container>
            <Label>User details</Label>
            <PropertyLabel>Id</PropertyLabel>
            <PropertyValue>{user.id}</PropertyValue>
            <PropertyLabel>Username</PropertyLabel>
            <PropertyValue>{user.username}</PropertyValue>
            <PropertyLabel>Role</PropertyLabel>
            <PropertyValue>{user.role}</PropertyValue>
            <PropertyLabel>Status</PropertyLabel>
            <PropertyValue>{user.active ? 'Active': 'Inactive'}</PropertyValue>
            <UserBakingProgramsList bakingPrograms={user.preparedPrograms} />
        </Container>
    );


};

export default UserDetailsView;