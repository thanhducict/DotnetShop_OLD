import {
    Button,
    ChoiceGroup,
    createToast,
    Heading,
    Inline,
    InputField,
    Layout,
    LayoutColumn,
    Radio,
    Stack,
} from "@kiwicom/orbit-components";
import axios from "axios";
import React, { useEffect, useState } from "react";
import { useNavigate, useParams } from "react-router-dom";
import { authHeader } from "../../Authentication/AuthenticationService";
import { BEARER_TOKEN } from "../../Constants";
import { CustomerUpdateDto } from "../Models/CustomerUpdateDto";

const CustomerDetail: React.FC = () => {
    const params = useParams();
    const navigate = useNavigate();
    const [customer, setCustomer] = useState<CustomerUpdateDto>({
        customerId: 0,
        customerName: "",
        gender: false,
        address: "",
        username: "",
        email: "",
    });
    const [orignalCustomer, setOriginalCustomer] = useState<CustomerUpdateDto>({
        customerId: 0,
        customerName: "",
        gender: false,
        address: "",
        username: "",
        email: "",
    });
    const isDirty = () => {
        return JSON.stringify(orignalCustomer) !== JSON.stringify(customer);
    };
    const [isLoading, setIsLoading] = useState<boolean>(true);

    useEffect(() => {
        axios
            .get<CustomerUpdateDto>(`api/v1/customers/${params.customerId}`, { headers: authHeader() })
            .then((result) => result.data)
            .then((data) => {
                // check if object is not empty
                if ((Object.keys(data).length === 0 && data.constructor === Object) == false) {
                    setCustomer(data);
                    setOriginalCustomer(data);
                }
            })
            .catch((error) => {
                if (error.response.status) {
                    localStorage.removeItem(BEARER_TOKEN);
                    navigate("/TokenExpired");
                }
            })
            .finally(() => {
                setIsLoading(false);
            });
    }, []);

    return (
        <Layout type="MMB">
            <LayoutColumn>
                {!isLoading && (
                    <>
                        <Heading type="displaySubtitle" spaceAfter="large">
                            {customer.customerName}
                        </Heading>
                        <Stack spacing="large">
                            <InputField label="Customer Id" type="text" disabled={true} value={customer.customerId} />
                            <InputField
                                label="Customer Name"
                                type="text"
                                value={customer.customerName}
                                onChange={(e: React.SyntheticEvent<HTMLInputElement, Event>) => {
                                    setCustomer({ ...customer, customerName: e.currentTarget.value });
                                }}
                            />
                            <ChoiceGroup
                                label="Gender"
                                onChange={(e: React.SyntheticEvent<HTMLInputElement, Event>) =>
                                    setCustomer({ ...customer, gender: e.currentTarget.value === "true" })
                                }
                            >
                                <Radio name="gender" label="Male" value="true" checked={customer.gender === true} />
                                <Radio name="gender" label="Female" value="false" checked={customer.gender === false} />
                            </ChoiceGroup>
                            <InputField
                                label="Customer Address"
                                type="text"
                                value={customer?.address}
                                onChange={(e: React.SyntheticEvent<HTMLInputElement, Event>) => {
                                    setCustomer({ ...customer, address: e.currentTarget.value });
                                }}
                            />
                            <InputField
                                label="Username"
                                type="text"
                                value={customer?.username}
                                onChange={(e: React.SyntheticEvent<HTMLInputElement, Event>) => {
                                    setCustomer({ ...customer, username: e.currentTarget.value });
                                }}
                            />
                            <InputField
                                label="Email"
                                type="text"
                                value={customer?.email}
                                onChange={(e: React.SyntheticEvent<HTMLInputElement, Event>) => {
                                    setCustomer({ ...customer, email: e.currentTarget.value });
                                }}
                            />
                            <Inline spacing="XSmall" justify="end">
                                <Button
                                    width="80px"
                                    type="secondary"
                                    onClick={() => {
                                        navigate(-1);
                                    }}
                                >
                                    Back
                                </Button>
                                <Button
                                    width="80px"
                                    disabled={!isDirty()}
                                    onClick={() => {
                                        setIsLoading(true);
                                        axios
                                            .put<CustomerUpdateDto>("api/v1/customers/", customer, {
                                                headers: authHeader(),
                                            })
                                            .then(() => {
                                                setOriginalCustomer(customer);
                                                createToast("Customer updated");
                                            })
                                            .catch(() => {
                                                navigate("/NotFound");
                                            })
                                            .finally(() => {
                                                setIsLoading(false);
                                            });
                                    }}
                                >
                                    Save
                                </Button>
                            </Inline>
                        </Stack>
                    </>
                )}
            </LayoutColumn>
        </Layout>
    );
};

export default CustomerDetail;
