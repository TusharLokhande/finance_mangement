// src/hooks/useTableState.ts
import { useTableStore } from "@/app/store/tableStore";
import { useMemo } from "react";

export const useTableState = (tableKey: string) => {
  const tableState = useTableStore((state) => state.tables[tableKey]);

  const pagination = tableState?.pagination ?? {
    pageIndex: 0,
    pageSize: 10,
  };

  const sorting = tableState?.sorting ?? [];
  const rowSelection = tableState?.rowSelection ?? {};

  const sort = useMemo(() => {
    if (!sorting.length) return null;
    const first = sorting[0];
    return {
      field: first.id,
      order: first.desc ? "desc" : "asc",
    };
  }, [sorting]);

  const handleResetPagination = () =>
    tableState &&
    useTableStore.getState().setPagination(tableKey, {
      pageIndex: 0,
      pageSize: pagination.pageSize ?? 10,
    });

  return {
    sorting,
    rowSelection,
    page: pagination.pageIndex + 1,
    pageSize: pagination.pageSize,
    sort,
    handleResetPagination,
  };
};
