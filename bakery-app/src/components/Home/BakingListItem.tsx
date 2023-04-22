import TableCell from "@mui/material/TableCell";
import TableRow from "@mui/material/TableRow";
import { FC, useEffect, useState } from "react";
import styled from "styled-components";
import { BakingTimeSlot as BakingProgram, BakingProgramStatus } from "../../models/BakingTimeSlot";
import { DateUtils } from "../../services/Utils";
import add from "date-fns/add";
import intervalToDuration from "date-fns/intervalToDuration";

export interface BakingListItemProps {
    bakingBakingProgram: BakingProgram;
    refreshView: Function;
    rowClickHandler: Function;
}

const TableRowStyled = styled(TableRow)`
    cursor: pointer;
    &:hover {
        background-color: #f5f5f5;
    }
`;

const BakingListItem: FC<BakingListItemProps> = ({ bakingBakingProgram: bp, refreshView, rowClickHandler }) => {
    const [duration, setDuration] = useState<Duration>();
    const [supposedEndTime, setSupposedEndTime] = useState<Date>();
    const [intervalVariable, setIntervalVarible] = useState<any>();

    function refreshCountdown() {
        const supposedEndTime = add(bp.bakingStartedAt!, { minutes: bp.bakingTimeInMins! });

        if (supposedEndTime < new Date()) {
            clearInterval(intervalVariable);
            refreshView();
        } else {
            const duration = intervalToDuration({
                start: Date.now(),
                end: supposedEndTime
            });
            setSupposedEndTime(supposedEndTime);
            setDuration(duration);
        }
    }

    useEffect(() => {
        refreshCountdown();

        const interval = setInterval(() => {
            refreshCountdown();
        }, 1000);

        setIntervalVarible(interval);

        return () => {
            clearInterval(interval);
        };
    }, []);

    return (
        <TableRowStyled sx={{ '&:last-child td, &:last-child th': { border: 0 } }} onClick={() => rowClickHandler()}>
            <TableCell component="th" scope="row">{bp.ovenCode}</TableCell>
            <TableCell align="right">{DateUtils.getMeaningfulDate(supposedEndTime!)}</TableCell>
            <TableCell align="right">{duration?.minutes}:{duration?.seconds}</TableCell>
        </TableRowStyled>
    );
};

export default BakingListItem;