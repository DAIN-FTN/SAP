import { FC } from "react";
import { BakingTimeSlot } from "../models/BakingTimeSlot";
import List from "@mui/material/List";
import ListItem from "@mui/material/ListItem";
import ListItemButton from "@mui/material/ListItemButton";
import ListItemIcon from "@mui/material/ListItemIcon";
import ListItemText from "@mui/material/ListItemText";
import RadioButtonUncheckedIcon from '@mui/icons-material/RadioButtonUnchecked';

export interface BakingTimeSlotsListProps {
    bakingTimeSlots: BakingTimeSlot[];
}

const BakingTimeSlotsList: FC<{ props: BakingTimeSlotsListProps }> = ({ props }) => {
    console.log('props.bakingTimeSlots', props.bakingTimeSlots)
    if (props.bakingTimeSlots.length === 0)
        return (
            <>
                <p>No baking time slots available</p>
            </>
        );
    return (
        // <FormControl>
        //     <FormLabel id="demo-radio-buttons-group-label">Baking time slots</FormLabel>
        //     <RadioGroup
        //         aria-labelledby="demo-radio-buttons-group-label"
        //         defaultValue={props.bakingTimeSlots[0].id}
        //         name="radio-buttons-group"
        //     >
        //         {props.bakingTimeSlots.map((bakingTimeSlot) => (
        //             <FormControlLabel
        //                 key={bakingTimeSlot.id}
        //                 value={bakingTimeSlot.id}
        //                 control={<Radio />}
        //                 label={bakingTimeSlot.bakingProgrammedAt.toISOString()}
        //             />
        //         ))}
        //     </RadioGroup>
        // </FormControl>
        <List>
            {props.bakingTimeSlots.map((bakingTimeSlot) => (
                // <FormControlLabel
                //     key={bakingTimeSlot.id}
                //     value={bakingTimeSlot.id}
                //     control={<Radio />}
                //     label={bakingTimeSlot.bakingProgrammedAt.toISOString()}
                // />
                <ListItem disablePadding>
                    <ListItemButton>
                        <ListItemIcon>
                            <RadioButtonUncheckedIcon />
                        </ListItemIcon>
                        <ListItemText
                            primary={bakingTimeSlot.bakingProgrammedAt?.toLocaleDateString("en-US")}
                            secondary={bakingTimeSlot.ovenCode} />
                    </ListItemButton>
                </ListItem>
            ))}
            <ListItem disablePadding>
                <ListItemButton>
                    <ListItemIcon>
                        <RadioButtonUncheckedIcon />
                    </ListItemIcon>
                    <ListItemText primary="Inbox" />
                </ListItemButton>
            </ListItem>
        </List>
    );
};

export default BakingTimeSlotsList;
