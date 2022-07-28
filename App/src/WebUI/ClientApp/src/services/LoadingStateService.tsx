import store from "../state/store"

const LoadingStateService = {
    StartLoading: () => {
        store.dispatch({ type: 'START_LOADING' })
    },
    FinishLoading: () => {
        store.dispatch({ type: 'FINISH_LOADING' })
    }
}

export default LoadingStateService;
