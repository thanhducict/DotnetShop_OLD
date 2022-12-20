import {
    Button,
    ButtonLink,
    Checkbox,
    createToast,
    Dialog,
    Heading,
    Layout,
    LayoutColumn,
    Pagination,
    Stack,
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
import { PaginationMetadata } from "../../Common/PaginationMetadata";
import { BEARER_TOKEN } from "../../Constants";
import { DeleteOrderDto } from "../Models/DeleteOrderDto";
import { OrderDto } from "../Models/OrderDto";

const OrderList: React.FC = () => {
    const navigate = useNavigate();
    const [orders, setOrders] = useState<OrderDto[]>([]);
    const [isDeleting, setIsDeleting] = useState<boolean>(false);
    const [isLoading, setIsLoading] = useState<boolean>(true);
    const [showDeleteDialog, setShowDeleteDialog] = useState<boolean>(false);
    const [deleteOrder, setDeleteOrder] = useState<OrderDto | null>(null);
    const [deleteOrders, setDeleteOrders] = useState<OrderDto[]>([]);
    const [checkedAll, setCheckedAll] = useState<boolean>(false);
    const [pagination, setPagination] = useState<PaginationMetadata>({} as PaginationMetadata);

    console.log(pagination);

    const fetchData = (pageNumber: number = 1) => {
        axios
            .get<OrderDto[]>("api/orders", {
                headers: authHeader(),
                params: {
                    pageNumber: pageNumber,
                    pageSize: 20,
                },
            })
            .then((result) => {
                // check if object is not empty
                if ((Object.keys(result.data).length === 0 && result.data.constructor === Object) == false) {
                    setOrders(result.data);
                    setPagination(JSON.parse(result.headers["x-pagination"]));
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
        fetchData();
    }, []);

    useEffect(() => {
        if (deleteOrders.length != 0 && deleteOrders.length == orders.length) {
            setCheckedAll(true);
        } else {
            setCheckedAll(false);
        }
    }, [deleteOrders]);

    return (
        <>
            <Layout type="MMB">
                <LayoutColumn>
                    {!isLoading && (
                        <>
                            <Stack spaceAfter="medium" direction="column">
                                <Heading type="displaySubtitle">All orders</Heading>
                                <Button
                                    type="critical"
                                    width="100px"
                                    onClick={() => {
                                        setDeleteOrders(deleteOrders);
                                        setShowDeleteDialog(true);
                                    }}
                                    disabled={deleteOrders.length == 0}
                                >
                                    Delete
                                </Button>
                                <Table>
                                    <TableHead>
                                        <TableRow>
                                            <TableCell as="th">
                                                <Heading>
                                                    <Checkbox
                                                        checked={checkedAll}
                                                        onChange={() => {
                                                            if (!checkedAll) {
                                                                setDeleteOrders(orders);
                                                            } else {
                                                                setDeleteOrders([]);
                                                            }
                                                        }}
                                                    />
                                                </Heading>
                                            </TableCell>
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
                                        {orders?.map((order: OrderDto, index: number) => (
                                            <TableRow key={index}>
                                                <TableCell>
                                                    <Checkbox
                                                        checked={deleteOrders.some((id) => id == order)}
                                                        onChange={() => {
                                                            if (deleteOrders.some((id) => id == order)) {
                                                                setDeleteOrders(
                                                                    deleteOrders.filter((id) => id != order)
                                                                );
                                                            } else {
                                                                setDeleteOrders([...deleteOrders, order]);
                                                            }
                                                        }}
                                                    />
                                                </TableCell>
                                                <TableCell>
                                                    <TextLink onClick={() => navigate(`/Orders/${order.orderId}`)}>
                                                        {order.orderId}
                                                    </TextLink>
                                                </TableCell>
                                                <TableCell>{new Date(order.orderPlaced).toUTCString()}</TableCell>
                                                <TableCell>{order.orderTotal}</TableCell>
                                                <TableCell>{order.customerName}</TableCell>
                                                <TableCell align="right">
                                                    <span
                                                        style={{ cursor: "pointer" }}
                                                        onClick={() => {
                                                            setDeleteOrder(order);
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
                                <Pagination
                                    pageCount={pagination.TotalPageCount}
                                    selectedPage={pagination.PageNumber}
                                    onPageChange={(pageNumber: number) => {
                                        fetchData(pageNumber);
                                    }}
                                />
                            </Stack>
                        </>
                    )}
                </LayoutColumn>
            </Layout>
            {showDeleteDialog && (
                <Dialog
                    title="Delete"
                    description={
                        deleteOrder
                            ? `Do you want to delete order #${deleteOrder.orderId} of ${deleteOrder.customerName}?`
                            : `Do you want to delete these orders? ${deleteOrders.map((o) => {
                                  return `[${o.orderId}]`;
                              })}`
                    }
                    primaryAction={
                        <Button
                            type="critical"
                            onClick={() => {
                                setIsDeleting(true);
                                let orderIds = deleteOrder
                                    ? [deleteOrder.orderId.toString()]
                                    : deleteOrders.map((o) => o.orderId.toString());
                                axios
                                    .delete(`api/orders`, {
                                        headers: authHeader(),
                                        data: { orderIds } as DeleteOrderDto,
                                    })
                                    .then(() => {
                                        if (deleteOrder) {
                                            createToast("Order deleted");
                                            if (deleteOrders.includes(deleteOrder)) {
                                                setDeleteOrders(
                                                    deleteOrders.filter((o) => o.orderId != deleteOrder.orderId)
                                                );
                                            }
                                        } else {
                                            createToast("Order(s) deleted");
                                            setCheckedAll(false);
                                            setDeleteOrders([]);
                                        }
                                    })
                                    .finally(() => {
                                        fetchData(pagination.PageNumber);
                                        setDeleteOrder(null);
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
                                if (deleteOrder) {
                                    setDeleteOrder(null);
                                }
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
