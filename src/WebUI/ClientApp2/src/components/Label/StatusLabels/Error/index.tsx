import Label from 'src/components/Label';

import ClearIcon from '@mui/icons-material/Clear';

const ErrorStatus = (props: any) => {

    return (
        <Label color="error">
            <ClearIcon fontSize={props.size || "small"} />
            <b>{props.text || "Error"}</b>
        </Label>
    );
}

export default (ErrorStatus);
