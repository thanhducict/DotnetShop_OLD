import React, { useState } from "react";
import { Routes, Route, HashRouter, Navigate } from "react-router-dom";
import "./global-style.scss";
import Carts from "./CartManagement/Carts/CartList";
import Authentication from "./Authentication/Authentication";
import { ButtonLink, LinkList, NavigationBar, Stack, Text, TextLink } from "@kiwicom/orbit-components";
import TokenExpired from "./TokenExpired/TokenExpired";
import { isTokenExpired } from "./Authentication/AuthenticationService";
import { BEARER_TOKEN } from "./Constants";

const App: React.FC = () => {
    const [isDialogOpen, setIsDialogOpen] = useState<boolean>(false);

    return (
        <>
            <NavigationBar>
                <Stack direction="row" align="center" justify="center">
                    <ButtonLink href="/">Web Client</ButtonLink>
                    <LinkList direction="row">
                        <TextLink type="secondary" href="#/Customers">
                            Customers
                        </TextLink>
                        <TextLink type="secondary" href="#/Carts">
                            Carts
                        </TextLink>
                        <TextLink type="secondary">Orders</TextLink>
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
                        <Route path="/" element={<Navigate to="/Carts" />} />
                        <Route path="Carts" element={<Carts setIsDialogOpen={setIsDialogOpen} />} />
                        {/* <Route path="CustomerCart/" element={<CustomerCart />} />
                    <Route path="Orders" element={<Orders />} />
                    <Route path="OrderEdit/:orderId" element={<OrderEdit />} /> */}

                        <Route path="/TokenExpired" element={<TokenExpired setIsDialogOpen={setIsDialogOpen} />} />
                    </Routes>
                </HashRouter>
            </div>
            {isDialogOpen && <Authentication setIsDialogOpen={setIsDialogOpen} />}
        </>
    );
};

export default App;
