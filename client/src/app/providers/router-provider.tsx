import { TooltipProvider } from "@/components/ui/tooltip";
import LoginPage from "@/features/auth/pages/Login";
import Expense from "@/features/expense/pages/Expense";
import { ProtectedRoute } from "@/layout/ProtectedRoute";
import RootLayout from "@/layout/root-layout.tsx";
import { createBrowserRouter, RouterProvider } from "react-router-dom";

const router = createBrowserRouter([
  {
    path: "/",
    element: <ProtectedRoute />,
    children: [
      {
        element: <RootLayout />,
        children: [
          { path: "/", element: <div>Home</div> },
          { path: "/expenses", element: <Expense /> },
          { path: "/reports", element: <div>Reports</div> },
          { path: "/settings", element: <div>Settings</div> },
        ],
      },
    ],
  },
  {
    path: "/login",
    element: <LoginPage />,
  },
  {
    path: "*",
    element: <div>404 Not Found</div>,
  },
]);

export function AppRouterProvider() {
  return (
    <TooltipProvider>
      <RouterProvider router={router} />
    </TooltipProvider>
  );
}
