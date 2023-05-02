import { useEffect, useState } from "react";
import { FC } from "react";
import styled from "styled-components";
import BasicProductInfo from "./Models/BasicProductInfo";
import TextField from "@mui/material/TextField";
import Alert from "@mui/material/Alert/Alert";
import { SxProps } from "@mui/material";

export interface BasicProductInfoProps {
    setBasicProductInfo: (basicProductInfo: BasicProductInfo | null) => void;
}

const Label = styled.p`
    font-size: 24px;
`;

const textFieldStyleProps: SxProps = {
    marginTop: '4px',
    marginBottom: '8px'
};

const alertStyleProps: SxProps = {
    marginBottom: '8px',
    width: 'fit-content'
};

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
        }
    }, [name, bakingTimeInMins, bakingTempInC, size]);

    function setNameHandler(name: string) {
        if (name.length > 3) {
            setName(name);
            setNameError(null);
        } else {
            setNameError("Name must be at least 3 characters long");
        }
    }

    function setBakingTimeInMinsHandler(bakingTimeInMins: string) {
        if (isPositiveInteger(bakingTimeInMins)) {
            const numericValue = parseInt(bakingTimeInMins);
            setBakingTimeInMins(numericValue);
            setBakingTimeInMinsError(null);
        } else {
            setBakingTimeInMinsError("Baking time must be a positive number");
        }
    }

    function setBakingTempInCHandler(bakingTempInC: string) {
        if (isPositiveInteger(bakingTempInC)) {
            const numericValue = parseInt(bakingTempInC);
            setBakingTempInC(numericValue);
            setBakingTempInCError(null);
        } else {
            setBakingTempInCError("Baking temperature must be a positive number");
        }
    }

    function setSizeHandler(size: string) {
        if (isPositiveInteger(size)) {
            const numericValue = parseInt(size);
            setSize(numericValue);
            setSizeError(null);
        } else {
            setSizeError("Size must be a positive number");
        }
    }

    function isPositiveInteger(str: string): boolean {
        const num = parseInt(str, 10);
        return !isNaN(num) && num.toString() === str && num > 0;
    }

    return (
        <>
            <Label>Basic product info</Label>

            <TextField id="id1" label="Name" variant="standard" sx={textFieldStyleProps} fullWidth onChange={(e) => setNameHandler(e.target.value)} />
            {nameError && <Alert severity="error" sx={alertStyleProps}>{nameError}</Alert>}

            <TextField id="id2" label="Baking time in minutes" variant="standard" sx={textFieldStyleProps} fullWidth onChange={(e) => setBakingTimeInMinsHandler(e.target.value)} />
            {bakingTimeInMinsError && <Alert severity="error" sx={alertStyleProps}>{bakingTimeInMinsError}</Alert>}

            <TextField id="id3" label="Baking temperature in °C" variant="standard" sx={textFieldStyleProps} fullWidth onChange={(e) => setBakingTempInCHandler(e.target.value)} />
            {bakingTempInCError && <Alert severity="error" sx={alertStyleProps}>{bakingTempInCError}</Alert>}

            <TextField id="id4" label="Size" variant="standard" sx={textFieldStyleProps} fullWidth onChange={(e) => setSizeHandler(e.target.value)} />
            {sizeError && <Alert severity="error" sx={alertStyleProps}>{sizeError}</Alert>}
        </>
    );
};

export default BasicProductInfoForm;
