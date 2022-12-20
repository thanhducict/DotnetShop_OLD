import {
    Heading,
    Layout,
    LayoutColumn,
    Table,
    TableBody,
    TableCell,
    TableHead,
    TableRow,
} from "@kiwicom/orbit-components";
import axios from "axios";
import React, { useEffect, useState } from "react";
import { useNavigate, useParams } from "react-router-dom";
import { BEARER_TOKEN } from "../../Constants";
import { CartDto } from "../Models/CartDto";
import { CartLineItemDto } from "../Models/CartLineItemDto";

const CartDetail: React.FC = () => {
    const params = useParams();
    const navigate = useNavigate();
    const [cart, setCart] = useState<CartDto>();
    const [isLoading, setIsLoading] = useState<boolean>(true);

    useEffect(() => {
        axios
            .get<CartDto>(`api/carts/customer/${params.customerId}`)
            .then((result) => result.data)
            .then((data) => {
                // check if object is not empty
                if ((Object.keys(data).length === 0 && data.constructor === Object) == false) {
                    setCart(data);
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
                        <Heading type="displaySubtitle" spaceAfter="medium">
                            {cart?.cartLineItemDtos[0].customerName}'s Cart
                        </Heading>
                        <Table>
                            <TableHead>
                                <TableRow>
                                    <TableCell as="th">
                                        <Heading type="title3">Customer Id</Heading>
                                    </TableCell>
                                    <TableCell as="th">
                                        <Heading type="title3">Customer Name</Heading>
                                    </TableCell>
                                    <TableCell as="th">
                                        <Heading type="title3">Product Id</Heading>
                                    </TableCell>
                                    <TableCell as="th">
                                        <Heading type="title3">Product Name</Heading>
                                    </TableCell>
                                    <TableCell as="th">
                                        <Heading type="title3">Quantity</Heading>
                                    </TableCell>
                                </TableRow>
                            </TableHead>
                            <TableBody>
                                {cart?.cartLineItemDtos?.map((l: CartLineItemDto, index) => (
                                    <TableRow key={index}>
                                        <TableCell>{l.customerId}</TableCell>
                                        <TableCell>{l.customerName}</TableCell>
                                        <TableCell>{l.productId}</TableCell>
                                        <TableCell>{l.productName}</TableCell>
                                        <TableCell>{l.quantity}</TableCell>
                                    </TableRow>
                                ))}
                            </TableBody>
                        </Table>
                    </>
                )}
            </LayoutColumn>
        </Layout>
    );
};

export default CartDetail;
