import DialogTitle from '@mui/material/DialogTitle';
import Dialog from '@mui/material/Dialog';
import styled from 'styled-components';

export interface ErrorDialogProps {
    open: boolean;
    errorMessage: string;
    onClose: (value: string) => void;
}

const ErrorHeader = styled(DialogTitle)`
    background-color: #dc3f3f;
    color: #fff;
`;

const ErrorMessage = styled.p`
    padding: 0px 24px;
`;

export default function ErrorDialog(props: ErrorDialogProps) {
    const { onClose, errorMessage, open } = props;

    const handleClose = () => {
        onClose(errorMessage);
    };

    return (
        <Dialog onClose={handleClose} open={open}>
            <ErrorHeader>Error occured</ErrorHeader>
            <ErrorMessage>{errorMessage}</ErrorMessage>
        </Dialog>
    );
}
