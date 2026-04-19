import { showToast } from "@/lib/toast";
import type { CommonResponse } from "@/types/common";
import { useMutation, useQueryClient } from "@tanstack/react-query";
import type { AxiosError } from "axios";
import { createExpense, deleteExpense, updateExpense } from "../../api";
import type { ExpenseRequest } from "../../types/requst.type";

export const useCreateExpense = () => {
  const queryClient = useQueryClient();
  return useMutation({
    mutationFn: (request: ExpenseRequest) => createExpense(request),
    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: ["expenses"] });
      queryClient.invalidateQueries({ queryKey: ["expense-stats"] });
      showToast.success("Expense created");
    },
    onError: (err: AxiosError) => {
      const error = err.response?.data as CommonResponse<unknown>;
      showToast.error(error.message || "Failed to create expense");
    },
  });
};

export const useUpdateExpense = (id: string) => {
  const queryClient = useQueryClient();

  return useMutation({
    mutationFn: (request: ExpenseRequest) => updateExpense(request, id),
    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: ["expenses"] });
      queryClient.invalidateQueries({ queryKey: ["expense-stats"] });
      showToast.success("Expense updated");
    },
    onError: (err: AxiosError) => {
      const error = err.response?.data as CommonResponse<unknown>;
      showToast.error(error.message || "Failed to update expense");
    },
  });
};

export const useDeleteExpense = () => {
  const queryClient = useQueryClient();
  return useMutation({
    mutationFn: (id: string) => deleteExpense(id),
    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: ["expenses"] });
      queryClient.invalidateQueries({ queryKey: ["expense-stats"] });
      showToast.success("Expense deleted");
    },
    onError: (err: AxiosError) => {
      const error = err.response?.data as CommonResponse<unknown>;
      showToast.error(error.message || "Failed to delete expense");
    },
  });
};
