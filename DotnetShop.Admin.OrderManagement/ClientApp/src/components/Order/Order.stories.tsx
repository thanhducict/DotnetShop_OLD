import { ComponentMeta, ComponentStory } from "@storybook/react";
import { rest } from "msw";
import { MemoryRouter, Route, Routes } from "react-router-dom";
import Order from "./Order";
import OrderDetailsData from "./order-details.json";

export default {
    title: "Order Management/Order",
    component: Order,
    decorators: [
        (Story) => (
            <MemoryRouter initialEntries={["/OrderDetail/1"]}>
                <Routes>
                    <Route path="OrderDetail/:orderId" element={<Story />} />
                </Routes>
            </MemoryRouter>
        ),
    ],
    parameters: {
        msw: [
            rest.get("api/OrderManagement/GetOrderByOrderId?orderId=1", (_req, res, ctx) => {
                return res(ctx.json(OrderDetailsData));
            }),
        ],
    },
} as ComponentMeta<typeof Order>;

const Template: ComponentStory<typeof Order> = (args) => <Order {...args} />;

export const DetailsOnly = Template.bind({});
DetailsOnly.args = {
    fullInfo: false,
};

export const FullInfo = Template.bind({});
FullInfo.args = {
    fullInfo: true,
};
