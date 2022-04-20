import Order from "./Order";

export default {
    title: "Order Management/Order",
    component: Order,
};

export const DetailsOnly = () => <Order fullInfo={false} />;

export const FullInfo = () => <Order fullInfo={true} />;
