import { FC, useState } from "react";
import RegisterRequest from "../../../models/Requests/Users/RegisterRequest";
import styled from "styled-components";
import { Alert, MenuItem, Select, SxProps, TextField } from "@mui/material";

type CreateUserProps = {
    registerUserRequestData?: RegisterRequest | null;
};

const Label = styled.p`
    font-size: 24px;
`;

const textFieldStyleProps: SxProps = {
    marginTop: '4px',
    marginBottom: '4px'
};

const alertStyleProps: SxProps = {
    marginBottom: '12px',
    width: 'fit-content'
};

const CreateUser: FC<CreateUserProps> = ({registerUserRequestData}) => {
    const [username, setUsername] = useState<string>(registerUserRequestData?.username || "");
    const [password, setPassword] = useState<string>(registerUserRequestData?.password || "");
    const [role, setRole] = useState<string>(registerUserRequestData?.roleId || "");

    const [usernameError, setUsernameError] = useState<string | null>(null);
    const [passwordError, setPasswordError] = useState<string | null>(null);
    const [roleError, setRoleError] = useState<string | null>(null);


    function setUsernameHandler(username: string) {
    };

    function setPasswordHandler(password: string) {
    };

    function setRoleHandler(role: string) {
    };
    
    return (
        <>
            <Label>User details</Label>

            <TextField id="standard-basic" label="Username" variant="standard" sx={textFieldStyleProps} fullWidth onChange={(e) => setUsernameHandler(e.target.value)} />
            {usernameError && <Alert severity="error" sx={alertStyleProps}>{usernameError}</Alert>}

            <TextField id="standard-basic" label="Initial password" variant="standard" sx={textFieldStyleProps} fullWidth onChange={(e) => setPasswordHandler(e.target.value)} />
            {passwordError && <Alert severity="error" sx={alertStyleProps}>{passwordError}</Alert>}

            <Select
                labelId="demo-simple-select-filled-label"
                id="demo-simple-select-filled"
                variant="standard" 
                sx={{marginTop: '8px', marginBottom: '8px'}}
                // value={''}
                // onChange={}
                //TODO: get roles from backend
            >
                <MenuItem value={"Admin"}>Admin</MenuItem>
                <MenuItem value={"Staff"}>Staff</MenuItem>
            </Select>

            {/* <TextField id="standard-basic" label="Telephone" variant="standard" sx={textFieldStyleProps} fullWidth onChange={(e) => setRoleHandler(e.target.value)} />
            {roleError && <Alert severity="error" sx={alertStyleProps}>{roleError}</Alert>} */}
        </>
    );
};

export default CreateUser;