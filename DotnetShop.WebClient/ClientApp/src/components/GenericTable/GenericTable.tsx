import React from "react";
import { Table, TableBody, TableCell, TableHead, TableRow } from "@kiwicom/orbit-components";

export type ColumnDefinitionType<T, K extends keyof T> = {
    key: K;
    header: string;
    width?: number;
};

type TableProps<T, K extends keyof T> = {
    data: Array<T>;
    columns: Array<ColumnDefinitionType<T, K>>;
};

type TableHeaderProps<T, K extends keyof T> = {
    columns: Array<ColumnDefinitionType<T, K>>;
};

type TableRowsProps<T, K extends keyof T> = {
    data: Array<T>;
    columns: Array<ColumnDefinitionType<T, K>>;
};

const GenericTableHeader = <T, K extends keyof T>({ columns }: TableHeaderProps<T, K>): JSX.Element => {
    const headers = columns.map((column, index) => {
        const style = {
            width: column.width ?? 100,
            borderBottom: "2px solid black",
        };

        return (
            <TableCell as="th" key={`headCell-${index}`}>
                {column.header}
            </TableCell>
        );
    });

    return (
        <TableHead>
            <TableRow>{headers}</TableRow>
        </TableHead>
    );
};

const GenericTableRows = <T, K extends keyof T>({ data, columns }: TableRowsProps<T, K>): JSX.Element => {
    const rows = data.map((row, index) => {
        return (
            <TableRow key={`row-${index}`}>
                {columns.map((column, index2) => (
                    <TableCell key={`cell-${index2}`}>
                        <>{row[column.key]}</>
                    </TableCell>
                ))}
            </TableRow>
        );
    });

    return <TableBody>{rows}</TableBody>;
};

const GenericTable = <T, K extends keyof T>({ data, columns }: TableProps<T, K>): React.JSX.Element => {
    return (
        <Table>
            <GenericTableHeader columns={columns} />
            <GenericTableRows data={data} columns={columns} />
        </Table>
    );
};

export default GenericTable;
