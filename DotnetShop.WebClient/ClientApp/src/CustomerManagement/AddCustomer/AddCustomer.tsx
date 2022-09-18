import {
    Button,
    ChoiceGroup,
    Inline,
    InputField,
    Modal,
    ModalFooter,
    ModalHeader,
    ModalSection,
    Radio,
    Stack,
} from "@kiwicom/orbit-components";
import axios from "axios";
import React, { useState } from "react";
import { useNavigate } from "react-router-dom";
import { authHeader } from "../../Authentication/AuthenticationService";
import { CustomerCreationDto } from "../Models/CustomerCreationDto";
import { CustomerDto } from "../Models/CustomerDto";

interface AddCustomerProps {
    showModal: boolean;
    setShowModal: React.Dispatch<React.SetStateAction<boolean>>;
    reloadData: () => void;
}

const AddCustomer: React.FC<AddCustomerProps> = (props) => {
    const navigate = useNavigate();
    const orignalCustomer = {
        customerName: "",
        gender: true,
        address: "",
        username: "",
        email: "",
    };
    const [customer, setCustomer] = useState<CustomerCreationDto>(orignalCustomer);
    const isDirty = () => {
        return JSON.stringify(orignalCustomer) !== JSON.stringify(customer);
    };
    const [isLoading, setIsLoading] = useState<boolean>(false);

    return (
        <>
            {props.showModal && (
                <Modal
                    onClose={() => {
                        props.setShowModal(false);
                    }}
                >
                    <ModalHeader title="Add new customer" />
                    <ModalSection>
                        <Stack spacing="large">
                            <InputField
                                label="Customer Name"
                                type="text"
                                value={customer?.customerName}
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
                                label="User Name"
                                type="text"
                                value={customer.username}
                                onChange={(e: React.SyntheticEvent<HTMLInputElement, Event>) => {
                                    setCustomer({ ...customer, username: e.currentTarget.value });
                                }}
                            />
                            <InputField
                                label="Email"
                                type="text"
                                value={customer.email}
                                onChange={(e: React.SyntheticEvent<HTMLInputElement, Event>) => {
                                    setCustomer({ ...customer, email: e.currentTarget.value });
                                }}
                            />
                        </Stack>
                    </ModalSection>
                    <ModalFooter>
                        <Inline spacing="XSmall" justify="end">
                            <Button
                                width="80px"
                                type="secondary"
                                onClick={() => {
                                    props.setShowModal(false);
                                }}
                            >
                                Cancel
                            </Button>
                            <Button
                                width="80px"
                                disabled={!isDirty()}
                                loading={isLoading}
                                onClick={() => {
                                    setIsLoading(true);
                                    axios
                                        .post<CustomerDto>("api/v1/customers/", customer, {
                                            headers: authHeader(),
                                        })
                                        .then(() => {
                                            props.setShowModal(false);
                                            props.reloadData();
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
                    </ModalFooter>
                </Modal>
            )}
        </>
    );
};

export default AddCustomer;
