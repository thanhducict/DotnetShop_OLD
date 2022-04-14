import React from "react";
import { Table } from "react-bootstrap";

const DataTable = () => {
    return (
        <>
            <h1>AHA1</h1>
            {/* <Table>
                    <thead>
                        <tr>
                            <th>Id</th>
                            <th>Order Placed</th>
                            <th>Order Total</th>
                            <th>Username</th>
                        </tr>
                    </thead>
                    <tbody>
                    @foreach (var product in Model)
            {
                <tr>
                    <th scope="row">@product.ProductId</th>
                    <td>@product.Name</td>
                    <td>@product.Price</td>
                    <td><img src="@product.ImageThumbnailUrl" height="40" /></td>
                    <td>
                        <a class="btn btn-outline-primary" asp-controller="Products"
                            asp-action="Details" asp-route-id="@product.ProductId">Details</a>
                        <a class="btn btn-outline-success"  asp-controller="Products"
                            asp-action="Edit" asp-route-id="@product.ProductId">Edit</a>
                        <a class="btn btn-outline-danger">Delete</a>
                    </td>
                </tr>
            
                    </tbody>
                </Table> */}
        </>
    );
};

export default DataTable;
