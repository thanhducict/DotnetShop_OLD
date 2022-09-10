import React from "react";
import { Routes, Route, HashRouter, Navigate } from "react-router-dom";
import OrderEdit from "./OrderManagement/OrderEdit/OrderEdit";
import "./global-style.scss";
import CustomerCart from "./CartManagement/CustomerCart/CustomerCart";
import Carts from "./CartManagement/Carts/CartList";
import Orders from "./OrderManagement/Orders/Orders";
import { Container, Nav, Navbar, NavDropdown } from "react-bootstrap";

const App: React.FC = () => {
    return (
        <>
            <Navbar bg="light">
                <Container>
                    <Navbar.Brand href="#/">Web Client</Navbar.Brand>
                    <Navbar.Toggle aria-controls="basic-navbar-nav" />
                    <Navbar.Collapse id="basic-navbar-nav">
                        <Nav className="me-auto">
                            <Nav.Link href="#/Carts">Carts</Nav.Link>
                            <Nav.Link href="#/CustomerCart">Customer Cart</Nav.Link>
                        </Nav>
                    </Navbar.Collapse>
                </Container>
            </Navbar>
            <Container style={{ marginTop: "20px" }}>
                <HashRouter>
                    <Routes>
                        <Route path="/" element={<Navigate to="/Carts" />} />
                        <Route path="Carts" element={<Carts />} />
                        <Route path="CustomerCart/" element={<CustomerCart />} />
                        <Route path="Orders" element={<Orders />} />
                        <Route path="OrderEdit/:orderId" element={<OrderEdit />} />
                    </Routes>
                </HashRouter>
            </Container>
        </>
    );
};

export default App;
