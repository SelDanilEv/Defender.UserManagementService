import ReactDOM from 'react-dom';
import { Provider } from "react-redux";
import { HelmetProvider } from 'react-helmet-async';
import { BrowserRouter } from 'react-router-dom';
import { GoogleOAuthProvider } from '@react-oauth/google';

import App from 'src/App';
import { SidebarProvider } from 'src/contexts/SidebarContext';
import store from "src/state/store";
import * as serviceWorkerRegistration from 'src/serviceWorkerRegistration';
import reportWebVitals from 'src/reportWebVitals';
import config from 'src/config.json';

import 'nprogress/nprogress.css';


ReactDOM.render(
  <Provider store={store}>
    <HelmetProvider>
      <SidebarProvider>
        <BrowserRouter>
          <GoogleOAuthProvider clientId={config.GOOGLE_CLIENT_ID}>
            <App />
          </GoogleOAuthProvider>
        </BrowserRouter>
      </SidebarProvider>
    </HelmetProvider>
  </Provider>,
  document.getElementById('root')
);


// If you want your app to work offline and load faster, you can change
// unregister() to register() below. Note this comes with some pitfalls.
// Learn more about service workers: https://cra.link/PWA
serviceWorkerRegistration.unregister();

// If you want to start measuring performance in your app, pass a function
// to log results (for example: reportWebVitals(console.log))
// or send to an analytics endpoint. Learn more: https://bit.ly/CRA-vitals
reportWebVitals();
