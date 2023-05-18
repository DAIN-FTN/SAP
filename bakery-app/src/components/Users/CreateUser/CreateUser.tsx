import { Visibility, VisibilityOff } from "@mui/icons-material";
import { Alert, Box, Button, CircularProgress, FormControl, IconButton, Input, InputAdornment, InputLabel, MenuItem, Select, SxProps, TextField } from "@mui/material";
import { FC, useEffect, useState } from "react";
import styled from "styled-components";
import RegisterRequest from "../../../models/Requests/Users/RegisterRequest";
import { getAll } from "../../../services/RoleService";
import { create } from "../../../services/UserService";
import { Form } from "react-router-dom";

type CreateUserProps = {
    registerUserRequestData?: RegisterRequest | null;
    setIsLoading: (isLoading: boolean) => void;
};

const Container = styled.div`
    padding: 8px;
    display: flex;
    flex-direction: column;
`;

const Label = styled.p`
    font-size: 24px;
`;

const textFieldStyleProps: SxProps = {
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

const CreateUser: FC<CreateUserProps> = ({ registerUserRequestData, setIsLoading }) => {
    const [username, setUsername] = useState<string>(registerUserRequestData?.username || "");
    const [password, setPassword] = useState<string>(registerUserRequestData?.password || "");
    const [role, setRole] = useState<string>(registerUserRequestData?.roleId || "");
    const [formIsValid, setFormIsValid] = useState<boolean>(false);
    const [rolesForSelection, setRolesForSelection] = useState<[string,string][]>([]);
    const [showPassword, setShowPassword] = useState(false);

    const [usernameError, setUsernameError] = useState<string | null>(null);
    const [passwordError, setPasswordError] = useState<string | null>(null);
    const [roleError, setRoleError] = useState<string | null>(null);

    const handleClickShowPassword = () => setShowPassword((show) => !show);
    const handleMouseDownPassword = (event: React.MouseEvent<HTMLButtonElement>) => {
        event.preventDefault();
    };

    //*Get roles from backend
    useEffect(() => {
        getAll().then((response) => {
            if (response?.length) {
                const roles: [string, string][] = response.map(r => [r.id, r.name]);
                setRolesForSelection(roles);
            }
        });
    }, []);


    function setUsernameHandler(username: string) {
        if (username?.length >= 3) {
            setUsername(username);
            setUsernameError(null);
            checkIfFormIsValid();
        } else {
            setUsernameError("Username must be at least 3 characters long");
        }
    };

    function setPasswordHandler(password: string) {
        if(password?.match(/^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)[a-zA-Z\d]{6,}$/)) {
            setPassword(password);
            setPasswordError(null);
            checkIfFormIsValid();
        }
        else {
            setPasswordError("Password must be at least 6 characters long, have one big letter, one small letter and one number");
        }
    };

    function setRoleHandler(role: string) {
        if (role) {
            setRole(role);
            setRoleError(null);
            checkIfFormIsValid();
        } else {
            setRoleError("Role must be selected");
        }

    };

    function checkIfFormIsValid() {
        if (username && password && role) {
            setFormIsValid(true);
        } else {
            setFormIsValid(false);
        }
    }

    async function saveUser() {
        setIsLoading(true);

        const user: RegisterRequest = {
            username: username,
            password: password,
            roleId: role
        };

        await create(user);

        await new Promise(resolve => setTimeout(resolve, 1000));

        setIsLoading(false);
    }

    return (
        <>
        <Container>
            <Label>User form</Label>
            
            <FormControl fullWidth sx={textFieldStyleProps}>
            <InputLabel htmlFor="standard-username-input">Username</InputLabel>
                <Input
                    id="standard-username-input"
                    fullWidth
                    sx={textFieldStyleProps}
                    onChange={(e) => setUsernameHandler(e.target.value)}
                    value={username}
                />
                {usernameError && <Alert severity="error" sx={alertStyleProps}>{usernameError}</Alert>}
            </FormControl>

            <FormControl fullWidth sx={textFieldStyleProps}>
                <InputLabel htmlFor="standard-password-input">Password</InputLabel>
                <Input
                    id="standard-password-input"
                    type={showPassword ? 'text' : 'password'}
                    fullWidth
                    sx={textFieldStyleProps}
                    onChange={(e) => setPasswordHandler(e.target.value)}
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

            <FormControl fullWidth sx={textFieldStyleProps}>
                <Select
                    labelId="role-select"
                    id="role-select"
                    variant="standard"
                    fullWidth
                    sx={selectStyleProps}
                    value={role}
                    onChange={(e) => setRoleHandler(e.target.value as string)}
                >
                    {rolesForSelection.map(([id, name]) => <MenuItem value={id}>{name}</MenuItem>)}
                </Select>
                {roleError && <Alert severity="error" sx={alertStyleProps}>{roleError}</Alert>}
            </FormControl>

            {formIsValid && <Button variant="contained" sx={{ marginTop: '24px' }} onClick={saveUser}>Save user</Button>}
        </Container>
        </>
    );
};

export default CreateUser;