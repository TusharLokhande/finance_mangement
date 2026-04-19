export interface ExpenseStatsResponse {
  percentChange: number;
  totalThisMonth: number;
  weekly: number;
  topCategory: number;
  avg: number;
  prevMonthTotal: number | null;
}
