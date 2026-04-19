import { FormDatePicker } from "@/components/form/date-picker";
import { FormInput } from "@/components/form/input";
import { FormDropdown } from "@/components/form/select";
import { FormTextarea } from "@/components/form/text-area";
import { Button } from "@/components/ui/button";
import { zodResolver } from "@hookform/resolvers/zod";
import { useState } from "react";
import { useForm } from "react-hook-form";
import { CATEGORY_OPTIONS, PAYMENT_MODE_OPTIONS } from "../constants";
import {
  useCreateExpense,
  useUpdateExpense,
} from "../hooks/mutations/use-create-expense-hook";
import { expenseSchema, type ExpenseInput } from "../schemas/expense.schema";
import type { ExpenseRequest } from "../types/requst.type";

const INITIAL_TAG_OPTIONS = [
  { label: "Food", value: "food" },
  { label: "Transport", value: "transport" },
  { label: "Entertainment", value: "entertainment" },
  { label: "Metro", value: "metro" },
  { label: "Petrol", value: "petrol" },
  { label: "Breakfast", value: "breakfast" },
  { label: "Lunch", value: "lunch" },
];

const AddExpenseForm = ({
  tempExpense,
  handleResetTempExpense,
}: {
  tempExpense: ExpenseRequest | null;
  handleResetTempExpense: () => void;
}) => {
  // inside component:
  const [tagsOptions, setTagsOptions] = useState(INITIAL_TAG_OPTIONS);

  const form = useForm<ExpenseInput>({
    resolver: zodResolver(expenseSchema),
    defaultValues: {
      id: tempExpense?.id || undefined,
      date: tempExpense?.date ? new Date(tempExpense.date) : new Date(),
      amount: tempExpense?.amount || 0,
      description: tempExpense?.description || "",
      tags: tempExpense?.tags || [],
      category: tempExpense?.category || 0,
      payment: tempExpense?.payment || 0,
    },
  });

  const { mutate: createMutate, isPending: isCreating } = useCreateExpense();
  const { mutate: updateMutate, isPending: isUpdating } = useUpdateExpense(
    String(tempExpense?.id),
  );

  const handleSubmit = form.handleSubmit((data: ExpenseInput) => {
    const request: ExpenseRequest = {
      amount: data.amount,
      description: data?.description || "",
      date: data.date.toISOString(),
      category: data.category,
      tags: data.tags,
      payment: data.payment,
    };
    if (tempExpense) {
      updateMutate(request);
    } else {
      createMutate(request);
    }
    handleResetTempExpense();
    form.reset({
      id: undefined,
      amount: 0,
      description: "",
      tags: [],
      category: 0,
    });
  });

  return (
    <form className="px-4 py-8 flex flex-col gap-4" onSubmit={handleSubmit}>
      <span className="font-sans text-xl font-medium">Add New Expense</span>

      <div className="grid grid-cols-1 md:grid-cols-2 gap-2">
        <FormInput
          label="Amount"
          type="number"
          control={form.control}
          name={"amount"}
        />
        <FormDatePicker label="Date" control={form.control} name="date" />
      </div>

      <div className="grid grid-cols-1 md:grid-cols-2 gap-2">
        <FormDropdown
          options={CATEGORY_OPTIONS}
          control={form.control}
          name={"category"}
          label="Category"
        />
        <FormDropdown
          options={tagsOptions}
          control={form.control}
          name={"tags"}
          label="Tags"
          onCreateOption={(str) => {
            setTagsOptions((prev) => [...prev, { label: str, value: str }]);
          }}
          type="creatable"
          isMulti
        />
      </div>

      <div className="col-span-2">
        <FormTextarea
          label="Description"
          placeholder="e.g finance, amazon"
          control={form.control}
          name={"description"}
          rows={2}
        />
      </div>

      <div>
        <FormDropdown
          options={PAYMENT_MODE_OPTIONS}
          control={form.control}
          name={"payment"}
          label="Payment Method"
        />
      </div>

      <div>
        <Button
          type="submit"
          className="w-full h-10"
          disabled={isCreating || isUpdating}
        >
          {tempExpense ? "Update Expense" : "Create Expense"} +
        </Button>
      </div>
    </form>
  );
};

export default AddExpenseForm;
