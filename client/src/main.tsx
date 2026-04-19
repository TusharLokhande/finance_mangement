import { StrictMode } from "react";
import { createRoot } from "react-dom/client";
import "./index.css";
import { QueryProvider } from "./app/providers/query-provider.tsx";
import { ThemeProvider } from "./app/providers/theme-provider.tsx";
import { AppRouterProvider } from "./app/providers/router-provider.tsx";
import "@fontsource/inter/400.css";
import "@fontsource/inter/500.css";
import "@fontsource/inter/700.css";

createRoot(document.getElementById("root")!).render(
  <StrictMode>
    <QueryProvider>
      <ThemeProvider>
        {/* <TooltipProvider> */}
        <AppRouterProvider />
        {/* </TooltipProvider> */}
      </ThemeProvider>
    </QueryProvider>
  </StrictMode>,
);
