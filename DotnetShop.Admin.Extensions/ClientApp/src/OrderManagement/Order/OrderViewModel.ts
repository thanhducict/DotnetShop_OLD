

export interface OrderViewModel {
    orderId: number;
    orderPlaced: string;
    orderTotal: number;
    orderDetailViewModels: OrderDetailViewModel[];
    customer: CustomerViewModel;
}

export interface OrderDetailViewModel {
    orderDetailId: number;
    productName: string;
    productPrice: number;
    quantity: number;
    lineItemTotal: number;
}


export interface CustomerViewModel {
    customerName: string;
    gender: boolean;
    address: string;
}

