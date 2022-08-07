import {
    Grid,
    ListItem,
    ListItemText,
    useTheme,
} from '@mui/material';
import { useEffect, useState } from 'react';

import APICallWrapper from 'src/api/APIWrapper/APICallWrapper';
import PendingStatus from 'src/components/Label/StatusLabels/Pending';
import SuccessStatus from 'src/components/Label/StatusLabels/Success';
import ErrorStatus from 'src/components/Label/StatusLabels/Error';
import apiUrls from 'src/api/apiUrls';

const HealthCheck = (props: any) => {
    const theme = useTheme();

    const [healthCheck, setHealthCheck]: any = useState();

    useEffect(() => {

        APICallWrapper(
            {
                url: apiUrls.home.healthcheck,
                options: {
                    method: 'GET'
                },
                onSuccess: async (response) => {
                    setHealthCheck(true)
                },
                onFailure: async (response) => {
                    setHealthCheck(false)
                }
            }
        )

    }, [])

    const isHealthy = () => {
        switch (healthCheck) {
            case undefined:
                return <PendingStatus text="Pending" />;
            case true:
                return <SuccessStatus text="Healthy" />;
            case false:
                return <ErrorStatus text="Unhealthy" />;
        }
    }

    return (
        <ListItem sx={{ p: 3 }} key="HealthCheck">
            <ListItemText
                primaryTypographyProps={{ variant: 'h5', gutterBottom: true, fontSize: theme.typography.pxToRem(15) }}
                primary="API status"
            />
            <Grid item xs={12} sm={8} md={9}>
                {isHealthy()}
            </Grid>
        </ListItem>
    );
}

export default (HealthCheck);
