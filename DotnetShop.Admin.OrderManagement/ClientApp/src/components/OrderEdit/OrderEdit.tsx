import axios from "axios";
import React, { useEffect, useState } from "react";
import { Button, FormControl, InputGroup, Table } from "react-bootstrap";
import { useParams } from "react-router-dom";
import { OrderDetailViewModel } from "../../models/OrderDetailViewModel";
import { OrderViewModel } from "../../models/OrderViewModel";

const OrderEdit: React.FC = () => {
    const params = useParams();
    const [order, setOrder] = useState<OrderViewModel>({
        orderId: 0,
        orderPlaced: "",
        orderTotal: 0,
        orderDetailViewModels: [],
        customer: { customerName: "", gender: true, address: "" },
    } as OrderViewModel);
    const [isLoading, setIsLoading] = useState<boolean>(true);

    useEffect(() => {
        axios
            .get<OrderViewModel>(`api/OrderManagement/GetOrderByOrderId?orderId=${params.orderId}`)
            .then((result) => {
                const data = result.data;
                // check if object is not empty
                if ((Object.keys(data).length === 0 && data.constructor === Object) == false) {
                    setOrder(data);
                }
            })
            .finally(() => {
                setIsLoading(false);
            });
    }, []);

    return (
        <>
            {!isLoading && order.orderId !== 0 ? (
                <>
                    <h2>Editing Order #{order.orderId}</h2>
                    <p>Order Id: {order.orderId}</p>
                    <p>Order Placed: {order.orderPlaced}</p>
                    <p>
                        Order Total:{" "}
                        {Math.round(
                            order.orderDetailViewModels
                                .map((o) => o.quantity * o.productPrice)
                                .reduce((accumulator, curr) => accumulator + curr) * 100
                        ) / 100}
                    </p>
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
                                    <td>
                                        <InputGroup className="mb-3">
                                            <FormControl
                                                data-testid={`quantity-${index}`}
                                                type="number"
                                                min={1}
                                                name="quantity"
                                                value={od.quantity}
                                                onChange={(e: React.ChangeEvent<HTMLInputElement>) => {
                                                    setOrder({
                                                        ...order,
                                                        orderDetailViewModels: order.orderDetailViewModels.map((item) =>
                                                            item.orderDetailId == od.orderDetailId
                                                                ? {
                                                                      ...item,
                                                                      quantity: parseInt(e.target.value),
                                                                  }
                                                                : item
                                                        ),
                                                    });
                                                }}
                                            />
                                        </InputGroup>
                                    </td>
                                    <td>{od.quantity * od.productPrice}</td>
                                </tr>
                            ))}
                        </tbody>
                    </Table>
                    <Button
                        variant="primary"
                        onClick={() => {
                            const data = {
                                ...order,
                                orderTotal: order.orderDetailViewModels
                                    .map((o) => o.quantity * o.productPrice)
                                    .reduce((accumulator, curr) => accumulator + curr),
                                orderDetailViewModels: order.orderDetailViewModels.map((item) => {
                                    return { ...item, lineItemTotal: item.quantity * item.productPrice };
                                }),
                            } as OrderViewModel;

                            axios
                                .post("api/OrderManagement/UpdateOrder", data)
                                .then((result) => {
                                    const status = document.getElementsByClassName("saving-result")[0];
                                    if (result.status === 200) {
                                        status.innerHTML = "Saved successfully.";
                                    } else {
                                        status.innerHTML = "Failed to save.";
                                    }
                                })
                                .finally(() => {
                                    setTimeout(() => {
                                        const status = document.getElementsByClassName("saving-result")[0];
                                        status.innerHTML = "";
                                    }, 2000);
                                });
                        }}
                    >
                        Save
                    </Button>
                    <span style={{ margin: "10px" }} className="saving-result"></span>
                </>
            ) : (
                <h2>No data</h2>
            )}
        </>
    );
};

export default OrderEdit;
