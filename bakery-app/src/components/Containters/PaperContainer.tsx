import { Paper } from '@mui/material';
import styled from "styled-components";
import { FC} from "react";

export interface PaperContainerProps {
    content: React.ReactNode;
    margin: string
    padding: string
    opacity: number,
    width: string
}


const PaperContainer: FC<PaperContainerProps> = ({ content, margin, opacity, width, padding}) => {

  return (
   <Paper sx={{margin: margin, width: width, padding: padding,  backgroundColor: `rgba(255,255,255,${opacity})`}}>
    {content}
   </Paper>
  )
}

export default PaperContainer
