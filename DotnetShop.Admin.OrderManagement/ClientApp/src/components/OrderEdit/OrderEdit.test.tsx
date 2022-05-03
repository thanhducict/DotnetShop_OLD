import OrderEdit from "./OrderEdit";
import { setupServer } from "msw/node";
import { fireEvent, render, screen } from "@testing-library/react";
import { handlers } from "../../../mocks/handlers";
import { MemoryRouter, Route, Routes } from "react-router-dom";
import userEvent from "@testing-library/user-event";

const server = setupServer(...handlers);

beforeAll(() => server.listen());
afterEach(() => server.resetHandlers());
afterAll(() => server.close());

describe("Order Edit tests", () => {
    beforeEach(async () => {
        render(
            <MemoryRouter initialEntries={["/OrderEdit/1"]}>
                <Routes>
                    <Route path="OrderEdit/:orderId" element={<OrderEdit />} />
                </Routes>
            </MemoryRouter>
        );
        await screen.findByText("Editing Order #1");
    });

    test("Should render correct title", async () => {
        expect(await screen.findByText("Editing Order #1")).toBeInTheDocument();
    });

    test("Should enter input values correctly", () => {
        const quantity0 = screen.getByTestId("quantity-0");
        quantity0.focus();
        fireEvent.keyDown(quantity0, { target: { value: 123 } });
        expect(quantity0).toHaveValue(123);
    });

    test("Should post successfully", async () => {
        const button = screen.getByText("Save");
        await userEvent.click(button);
        expect(await screen.findByText("Saved successfully.")).toBeInTheDocument();
    });
});

describe("If there is no data to edit", () => {
    test("Should render correct when no data", async () => {
        render(
            <MemoryRouter initialEntries={["/OrderEdit/0"]}>
                <Routes>
                    <Route path="OrderEdit/:orderId" element={<OrderEdit />} />
                </Routes>
            </MemoryRouter>
        );
        expect(await screen.findByText("No data")).toBeInTheDocument();
    });
});
