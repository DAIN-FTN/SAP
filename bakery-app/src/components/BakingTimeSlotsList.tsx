import { FC } from "react";
import FormControl from "@mui/material/FormControl";
import FormLabel from "@mui/material/FormLabel";
import RadioGroup from "@mui/material/RadioGroup";
import FormControlLabel from "@mui/material/FormControlLabel";
import Radio from "@mui/material/Radio";
import { BakingTimeSlot } from "../models/BakingTimeSlot";

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
        <FormControl>
            <FormLabel id="demo-radio-buttons-group-label">Baking time slots</FormLabel>
            <RadioGroup
                aria-labelledby="demo-radio-buttons-group-label"
                defaultValue={props.bakingTimeSlots[0].id}
                name="radio-buttons-group"
            >
                {props.bakingTimeSlots.map((bakingTimeSlot) => (
                    <FormControlLabel
                        key={bakingTimeSlot.id}
                        value={bakingTimeSlot.id}
                        control={<Radio />}
                        label={bakingTimeSlot.bakingProgrammedAt.toISOString()}
                    />
                ))}
            </RadioGroup>
        </FormControl>
    );
};

export default BakingTimeSlotsList;
