import { ComponentMeta, ComponentStory } from "@storybook/react";
import { MemoryRouter, Route, Routes } from "react-router-dom";
import OrderEdit from "./OrderEdit";

export default {
    title: "Order Management/OrderEdit",
    component: OrderEdit,
} as ComponentMeta<typeof OrderEdit>;

const Template: ComponentStory<typeof OrderEdit> = (args) => <OrderEdit {...args} />;

export const OrderEditWithData = Template.bind({});
OrderEditWithData.decorators = [
    (Story) => (
        <MemoryRouter initialEntries={["/OrderEdit/1"]}>
            <Routes>
                <Route path="OrderEdit/:orderId" element={<Story />} />
            </Routes>
        </MemoryRouter>
    ),
];

export const OrderEditWithoutData = Template.bind({});
OrderEditWithoutData.decorators = [
    (Story) => (
        <MemoryRouter initialEntries={["/OrderEdit/0"]}>
            <Routes>
                <Route path="OrderEdit/:orderId" element={<Story />} />
            </Routes>
        </MemoryRouter>
    ),
];
