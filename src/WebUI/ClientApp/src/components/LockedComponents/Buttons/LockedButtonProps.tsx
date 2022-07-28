import { ButtonProps } from "@mui/material";

export default interface LockedButtonProps extends Omit<ButtonProps, "disabled"> {
  isLoading?: boolean,
  dispatch?: any
}
