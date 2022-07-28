import Box from "@mui/material/Box";
import LinearProgress from "@mui/material/LinearProgress";
import { connect } from "react-redux";

const LoadingBar = (props: any) => {

    return (
        <Box
            sx={{
                position: "absolute",
                top: 0,
                zIndex: 10000,
                width: "100%",
                height: "5px",
            }}
        >
            {props.isLoading && <LinearProgress />}
        </Box>
    );
}

const mapStateToProps = (state: any) => {
    return {
        isLoading: state.loading.loading
    };
};

export default
    connect(mapStateToProps)
        (LoadingBar);
