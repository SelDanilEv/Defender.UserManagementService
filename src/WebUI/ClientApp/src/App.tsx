import { useRoutes } from 'react-router-dom';
import router from 'src/router';

import AdapterDateFns from '@mui/lab/AdapterDateFns';
import LocalizationProvider from '@mui/lab/LocalizationProvider';

import { CssBaseline } from '@mui/material';
import ThemeProvider from './theme/ThemeProvider';

import { Provider } from "react-redux";
import store from "./state/store";

import { ToastContainer } from 'react-toastify';
import LoadingBar from './helpers/LoadingBar/LoadingBar';

import './custom.css'
import 'react-toastify/dist/ReactToastify.css';
import stateLoader from './state/StateLoader';

function App() {
  const content = useRoutes(router);

  store.subscribe(() => {
    stateLoader.saveState(store.getState());
  });

  return (
    <Provider store={store}>
      <ThemeProvider>
        <LocalizationProvider dateAdapter={AdapterDateFns}>
          <ToastContainer
            position="top-right"
            autoClose={5000}
            hideProgressBar={false}
            newestOnTop={false}
            closeOnClick
            rtl={false}
            pauseOnFocusLoss
            draggable
            pauseOnHover
            theme='dark'
          />
          <LoadingBar />
          <CssBaseline />
          {content}
        </LocalizationProvider>
      </ThemeProvider>
    </Provider>
  );
}
export default App;
