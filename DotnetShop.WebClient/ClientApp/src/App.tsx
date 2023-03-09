import React, { useState } from "react";
import { Routes, Route, HashRouter, Navigate } from "react-router-dom";
import "./global-style.scss";
import "./global-style.css";
import CartList from "./CartManagement/CartList/CartList";
import Authentication from "./Authentication/Authentication";
import { ButtonLink, LinkList, NavigationBar, Stack, Text, TextLink, ToastRoot } from "@kiwicom/orbit-components";
import TokenExpired from "./TokenExpired/TokenExpired";
import { isTokenExpired } from "./Authentication/AuthenticationService";
import { BEARER_TOKEN } from "./Constants";
import CustomerList from "./CustomerManagement/CustomerList/CustomerList";
import CustomerDetail from "./CustomerManagement/CustomerDetail/CustomerDetail";
import NotFound from "./NotFound/NotFound";
import OrderList from "./OrderManagement/OrderList/OrderList";
import OrderDetail from "./OrderManagement/OrderDetail/OrderDetail";

const App: React.FC = () => {
    const [isDialogOpen, setIsDialogOpen] = useState<boolean>(false);

    return (
        <>
            <NavigationBar hideOnScroll={false}>
                <Stack direction="row" align="center" justify="center">
                    <ButtonLink href="/">Dotnet Shop</ButtonLink>
                    <LinkList direction="row">
                        <TextLink type="secondary" href="#/Customers">
                            Customers
                        </TextLink>
                        <TextLink type="secondary" href="#/Carts">
                            Carts
                        </TextLink>
                        <TextLink type="secondary" href="#/Orders">
                            Orders
                        </TextLink>
                    </LinkList>
                    <Stack inline>
                        <LinkList direction="row">
                            {isTokenExpired(localStorage.getItem(BEARER_TOKEN) ?? "") ? (
                                <TextLink type="secondary" onClick={() => setIsDialogOpen(true)}>
                                    Authenticate
                                </TextLink>
                            ) : (
                                <Text>Authenticated</Text>
                            )}
                        </LinkList>
                    </Stack>
                </Stack>
            </NavigationBar>
            <div className="web-client-container">
                <HashRouter>
                    <Routes>
                        <Route path="/" element={<Navigate to="/Customers" />} />
                        <Route path="Customers" element={<CustomerList />} />
                        <Route path="Customers/:customerId" element={<CustomerDetail />} />
                        <Route path="Carts" element={<CartList />} />
                        <Route path="Orders" element={<OrderList />} />
                        <Route path="Orders/:orderId" element={<OrderDetail />} />
                        <Route path="/TokenExpired" element={<TokenExpired setIsDialogOpen={setIsDialogOpen} />} />
                        <Route path="/NotFound" element={<NotFound />} />
                    </Routes>
                </HashRouter>
            </div>
            <ToastRoot dismissTimeout={3000} placement="top-center" />
            {isDialogOpen && <Authentication setIsDialogOpen={setIsDialogOpen} />}
        </>
    );
};

export default App;
