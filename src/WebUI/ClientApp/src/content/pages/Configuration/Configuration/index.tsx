import {
    Divider,
    Grid,
    ListItem,
    ListItemText,
    MenuItem,
    TextField,
    useTheme,
} from '@mui/material';
import { useEffect, useState } from 'react';

import APICallWrapper from 'src/api/APIWrapper/APICallWrapper';
import PendingStatus from 'src/components/Label/StatusLabels/Pending';
import apiUrls from 'src/api/apiUrls';

const Configuration = (props: any) => {
    const theme = useTheme();

    const [configuration, setConfiguration]: any = useState();

    useEffect(() => {

        APICallWrapper(
            {
                url: apiUrls.home.configuration,
                options: {
                    method: 'GET'
                },
                onSuccess: async (response) => {
                    setConfiguration(await response.json())
                }
            }
        )

    }, [])

    const renderConfiguration = () => {
        if (!configuration) {
            return <Grid item xs={12} sm={12} md={12} textAlign="center">
                <PendingStatus text="Pending" />
            </Grid>
        }

        let envVariables = Object.keys(configuration);

        if (envVariables.length == 0) {
            return <Grid item xs={12} sm={12} md={12} textAlign="center">
                <PendingStatus text="No data" />
            </Grid>
        }

        return envVariables.map((option) => (
            <Grid container item xs={12} key={option}>
                <Grid item xs={4}>
                    {option}
                </Grid>
                <Grid item xs={8}>
                    {configuration[option]}
                </Grid>
                <Grid item xs={12} margin={1}>
                    <Divider />
                </Grid>
            </Grid>
        ))

    }

    const configurationLevels = [
        {
            value: 'Hide',
            label: 'Hide'
        },
        {
            value: 'Admin',
            label: 'Admin'
        },
        {
            value: 'All',
            label: 'All'
        }
    ];

    const [configurationLevel, setConfigurationLevel] = useState('Hide');

    const handleChange = (event) => {
        APICallWrapper(
            {
                url: `${apiUrls.home.configuration}?configurationLevel=${event.target.value}`,
                options: {
                    method: 'GET'
                },
                onSuccess: async (response) => {
                    setConfiguration(await response.json())
                }
            }
        )

        setConfigurationLevel(event.target.value);
    };

    return (
        <ListItem sx={{ p: 3 }} key="Configuration">
            <Grid container >
                <Grid item xs={6} sm={7} md={8} >
                    <ListItemText
                        primaryTypographyProps={{ variant: 'h5', gutterBottom: true, fontSize: theme.typography.pxToRem(15) }}
                        primary="Configuration" />

                </Grid>

                <Grid item xs={6} sm={5} md={4}>
                    <TextField
                        select
                        fullWidth
                        label="Configuration level"
                        value={configurationLevel}
                        onChange={handleChange}>
                        {configurationLevels.map((option) => (
                            <MenuItem key={option.value} value={option.value}>
                                {option.label}
                            </MenuItem>
                        ))}
                    </TextField>
                </Grid>

                <Grid container sx={{ mt: 3 }} >
                    {renderConfiguration()}
                </Grid>
            </Grid>
        </ListItem >
    );
}

export default (Configuration);
