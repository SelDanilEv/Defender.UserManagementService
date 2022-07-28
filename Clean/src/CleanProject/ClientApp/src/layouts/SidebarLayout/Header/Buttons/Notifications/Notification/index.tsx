import {
  Box,
  Typography
} from '@mui/material';

const Notification = (props: any) => {
  return (
    <>
      <Box flex="1" pb={1}>
        <Box display="flex" justifyContent="space-between">
          <Typography sx={{ fontWeight: 'bold' }}>
            {props.topic}
          </Typography>
        </Box>
        <Typography
          component="span"
          variant="body2"
          color="text.secondary"
        >
          {' '}
          {props.body}
        </Typography>
      </Box>
    </>
  );
}

export default Notification;
