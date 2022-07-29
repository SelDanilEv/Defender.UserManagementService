import store from "src/state/store"

const LoadingStateService = {
    StartLoading: () => {
        store.dispatch({ type: 'START_LOADING' })
    },
    FinishLoading: () => {
        store.dispatch({ type: 'FINISH_LOADING' })
    }
}

export default LoadingStateService;
