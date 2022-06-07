import axios from "axios";
import React, { useEffect, useState } from "react";
import { Table } from "react-bootstrap";
import { CartLineItemViewModel, CartListViewModel } from "./CartListViewModel";
import "./CartList.scss";

const CartList: React.FC = () => {
    const [carts, setCarts] = useState<CartListViewModel>();
    const [isLoading, setIsLoading] = useState<boolean>(true);

    useEffect(() => {
        axios
            .get<CartListViewModel>("api/CartManagement/GetCarts")
            .then((result) => result.data)
            .then((data) => {
                // check if object is not empty
                if ((Object.keys(data).length === 0 && data.constructor === Object) == false) {
                    setCarts(data);
                }
            })
            .finally(() => {
                setIsLoading(false);
            });
    }, []);

    return (
        <>
            {!isLoading && (
                <div className="cart-list">
                    <h2>All carts</h2>
                    <Table>
                        <thead>
                            <tr>
                                <th>Customer Id</th>
                                <th>Customer Name</th>
                                <th>Product Id</th>
                                <th>Product Name</th>
                                <th>Quantity</th>
                            </tr>
                        </thead>
                        <tbody>
                            {carts?.allCartLineItems.map((c: CartLineItemViewModel, index) => (
                                <tr key={index}>
                                    <th scope="row">{c.customerId}</th>
                                    <td>{c.customerName}</td>
                                    <td>{c.productId}</td>
                                    <td>{c.productName}</td>
                                    <td>{c.quantity}</td>
                                </tr>
                            ))}
                        </tbody>
                    </Table>
                </div>
            )}
        </>
    );
};

export default CartList;
