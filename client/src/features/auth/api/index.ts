import apiClient from "@/app/providers/axios-provider";

export const handleServerLogin = () => {
  const result = apiClient.get("/auth/login");
  return result;
};
