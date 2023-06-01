import React, { useEffect, useState } from "react";
import GenericTable, { ColumnDefinitionType } from "../GenericTable/GenericTable";
import { authHeader } from "../../pages/Authentication/AuthenticationService";
import axios from "axios";
import Cat from "./Cat";
import { BEARER_TOKEN } from "../../Constants";
import { useNavigate } from "react-router-dom";
import { Heading, Layout, LayoutColumn } from "@kiwicom/orbit-components";

const ComponentsPage: React.FC = () => {
    const navigate = useNavigate();
    const [cats, setCats] = useState<Cat[]>([]);
    const [columns, setColumns] = useState<ColumnDefinitionType<Cat, keyof Cat>[]>([]);
    const [isLoading, setIsLoading] = useState<boolean>(true);

    const getCats = () => {
        axios
            .get<Cat[]>("api/sample/cats", { headers: authHeader() })
            .then((result) => result.data)
            .then((data) => {
                // check if object is not empty
                if ((Object.keys(data).length === 0 && data.constructor === Object) == false) {
                    setCats(data);
                }
            })
            .catch((error) => {
                if (error.response.status === 401) {
                    localStorage.removeItem(BEARER_TOKEN);
                    navigate("/TokenExpired");
                } else {
                    navigate("/NotFound");
                }
            });
    };

    const getColumns = () => {
        axios
            .get<ColumnDefinitionType<Cat, keyof Cat>[]>("api/sample/columns", { headers: authHeader() })
            .then((result) => result.data)
            .then((data) => {
                // check if object is not empty
                if ((Object.keys(data).length === 0 && data.constructor === Object) == false) {
                    setColumns(data);
                }
            })
            .catch((error) => {
                if (error.response.status === 401) {
                    localStorage.removeItem(BEARER_TOKEN);
                    navigate("/TokenExpired");
                } else {
                    navigate("/NotFound");
                }
            })
            .finally(() => {
                setIsLoading(false);
            });
    };

    useEffect(() => {
        getCats();
        getColumns();
    }, []);

    return (
        <>
            {!isLoading && (
                <>
                    <Layout type="MMB">
                        <LayoutColumn>
                            <Heading type="displaySubtitle" spaceAfter="medium">
                                Cats
                            </Heading>
                            <GenericTable data={cats} columns={columns} />
                        </LayoutColumn>
                    </Layout>
                </>
            )}
        </>
    );
};

export default ComponentsPage;
