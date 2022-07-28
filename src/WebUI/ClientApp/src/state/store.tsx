import { legacy_createStore, combineReducers, applyMiddleware } from "redux";
import { createLogger } from "redux-logger";
import thunk from "redux-thunk";
import auth from "../reducers/authReducer";
import loading from "../reducers/loadingReducer";
import stateLoader from "./StateLoader";

export default legacy_createStore(
    combineReducers(
        {
            auth,
            loading
        }),
    stateLoader.loadState(),
    applyMiddleware(createLogger(), thunk),
);
