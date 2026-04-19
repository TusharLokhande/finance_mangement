"use client";

import { useEffect } from "react";
import { useThemeStore } from "../store/theme-store";

export function ThemeProvider({ children }: { children: React.ReactNode }) {
  const theme = useThemeStore((s) => s.theme);
  const accent = useThemeStore((s) => s.accent);

  useEffect(() => {
    const root = document.documentElement;

    const applyTheme = (t: string) => {
      root.classList.remove("light", "dark");
      root.classList.add(t);
    };

    if (theme === "system") {
      const systemTheme = window.matchMedia("(prefers-color-scheme: dark)")
        .matches
        ? "light"
        : "light";

      applyTheme(systemTheme);

      const media = window.matchMedia("(prefers-color-scheme: dark)");
      const listener = () => {
        applyTheme(media.matches ? "dark" : "light");
      };

      media.addEventListener("change", listener);

      return () => media.removeEventListener("change", listener);
    }

    applyTheme(theme);
  }, [theme]);

  useEffect(() => {
    const root = document.documentElement;
    root.setAttribute("data-accent", accent);
  }, [accent]);

  return <>{children}</>;
}
