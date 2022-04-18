import axios from "axios";
import React, { useEffect, useState } from "react";
import { Table } from "react-bootstrap";
import { useParams } from "react-router-dom";
import { OrderDetailViewModel } from "../models/OrderDetailViewModel";
import { OrderViewModel } from "../models/OrderViewModel";

const Order: React.FC = () => {
    const params = useParams();
    const [order, setOrder] = useState<OrderViewModel>({
        orderId: 0,
        orderPlaced: "",
        orderTotal: 0,
        orderDetailViewModels: [],
        customer: { customerName: "", gender: true, address: "" },
    } as OrderViewModel);

    useEffect(() => {
        axios.get<OrderViewModel>(`api/OrderManagement/GetOrderByOrderId?orderId=${params.orderId}`).then((result) => {
            setOrder(result.data);
        });
    }, []);

    return (
        <>
            <h2>Order Details</h2>
            <p>Order Id: {order.orderId}</p>
            <p>Order Placed: {order.orderPlaced}</p>
            <p>Order Total: {order.orderTotal}</p>
            <p>Customer Name: {order.customer.customerName}</p>
            <p>Gender: {order.customer.gender ? "Male" : "Female"}</p>
            <p>Address: {order.customer.address}</p>
            <Table>
                <thead>
                    <tr>
                        <th>Name</th>
                        <th>Product Price</th>
                        <th>Quantity</th>
                        <th>Line Item Total</th>
                        <th></th>
                    </tr>
                </thead>
                <tbody>
                    {order.orderDetailViewModels.map((od: OrderDetailViewModel, index) => (
                        <tr key={index}>
                            <th scope="row">{od.productName}</th>
                            <td>{od.productPrice}</td>
                            <td>{od.quantity}</td>
                            <td>{od.lineItemTotal}</td>
                        </tr>
                    ))}
                </tbody>
            </Table>
        </>
    );
};

export default Order;
