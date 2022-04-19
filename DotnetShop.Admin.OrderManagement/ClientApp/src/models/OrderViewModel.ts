import { CustomerViewModel } from "./CustomerViewModel";
import { OrderDetailViewModel } from "./OrderDetailViewModel";

export interface OrderViewModel {
    orderId: number;
    orderPlaced: string;
    orderTotal: number;
    orderDetailViewModels: OrderDetailViewModel[];
    customer: CustomerViewModel;
}
