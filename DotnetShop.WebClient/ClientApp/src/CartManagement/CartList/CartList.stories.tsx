import { ComponentMeta, ComponentStory } from "@storybook/react";
import { MemoryRouter, Route, Routes } from "react-router-dom";
import CartList from "./CartList";

export default {
    title: "Cart Management/Cart List",
    component: CartList,
    decorators: [
        (Story) => (
            <MemoryRouter initialEntries={["/CartList"]}>
                <Routes>
                    <Route path="CartList" element={<Story />} />
                </Routes>
            </MemoryRouter>
        ),
    ],
} as ComponentMeta<typeof CartList>;

const Template: ComponentStory<typeof CartList> = (args) => <CartList {...args} />;

export const DefaultCartList = Template.bind({});
