import { ComponentMeta, ComponentStory } from "@storybook/react";
import { MemoryRouter, Route, Routes } from "react-router-dom";
import Order from "./Order";

export default {
    title: "Order Management/Order",
    component: Order,
} as ComponentMeta<typeof Order>;

const Template: ComponentStory<typeof Order> = (args) => <Order {...args} />;

export const OrderWithData = Template.bind({});
OrderWithData.decorators = [
    (Story) => (
        <MemoryRouter initialEntries={["/OrderDetail/1"]}>
            <Routes>
                <Route path="OrderDetail/:orderId" element={<Story />} />
            </Routes>
        </MemoryRouter>
    ),
];

export const OrderWithoutData = Template.bind({});
OrderWithoutData.decorators = [
    (Story) => (
        <MemoryRouter initialEntries={["/OrderDetail/0"]}>
            <Routes>
                <Route path="OrderDetail/:orderId" element={<Story />} />
            </Routes>
        </MemoryRouter>
    ),
];
