import {
    Button,
    ButtonLink,
    createToast,
    Dialog,
    Heading,
    Layout,
    LayoutColumn,
    Table,
    TableBody,
    TableCell,
    TableHead,
    TableRow,
    TextLink,
} from "@kiwicom/orbit-components";
import { BaggagePersonalNone } from "@kiwicom/orbit-components/lib/icons";
import axios from "axios";
import React, { useEffect, useState } from "react";
import { useNavigate } from "react-router-dom";
import { authHeader } from "../../Authentication/AuthenticationService";
import { BEARER_TOKEN } from "../../Constants";
import { OrderDto } from "../Models/OrderDto";

const OrderList: React.FC = () => {
    const navigate = useNavigate();
    const [orders, setOrders] = useState<OrderDto[]>();
    const [isDeleting, setIsDeleting] = useState<boolean>(false);
    const [isLoading, setIsLoading] = useState<boolean>(true);
    const [deleteOrder, setDeleteOrder] = useState<OrderDto>();
    const [showDeleteDialog, setShowDeleteDialog] = useState<boolean>(false);

    useEffect(() => {
        axios
            .get<OrderDto[]>("api/orders", { headers: authHeader() })
            .then((result) => result.data)
            .then((data) => {
                // check if object is not empty
                if ((Object.keys(data).length === 0 && data.constructor === Object) == false) {
                    setOrders(data);
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
    }, []);

    return (
        <>
            <Layout type="MMB">
                <LayoutColumn>
                    {!isLoading && (
                        <>
                            <Heading type="displaySubtitle" spaceAfter="medium">
                                All orders
                            </Heading>
                            <Table>
                                <TableHead>
                                    <TableRow>
                                        <TableCell as="th">
                                            <Heading type="title3">Order Id</Heading>
                                        </TableCell>
                                        <TableCell as="th">
                                            <Heading type="title3">Order Placed</Heading>
                                        </TableCell>
                                        <TableCell as="th">
                                            <Heading type="title3">Order Total</Heading>
                                        </TableCell>
                                        <TableCell as="th">
                                            <Heading type="title3">Customer Name</Heading>
                                        </TableCell>
                                        <TableCell as="th" align="right">
                                            <></>
                                        </TableCell>
                                    </TableRow>
                                </TableHead>
                                <TableBody>
                                    {orders?.map((c: OrderDto, index: number) => (
                                        <TableRow key={index}>
                                            <TableCell>
                                                <TextLink onClick={() => navigate(`/Orders/${c.orderId}`)}>
                                                    {c.orderId}
                                                </TextLink>
                                            </TableCell>
                                            <TableCell>{new Date(c.orderPlaced).toLocaleString()}</TableCell>
                                            <TableCell>{c.orderTotal}</TableCell>
                                            <TableCell>{c.customerName}</TableCell>
                                            <TableCell align="right">
                                                <span
                                                    style={{ cursor: "pointer" }}
                                                    onClick={() => {
                                                        setDeleteOrder(c);
                                                        setShowDeleteDialog(true);
                                                    }}
                                                >
                                                    <BaggagePersonalNone color="critical" />
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
            {showDeleteDialog && (
                <Dialog
                    title="Delete"
                    description={`Do you want to delete order #${deleteOrder?.orderId}?`}
                    primaryAction={
                        <Button
                            type="critical"
                            onClick={() => {
                                setIsDeleting(true);
                                axios
                                    .delete(`api/orders/${deleteOrder?.orderId}`, {
                                        headers: authHeader(),
                                        data: deleteOrder?.orderId,
                                    })
                                    .then(() => {
                                        setOrders(orders?.filter((o) => o !== deleteOrder));
                                        createToast("Order deleted");
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

export default OrderList;
