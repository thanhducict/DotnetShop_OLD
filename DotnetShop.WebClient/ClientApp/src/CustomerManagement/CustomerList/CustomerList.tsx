import {
    Button,
    ButtonLink,
    Dialog,
    Heading,
    InputField,
    Layout,
    LayoutColumn,
    Stack,
    Table,
    TableBody,
    TableCell,
    TableHead,
    TableRow,
    TextLink,
} from "@kiwicom/orbit-components";
import {
    BaggageCabinNone,
    GenderMan,
    GenderWoman,
    MinusCircle,
    PassengerAdd,
    PassengerRemove,
} from "@kiwicom/orbit-components/lib/icons";
import ButtonPrimitive from "@kiwicom/orbit-components/lib/primitives/ButtonPrimitive";
import axios from "axios";
import React, { useEffect, useState } from "react";
import { useNavigate } from "react-router-dom";
import { authHeader } from "../../Authentication/AuthenticationService";
import { BEARER_TOKEN } from "../../Constants";
import AddCustomer from "../AddCustomer/AddCustomer";
import { CustomerDto } from "../Models/CustomerDto";

const CustomerList: React.FC = () => {
    const navigate = useNavigate();
    const [isLoading, setIsLoading] = useState<boolean>(true);
    const [isDeleting, setIsDeleting] = useState<boolean>(false);
    const [customers, setCustomers] = useState<CustomerDto[]>();
    const [deleteCustomer, setDeleteCustomer] = useState<CustomerDto>();
    const [showAddNewModal, setShowAddNewModal] = useState<boolean>(false);
    const [showDeleteDialog, setShowDeleteDialog] = useState<boolean>(false);

    const fetchData = () => {
        axios
            .get<CustomerDto[]>("api/v1/customers", { headers: authHeader() })
            .then((result) => result.data)
            .then((data) => {
                // check if object is not empty
                if ((Object.keys(data).length === 0 && data.constructor === Object) == false) {
                    setCustomers(data);
                }
            })
            .catch((error) => {
                if (error.response.status === 401) {
                    localStorage.removeItem(BEARER_TOKEN);
                    navigate("/TokenExpired");
                } else {
                    navigate("./NotFound");
                }
            })
            .finally(() => {
                setIsLoading(false);
            });
    };

    useEffect(() => {
        fetchData();
    }, []);

    return (
        <>
            <Layout type="MMB">
                <LayoutColumn>
                    {!isLoading && (
                        <>
                            <Heading type="displaySubtitle" spaceAfter="large">
                                All customers
                            </Heading>
                            <Stack spaceAfter="large" spacing="large" direction="row">
                                <InputField placeholder="Find by customer name" />
                                <Button
                                    iconLeft={<PassengerAdd customColor="white" />}
                                    width="100px"
                                    onClick={() => {
                                        setShowAddNewModal(true);
                                    }}
                                ></Button>
                            </Stack>
                            <Table>
                                <TableHead>
                                    <TableRow>
                                        <TableCell as="th">
                                            <Heading type="title3">Id</Heading>
                                        </TableCell>
                                        <TableCell as="th">
                                            <Heading type="title3">Name</Heading>
                                        </TableCell>
                                        <TableCell as="th">
                                            <Heading type="title3">Gender</Heading>
                                        </TableCell>
                                        <TableCell as="th">
                                            <Heading type="title3">Address</Heading>
                                        </TableCell>
                                        <TableCell as="th" align="right">
                                            <></>
                                        </TableCell>
                                    </TableRow>
                                </TableHead>
                                <TableBody>
                                    {customers?.map((c: CustomerDto, index: number) => (
                                        <TableRow key={index}>
                                            <TableCell>{c.customerId}</TableCell>
                                            <TableCell>
                                                <TextLink onClick={() => navigate(`/Customers/${c.customerId}`)}>
                                                    {c.customerName}
                                                </TextLink>
                                            </TableCell>
                                            <TableCell>{c.gender ? "Male" : "Female"}</TableCell>
                                            <TableCell>{c.address}</TableCell>
                                            <TableCell align="right">
                                                <span
                                                    style={{ cursor: "pointer" }}
                                                    onClick={() => {
                                                        setDeleteCustomer(c);
                                                        setShowDeleteDialog(true);
                                                    }}
                                                >
                                                    <PassengerRemove color="critical" />
                                                </span>
                                            </TableCell>
                                        </TableRow>
                                    ))}
                                </TableBody>
                            </Table>
                        </>
                    )}
                </LayoutColumn>
            </Layout>
            <AddCustomer showModal={showAddNewModal} setShowModal={setShowAddNewModal} reloadData={fetchData} />
            {showDeleteDialog && (
                <Dialog
                    title="Delete"
                    description={`Do you want to delete ${deleteCustomer?.customerName}?`}
                    primaryAction={
                        <Button
                            type="critical"
                            onClick={() => {
                                setIsDeleting(true);
                                axios
                                    .delete(`api/v1/customers/${deleteCustomer?.customerId}`, {
                                        headers: authHeader(),
                                        data: deleteCustomer?.customerId,
                                    })
                                    .then(() => {
                                        setCustomers(customers?.filter((c) => c !== deleteCustomer));
                                    })
                                    .finally(() => {
                                        setIsDeleting(false);
                                        setShowDeleteDialog(false);
                                    });
                            }}
                            loading={isDeleting}
                        >
                            Delete
                        </Button>
                    }
                    secondaryAction={
                        <ButtonLink
                            type="secondary"
                            onClick={() => {
                                setShowDeleteDialog(false);
                            }}
                        >
                            Cancel
                        </ButtonLink>
                    }
                />
            )}
        </>
    );
};

export default CustomerList;
