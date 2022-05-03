import { rest } from "msw";
import OrderListData from "./orders-list.json";
import OrderDetailsData from "./order-details.json";

export const handlers = [
    rest.get("api/OrderManagement/GetOrders", (_req, res, ctx) => {
        return res(ctx.json(OrderListData), ctx.status(200));
    }),

    rest.get("api/OrderManagement/GetOrderByOrderId", (_req, res, ctx) => {
        const orderId = _req.url.searchParams.get("orderId");
        if (orderId === "0") {
            return res(ctx.json({}), ctx.status(200));
        } else {
            return res(ctx.json(OrderDetailsData), ctx.status(200));
        }
    }),

    rest.post("api/OrderManagement/UpdateOrder", (_req, res, ctx) => {
        return res(ctx.status(200));
    }),
];
