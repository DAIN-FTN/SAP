import '@fontsource/roboto/300.css';
import '@fontsource/roboto/400.css';
import '@fontsource/roboto/500.css';
import '@fontsource/roboto/700.css';
import { AdapterLuxon } from '@mui/x-date-pickers/AdapterLuxon';
import { LocalizationProvider } from '@mui/x-date-pickers/LocalizationProvider';
import React from 'react';
import ReactDOM from 'react-dom/client';
import {
    createBrowserRouter,
    RouterProvider,
} from "react-router-dom";
import App from './App';
import AuthProvider from './AuthProvider';
import { ErrorPage } from './components/ErrorPage';
import HomePage from './components/Home/HomePage';
import LoginPage from './components/Login/LoginPage';
import CreateOrderPage from './components/Orders/CreateOrderPage/CreateOrderPage';
import ViewOrdersPage from './components/Orders/ViewOrders/ViewOrdersPage';
import CreateProductPage from './components/Products/CreateProduct/CreateProductPage';
import ProductsPage from './components/Products/ViewProducts/ProductsPage';
import ViewUsersPage from './components/Users/ViewUsers/ViewUsersPage';
import './index.css';
import reportWebVitals from './reportWebVitals';

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
            },
            {
                path: "users",
                element: <ViewUsersPage />,
               
            },
            { 
                path: "users/:userId",
                element: <ViewUsersPage />,
            },
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
