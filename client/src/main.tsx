import { MsalProvider } from "@azure/msal-react";
import "@fontsource/inter/400.css";
import "@fontsource/inter/500.css";
import "@fontsource/inter/700.css";
import { StrictMode } from "react";
import { createRoot } from "react-dom/client";
import { QueryProvider } from "./app/providers/query-provider.tsx";
import { AppRouterProvider } from "./app/providers/router-provider.tsx";
import { ThemeProvider } from "./app/providers/theme-provider.tsx";
import { msalInstance } from "./config/authConfig.ts";
import "./index.css";

async function bootstrap() {
  await msalInstance.initialize();

  await msalInstance.handleRedirectPromise();

  createRoot(document.getElementById("root")!).render(
    <StrictMode>
      <MsalProvider instance={msalInstance}>
        <QueryProvider>
          <ThemeProvider>
            <AppRouterProvider />
          </ThemeProvider>
        </QueryProvider>
      </MsalProvider>
    </StrictMode>,
  );
}

bootstrap();
