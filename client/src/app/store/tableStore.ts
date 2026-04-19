// src/store/tableStore.ts
import type {
  PaginationState,
  RowSelectionState,
  SortingState,
} from "@tanstack/react-table";
import { create } from "zustand";
import { createJSONStorage, persist } from "zustand/middleware";

type TableState = {
  pagination: PaginationState;
  sorting: SortingState;
  rowSelection: RowSelectionState;
};

type TableStore = {
  tables: Record<string, TableState>;
  initTable: (key: string, initial?: Partial<TableState>) => void;
  setPagination: (key: string, pagination: PaginationState) => void;
  setSorting: (key: string, sorting: SortingState) => void;
  setRowSelection: (key: string, selection: RowSelectionState) => void;
  resetTable: (key: string) => void;
  resetPagination(key: string): void;
};

const defaultState: TableState = {
  pagination: { pageIndex: 0, pageSize: 10 },
  sorting: [],
  rowSelection: {},
};

export const useTableStore = create<TableStore>()(
  persist(
    (set, get) => ({
      tables: {},

      initTable: (key, initial) => {
        const tables = get().tables;
        if (tables[key]) return;

        set({
          tables: {
            ...tables,
            [key]: { ...defaultState, ...initial },
          },
        });
      },

      setPagination: (key, pagination) =>
        set((state) => ({
          tables: {
            ...state.tables,
            [key]: { ...state.tables[key], pagination },
          },
        })),

      setSorting: (key, sorting) =>
        set((state) => ({
          tables: {
            ...state.tables,
            [key]: { ...state.tables[key], sorting },
          },
        })),

      setRowSelection: (key, rowSelection) =>
        set((state) => ({
          tables: {
            ...state.tables,
            [key]: { ...state.tables[key], rowSelection },
          },
        })),

      resetTable: (key) =>
        set((state) => ({
          tables: {
            ...state.tables,
            [key]: defaultState,
          },
        })),

      resetPagination: (key) =>
        set((state) => ({
          tables: {
            ...state.tables,
            [key]: {
              ...state.tables[key],
              pagination: { ...state.tables[key].pagination, pageIndex: 0 },
            },
          },
        })),
    }),
    {
      name: "table-storage",
      storage: createJSONStorage(() => sessionStorage),
    },
  ),
);
