import OrderList from "./OrderList";
import { setupServer } from "msw/node";
import { render, screen } from "@testing-library/react";
import { handlers } from "../../../mocks/handlers";
import { HashRouter } from "react-router-dom";

const server = setupServer(...handlers);

beforeAll(() => server.listen());
afterEach(() => server.resetHandlers());
afterAll(() => server.close());

describe("Order list tests", () => {
    beforeEach(() => {
        render(<OrderList />, { wrapper: HashRouter });
    });

    test("Should render correct title", async () => {
        expect(await screen.findByText("All orders")).toBeInTheDocument();
    });
});
