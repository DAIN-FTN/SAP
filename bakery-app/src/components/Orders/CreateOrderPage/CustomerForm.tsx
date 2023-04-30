import { FC, useEffect, useState } from "react";
import TextField from "@mui/material/TextField";
import Customer from "../../../models/Requests/Customer";
import styled from "styled-components";

export interface CustomerFormProps {
    setCustomer: (customer: Customer | null) => void;
}

const Label = styled.p`
    font-size: 24px;
`;

const CustomerForm: FC<CustomerFormProps> = ({ setCustomer }) => {
    const [fullName, setFullName] = useState<string>("");
    const [email, setEmail] = useState<string>("");
    const [telephone, setTelephone] = useState<string>("");

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

    return (
        <>
            <Label>Customer</Label>
            <TextField id="standard-basic" label="Name" variant="standard" fullWidth onChange={(e) => setFullName(e.target.value)} />
            <TextField id="standard-basic" label="Address" variant="standard" fullWidth onChange={(e) => setEmail(e.target.value)} />
            <TextField id="standard-basic" label="Telephone" variant="standard" fullWidth onChange={(e) => setTelephone(e.target.value)} />
        </>
    );
};

export default CustomerForm;
