import { rest } from "msw";
import Carts from "./CartManagement/Carts.json";
import CustomerCart from "./CartManagement/CustomerCart.json";

const token = `eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiIyMTU5MTVkNy0yMmMzLTQ5YTQtYWM3MS0yMzg5NzdkZDk4YmEiLCJ1c2V
    yX25hbWUiOiJ0cnVua3MiLCJlbWFpbCI6InRydW5rc0BleGFtcGxlLmNvbSIsImxldmVsIjoiTGVhZGVyc2hpcCIsImh0dHA6Ly9zY2hlbWFzLm1pY3
    Jvc29mdC5jb20vd3MvMjAwOC8wNi9pZGVudGl0eS9jbGFpbXMvcm9sZSI6IkFkbWluIiwibmJmIjoxNjYzMzEyMDUxLCJleHAiOjE2NjMzMTI5NTEsI
    mlzcyI6Imh0dHBzOi8vbG9jYWxob3N0OjcwMjUiLCJhdWQiOiJEb3RuZXRTaG9wIn0.fYkfAGGX7VBl3ULe56rkjOVPQW6PRnSnv8tRvdYID3Q`;

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

    rest.post("api/authentication/authenticate", (req, res, ctx) => {
        return res(ctx.text(token), ctx.status(200));
    }),
];
