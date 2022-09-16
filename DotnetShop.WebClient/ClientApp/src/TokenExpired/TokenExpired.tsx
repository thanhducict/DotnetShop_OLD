import React, { useEffect } from "react";
import { Button, Heading, Illustration, Layout, LayoutColumn } from "@kiwicom/orbit-components";
import { isTokenExpired } from "../Authentication/AuthenticationService";
import { useNavigate } from "react-router-dom";
import { BEARER_TOKEN } from "../Constants";
import "./TokenExpired.scss";

interface TokenExpiredProps {
    setIsDialogOpen: React.Dispatch<React.SetStateAction<boolean>>;
}

const TokenExpired: React.FC<TokenExpiredProps> = (props) => {
    const navigate = useNavigate();

    useEffect(() => {
        if (!isTokenExpired(localStorage.getItem(BEARER_TOKEN) ?? "")) {
            navigate(-1);
        }
    }, []);

    return (
        <Layout type="MMB">
            <LayoutColumn>
                <div className="token-expired">
                    <Illustration size="large" name="Insurance"></Illustration>
                    <div className="token-expired__content">
                        <Heading type="displaySubtitle" spaceAfter="medium">
                            You need to authenticate first.
                        </Heading>
                        <Button onClick={() => props.setIsDialogOpen(true)}>Authenticate</Button>
                    </div>
                </div>
            </LayoutColumn>
        </Layout>
    );
};

export default TokenExpired;
