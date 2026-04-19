import type { PageRequest } from "@/types/common";

export interface ExpenseRequest {
  id?: string;
  amount: number;
  description: string;
  date: string;
  category: number;
  tags?: string[];
  payment: number;
}

export type ExpenseDashboardRequest = PageRequest & {
  startDate?: string | Date;
  endDate?: string | Date;
  category?: number;
  searchText?: string;
};
