import React from 'react';
import ReactDOM from 'react-dom';
import { Provider } from 'react-redux';
import App from './App';
import './index.css';
import * as serviceWorker from './serviceWorker';
import configureStore from './store';
import { BrowserRouter as Router, Route, Switch } from 'react-router-dom'

const store = configureStore();

ReactDOM.render(
    <Provider store={store}>
      <Router>
        <Switch>
          <Route exact path='/' component={App} />
        </Switch>
      </Router>    
    </Provider>,
    document.getElementById('root')
);

serviceWorker.unregister();
