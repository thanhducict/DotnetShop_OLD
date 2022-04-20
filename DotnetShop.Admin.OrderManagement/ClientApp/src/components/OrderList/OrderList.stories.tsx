import { ComponentMeta, ComponentStory } from "@storybook/react";
import { rest } from "msw";
import { MemoryRouter, Route, Routes } from "react-router-dom";
import OrderList from "./OrderList";
import OrderListData from "./orders-list.json";

export default {
    title: "Order Management/Order List",
    component: OrderList,
    decorators: [
        (Story) => (
            <MemoryRouter initialEntries={["/OrderList"]}>
                <Routes>
                    <Route path="OrderList" element={<Story />} />
                </Routes>
            </MemoryRouter>
        ),
    ],
    parameters: {
        msw: [
            rest.get("api/OrderManagement", (_req, res, ctx) => {
                return res(ctx.json(OrderListData));
            }),
        ],
    },
} as ComponentMeta<typeof OrderList>;

export const FirstStyle = () => (
    <OrderList titleColor="title-black" stripedTable={false} borderedTable={false} hoverTable={false} />
);

export const SecondStyle = () => (
    <OrderList titleColor="title-red" stripedTable={true} borderedTable={false} hoverTable={false} />
);

export const ThirdStyle = () => (
    <OrderList titleColor="title-blue" stripedTable={true} borderedTable={true} hoverTable={true} />
);

FirstStyle.storyName = "Default Style";

const Template: ComponentStory<typeof OrderList> = (args) => <OrderList {...args} />;

export const FourthStyle = Template.bind({});
FourthStyle.args = {
    titleColor: "title-black",
    stripedTable: true,
    borderedTable: true,
    hoverTable: false,
};

export const FifthStyle = Template.bind({});
FifthStyle.args = {
    titleColor: "title-red",
    stripedTable: false,
    borderedTable: false,
    hoverTable: false,
};

export const SixthStyle = Template.bind({});
SixthStyle.args = {
    titleColor: "title-blue",
    stripedTable: false,
    borderedTable: true,
    hoverTable: false,
};
