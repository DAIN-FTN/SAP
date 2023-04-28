import { FC, useState } from "react";
import List from "@mui/material/List";
import ListItem from "@mui/material/ListItem";
import ListItemButton from "@mui/material/ListItemButton";
import ListItemIcon from "@mui/material/ListItemIcon";
import ListItemText from "@mui/material/ListItemText";
import RadioButtonUncheckedIcon from '@mui/icons-material/RadioButtonUnchecked';
import RadioButtonCheckedIcon from '@mui/icons-material/RadioButtonChecked';
import { Label } from "@mui/icons-material";
import TextField from "@mui/material/TextField";

export interface CheckDeliveryTimeProps {
    setDeliveryTime: (deliveryTime: Date) => void;
    setCreationPossible: (creationPossible: boolean) => void;
}

const CheckDeliveryTime: FC<CheckDeliveryTimeProps> = ({ setDeliveryTime, setCreationPossible }) => {
    const [selectedTimeSlotId, setSelectedTimeSlotId] = useState<string | null>(null);

    function setDateTimeHandler(e: any) {
        let dateTime = new Date(e.target.value);

        console.log("setDateTimeHandler", dateTime);

        // getAvailable(dateTime, orderProducts)
        //     .then((availableProgramsResponse) => {
        //         console.log('fetch finished for getAvailable() in CreateOrderPage');
        //         setBakingTimeSlots(availableProgramsResponse.bakingPrograms);
        //     });
    };

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
                    onChange={(e) => setDateTimeHandler(e)}
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
