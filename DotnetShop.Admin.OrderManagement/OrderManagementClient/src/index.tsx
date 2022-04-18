import axios from "axios";
import { createRoot } from "react-dom/client";
import App from "./App";

const container = document.getElementById("root") as HTMLElement;
axios.defaults.baseURL = container.dataset.baseApiUrl != null ? container.dataset.baseApiUrl : "";

if (container != null) {
    const root = createRoot(container);
    root.render(<App />);
}
