import { OrderDetailDto } from "./OrderDetailDto";
import { OrderCustomerDto } from "./OrderCustomerDto";

export interface OrderWithDetailDto {
    orderId: number;
    orderPlaced: string;
    orderTotal: number;
    orderDetailViewModels: OrderDetailDto[];
    customer: OrderCustomerDto;
}
