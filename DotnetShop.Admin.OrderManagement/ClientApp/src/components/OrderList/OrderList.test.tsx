import { render, RenderResult } from "@testing-library/react";
import OrderList from "./OrderList";

let documentBody: RenderResult;

describe("Order list tests", () => {
    beforeEach(() => {
        documentBody = render(<OrderList />);
    });
    it("Should render correct title", () => {
        expect(documentBody.getByText("All orders")).toBeInTheDocument();
    });
});
