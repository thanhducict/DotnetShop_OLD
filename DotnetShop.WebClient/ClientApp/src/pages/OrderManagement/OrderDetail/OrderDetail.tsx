import {
    Button,
    createToast,
    Heading,
    Inline,
    InputField,
    Layout,
    LayoutColumn,
    Stack,
    Table,
    TableBody,
    TableCell,
    TableHead,
    TableRow,
} from "@kiwicom/orbit-components";
import axios from "axios";
import React, { useEffect, useState } from "react";
import { useNavigate, useParams } from "react-router-dom";
import { authHeader } from "../../Authentication/AuthenticationService";
import { BEARER_TOKEN } from "../../../Constants";
import { OrderDetailDto } from "../Models/OrderDetailDto";
import { OrderWithDetailDto } from "../Models/OrderWithDetailDto";

const OrderDetail: React.FC = () => {
    const params = useParams();
    const navigate = useNavigate();
    const [order, setOrder] = useState<OrderWithDetailDto>({
        orderId: 0,
        orderPlaced: "",
        orderTotal: 0,
        orderDetailViewModels: [],
        customer: { customerId: 0, customerName: "", gender: false, address: "" },
    });
    const [triggerValidation, setTriggerValidation] = useState<boolean>(false);
    const [isDirty, setIsDirty] = useState<boolean>(false);
    const [isLoading, setIsLoading] = useState<boolean>(true);
    const [utcDate, setUtcDate] = useState<string>("");
    const [utcTime, setUtcTime] = useState<string>("");
    const getDate = (value: Date) =>
        `${("0" + value.getUTCDate()).slice(-2)}-${("0" + (value.getUTCMonth() + 1)).slice(
            -2
        )}-${value.getUTCFullYear()}`;
    const getTime = (value: Date) =>
        `${("0" + value.getUTCHours()).slice(-2)}:${("0" + value.getUTCMinutes()).slice(-2)}:${(
            "0" + value.getUTCSeconds()
        ).slice(-2)}`;
    const getDateTime = (date: string, time: string) =>
        `${date.slice(6, 10)}-${date.slice(3, 5)}-${date.slice(0, 2)}T${time}Z`;

    const regexDate = /(^0[1-9]|[12][0-9]|3[01])-(0[1-9]|1[0-2])-(\d{4}$)/;
    const regexTime = /(?:[01]\d|2[0123]):(?:[012345]\d):(?:[012345]\d)/;

    useEffect(() => {
        axios
            .get<OrderWithDetailDto>(`api/orders/${params.orderId}`, { headers: authHeader() })
            .then((result) => result.data)
            .then((data) => {
                // check if object is not empty
                if ((Object.keys(data).length === 0 && data.constructor === Object) == false) {
                    setOrder(data);
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

    useEffect(() => {
        setUtcDate(getDate(new Date(order.orderPlaced)));
        setUtcTime(getTime(new Date(order.orderPlaced)));
    }, [order.orderPlaced]);

    return (
        <Layout type="MMB">
            <LayoutColumn>
                {!isLoading && (
                    <>
                        <Heading type="displaySubtitle" spaceAfter="large">
                            Order #{order.orderId}
                        </Heading>
                        <Stack spacing="large">
                            <Stack direction="row" grow={true}>
                                <InputField
                                    label="Order placed UTC date (dd-mm-yyyy)"
                                    type="text"
                                    maxLength={10}
                                    value={utcDate}
                                    onChange={(e: React.SyntheticEvent<HTMLInputElement, Event>) => {
                                        setIsDirty(true);
                                        setUtcDate(e.currentTarget.value);
                                    }}
                                    error={
                                        triggerValidation ? (
                                            new RegExp(regexDate).test(utcDate) ? (
                                                ""
                                            ) : (
                                                <>Invalid Date</>
                                            )
                                        ) : (
                                            ""
                                        )
                                    }
                                />
                                <InputField
                                    label="Order placed UTC time (hh:mm:ss)"
                                    maxLength={8}
                                    type="text"
                                    value={utcTime}
                                    onChange={(e: React.SyntheticEvent<HTMLInputElement, Event>) => {
                                        setIsDirty(true);
                                        setUtcTime(e.currentTarget.value);
                                    }}
                                    error={
                                        triggerValidation ? (
                                            new RegExp(regexTime).test(utcTime) ? (
                                                ""
                                            ) : (
                                                <>Invalid Time</>
                                            )
                                        ) : (
                                            ""
                                        )
                                    }
                                />
                            </Stack>
                            <InputField label="Order Total" type="number" readOnly={true} value={order.orderTotal} />
                            <Heading type="title3">Order Details</Heading>
                            <Table type="secondary" striped={false} compact={true}>
                                <TableHead>
                                    <TableRow>
                                        <TableCell as="th">
                                            <Heading type="title3">Order Detail Id</Heading>
                                        </TableCell>
                                        <TableCell as="th">
                                            <Heading type="title3">Product Name</Heading>
                                        </TableCell>
                                        <TableCell as="th">
                                            <Heading type="title3">Product Price</Heading>
                                        </TableCell>
                                        <TableCell as="th">
                                            <Heading type="title3">Quantity</Heading>
                                        </TableCell>
                                        <TableCell as="th">
                                            <Heading type="title3">Line Item Total</Heading>
                                        </TableCell>
                                    </TableRow>
                                </TableHead>
                                <TableBody>
                                    {order.orderDetailViewModels?.map((o: OrderDetailDto, index: number) => (
                                        <TableRow key={index}>
                                            <TableCell>{o.orderDetailId}</TableCell>
                                            <TableCell>{o.productName}</TableCell>
                                            <TableCell>{o.productPrice}</TableCell>
                                            <TableCell>{o.quantity}</TableCell>
                                            <TableCell>{o.lineItemTotal}</TableCell>
                                        </TableRow>
                                    ))}
                                </TableBody>
                            </Table>
                            <Heading type="title3">Customer</Heading>
                            <InputField
                                label="Customer Id"
                                type="text"
                                readOnly={true}
                                value={order.customer.customerId}
                            />
                            <InputField
                                label="Customer Name"
                                type="text"
                                readOnly={true}
                                value={order.customer.customerName}
                            />
                            <InputField
                                label="Gender"
                                type="text"
                                readOnly={true}
                                value={order.customer.gender ? "Male" : "Female"}
                            />
                            <InputField
                                label="Customer Address"
                                type="text"
                                readOnly={true}
                                value={order.customer.address}
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
                                    disabled={!isDirty}
                                    onClick={() => {
                                        setIsLoading(true);
                                        if (
                                            new RegExp(regexDate).test(utcDate) &&
                                            new RegExp(regexTime).test(utcTime)
                                        ) {
                                            let updatedOrder = { ...order, orderPlaced: getDateTime(utcDate, utcTime) };
                                            axios
                                                .put<OrderWithDetailDto>("api/orders/", updatedOrder, {
                                                    headers: authHeader(),
                                                })
                                                .then(() => {
                                                    setOrder(updatedOrder);
                                                    createToast("Order updated");
                                                })
                                                .catch(() => {
                                                    navigate("/NotFound");
                                                })
                                                .finally(() => {
                                                    setTriggerValidation(false);
                                                    setIsLoading(false);
                                                });
                                        } else {
                                            setTriggerValidation(true);
                                            setIsLoading(false);
                                        }
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

export default OrderDetail;
