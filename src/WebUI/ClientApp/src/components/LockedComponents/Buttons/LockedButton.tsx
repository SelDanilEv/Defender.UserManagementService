import { Button } from "@mui/material";
import { connect } from "react-redux";

import LockedButtonProps from "./LockedButtonProps";

const LockedButton = ({ isLoading, dispatch, ...restProps }: LockedButtonProps) => {
  return <Button disabled={isLoading} {...restProps} />;
};

const mapStateToProps = (state: any) => {
  return {
    isLoading: state.loading.loading
  };
};

export default
  connect(mapStateToProps)
    (LockedButton);
