import { useQuery } from "@tanstack/react-query";
import { handleServerLogin } from "../../api";

export const useLogin = () => {
  return useQuery({
    queryKey: ["login"],
    queryFn: handleServerLogin,
    enabled: false, // Disable automatic execution on mount
  });
};
