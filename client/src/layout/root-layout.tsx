import { Loader } from "@/components/loader";
import { SidebarProvider } from "@/components/ui/sidebar";
import { Outlet } from "react-router-dom";
import { Toaster } from "sonner";
import AppHeader from "./header";
import AppSideBar from "./sidebar/app-sidebar";

const RootLayout = () => {
  return (
    <SidebarProvider>
      <Loader />
      <div className="flex min-h-screen bg-background">
        {/* Sidebar */}
        <AppSideBar />

        {/* Right Side (Header + Content) */}
        <div className="flex flex-1 flex-col">
          {/* Header */}
          <AppHeader />

          {/* Main Content */}
          <main className="flex-1 overflow-auto">
            <div className="p-6 md:p-8 lg:p-10">
              <Outlet />
            </div>
          </main>
        </div>
      </div>

      <Toaster position="top-right" richColors />
    </SidebarProvider>
  );
};

export default RootLayout;
