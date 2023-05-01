import { useEffect, useState } from "react";
import { FC } from "react";
import styled from "styled-components";
import BasicProductInfo from "./Models/BasicProductInfo";
import TextField from "@mui/material/TextField";
import Alert from "@mui/material/Alert/Alert";

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

    const [nameError, setNameError] = useState<string | null>(null);
    const [bakingTimeInMinsError, setBakingTimeInMinsError] = useState<string | null>(null);
    const [bakingTempInCError, setBakingTempInCError] = useState<string | null>(null);
    const [sizeError, setSizeError] = useState<string | null>(null);

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
            console.log("Basic product info set");
        }
    }, [name, bakingTimeInMins, bakingTempInC, size]);

    function setNameHandler(name: string) {
        if (name.length < 3) {
            setNameError("Name must be at least 3 characters long");
        } else {
            setNameError(null);
        }
    }

    function setBakingTimeInMinsHandler(bakingTimeInMins: number) {
        if (bakingTimeInMins < 0) {
            setBakingTimeInMinsError("Baking time must be a positive number");
        } else {
            setBakingTimeInMinsError(null);
        }
    }

    function setBakingTempInCHandler(bakingTempInC: number) {
        if (bakingTempInC < 0) {
            setBakingTempInCError("Baking temperature must be a positive number");
        } else {
            setBakingTempInCError(null);
        }
    }

    function setSizeHandler(size: number) {
        if (size < 0) {
            setSizeError("Size must be a positive number");
        } else {
            setSizeError(null);
        }
    }

    return (
        <>
            <Label>Basic product info</Label>

            <TextField id="id1" label="Name" variant="standard" sx={{ marginBottom: '16px' }} fullWidth onChange={(e) => setNameHandler(e.target.value)} />
            {nameError && <Alert severity="error" sx={{ marginBottom: '16px' }}>{nameError}</Alert>}
            
            <TextField id="id2" label="Baking time in minutes" variant="standard" fullWidth onChange={(e) => setBakingTimeInMinsHandler(parseInt(e.target.value))} />
            {bakingTimeInMinsError && <Alert severity="error" sx={{ marginBottom: '16px' }}>{bakingTimeInMinsError}</Alert>}

            <TextField id="id3" label="Baking temperature in °C" variant="standard" fullWidth onChange={(e) => setBakingTempInCHandler(parseInt(e.target.value))} />
            {bakingTempInCError && <Alert severity="error" sx={{ marginBottom: '16px' }}>{bakingTempInCError}</Alert>}

            <TextField id="id4" label="Size" variant="standard" fullWidth onChange={(e) => setSizeHandler(parseInt(e.target.value))} />
            {sizeError && <Alert severity="error" sx={{ marginBottom: '16px' }}>{sizeError}</Alert>}
        </>
    );
};

export default BasicProductInfoForm;
