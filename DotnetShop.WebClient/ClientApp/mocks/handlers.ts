import { rest } from "msw";
import Carts from "./CartManagement/Carts.json";
import CustomerCart from "./CartManagement/CustomerCart.json";

export const handlers = [
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

    // rest.post("api/OrderManagement/UpdateOrder", (req, res, ctx) => {
    //     return res(ctx.status(200));
    // }),
];
