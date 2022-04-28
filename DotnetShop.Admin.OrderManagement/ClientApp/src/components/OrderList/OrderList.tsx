import axios from "axios";
import React, { useEffect, useState } from "react";
import { Table } from "react-bootstrap";
import { Link } from "react-router-dom";
import { OrderListViewModel } from "../../models/OrderListViewModel";
import "./OrderList.scss";

interface OrderListProps {
    titleColor?: string;
    stripedTable?: boolean;
    borderedTable?: boolean;
    hoverTable?: boolean;
}

const OrderList: React.FC<OrderListProps> = (props) => {
    const [orders, setOrders] = useState<OrderListViewModel[]>([]);
    const [isLoading, setIsLoading] = useState<boolean>(true);

    useEffect(() => {
        axios
            .get<OrderListViewModel[]>("api/OrderManagement")
            .then((result) => {
                const data = result.data;
                // check if object is not empty
                if ((Object.keys(data).length === 0 && data.constructor === Object) == false) {
                    setOrders(data);
                }
            })
            .finally(() => {
                setIsLoading(false);
            });
    }, []);

    return (
        <>
            {!isLoading && (
                <>
                    <h2 className={props.titleColor}>All orders</h2>
                    <Table striped={props.stripedTable} bordered={props.borderedTable} hover={props.hoverTable}>
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
            )}
        </>
    );
};

export default OrderList;
