import { FC, useEffect, useState } from "react";
import TextField from "@mui/material/TextField";
import Customer from "../../../models/Requests/Customer";
import styled from "styled-components";
import { SxProps } from "@mui/material/styles";
import Alert from "@mui/material/Alert/Alert";

export interface CustomerFormProps {
    setCustomer: (customer: Customer | null) => void;
}

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

const emailRegex = /^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$/;
const serbianMobilePhoneNumberRegex = /^(\+381)?[-\s.]?06\d[-\s.]?\d{6,7}$/;

const CustomerForm: FC<CustomerFormProps> = ({ setCustomer }) => {
    const [fullName, setFullName] = useState<string>("");
    const [email, setEmail] = useState<string>("");
    const [telephone, setTelephone] = useState<string>("");

    const [nameError, setNameError] = useState<string | null>(null);
    const [addressError, setAddressError] = useState<string | null>(null);
    const [telephoneError, setTelephoneError] = useState<string | null>(null);

    useEffect(() => {
        if (!fullName || !email || !telephone) {
            setCustomer(null);
        } else {
            setCustomer({
                fullName,
                email,
                telephone
            });
        }
    }, [fullName, email, telephone]);

    function setFullNameHandler(fullName: string) {
        if (fullName.length >= 3) {
            setFullName(fullName);
            setNameError(null);
        } else {
            setNameError("Name must be at least 3 characters long");
        }
    }

    function setEmailHandler(email: string) {
        if (emailRegex.test(email)) {
            setEmail(email);
            setAddressError(null);
        } else {
            setAddressError("Invalid email, does not match format \"email@example.com\"");
        }
    }

    function setTelephoneHandler(telephone: string) {
        if (serbianMobilePhoneNumberRegex.test(telephone)) {
            setTelephone(telephone);
            setTelephoneError(null);
        } else {
            setTelephoneError("Invalid telephone number, does not match format \"06x123456(7)\"");
        }
    }

    return (
        <>
            <Label>Customer details</Label>

            <TextField id="standard-basic" label="Name" variant="standard" sx={textFieldStyleProps} fullWidth onChange={(e) => setFullNameHandler(e.target.value)} />
            {nameError && <Alert severity="error" sx={alertStyleProps}>{nameError}</Alert>}

            <TextField id="standard-basic" label="Address" variant="standard" sx={textFieldStyleProps} fullWidth onChange={(e) => setEmailHandler(e.target.value)} />
            {addressError && <Alert severity="error" sx={alertStyleProps}>{addressError}</Alert>}

            <TextField id="standard-basic" label="Telephone" variant="standard" sx={textFieldStyleProps} fullWidth onChange={(e) => setTelephoneHandler(e.target.value)} />
            {telephoneError && <Alert severity="error" sx={alertStyleProps}>{telephoneError}</Alert>}
        </>
    );
};

export default CustomerForm;
