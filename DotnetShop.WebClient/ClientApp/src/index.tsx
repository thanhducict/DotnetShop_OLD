import axios from "axios";
import { createRoot } from "react-dom/client";
import App from "./App";
import { BEARER_TOKEN } from "./Constants";

const container = document.getElementById("root") as HTMLElement;
const token = localStorage.getItem(BEARER_TOKEN);
axios.defaults.baseURL = container.dataset.baseApiUrl != null ? container.dataset.baseApiUrl : "";
axios.defaults.headers.common = {
    Authorization: `Bearer ${token}`,
};

if (container != null) {
    if (process.env.NODE_ENV === "development") {
        const { worker } = require("../mocks/browser");
        worker.start();
    }

    const root = createRoot(container);
    root.render(<App />);
}
