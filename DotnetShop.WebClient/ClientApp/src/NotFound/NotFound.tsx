import React from "react";
import { Heading, Illustration, Layout, LayoutColumn } from "@kiwicom/orbit-components";
import "./NotFound.scss";

const NotFound: React.FC = () => {
    return (
        <Layout type="MMB">
            <LayoutColumn>
                <div className="not-found">
                    <Illustration size="display" name="Error404"></Illustration>
                    <div className="not-found__content">
                        <Heading type="displaySubtitle" spaceAfter="medium">
                            Something went wrong.
                        </Heading>
                    </div>
                </div>
            </LayoutColumn>
        </Layout>
    );
};

export default NotFound;
