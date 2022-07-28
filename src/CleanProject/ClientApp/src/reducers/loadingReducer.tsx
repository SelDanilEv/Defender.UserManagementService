const loadingReducer = (state = {
  callsCounter: 0,
  loading: false
}, action: any) => {
  let newValue = state.callsCounter;
  switch (action.type) {
    case "START_LOADING":
      state = {
        ...state,
        callsCounter: ++newValue,
        loading: true,
      };
      break;
    case "FINISH_LOADING":
      state = {
        ...state,
        callsCounter: --newValue,
        loading: newValue > 0,
      };
      break;
    default:
      break;
  }
  return state;
};

export default loadingReducer;
