import { DatePicker } from "@/components/date-picker";
import { Input } from "@/components/input/Input";
import { Dropdown } from "@/components/select/Dropdown";
import { useEffect, useState } from "react";
import {
  CATEGORY_OPTIONS,
  DATE_FILTER,
  DATE_FILTER_OPTIONS,
} from "../constants";
import { getDateRangeFromPreset } from "../helper/date-filter";

export interface ExpenseDashboardFilterType {
  searchText: string;
  category: any;
  startDate: Date | null;
  endDate: Date | null;
  datePreset: any;
}

interface ExpenseDashboardFilterProps {
  filters: ExpenseDashboardFilterType;
  setFilters: React.Dispatch<React.SetStateAction<ExpenseDashboardFilterType>>;
}

const ExpenseDashboardFilter = ({
  filters,
  setFilters,
}: ExpenseDashboardFilterProps) => {
  const [category, setCategory] = useState<any | null>(null);

  useEffect(() => {
    if (filters.category) {
      const selectedCategory = CATEGORY_OPTIONS.find(
        (opt) => opt.value === filters.category,
      );
      setCategory(selectedCategory);
    } else {
      setCategory(null);
    }
  }, []);

  useEffect(() => {
    handleChange("category", category?.value ?? null);
  }, [category]);

  const handleChange = (key: string, value: any) => {
    setFilters((prev) => {
      // Manual override → switch to Custom
      if (key === "startDate" || key === "endDate") {
        return {
          ...prev,
          [key]: value,
          datePreset: DATE_FILTER.Custom,
        };
      }

      //Preset selected
      if (key === "datePreset") {
        const presetValue = value?.value || value;
        const { startDate, endDate } = getDateRangeFromPreset(presetValue);

        return {
          ...prev,
          datePreset: value,
          startDate,
          endDate,
        };
      }

      return {
        ...prev,
        [key]: value,
      };
    });
  };

  return (
    <div className="flex justify-end gap-4 py-4 items-center">
      <div className="flex gap-2">
        <div>
          <Input
            placeholder="Search Anything..."
            value={filters.searchText}
            onChange={(e) => handleChange("searchText", e.target.value)}
            className=""
          />
        </div>

        <div>
          <Dropdown
            options={CATEGORY_OPTIONS}
            placeholder={"Category"}
            value={category}
            onChange={(e) => {
              setCategory(e);
            }}
          />
        </div>

        <div>
          <Dropdown
            options={DATE_FILTER_OPTIONS}
            placeholder={"Time Range"}
            value={filters.datePreset}
            onChange={(e) => handleChange("datePreset", e)}
          />
        </div>
        <div>
          <DatePicker
            placeholder="Start Date"
            value={filters?.startDate || undefined}
            onChange={(date) => handleChange("startDate", date)}
          />
        </div>
        <div>
          <DatePicker
            placeholder="End Date"
            value={filters?.endDate || undefined}
            onChange={(date) => handleChange("endDate", date)}
          />
        </div>
      </div>
    </div>
  );
};

export default ExpenseDashboardFilter;
