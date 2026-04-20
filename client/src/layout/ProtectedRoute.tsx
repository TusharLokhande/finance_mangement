// src/components/ProtectedRoute.tsx
import { useAuthStore } from "@/app/store/user-store";
import { Navigate, Outlet, useLocation } from "react-router-dom";

export function ProtectedRoute() {
  const { user } = useAuthStore();
  const location = useLocation();

  if (!user) {
    // Preserve the attempted URL so you can redirect back after login
    return <Navigate to="/login" state={{ from: location }} replace />;
  }

  return <Outlet />;
}
