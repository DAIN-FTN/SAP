import styled from "styled-components";
import { FC, useState} from "react";
import { Button, FormControl, Grid, IconButton, InputAdornment, InputLabel, OutlinedInput, TextField } from "@mui/material";
import { Visibility, VisibilityOff } from "@mui/icons-material";

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

const StyledButton = styled(Button)`
    background-color: rgba(0,0,255,1);
`

const LoginForm: FC<LoginFormProps> = ({width}) => {
    const [values, setValues] = useState<LoginValues>({username: '', password:''});
    const [showPassword, setShowPassword] = useState(false);

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
    
    return (
        <form>
            <Grid container direction="column" justifyContent="center">
                <ControlContainer> 
                    <TextField sx={{width: width}} label="Username" name="username" value={values.username} onChange={handleInputChange}/>
                </ControlContainer>
                <ControlContainer> 
                    <FormControl sx={{ width: width }} variant="outlined">
                    <InputLabel htmlFor="password">Password</InputLabel>
                    <OutlinedInput
                        id="password"
                        type={showPassword ? 'text' : 'password'}
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
                <ControlContainer>
                    <StyledButton variant="contained" sx={{width: width}}>Log in</StyledButton>
                </ControlContainer>
            </Grid>
        </form>
    )
}

export default LoginForm