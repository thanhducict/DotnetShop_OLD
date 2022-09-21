import {
    Button,
    ChoiceGroup,
    Collapse,
    createToast,
    Heading,
    Inline,
    InputField,
    Layout,
    LayoutColumn,
    Radio,
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
import { BEARER_TOKEN } from "../../Constants";
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
    const [orignalCustomer, setOriginalOrder] = useState<OrderWithDetailDto>({
        orderId: 0,
        orderPlaced: "",
        orderTotal: 0,
        orderDetailViewModels: [],
        customer: { customerId: 0, customerName: "", gender: false, address: "" },
    });
    const isDirty = () => {
        return JSON.stringify(orignalCustomer) !== JSON.stringify(order);
    };
    const [isLoading, setIsLoading] = useState<boolean>(true);

    useEffect(() => {
        axios
            .get<OrderWithDetailDto>(`api/orders/${params.orderId}`, { headers: authHeader() })
            .then((result) => result.data)
            .then((data) => {
                // check if object is not empty
                if ((Object.keys(data).length === 0 && data.constructor === Object) == false) {
                    setOrder(data);
                    setOriginalOrder(data);
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
                            Order #{order.orderId}
                        </Heading>
                        <Stack spacing="large">
                            <InputField
                                label="Order placed"
                                type="text"
                                readOnly={true}
                                disabled={true}
                                value={order.orderPlaced}
                            />
                            <InputField
                                label="Order Total"
                                type="number"
                                readOnly={true}
                                disabled={true}
                                value={order.orderTotal}
                            />
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
                                disabled={true}
                                value={order.customer.customerId}
                            />
                            <InputField
                                label="Customer Name"
                                type="text"
                                readOnly={true}
                                disabled={true}
                                value={order.customer.customerName}
                            />
                            <InputField
                                label="Gender"
                                type="text"
                                readOnly={true}
                                disabled={true}
                                value={order.customer.gender ? "Male" : "Female"}
                            />
                            <InputField
                                label="Customer Address"
                                type="text"
                                readOnly={true}
                                disabled={true}
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
                            </Inline>
                        </Stack>
                    </>
                )}
            </LayoutColumn>
        </Layout>
    );
};

export default OrderDetail;
