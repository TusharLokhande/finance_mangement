import { useQuery } from "@tanstack/react-query";
import { getDashboardDataAsync, getExpense, getExpenseStats } from "../../api";
import type { ExpenseDashboardRequest } from "../../types/requst.type";

export const useGetExpenseDashboardData = ({
  request,
}: {
  request: ExpenseDashboardRequest;
}) => {
  const query = useQuery({
    queryKey: [
      "expenses",
      request,
      request.category,
      request.startDate,
      request.endDate,
      request.searchText,
      request.sorting,
    ],
    queryFn: () => getDashboardDataAsync(request),
    select: (data) => data,
  });

  return query;
};

export const useGetExpense = (id: string) => {
  const query = useQuery({
    queryKey: ["expense", id],
    queryFn: () => getExpense(id),
    select: (data) => data.data,
  });
  return query;
};

export const useGetExpenseStats = () => {
  const query = useQuery({
    queryKey: ["expense-stats"],
    queryFn: () => getExpenseStats(),
  });

  return query;
};
