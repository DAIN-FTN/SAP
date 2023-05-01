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
import LoginPage from './components/Login/LoginPage';
import CreateProductPage from './components/Products/CreateProduct/CreateProductPage';
import { LocalizationProvider } from '@mui/x-date-pickers/LocalizationProvider';
import AuthProvider from './AuthProvider';
import { AdapterLuxon } from '@mui/x-date-pickers/AdapterLuxon';

const router = createBrowserRouter([
    {
        path: "login",
        element: <LoginPage />
    },
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
            {
                path: "products/create",
                element: <CreateProductPage />,
            },
            {
                path: "products/:productId",
                element: <ProductsPage />,
            }
        ],
    },
]);

const root = ReactDOM.createRoot(
    document.getElementById('root') as HTMLElement
);

root.render(
    <React.StrictMode>
        <AuthProvider>
            <LocalizationProvider dateAdapter={AdapterLuxon}>
                <RouterProvider router={router} />
            </LocalizationProvider>
        </AuthProvider>
    </React.StrictMode>
);

// If you want to start measuring performance in your app, pass a function
// to log results (for example: reportWebVitals(console.log))
// or send to an analytics endpoint. Learn more: https://bit.ly/CRA-vitals
reportWebVitals();
