export interface CartListViewModel {
    allCartLineItems: CartLineItemViewModel[];
}

export interface CartLineItemViewModel {
    customerId: number;
    customerName: string;
    productId: number;
    productName: string;
    quantity: number;
}
