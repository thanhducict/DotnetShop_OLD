import axios from "axios";
import React, { useEffect, useState } from "react";
import { Table } from "react-bootstrap";
import { useParams } from "react-router-dom";
import { CartDto } from "../Models/CartDto";
import { CartLineItemDto } from "../Models/CartLineItemDto";

const CustomerCart: React.FC = () => {
    const { customerId } = useParams<{ customerId: string }>();
    const [cart, setCart] = useState<CartDto>();
    const [isLoading, setIsLoading] = useState<boolean>(true);

    //TODO: Searchbox by customer ID, use project harmony UX
    // useEffect(() => {
    //     axios
    //         .get<CartDto>(`api/carts/customer/${customerId}`)
    //         .then((result) => result.data)
    //         .then((data) => {
    //             // check if object is not empty
    //             if ((Object.keys(data).length === 0 && data.constructor === Object) == false) {
    //                 setCart(data);
    //             }
    //         })
    //         .finally(() => {
    //             setIsLoading(false);
    //         });
    // }, []);

    return (
        <>
            {!isLoading && (
                <>
                    <h2>{cart?.cartLineItemDtos[0].customerName}'s Cart</h2>
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
                            {cart?.cartLineItemDtos?.map((l: CartLineItemDto, index) => (
                                <tr key={index}>
                                    <th scope="row">{l.customerId}</th>
                                    <td>{l.customerName}</td>
                                    <td>{l.productId}</td>
                                    <td>{l.productName}</td>
                                    <td>{l.quantity}</td>
                                </tr>
                            ))}
                        </tbody>
                    </Table>
                </>
            )}
        </>
    );
};

export default CustomerCart;
