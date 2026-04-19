import { createColumns } from "@/components/datatable/lib/create-columns";
import { MultiChipOverflow } from "@/components/multi-chip";
import { Badge } from "@/components/ui/badge";
import { CATEGORY_OPTIONS } from "@/features/expense/constants";
import type { ColumnDef } from "@tanstack/react-table";
import { Edit, EyeIcon, Trash2 } from "lucide-react";
import { getPaymentModeName } from "../../helper";
import type { ExpenseOutput } from "../../schemas/expense.schema";
import type { ExpenseTableMeta } from "../expense-datatable";

export const EXPENSE_COLUMNS: ColumnDef<ExpenseOutput>[] = [
  ...createColumns<ExpenseOutput>([
    { accessorKey: "date", header: "Date", type: "date", id: "date" },
    { accessorKey: "amount", header: "Amount", type: "currency", id: "amount" },
  ]),
  {
    accessorKey: "category",
    header: "Category",
    id: "category",
    cell: ({ row }) => {
      const category = CATEGORY_OPTIONS.find(
        (v) => v.value === Number(row.original.category),
      );
      return (
        <Badge variant="outline" className={category?.className}>
          {category?.label}
        </Badge>
      );
    },
  },
  {
    accessorKey: "payment",
    header: "Payment Method",
    id: "payment",
    cell: ({ row }) => {
      const payment = getPaymentModeName(Number(row.original.payment));
      return <span className="capitalize">{payment}</span>;
    },
  },
  {
    accessorKey: "tags",
    header: "Tags",
    id: "tags",
    cell: ({ row }) => {
      const tags: string[] = row.original.tags || [];
      if (tags.length === 0) return <span className=""></span>;
      return (
        <div className="flex gap-2 flex-wrap">
          <MultiChipOverflow items={tags} maxVisible={1} />
        </div>
      );
    },
  },
  {
    id: "actions",
    header: "Actions",
    enableSorting: false,
    cell: ({ row, table }) => {
      const meta = table.options.meta as ExpenseTableMeta;
      const id = row.original.id;

      return (
        // <DropdownMenu>
        //   <DropdownMenuTrigger asChild>
        //     <Button variant="ghost" className="h-8 w-8 p-0">
        //       <span className="sr-only">Open menu</span>
        //       <MoreHorizontal className="h-4 w-4" />
        //     </Button>
        //   </DropdownMenuTrigger>
        //   <DropdownMenuContent align="end">
        //     <DropdownMenuLabel>Actions</DropdownMenuLabel>
        //     <DropdownMenuItem
        //       onClick={(e) => {
        //         e.stopPropagation();
        //         meta?.onClickEdit?.(row.original);
        //       }}
        //     >
        //       Edit
        //     </DropdownMenuItem>
        //     <DropdownMenuItem
        //       onClick={() => {
        //         meta?.onClickView?.(row.original);
        //       }}
        //     >
        //       View Details
        //     </DropdownMenuItem>
        //     <DropdownMenuSeparator />
        //     <DropdownMenuItem
        //       className="bg-red-500 text-white"
        //       onClick={() => {
        //         meta?.onClickDelete?.(row.original.id);
        //       }}
        //     >
        //       Delete
        //     </DropdownMenuItem>
        //   </DropdownMenuContent>
        // </DropdownMenu>

        <div className="flex gap-2">
          <EyeIcon
            className="h-4 w-4"
            onClick={(e) => {
              e.stopPropagation();
              meta?.onClickView?.(row.original);
            }}
          />

          <Edit
            className="h-4 w-4"
            onClick={(e) => {
              e.stopPropagation();
              meta?.onClickEdit?.(row.original);
            }}
          />

          <Trash2
            className="h-4 w-4 text-red-500"
            onClick={(e) => {
              e.stopPropagation();
              meta?.onClickDelete?.(row.original.id);
            }}
          />
        </div>
      );
    },
  },
];
