import axios from "axios";
import React, { useEffect, useState } from "react";
import { Table } from "react-bootstrap";
import { Link } from "react-router-dom";
import { OrderListViewModel } from "../models/OrderListViewModel";

const OrderList: React.FC = () => {
    const [orders, setOrders] = useState<OrderListViewModel[]>([]);

    useEffect(() => {
        axios.get<OrderListViewModel[]>("api/OrderManagement").then((result) => {
            setOrders(result.data);
        });
    }, []);

    return (
        <>
            <h2>All orders</h2>
            <Table>
                <thead>
                    <tr>
                        <th>Id</th>
                        <th>Order Placed</th>
                        <th>Order Total</th>
                        <th>Username</th>
                        <th></th>
                    </tr>
                </thead>
                <tbody>
                    {orders.map((o: OrderListViewModel, index) => (
                        <tr key={index}>
                            <th scope="row">{o.orderId}</th>
                            <td>
                                {new Date(o.orderPlaced).toLocaleDateString("en-gb", {
                                    year: "numeric",
                                    month: "long",
                                    day: "numeric",
                                    timeZone: "utc",
                                })}
                            </td>
                            <td>{o.orderTotal}</td>
                            <td>{o.customerName}</td>
                            <td>
                                <Link className="btn btn-outline-primary" to={`/OrderDetail/${o.orderId}`}>
                                    Details
                                </Link>
                            </td>
                        </tr>
                    ))}
                </tbody>
            </Table>
        </>
    );
};

export default OrderList;
