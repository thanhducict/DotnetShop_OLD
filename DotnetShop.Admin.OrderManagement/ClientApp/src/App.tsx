import React from "react";
import { Routes, Route, HashRouter, Navigate } from "react-router-dom";
import Order from "./components/Order/Order";
import OrderList from "./components/OrderList/OrderList";
import "./global-style.scss";

const App: React.FC = () => {
    return (
        <HashRouter>
            <Routes>
                <Route path="/" element={<Navigate to="/OrderList" />} />
                <Route path="OrderList" element={<OrderList />} />
                <Route path="OrderDetail/:orderId" element={<Order />} />
            </Routes>
        </HashRouter>
    );
};

export default App;
