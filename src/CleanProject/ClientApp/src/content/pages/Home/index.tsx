import {
    Box,
    Typography,
    Card,
    Grid,
    List,
} from '@mui/material';

import HealthCheck from './HealthCheck';
import Configuration from './Configuration';

const Home = (props: any) => {

    return (
        <Grid sx={{ p: "5%" }}>
            <Box pb={2}>
                <Typography variant="h2">Service status</Typography>
            </Box>
            <Card>
                <List>
                    <HealthCheck />
                </List>
            </Card>

            <Card sx={{ mt: 2 }}>
                <List>
                    <Configuration />
                </List>
            </Card>
        </Grid>
    );
}

export default (Home);
