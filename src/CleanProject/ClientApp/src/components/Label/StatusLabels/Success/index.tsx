import Label from 'src/components/Label';

import DoneTwoToneIcon from '@mui/icons-material/DoneTwoTone';

const SuccessStatus = (props: any) => {

    return (
        <Label color="success">
            <DoneTwoToneIcon fontSize={props.size || "small"} />
            <b>{props.text || "Success"}</b>
        </Label>
    );
}

export default (SuccessStatus);
