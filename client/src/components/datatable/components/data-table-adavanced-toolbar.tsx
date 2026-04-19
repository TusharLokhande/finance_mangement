import { Button } from "@/components/ui/button";
import { cn } from "@/lib/utils";
import type { Table } from "@tanstack/react-table";
import { DataGridViewMenu } from "./data-table-grid-view-menu";

interface DataTableAdvanceToolbarProps<
  T,
> extends React.HTMLAttributes<HTMLDivElement> {
  table: Table<T>;
}

const DataTableAdvanceToolbar = <T,>({
  className,
  children,
  table,
  ...props
}: DataTableAdvanceToolbarProps<T>) => {
  return (
    <div
      role="toolbar"
      aria-orientation="horizontal"
      className={cn(
        "flex items-start  w-full justify-between space-x-1",
        className,
      )}
      {...props}
    >
      <div className="flex flex-1 flex-wrap items-center gap-3">{children}</div>
      <div className="flex items-center gap-3">
        <Button
          aria-label="Toggle columns"
          role="combobox"
          variant="outline"
          size="sm"
          className="ms-auto hidden h-8 font-normal lg:flex"
          disabled={false}
          onClick={() => {
            table.resetSorting();
            table.resetColumnVisibility();
            table.setPageIndex(0);
            table.setPageSize(10);
          }}
        >
          Reset
        </Button>
      </div>
      <div className="flex items-center gap-3">
        <DataGridViewMenu table={table} align="end" />
      </div>
    </div>
  );
};

export default DataTableAdvanceToolbar;
