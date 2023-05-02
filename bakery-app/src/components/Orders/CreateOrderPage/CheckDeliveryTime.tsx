import { FC, useState } from "react";
import List from "@mui/material/List";
import TextField from "@mui/material/TextField";
import { getAvailable } from "../../../services/BakingProgramService";
import ProductRequestItem from "./Models/ProductRequestItem";
import styled from "styled-components";
import Alert from "@mui/material/Alert/Alert";
import { SxProps } from "@mui/material/styles";

export interface CheckDeliveryTimeProps {
    orderProducts: ProductRequestItem[];
    setDeliveryTime: (deliveryTime: Date | null) => void;
    setCreationPossible: (creationPossible: boolean) => void;
}

const Label = styled.p`
    font-size: 24px;
`;

const alertStyleProps: SxProps = {
    marginTop: '12px',
    width: 'fit-content'
};

const CheckDeliveryTime: FC<CheckDeliveryTimeProps> = ({ orderProducts, setDeliveryTime, setCreationPossible }) => {
    const [dateInPastError, setDateInPastError] = useState<boolean>(false);
    
    function setDateTimeHandler(value: any) {
        let dateTime = new Date(value);

        getAvailable(dateTime, mapProductRequestItemsToProductRequest(orderProducts))
            .then((availableProgramsResponse) => {
                const createPossible = availableProgramsResponse.allProductsCanBeSuccessfullyArranged && availableProgramsResponse.isThereEnoughStockedProducts;
                setCreationPossible(createPossible);
                setDateInPastError(false);
                setDeliveryTime(createPossible ? dateTime : null);
            }).catch((error) => {
                setDateInPastError(true);
            });
    };

    function mapProductRequestItemsToProductRequest(productRequestItems: ProductRequestItem[]) {
        return productRequestItems.map((productRequestItem) => {
            return {
                productId: productRequestItem.id,
                quantity: productRequestItem.requestedQuantity
            };
        });
    }

    return (
        <>
            <List>
                <Label>Order delivery date and time</Label>
                <TextField
                    id="datetime-local"
                    type="datetime-local"
                    defaultValue={new Date().toISOString().slice(0, 16)}
                    sx={{ width: 250 }}
                    InputLabelProps={{
                        shrink: true,
                    }}
                    onChange={(e) => setDateTimeHandler(e.target.value)}
                />
                {dateInPastError && <Alert severity="error" sx={alertStyleProps}>The delivery date should be in the future.</Alert>}
                {/* <Label>Available baking time slots</Label> */}
                {/* <BakingTimeSlotsList props={{ bakingTimeSlots }} /> */}
                {/* {props.bakingTimeSlots.map((bakingTimeSlot) => (
                    <ListItem disablePadding key={bakingTimeSlot.id}
                        onClick={() => setSelectedTimeSlotId(bakingTimeSlot.id)}>
                        <ListItemButton>
                            <ListItemIcon>
                                {selectedTimeSlotId === bakingTimeSlot.id ? <RadioButtonCheckedIcon /> : <RadioButtonUncheckedIcon />}
                            </ListItemIcon>
                            <ListItemText
                                primary={bakingTimeSlot.bakingProgrammedAt?.toLocaleDateString("en-US")}
                                secondary={bakingTimeSlot.ovenCode} />
                        </ListItemButton>
                    </ListItem>
                ))} */}
            </List>
        </>
    );
};

export default CheckDeliveryTime;
