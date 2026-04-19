import {
  endOfDay,
  endOfMonth,
  endOfWeek,
  startOfDay,
  startOfMonth,
  startOfWeek,
  subMonths,
} from "date-fns";
import { DATE_FILTER } from "../constants";

export const getDateRangeFromPreset = (preset: number) => {
  const today = new Date();

  switch (preset) {
    case DATE_FILTER.Today:
      return {
        startDate: startOfDay(today),
        endDate: endOfDay(today),
      };

    case DATE_FILTER.ThisWeek:
      return {
        startDate: startOfWeek(today, { weekStartsOn: 1 }),
        endDate: endOfWeek(today, { weekStartsOn: 1 }),
      };

    case DATE_FILTER.ThisMonth:
      return {
        startDate: startOfMonth(today),
        endDate: endOfMonth(today),
      };

    case DATE_FILTER.LastMonth:
      const lastMonthDate = subMonths(today, 1);
      return {
        startDate: startOfMonth(lastMonthDate),
        endDate: endOfMonth(lastMonthDate),
      };

    default:
      return {
        startDate: null,
        endDate: null,
      };
  }
};
