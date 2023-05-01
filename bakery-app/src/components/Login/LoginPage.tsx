
import styled from "styled-components";
import { FC } from "react";
import PaperContainer from "../Containters/PaperContainer";
import LoginForm from "./LoginForm";
import { Grid } from "@mui/material";
import AuthProvider from "../../AuthProvider";

const Container = styled.div`
    background-image: url('croissants.jpg');
    height: 100vh;
    background-size: cover;
    background-repeat: no-repeat;
`

const LoginPage: FC = () => {

    return (
        <AuthProvider>
            <Container>
                <Grid container>
                    <Grid item xs={4} />
                    <Grid item xs={4}>
                        <PaperContainer margin={'50px 0px 0px 0px'} content={<LoginForm width={'400px'} />} padding={'10px 10px 10px 10px'} opacity={0.7} width={'400px'} />
                    </Grid>
                    <Grid item xs={4} />
                </Grid>
            </Container>
        </AuthProvider>
    )
}

export default LoginPage