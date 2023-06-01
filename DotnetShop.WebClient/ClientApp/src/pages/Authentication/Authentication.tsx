import React, { useState } from "react";
import { AuthenticationRequestBody } from "./AuthenticationRequestBody";
import axios from "axios";
import { BEARER_TOKEN } from "../../Constants";
import {
    Button,
    Illustration,
    InputField,
    Modal,
    ModalFooter,
    ModalHeader,
    ModalSection,
} from "@kiwicom/orbit-components";

interface AuthenticationProps {
    setIsDialogOpen: React.Dispatch<React.SetStateAction<boolean>>;
}

const Authentication: React.FC<AuthenticationProps> = (props) => {
    const [isLoading, setIsLoading] = useState<boolean>(false);
    const [authInfo, setAuthInfo] = useState<AuthenticationRequestBody>({ username: "trunks", password: "Cvbnm123@" });

    const authenticate = () => {
        setIsLoading(true);
        axios
            .post("api/authentication/authenticate", {
                username: authInfo.username,
                password: authInfo.password,
            })
            .then((result) => result.data)
            .then((data) => {
                localStorage.setItem(BEARER_TOKEN, data);
                location.reload();
            })
            .catch(() => {
                setIsLoading(false);
            });
    };

    return (
        <Modal
            onClose={() => {
                props.setIsDialogOpen(false);
            }}
        >
            <ModalHeader title="Authentication" illustration={<Illustration size="small" name="FareLockSuccess" />} />
            <ModalSection>
                <InputField
                    label="Username"
                    type="text"
                    onChange={(e: React.SyntheticEvent<HTMLInputElement, Event>) => {
                        setAuthInfo({ ...authInfo, username: e.currentTarget.value });
                    }}
                    value={authInfo.username}
                    placeholder="Username"
                    spaceAfter="medium"
                />
                <InputField
                    label="Password"
                    type="password"
                    onChange={(e: React.SyntheticEvent<HTMLInputElement, Event>) => {
                        setAuthInfo({ ...authInfo, password: e.currentTarget.value });
                    }}
                    value={authInfo.password}
                    placeholder="Password"
                    spaceAfter="medium"
                />
            </ModalSection>

            <ModalFooter>
                <Button onClick={authenticate} loading={isLoading}>
                    Submit
                </Button>
            </ModalFooter>
        </Modal>
    );
};

export default Authentication;
