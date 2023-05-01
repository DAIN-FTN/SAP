import styled from "styled-components";
import { FC, useEffect, useState} from "react";
import { Button, FormControl, Grid, IconButton, InputAdornment, InputLabel, OutlinedInput, TextField } from "@mui/material";
import { Visibility, VisibilityOff } from "@mui/icons-material";
import { getUserFromToken, postLogin } from "../../services/AuthService";
import LoginRequest from "../../models/Requests/Auth/LoginRequest";
import LoginResponse from "../../models/Requests/Auth/LoginResponse";
import { useNavigate } from "react-router-dom";

import UserDetailsResponse from "../../models/Responses/User/UserDetailsResponse";
import { useAuth } from "../../hooks/useAuth";
import { useUser } from "../../hooks/useUser";

export interface LoginFormProps{
    width: string
}

export interface LoginValues{
    username: string,
    password: string
}


const ControlContainer = styled.div`
    margin-top: 10px;
    display: flex;
    justify-content: center;
`

const ErrorContainer = styled(ControlContainer)`
    background-color: rgba(255,0,0,0.4);
    border-radius: 4px;
`

const ErrorParagraph = styled.p`
    color: rgba(0,0,0,0.8);
`

const StyledButton = styled(Button)`
    background-color: rgba(0,0,255,1);
`

const LoginForm: FC<LoginFormProps> = ({width}) => {
    const [values, setValues] = useState<LoginValues>({username: '', password:''});
    const [showPassword, setShowPassword] = useState(false);
    const [error, setError] = useState('');
    const navigate = useNavigate();
    const { user, login, logout } = useAuth();

    useEffect(() => {
       console.log('user in LoginForm: ', user);
    }, [user]);

    const handleClickShowPassword = () => setShowPassword((show) => !show);

    const handleMouseDownPassword = (event: React.MouseEvent<HTMLButtonElement>) => {
      event.preventDefault();
    };

    const handleInputChange = (e: { target: { name: any; value: any; }; }) => {
        const {name, value} = e.target
        setValues({
            ...values,
            [name]: value
        })
    }

   function resetError(){
        setError('')
   }

    async function handleLogin() {
        if(!values.username || !values.password ){
            return;
        }
        
        const loginRequest: LoginRequest = {
            username: values.username, 
            password: values.password
        }

        login(loginRequest);
        console.log(user);

        if(user){
            setError('');
            navigate("/");
        }else{
            setError('Wrong username or password');
        }
    }
    
    return (
        <form>
            <Grid container direction="column" justifyContent="center">
                <ControlContainer> 
                    <TextField sx={{width: width}} label="Username" name="username" value={values.username} onClick={resetError} onChange={handleInputChange}/>
                </ControlContainer>
                <ControlContainer> 
                    <FormControl sx={{ width: width }} variant="outlined">
                    <InputLabel htmlFor="password">Password</InputLabel>
                    <OutlinedInput
                        id="password"
                        type={showPassword ? 'text' : 'password'}
                        name="password"
                        value={values.password}
                        onClick={resetError}
                        onChange={handleInputChange}
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
                        label="Password"
                    />
                    </FormControl>
                </ControlContainer>
                <ErrorContainer>
                    {error!=='' && <ErrorParagraph>{error}</ErrorParagraph>}  
                </ErrorContainer>
                <ControlContainer>
                    <StyledButton variant="contained" sx={{width: width}} onClick={handleLogin}>Log in</StyledButton>
                </ControlContainer>
            </Grid>
        </form>
    )
}

export default LoginForm