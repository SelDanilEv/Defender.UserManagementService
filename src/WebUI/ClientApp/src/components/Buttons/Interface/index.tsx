import { ButtonProps } from "@mui/material";

export default interface CustomButtonProps extends Omit<ButtonProps, "disabled"> {
    text: string;
}