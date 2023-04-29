import React from 'react';
import ReactDOM from 'react-dom/client';
import './index.css';
import App from './App';
import reportWebVitals from './reportWebVitals';
import '@fontsource/roboto/300.css';
import '@fontsource/roboto/400.css';
import '@fontsource/roboto/500.css';
import '@fontsource/roboto/700.css';
import {
  createBrowserRouter,
  RouterProvider,
} from "react-router-dom";
import { ErrorPage } from './components/ErrorPage';
import CreateOrderPage from './components/Orders/CreateOrderPage/CreateOrderPage';
import ProductsPage from './components/Products/ViewProducts/ProductsPage';
import HomePage from './components/Home/HomePage';
import ViewOrdersPage from './components/Orders/ViewOrders/ViewOrdersPage';

const router = createBrowserRouter([
  {
    path: "/",
    element: <App />,
    errorElement: <ErrorPage />,
    children: [
      {
        path: "",
        element: <HomePage />,
      },
      {
        path: "home",
        element: <HomePage />,
      },
      {
        path: "order/view",
        element: <ViewOrdersPage />,
      },
      {
        path: "order/view/:orderId",
        element: <ViewOrdersPage />,
      },
      {
        path: "order/create",
        element: <CreateOrderPage />,
      },
      {
        path: "products",
        element: <ProductsPage />,
      },
    ],
  },
]);

const root = ReactDOM.createRoot(
  document.getElementById('root') as HTMLElement
);

root.render(
  <React.StrictMode>
    <RouterProvider router={router} />
  </React.StrictMode>
);

// If you want to start measuring performance in your app, pass a function
// to log results (for example: reportWebVitals(console.log))
// or send to an analytics endpoint. Learn more: https://bit.ly/CRA-vitals
reportWebVitals();
