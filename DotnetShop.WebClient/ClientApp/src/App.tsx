import React, { useState } from "react";
import { Routes, Route, HashRouter, Navigate } from "react-router-dom";
import { ButtonLink, LinkList, NavigationBar, Stack, Text, TextLink, ToastRoot } from "@kiwicom/orbit-components";
import { BEARER_TOKEN } from "./Constants";
import "./global-style.scss";
import CartList from "./pages/CartManagement/CartList/CartList";
import Authentication from "./pages/Authentication/Authentication";
import TokenExpired from "./pages/TokenExpired/TokenExpired";
import { isTokenExpired } from "./pages/Authentication/AuthenticationService";
import CustomerList from "./pages/CustomerManagement/CustomerList/CustomerList";
import CustomerDetail from "./pages/CustomerManagement/CustomerDetail/CustomerDetail";
import NotFound from "./pages/NotFound/NotFound";
import OrderList from "./pages/OrderManagement/OrderList/OrderList";
import OrderDetail from "./pages/OrderManagement/OrderDetail/OrderDetail";
import ComponentsPage from "./components/ComponentsPage/ComponentsPage";

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
                        <TextLink type="secondary" href="#/Components">
                            [Sample] Components
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
                        <Route path="/Components" element={<ComponentsPage />} />
                    </Routes>
                </HashRouter>
            </div>
            <ToastRoot dismissTimeout={3000} placement="top-center" />
            {isDialogOpen && <Authentication setIsDialogOpen={setIsDialogOpen} />}
        </>
    );
};

export default App;
