import { FC, useState } from "react";
import RegisterRequest from "../../../models/Requests/Users/RegisterRequest";
import styled from "styled-components";
import { Alert, FormControl, IconButton, Input, InputAdornment, InputLabel, MenuItem, Select, SxProps, TextField } from "@mui/material";
import { Visibility, VisibilityOff } from "@mui/icons-material";

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

const selectStyleProps: SxProps = {
    marginTop: '12px',
    marginBottom: '12px'
};

// const InputContainer = styled.div`
//     display: flex;
//     flex-direction: column;
//     marginTop: '4px',
//     marginBottom: '4px'
// `;

const CreateUser: FC<CreateUserProps> = ({registerUserRequestData}) => {
    const [username, setUsername] = useState<string>(registerUserRequestData?.username || "");
    const [password, setPassword] = useState<string>(registerUserRequestData?.password || "");
    const [role, setRole] = useState<string>(registerUserRequestData?.roleId || "");
    const [showPassword, setShowPassword] = useState(false);
   

    const [usernameError, setUsernameError] = useState<string | null>(null);
    const [passwordError, setPasswordError] = useState<string | null>(null);
    const [roleError, setRoleError] = useState<string | null>(null);

    const handleClickShowPassword = () => setShowPassword((show) => !show);
    const handleMouseDownPassword = (event: React.MouseEvent<HTMLButtonElement>) => {
    event.preventDefault();
     };

    function setUsernameHandler(username: string) {
        if (username.length >= 3) {
            setUsername(username);
            setUsernameError(null);
        } else {
            setUsernameError("Username must be at least 3 characters long");
        }
    };

    function setPasswordHandler(password: string) {
    };

    function setRoleHandler(role: string) {
        if (role) {
            setRole(role);
            setRoleError(null);
        } else {
            setRoleError("Role must be selected");
        }

    };
    
    return (
        <>
            <Label>User details</Label>

            <TextField id="standard-basic" label="Username" variant="standard" sx={textFieldStyleProps} fullWidth onChange={(e) => setUsernameHandler(e.target.value)} />
            {usernameError && <Alert severity="error" sx={alertStyleProps}>{usernameError}</Alert>}

            <FormControl fullWidth sx={textFieldStyleProps}>
                <InputLabel htmlFor="standard-password-input">Password</InputLabel>
                <Input
                    id="standard-password-input"
                    type={showPassword ? 'text' : 'password'}
                    fullWidth
                    sx={textFieldStyleProps}

                    endAdornment={
                        <InputAdornment position="end">
                            <IconButton
                                aria-label="toggle password visibility"
                                onClick={handleClickShowPassword}
                                onMouseDown={handleMouseDownPassword}
                                edge="end"
                            >
                                {showPassword ? <VisibilityOff /> : <Visibility />}
                            </IconButton>
                        </InputAdornment>
                    }
                />
                {passwordError && <Alert severity="error" sx={alertStyleProps}>{passwordError}</Alert>}
            </FormControl>
           
            <Select
                labelId="role-select"
                id="role-select"
                variant="standard" 
                fullWidth
                sx={selectStyleProps}
                onChange={(e) => setRoleHandler(e.target.value as string)}
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