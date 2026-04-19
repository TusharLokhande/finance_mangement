import { useThemeStore } from "@/app/store/theme-store";
import { useSidebar } from "@/components/ui/sidebar";
import { Switch } from "@/components/ui/switch";
import { Moon, Sidebar, Sun, Wallet } from "lucide-react";

const AppHeader = () => {
  const { toggleSidebar } = useSidebar();
  const { theme, setTheme } = useThemeStore();

  const isDark = theme === "dark";

  const handleToggle = (checked: boolean) => {
    setTheme(checked ? "dark" : "light");
  };

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

      {/* Right: Theme Toggle */}
      <div className="flex items-center gap-2">
        <Sun size={14} className="text-muted-foreground" />
        <Switch
          checked={isDark}
          className="border-2 bg-muted"
          onCheckedChange={handleToggle}
        />
        <Moon size={14} className="text-muted-foreground" />
      </div>
    </header>
  );
};

export default AppHeader;
