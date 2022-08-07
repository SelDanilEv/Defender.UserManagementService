import config from 'src/config.json';


const stateName = config.LOCAL_STORAGE_KEY + ":state";

class StateLoader {

    loadState = () => {
        try {
            let serializedState = localStorage.getItem(stateName);

            if (serializedState === null) {
                return this.initializeState();
            }

            let stateJson = JSON.parse(serializedState);

            stateJson.loading = {
                callsCounter: 0,
                loading: false
            }

            return stateJson;
        }
        catch (err) {
            return this.initializeState();
        }
    }

    saveState = (state) => {
        try {
            let serializedState = JSON.stringify(state);
            localStorage.setItem(stateName, serializedState);
        }
        catch (err) {
        }
    }

    cleanState = () => {
        try {
            localStorage.removeItem(stateName);
        }
        catch (err) {
            console.error("error clean state")
        }
    }

    initializeState = () => {
        return {

        }
    };
}

const stateLoader = new StateLoader();

export default stateLoader
