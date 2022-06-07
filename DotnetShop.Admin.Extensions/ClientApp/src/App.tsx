import React from "react";
import { Routes, Route, HashRouter, Navigate } from "react-router-dom";
import Order from "./OrderManagement/Order/Order";
import OrderEdit from "./OrderManagement/OrderEdit/OrderEdit";
import OrderList from "./OrderManagement/OrderList/OrderList";
import "./global-style.scss";
import CartList from "./CartManagement/CartList";

const App: React.FC = () => {
    return (
        <>
            <HashRouter>
                <Routes>
                    <Route path="/" element={<Navigate to="/OrderList" />} />
                    <Route path="OrderList" element={<OrderList />} />
                    <Route path="OrderDetail/:orderId" element={<Order />} />
                    <Route path="OrderEdit/:orderId" element={<OrderEdit />} />
                    <Route path="CartList" element={<CartList />} />
                </Routes>
            </HashRouter>
        </>
    );
};

export default App;
