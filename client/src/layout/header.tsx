import { useThemeStore } from "@/app/store/theme-store";
import { useAuthStore } from "@/app/store/user-store";
import { Button } from "@/components/ui/button";
import {
  Popover,
  PopoverContent,
  PopoverTrigger,
} from "@/components/ui/popover";
import { useSidebar } from "@/components/ui/sidebar";
import { Switch } from "@/components/ui/switch";
import { msalLogout } from "@/config/authConfig";
import { useMsal } from "@azure/msal-react";
import { LogOut, Moon, Sidebar, Sun, Wallet } from "lucide-react";
import { useNavigate } from "react-router-dom";

const AppHeader = () => {
  const { toggleSidebar } = useSidebar();
  const { theme, setTheme } = useThemeStore();
  const { user, clearSession } = useAuthStore();
  const navigate = useNavigate();
  const { accounts } = useMsal();

  console.log(user, accounts[0]);

  const isDark = theme === "dark";

  const handleToggle = (checked: boolean) => {
    setTheme(checked ? "dark" : "light");
  };

  // Extract initials from user name
  const getInitials = (name: string | undefined) => {
    if (!name) return "U";
    return name
      .split(" ")
      .map((word) => word[0])
      .join("")
      .toUpperCase();
  };

  const handleLogout = async () => {
    try {
      clearSession();
      await msalLogout();
    } catch (err) {
      console.error("Logout failed", err);
      navigate("/login");
    }
  };

  const initials = getInitials(user?.name);

  return (
    <header className="sticky top-0 z-10 min-w-full flex items-center justify-between px-6 py-3 border-b border-border bg-surface/70 backdrop-blur supports-backdrop-filter:bg-surface/60">
      {/* Left */}
      <div className="flex items-center gap-3">
        <Sidebar className="cursor-pointer" onClick={toggleSidebar} />

        <div className="p-2 rounded-lg bg-primary text-primary-foreground">
          <Wallet size={18} />
        </div>

        <div className="flex flex-col leading-tight">
          <span className="text-base font-semibold tracking-tight">
            Expense Tracker
          </span>
          <span className="text-xs text-muted-foreground">
            Track. Control. Grow.
          </span>
        </div>
      </div>

      {/* Right: Theme Toggle & User Profile */}
      <div className="flex items-center gap-4">
        <div className="flex items-center gap-2">
          <Sun size={14} className="text-muted-foreground" />
          <Switch
            checked={isDark}
            className="border-2 bg-muted"
            onCheckedChange={handleToggle}
          />
          <Moon size={14} className="text-muted-foreground" />
        </div>

        {/* User Avatar with Popover */}
        <Popover>
          <PopoverTrigger asChild>
            <button className="flex items-center justify-center w-10 h-10 rounded-full bg-primary text-primary-foreground font-semibold cursor-pointer hover:opacity-80 transition-opacity">
              {initials}
            </button>
          </PopoverTrigger>
          <PopoverContent className="w-56" align="end">
            <div className="space-y-3">
              <div className="space-y-1">
                <p className="text-sm font-semibold">{user?.name}</p>
                <p className="text-xs text-muted-foreground">{user?.email}</p>
              </div>
              <Button
                variant="destructive"
                size="sm"
                className="w-full"
                onClick={handleLogout}
              >
                <LogOut size={16} className="mr-2" />
                Logout
              </Button>
            </div>
          </PopoverContent>
        </Popover>
      </div>
    </header>
  );
};

export default AppHeader;
