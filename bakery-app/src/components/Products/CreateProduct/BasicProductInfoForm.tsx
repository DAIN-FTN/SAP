import { useEffect, useState } from "react";
import { FC } from "react";
import styled from "styled-components";
import BasicProductInfo from "./Models/BasicProductInfo";
import TextField from "@mui/material/TextField";

export interface BasicProductInfoProps {
    setBasicProductInfo: (basicProductInfo: BasicProductInfo | null) => void;
}

const Label = styled.p`
    font-size: 24px;
`;

const BasicProductInfoForm: FC<BasicProductInfoProps> = ({ setBasicProductInfo }) => {
    const [name, setName] = useState<string>();
    const [bakingTimeInMins, setBakingTimeInMins] = useState<number>();
    const [bakingTempInC, setBakingTempInC] = useState<number>();
    const [size, setSize] = useState<number>();

    useEffect(() => {
        if (!name || !bakingTimeInMins || !bakingTempInC || !size) {
            setBasicProductInfo(null);
        } else {
            setBasicProductInfo({
                name,
                bakingTimeInMins,
                bakingTempInC,
                size
            }); 
        }
    }, [name, bakingTimeInMins, bakingTempInC, size]);

    return (
        <>
            <Label>Basic product info</Label>
            <TextField id="standard-basic" label="Name" sx={{ marginBottom: '16px' }} variant="standard" fullWidth onChange={(e) => setName(e.target.value)} />
            <TextField id="standard-basic" label="Baking time in minutes" sx={{ marginBottom: '16px' }} variant="standard" fullWidth onChange={(e) => setBakingTimeInMins(parseInt(e.target.value))} />
            <TextField id="standard-basic" label="Baking temperature in °C" sx={{ marginBottom: '16px' }} variant="standard" fullWidth onChange={(e) => setBakingTempInC(parseInt(e.target.value))} />
            <TextField id="standard-basic" label="Size" variant="standard" sx={{ marginBottom: '16px' }} fullWidth onChange={(e) => setSize(parseInt(e.target.value))} />
        </>
    );
};

export default BasicProductInfoForm;
