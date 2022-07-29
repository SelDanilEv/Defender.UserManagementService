import { legacy_createStore, combineReducers, applyMiddleware } from "redux";
import { createLogger } from "redux-logger";
import thunk from "redux-thunk";

import stateLoader from "./StateLoader";

import auth from "src/reducers/authReducer";
import loading from "src/reducers/loadingReducer";

export default legacy_createStore(
    combineReducers(
        {
            auth,
            loading
        }),
    stateLoader.loadState(),
    applyMiddleware(
        // createLogger(), comment for production
        thunk),
);
