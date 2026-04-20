import {
  Sidebar,
  SidebarContent,
  SidebarHeader,
} from "@/components/ui/sidebar";
import { cn } from "@/lib/utils";
import {
  BarChart3,
  LayoutDashboard,
  Receipt,
  Settings,
  Wallet,
} from "lucide-react";
import { useLocation, useNavigate } from "react-router-dom";

const OPTIONS = [
  {
    label: "Dashboard",
    value: "dashboard",
    icon: LayoutDashboard,
    link: "/",
  },
  { label: "Expenses", value: "expenses", icon: Receipt, link: "/expenses" },
  { label: "Reports", value: "reports", icon: BarChart3, link: "/reports" },
  { label: "Settings", value: "settings", icon: Settings, link: "/settings" },
];

const AppSideBar = () => {
  const navigate = useNavigate();
  const { pathname } = useLocation();

  return (
    <Sidebar collapsible="offcanvas">
      {/* Header */}
      <SidebarHeader className="bg-transparent border-b border-border h-16 flex items-center justify-center">
        <div className="flex gap-2 items-center justify-center text-2xl">
          <div className="p-2 rounded-lg bg-primary text-primary-foreground">
            <Wallet size={18} />
          </div>
          <span className="text-base font-semibold tracking-tight">
            Effica Nova
          </span>
        </div>
      </SidebarHeader>

      {/* Content */}
      <SidebarContent className="p-3 space-y-1">
        {OPTIONS.map((item) => {
          const Icon = item.icon;
          const isActive =
            item.link === "/"
              ? pathname === "/"
              : pathname.startsWith(item.link);

          return (
            <button
              key={item.value}
              onClick={() => navigate(item.link)}
              className={cn(
                "w-full flex items-center gap-3 px-3 py-2 rounded-lg text-sm transition",
                isActive
                  ? "bg-primary text-primary-foreground"
                  : "hover:bg-muted text-muted-foreground hover:text-foreground",
              )}
            >
              <Icon size={18} />
              <span>{item.label}</span>
            </button>
          );
        })}
      </SidebarContent>
    </Sidebar>
  );
};

export default AppSideBar;
