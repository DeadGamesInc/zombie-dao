import 'index.css';

import React from 'react';
import { Provider } from 'react-redux';
import { createRoot } from 'react-dom/client';

import { store } from 'store';

import App from 'App';

const container = document.getElementById('root');
if (!container) throw new Error('Document root container not found!!');
const root = createRoot(container);

root.render(
  <Provider store={store}>
    <React.StrictMode>
      <App />
    </React.StrictMode>
  </Provider>,
);
