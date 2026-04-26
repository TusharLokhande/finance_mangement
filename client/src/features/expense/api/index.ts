import apiClient from "@/app/providers/axios-provider";
import type { CommonDashboardResponse, CommonResponse } from "@/types/common";
import type {
  ExpenseDashboardRequest,
  ExpenseRequest,
} from "../types/requst.type";
import type { ExpenseStatsResponse } from "../types/response.type";

export const createExpense: (
  expense: ExpenseRequest,
) => Promise<CommonResponse<unknown>> = async (expense: ExpenseRequest) => {
  try {
    const { data } = await apiClient.post("/expense", expense);
    return data;
  } catch (error) {
    console.error("Failed to create expense:", error);
    throw error;
  }
};

export const updateExpense: (
  expense: ExpenseRequest,
  id: string,
) => Promise<CommonResponse<unknown>> = async (
  expense: ExpenseRequest,
  id: string,
) => {
  try {
    const { data } = await apiClient.put(`/expense/${id}`, expense);
    return data;
  } catch (error) {
    console.error("Failed to update expense:", error);
    throw error;
  }
};

export const deleteExpense: (
  id: string,
) => Promise<CommonResponse<unknown>> = async (id: string) => {
  try {
    const { data } = await apiClient.delete(`/expense/${id}`);
    return data;
  } catch (error) {
    console.error("Failed to delete expense:", error);
    throw error;
  }
};

export const getExpense: (
  id: string,
) => Promise<CommonResponse<unknown>> = async (id: string) => {
  const { data } = await apiClient.get(`/expense/${id}`);
  return data;
};

export const getDashboardDataAsync: (
  params: ExpenseDashboardRequest,
) => Promise<CommonDashboardResponse<unknown>> = async (
  params: ExpenseDashboardRequest,
) => {
  const { data } = await apiClient.post("/expense/dashboard", { ...params });
  return data;
};

export const getExpenseStats: ({
  startDate,
  endDate,
  category,
}: {
  startDate: Date;
  endDate: Date;
  category: number | null;
}) => Promise<ExpenseStatsResponse> = async ({
  startDate,
  endDate,
  category,
}) => {
  const { data } = await apiClient.get("/expense/stats", {
    params: { startDate, endDate, category: category?.toString() },
  });
  return data;
};
