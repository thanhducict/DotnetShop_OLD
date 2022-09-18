import { rest } from "msw";
import Carts from "./CartManagement/Carts.json";
import CustomerCart from "./CartManagement/CustomerCart.json";
import Customers from "./CustomerManagement/Customers.json";
import Customer from "./CustomerManagement/Customer.json";

const token = "ABCXYZ";

export const handlers = [
    rest.post("api/authentication/authenticate", (req, res, ctx) => {
        return res(ctx.text(token), ctx.status(200));
    }),

    rest.get("api/carts", (req, res, ctx) => {
        return res(ctx.json(Carts), ctx.status(200));
    }),

    rest.get("api/carts/customer/:customerId", (req, res, ctx) => {
        const { customerId } = req.params;

        if (customerId === "1") {
            return res(ctx.json(CustomerCart), ctx.status(200));
        } else {
            return res(ctx.json({}), ctx.status(200));
        }
    }),

    rest.get("api/v1/customers?pageNumber=1&pageSize=5", (req, res, ctx) => {
        return res(ctx.json(Customers), ctx.status(200));
    }),

    rest.get("/api/v1/customers/:customerId", (req, res, ctx) => {
        const { customerId } = req.params;

        if (customerId === "1") {
            return res(ctx.json(Customer), ctx.status(200));
        } else {
            return res(ctx.json({}), ctx.status(200));
        }
    }),

    rest.post("api/v1/customers/", (req, res, ctx) => {
        return res(ctx.status(200));
    }),

    rest.delete("api/v1/customers/", (req, res, ctx) => {
        return res(ctx.status(200));
    }),
];
