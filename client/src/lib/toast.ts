// src/lib/toast.ts
import { toast } from "sonner";

export const showToast = {
  success: (message: string, description?: string) =>
    toast.success(message, {
      description,
      position: "top-center",
    }),

  error: (message: string, description?: string) =>
    toast.error(message, {
      description,
      position: "top-center",
    }),

  warning: (message: string, description?: string) =>
    toast.warning(message, {
      description,
      position: "top-center",
    }),

  info: (message: string, description?: string) =>
    toast(message, {
      description,
      position: "top-center",
    }),

  loading: (message: string) => toast.loading(message),
};
