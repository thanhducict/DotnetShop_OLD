import Order from "./Order";
import { setupServer } from "msw/node";
import { render, screen } from "@testing-library/react";
import { handlers } from "../../../mocks/handlers";

const server = setupServer(...handlers);

beforeAll(() => server.listen());
afterEach(() => server.resetHandlers());
afterAll(() => server.close());

describe("Order tests", () => {
    beforeEach(() => {
        render(<Order />);
    });

    test("Should render correct title", async () => {
        expect(await screen.findByText("Order Details")).toBeInTheDocument();
    });
});
