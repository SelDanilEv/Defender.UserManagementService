import Label from 'src/components/Label';

import SyncIcon from '@mui/icons-material/Sync';

const WarningStatus = (props: any) => {

    return (
        <Label color="warning">
            <SyncIcon fontSize={props.size || "small"} />
            <b>{props.text || "Warning"}</b>
        </Label>
    );
}

export default (WarningStatus);
