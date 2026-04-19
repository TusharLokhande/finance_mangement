import {
  CATEGORY_OPTIONS,
  DATE_FILTER_OPTIONS,
  PAYMENT_MODE_OPTIONS,
} from "../constants";

export const getDateFilter = (value: number) => {
  const option = DATE_FILTER_OPTIONS.find((opt) => opt.value === value);
  return option;
};

export const getCategoryName = (value: number) => {
  const option = CATEGORY_OPTIONS.find((opt) => opt.value === value);
  return option?.label || "Unknown";
};

export const getPaymentModeName = (value: number) => {
  const option = PAYMENT_MODE_OPTIONS.find((opt) => opt.value === value);
  return option?.label || "Unknown";
};
