import { formatDate } from "@/lib/utils";
import { type ColumnDef } from "@tanstack/react-table";
import { DataTableColumnHeader } from "../components/data-table-col-header";

type ColumnType = "text" | "number" | "date" | "currency";

type ColumnConfig<T> = {
  accessorKey: keyof T;
  header: string;
  type?: ColumnType;
  id?: string;
};

export function createColumns<T>(configs: ColumnConfig<T>[]): ColumnDef<T>[] {
  return configs.map((config) => {
    const { accessorKey, header, type = "text" } = config;

    return {
      accessorKey: accessorKey as string,
      header: ({ column }) => (
        <DataTableColumnHeader column={column} title={header} />
      ),
      meta: { label: header },
      cell: ({ row }) => {
        const value = row.original[accessorKey];
        switch (type) {
          case "date":
            return <span>{value ? formatDate(String(value)) : ""}</span>;

          case "number":
            return <span>{Number(value).toLocaleString()}</span>;

          case "currency":
            return <span>₹ {Number(value).toFixed(2)}</span>;

          default:
            return <span>{String(value ?? "")}</span>;
        }
      },
    } satisfies ColumnDef<T>;
  });
}
