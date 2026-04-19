import { ErrorStatus } from "@/types/common";
import { showToast } from "./toast";

export const errorMessageMap: Record<ErrorStatus, string> = {
  [-1]: "No error",
  [1]: "Duplicate record already exists",
  [2]: "Resource not found",
  [3]: "Invalid credentials",
  [5]: "You are not authorized",
  [6]: "Something went wrong",
  [7]: "Validation failed",
  [8]: "Session expired. Please login again",
};

export const handleApiError = (error: any) => {
  const status: ErrorStatus | undefined = error?.response?.data?.errorStatus;

  const message =
    (status && errorMessageMap[status]) ||
    error?.response?.data?.message ||
    "Unexpected error";
  showToast.error(message);
};
