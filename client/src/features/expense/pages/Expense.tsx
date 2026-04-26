import { useState, type FC } from "react";
import AddExpenseForm from "../components/add-expense-form";
import ExpenseSummaryCards from "../components/expense-card";
import ExpenseTable from "../components/expense-datatable";
import ExpenseDashboardFilter, {
  type ExpenseDashboardFilterType,
} from "../components/expense-filter";
import { DATE_FILTER } from "../constants";
import { getDateFilter } from "../helper";
import { getDateRangeFromPreset } from "../helper/date-filter";
import type { ExpenseRequest } from "../types/requst.type";

interface ExpenseProps {}

const Expense: FC<ExpenseProps> = ({}) => {
  const [tempExpense, setTempExpense] = useState<ExpenseRequest | null>(null);

  const initialRange = getDateRangeFromPreset(DATE_FILTER.ThisMonth);
  const [filters, setFilters] = useState<ExpenseDashboardFilterType>({
    searchText: "",
    category: null,
    startDate: initialRange.startDate,
    endDate: initialRange.endDate,
    datePreset: getDateFilter(DATE_FILTER.ThisMonth),
  });

  const handleResetTempExpense = () => {
    setTempExpense(null);
  };

  const handleEditExpense = (expense: any) => {
    console.log("Editing expense:", expense);
    setTempExpense(expense);
  };

  return (
    <div className="w-full">
      {/* GLOBAL FILTER */}
      <ExpenseDashboardFilter filters={filters} setFilters={setFilters} />

      {/* STATS */}
      <ExpenseSummaryCards
        startDate={filters.startDate as Date}
        endDate={filters.endDate as Date}
        category={filters.category}
      />

      {/* BODY & RECENT TRANSACTIONS  */}
      <div className="mt-6 flex gap-4 min-h-40">
        <div className="flex-1  max-h-full rounded-xl shadow px-4 py-4">
          <ExpenseTable onEdit={handleEditExpense} filters={filters} />
        </div>

        <div className="w-1/3 border border-border min-h-full rounded-xl shadow">
          <AddExpenseForm
            key={tempExpense?.id ?? "new"}
            tempExpense={tempExpense}
            handleResetTempExpense={handleResetTempExpense}
          />
        </div>
      </div>
    </div>
  );
};

export default Expense;
