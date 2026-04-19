import type { ColumnSort, RowData } from "@tanstack/react-table";

export interface ExtendedColumnSort<TData> extends Omit<ColumnSort, "id"> {
  id: Extract<keyof TData, string>;
}

declare module "@tanstack/react-table" {
  interface ColumnMeta<TData extends RowData, TValue> {
    label?: string;
    placeHolder?: string;
    icon?: React.ReactNode;
  }
}

declare module "@tanstack/react-table" {
  interface TableMeta<TData extends RowData> {
    onRowClick?: (row: TData) => void;
  }
}
