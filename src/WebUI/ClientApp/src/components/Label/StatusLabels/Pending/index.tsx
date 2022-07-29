import SyncIcon from '@mui/icons-material/Sync';

import Label from 'src/components/Label';

const PendingStatus = (props: any) => {

    return (
        <Label color="info">
            <SyncIcon fontSize={props.size || "small"} />
            <b>{props.text || "Pending"}</b>
        </Label>
    );
}

export default (PendingStatus);
