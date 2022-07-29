import {
    Button,
    useTheme,
    styled,
} from '@mui/material';

import CustomButtonProps from '../Interface';

const GreenButton = styled(Button)(
    ({ theme }) => `
       background: ${theme.colors.success.main};
       color: ${theme.palette.success.contrastText};
  
       &:hover {
          background: ${theme.colors.success.dark};
       }
      `
);

const ButtonSuccess = ({ text, ...props }: CustomButtonProps) => {
    const theme = useTheme();

    props.variant = "contained"

    return (
        <GreenButton {...props} variant="contained">
            {text}
        </GreenButton>
    );
}

export default (ButtonSuccess);
