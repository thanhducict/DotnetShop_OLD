import React from "react";
import DataTable from "./DataTable";
import "./global-style.scss";

const App: React.FC = () => {
    return (
        <>
            <h2>All products</h2>
            <DataTable />
        </>
    );
};

export default App;
