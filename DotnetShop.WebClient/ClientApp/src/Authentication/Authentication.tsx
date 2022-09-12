import React, { useState } from "react";
import { Button, Form } from "react-bootstrap";
import { AuthenticationRequestBody } from "./AuthenticationRequestBody";
import axios from "axios";
import { BEARER_TOKEN } from "../Constants";

const Authentication: React.FC = () => {
    const [authInfo, setAuthInfo] = useState<AuthenticationRequestBody>({ username: "trunks", password: "Cvbnm123@" });
    const authenticate = () => {
        axios
            .post("api/authentication/authenticate", {
                username: authInfo.username,
                password: authInfo.password,
            })
            .then((result) => result.data)
            .then((data) => {
                alert("Token is set.");
                localStorage.setItem(BEARER_TOKEN, data);
            })
            .catch((error) => {
                alert(error);
            });
    };

    return (
        <>
            {localStorage.getItem(BEARER_TOKEN) ? (
                <h5>You are already authenticated.</h5>
            ) : (
                <>
                    <h3>Authentication</h3>
                    <Form>
                        <Form.Group className="mb-3" controlId="formBasicEmail">
                            <Form.Label>Username</Form.Label>
                            <Form.Control
                                type="text"
                                value={authInfo.username}
                                onChange={(e: React.ChangeEvent<HTMLInputElement>) => {
                                    setAuthInfo({ ...authInfo, username: e.target.value });
                                }}
                                placeholder="Username"
                            />
                        </Form.Group>
                        <Form.Group className="mb-3" controlId="formBasicPassword">
                            <Form.Label>Password</Form.Label>
                            <Form.Control
                                type="password"
                                value={authInfo.password}
                                onChange={(e: React.ChangeEvent<HTMLInputElement>) => {
                                    setAuthInfo({ ...authInfo, password: e.target.value });
                                }}
                                placeholder="Password"
                            />
                        </Form.Group>
                        <Button variant="primary" type="submit" onClick={authenticate}>
                            Submit
                        </Button>
                    </Form>
                </>
            )}
        </>
    );
};

export default Authentication;
