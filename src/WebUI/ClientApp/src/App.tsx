import { useRoutes } from 'react-router-dom';
import { Provider } from "react-redux";
import AdapterDateFns from '@mui/lab/AdapterDateFns';
import LocalizationProvider from '@mui/lab/LocalizationProvider';
import { CssBaseline } from '@mui/material';
import { ToastContainer } from 'react-toastify';

import stateLoader from 'src/state/StateLoader';
import store from "src/state/store";
import LoadingBar from 'src/components/LoadingBar/LoadingBar';
import ThemeProvider from 'src/theme/ThemeProvider';
import router from 'src/router';

import 'src/custom.css'
import 'react-toastify/dist/ReactToastify.css';


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
