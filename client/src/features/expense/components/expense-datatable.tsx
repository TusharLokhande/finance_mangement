import { CommonDrawer } from "@/components/common-drawer/common-drawer";
import DataTableAdvanceToolbar from "@/components/datatable/components/data-table-adavanced-toolbar";
import { DataTable } from "@/components/datatable/data-table";
import useDataTable from "@/components/datatable/hooks/use-datatable";
import { useTableState } from "@/components/datatable/hooks/use-table-state";
import { MultiChipOverflow } from "@/components/multi-chip";
import { Badge } from "@/components/ui/badge";
import { useConfirm } from "@/hooks/use-confirm";
import { formatDate } from "@/lib/utils";
import { useEffect, useState } from "react";
import { CATEGORY_OPTIONS } from "../constants";
import { EXPENSE_DASHBOARD_QUERY_KEY } from "../constants/query";
import { useDeleteExpense } from "../hooks/mutations/use-create-expense-hook";
import { useGetExpenseDashboardData } from "../hooks/queries/use-expense-hook";
import type { ExpenseInput } from "../schemas/expense.schema";
import { EXPENSE_COLUMNS } from "./datatable-columns/expense-columns";

export type ExpenseTableMeta = {
  onClickEdit?: (row: any) => void;
  onClickDelete?: (row: any) => void;
  onClickView?: (row: any) => void;
};

interface ExpenseTableProps {
  onEdit: (expense: ExpenseInput) => void;
  filters: {
    searchText: string;
    category: number | null;
    startDate: Date | null;
    endDate: Date | null;
    datePreset: any;
  };
}

const ExpenseTable = ({ onEdit, filters }: ExpenseTableProps) => {
  const { page, pageSize, sort, handleResetPagination } = useTableState(
    EXPENSE_DASHBOARD_QUERY_KEY,
  );
  const { confirm, ConfirmComponent } = useConfirm();
  const [viewPanelOpen, setViewPanelOpen] = useState(false);
  const [tempExpense, setTempExpense] = useState<ExpenseInput | null>(null);

  const { data } = useGetExpenseDashboardData({
    request: {
      page,
      pageSize,
      sorting: sort?.field
        ? {
            field: sort?.field || "date",
            direction: sort?.order === "asc" ? "asc" : "desc",
          }
        : undefined,
      startDate: filters.startDate ? filters.startDate : undefined,
      endDate: filters.endDate ? filters.endDate : undefined,
      category: Number(filters.category) || undefined,
      searchText: filters.searchText,
    },
  });

  const { mutate: deleteExpense } = useDeleteExpense();

  useEffect(() => {
    handleResetPagination();
  }, [filters, sort]);

  const expenseData = data?.data || [];
  const PAGE_COUNT = data?.pageCount || 0;

  const { table } = useDataTable<ExpenseInput>({
    data: expenseData as ExpenseInput[],
    columns: EXPENSE_COLUMNS,
    type: "server",
    pageCount: PAGE_COUNT,
    tableKey: EXPENSE_DASHBOARD_QUERY_KEY,
    scroll: true,
    meta: {
      onClickEdit: (row: any) => onEdit(row),
      onClickDelete: async (id: string) => await handleDeleteExpense(id),
      onClickView: (row: any) => {
        setTempExpense(row);
        setViewPanelOpen(true);
      },
      onRowClick: (row: any) => {
        setTempExpense(row);
        setViewPanelOpen(true);
      },
    } as any,
  });

  async function handleDeleteExpense(id: string) {
    const ok = await confirm({
      title: "Are you sure you want to delete this expense?",
      description: "You won't be able to recover this.",
      confirmText: "Delete",
      variant: "destructive",
    });
    if (!ok) return;
    deleteExpense(id);
  }

  return (
    <>
      <DataTable table={table}>
        <DataTableAdvanceToolbar table={table}></DataTableAdvanceToolbar>
      </DataTable>

      <ConfirmComponent />

      <CommonDrawer
        open={viewPanelOpen}
        onOpenChange={(open: boolean): void => setViewPanelOpen(open)}
        title={"Expense Details"}
      >
        <div className="space-y-6">
          <div className="grid grid-cols-2 gap-6">
            <InfoItem label="Amount" value={tempExpense?.amount} />
            <InfoItem
              label="Category"
              value={CategoryLabel(tempExpense?.category || 0)}
            />
          </div>

          <InfoItem label="Description" value={tempExpense?.description} />

          <div className="grid grid-cols-2 gap-6">
            <InfoItem
              label="Tags"
              value={<MultiChipOverflow items={tempExpense?.tags || []} />}
            />
            <InfoItem
              label="Date"
              value={formatDate(tempExpense?.date || "")}
            />
          </div>
        </div>
      </CommonDrawer>
    </>
  );
};

export default ExpenseTable;

const InfoItem = ({ label, value }: { label: string; value: any }) => {
  return (
    <div className="space-y-1">
      <p className="text-xs text-muted-foreground uppercase tracking-wide">
        {label}
      </p>
      <div className="text-lg font-semibold">{value || "-"}</div>
    </div>
  );
};

export const CategoryLabel = (value: number) => {
  const option = CATEGORY_OPTIONS.find((opt) => opt.value === value);
  return (
    <Badge variant="outline" className={option?.className}>
      {option?.label}
    </Badge>
  );
};
