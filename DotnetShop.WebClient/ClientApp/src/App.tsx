import React, { useState } from "react";
import { Routes, Route, HashRouter, Navigate } from "react-router-dom";
import OrderEdit from "./OrderManagement/OrderEdit/OrderEdit";
import "./global-style.scss";
import CustomerCart from "./CartManagement/CustomerCart/CustomerCart";
import Carts from "./CartManagement/Carts/CartList";
import Orders from "./OrderManagement/Orders/Orders";
import { Container, Nav, Navbar } from "react-bootstrap";
import Authentication from "./Authentication/Authentication";

interface GlobalStates {
    authenticated: boolean;
    setAuthenticated: React.Dispatch<React.SetStateAction<boolean>>;
}

export const GlobalContext = React.createContext<GlobalStates>({} as GlobalStates);

const App: React.FC = () => {
    const [authenticated, setAuthenticated] = useState<boolean>(false);

    return (
        <GlobalContext.Provider value={{ authenticated, setAuthenticated }}>
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
                    <div className="d-flex">
                        {authenticated ? (
                            <span>Authenticated</span>
                        ) : (
                            <Nav.Link href="#/Authentication">Authenticate</Nav.Link>
                        )}
                    </div>
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
                        <Route path="Authentication" element={<Authentication />} />
                    </Routes>
                </HashRouter>
            </Container>
        </GlobalContext.Provider>
    );
};

export default App;
