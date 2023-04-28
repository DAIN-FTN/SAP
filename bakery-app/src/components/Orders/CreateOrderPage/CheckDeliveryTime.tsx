import { FC } from "react";
import List from "@mui/material/List";
import { Label } from "@mui/icons-material";
import TextField from "@mui/material/TextField";
import { getAvailable } from "../../../services/BakingProgramService";
import ProductRequestItem from "./Models/ProductRequestItem";

export interface CheckDeliveryTimeProps {
    orderProducts: ProductRequestItem[];
    setDeliveryTime: (deliveryTime: Date | null) => void;
    setCreationPossible: (creationPossible: boolean) => void;
}

const CheckDeliveryTime: FC<CheckDeliveryTimeProps> = ({ orderProducts, setDeliveryTime, setCreationPossible }) => {
    
    function setDateTimeHandler(value: any) {
        let dateTime = new Date(value);

        console.log("setDateTimeHandler", dateTime);

        getAvailable(dateTime, mapProductRequestItemsToProductRequest(orderProducts))
            .then((availableProgramsResponse) => {
                console.log('fetch finished for getAvailable() in CreateOrderPage', availableProgramsResponse);
                const createPossible = availableProgramsResponse.allProductsCanBeSuccessfullyArranged && availableProgramsResponse.isThereEnoughStockedProducts;
                setCreationPossible(createPossible);
                setDeliveryTime(createPossible ? dateTime : null);
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

    // console.log('props.bakingTimeSlots', props.bakingTimeSlots)

    // if (props.bakingTimeSlots.length === 0)
    //     return (
    //         <>
    //             <p>No baking time slots available</p>
    //         </>
    //     );

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
                <Label>Available baking time slots</Label>
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
