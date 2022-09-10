import axios from "axios";
import React, { useEffect, useState } from "react";
import { Table } from "react-bootstrap";
import { CartDto } from "../Models/CartDto";
import { CartLineItemDto } from "../Models/CartLineItemDto";

const Carts: React.FC = () => {
    const [carts, setCarts] = useState<CartDto[]>();
    const [isLoading, setIsLoading] = useState<boolean>(true);

    useEffect(() => {
        axios
            .get<CartDto[]>("api/carts")
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
                <>
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
                            {carts?.map((c: CartDto) =>
                                c.cartLineItemDtos.map((l: CartLineItemDto, index) => (
                                    <tr key={index}>
                                        <th scope="row">{l.customerId}</th>
                                        <td>{l.customerName}</td>
                                        <td>{l.productId}</td>
                                        <td>{l.productName}</td>
                                        <td>{l.quantity}</td>
                                    </tr>
                                ))
                            )}
                        </tbody>
                    </Table>
                </>
            )}
        </>
    );
};

export default Carts;
