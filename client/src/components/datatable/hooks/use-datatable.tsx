import { useTableStore } from "@/app/store/tableStore";
import {
  getCoreRowModel,
  getFacetedMinMaxValues,
  getFacetedRowModel,
  getFacetedUniqueValues,
  getFilteredRowModel,
  getPaginationRowModel,
  getSortedRowModel,
  useReactTable,
  type ColumnSort,
  type TableOptions,
  type TableState,
} from "@tanstack/react-table";
import React, { useMemo } from "react";

interface UseDataTableProps<T>
  extends
    Omit<
      TableOptions<T>,
      | "state"
      | "pageCount"
      | "getCoreRowModel"
      | "manualFiltering"
      | "manualPagination"
      | "manualSorting"
    >,
    Required<Pick<TableOptions<T>, "pageCount">> {
  tableKey: string;
  initialState?: Omit<Partial<TableState>, "sorting"> & {
    sorting?: ColumnSort[];
  };
  history?: "push" | "replace";
  debounceMs?: number;
  throttleMs?: number;
  clearOnDefault?: boolean;
  enableAdvancedFilter?: boolean;
  scroll?: boolean;
  shallow?: boolean;
  startTransition?: React.TransitionStartFunction;
  type?: "client" | "server";
}

export default function useDataTable<T>(props: UseDataTableProps<T>) {
  const { columns, pageCount, initialState, data, type, tableKey, meta } =
    props;

  if (!tableKey) {
    throw new Error("tableKey is required for Zustand table store");
  }

  const { tables, initTable, setPagination, setSorting, setRowSelection } =
    useTableStore();

  // ✅ initialize once
  React.useEffect(() => {
    initTable(tableKey, {
      pagination: initialState?.pagination,
      sorting: initialState?.sorting,
      rowSelection: initialState?.rowSelection,
    });
  }, [tableKey]);

  const tableState = tables[tableKey];

  const pagination = tableState?.pagination ?? {
    pageIndex: 0,
    pageSize: 10,
  };

  const sortingState = tableState?.sorting ?? [];
  const rowSelection = tableState?.rowSelection ?? {};

  const sort = useMemo(() => {
    if (!sortingState.length) return null;

    const first = sortingState[0];
    return {
      field: first.id,
      order: first.desc ? "desc" : "asc",
    };
  }, [sortingState]);

  const table = useReactTable({
    columns,
    data,
    pageCount:
      type === "client"
        ? Math.ceil(data.length / pagination.pageSize)
        : pageCount,
    state: {
      pagination,
      sorting: sortingState,
      rowSelection,
    },
    onPaginationChange: (updater) => {
      const next =
        typeof updater === "function" ? updater(pagination) : updater;
      setPagination(tableKey, next);
    },

    onSortingChange: (updater) => {
      const next =
        typeof updater === "function" ? updater(sortingState) : updater;
      setSorting(tableKey, next);
    },
    onRowSelectionChange: (updater) => {
      const next =
        typeof updater === "function" ? updater(rowSelection) : updater;
      setRowSelection(tableKey, next);
    },
    enableRowSelection: true,
    getCoreRowModel: getCoreRowModel(),
    getFilteredRowModel: getFilteredRowModel(),
    getPaginationRowModel: getPaginationRowModel(),
    getSortedRowModel: getSortedRowModel(),
    getFacetedRowModel: getFacetedRowModel(),
    getFacetedUniqueValues: getFacetedUniqueValues(),
    getFacetedMinMaxValues: getFacetedMinMaxValues(),
    manualPagination: type === "server",
    manualSorting: type === "server",
    manualFiltering: type === "server",
    meta: {
      ...meta,
    },
  });

  return {
    table,
    page: pagination.pageIndex,
    pageSize: pagination.pageSize,
    rowSelection,
    sorting: sort,
  };
}
