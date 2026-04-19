// store/theme-store.ts
import { create } from "zustand";
import { persist } from "zustand/middleware";

type Accent = "blue" | "purple" | "green" | "pink";

type ThemeState = {
  theme: "light" | "dark" | "system";
  accent: Accent;
  setTheme: (theme: ThemeState["theme"]) => void;
  setAccent: (accent: Accent) => void;
};

export const useThemeStore = create<ThemeState>()(
  persist(
    (set) => ({
      theme: "system",
      accent: "blue",
      setTheme: (theme) => set({ theme }),
      setAccent: (accent) => set({ accent }),
    }),
    { name: "theme" },
  ),
);
