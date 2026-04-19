import { TooltipProvider } from "@/components/ui/tooltip";
import Expense from "@/features/expense/pages/Expense";
import RootLayout from "@/layout/root-layout.tsx";
import { createBrowserRouter, RouterProvider } from "react-router-dom";

const router = createBrowserRouter([
  {
    path: "/",
    element: <RootLayout />,
    children: [
      { path: "/", element: <div>Home</div> },
      { path: "/expenses", element: <Expense /> },
      { path: "/reports", element: <div>Reports</div> },
      { path: "/settings", element: <div>Settings</div> },
    ],
  },
]);

export function AppRouterProvider() {
  return (
    <TooltipProvider>
      <RouterProvider router={router} />
    </TooltipProvider>
  );
}
