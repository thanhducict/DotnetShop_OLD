import { ComponentMeta, ComponentStory } from "@storybook/react";
import OrderList from "./OrderList";

export default {
    title: "Order Management/Order List",
    component: OrderList,
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
