import { type Column } from "@tanstack/react-table";

import {
  DropdownMenu,
  DropdownMenuCheckboxItem,
  DropdownMenuContent,
  DropdownMenuItem,
  DropdownMenuTrigger,
} from "@/components/ui/dropdown-menu";
import { cn } from "@/lib/utils";
import { ChevronDown, ChevronsUpDown, ChevronUp, X } from "lucide-react";

interface DataTableColumnHeaderProps<
  TData,
  TValue,
> extends React.HTMLAttributes<HTMLDivElement> {
  column: Column<TData, TValue>;
  title: string;
}

export function DataTableColumnHeader<TData, TValue>({
  column,
  title,
  className,
}: DataTableColumnHeaderProps<TData, TValue>) {
  if (!column.getCanSort()) {
    return <div className={cn(className)}>{title}</div>;
  }

  if (!column.getCanSort()) {
    return <div className={cn(className)}>{title}</div>;
  }

  return (
    <div className={cn("flex items-center gap-2 ml-2", className)}>
      <DropdownMenu>
        <DropdownMenuTrigger className="flex items-center gap-2 outline-none border-none">
          <span>{title}</span>

          {column.getIsSorted() === "desc" ? (
            <ChevronDown />
          ) : column.getIsSorted() === "asc" ? (
            <ChevronUp />
          ) : (
            <ChevronsUpDown />
          )}
        </DropdownMenuTrigger>

        <DropdownMenuContent className="w-28" align="start">
          <DropdownMenuCheckboxItem
            className=""
            checked={column.getIsSorted() === "asc"}
            onCheckedChange={() => column.toggleSorting(false)}
          >
            <ChevronUp /> Asc
          </DropdownMenuCheckboxItem>
          <DropdownMenuCheckboxItem
            className=""
            checked={column.getIsSorted() === "desc"}
            onCheckedChange={() => column.toggleSorting(true)}
          >
            <ChevronDown /> Desc
          </DropdownMenuCheckboxItem>

          <DropdownMenuItem onClick={() => column.clearSorting()}>
            <X /> Clear Sort
          </DropdownMenuItem>
        </DropdownMenuContent>
      </DropdownMenu>
    </div>
  );
}
