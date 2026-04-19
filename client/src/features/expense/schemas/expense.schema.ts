import { z } from "zod/v4";

export const expenseSchema = z.object({
  id: z.string().optional(),
  date: z.date(),
  amount: z
    .number()
    .min(1, "Amount is required")
    .refine((val) => !isNaN(Number(val)), "Amount must be number"),

  description: z
    .string()
    .max(1000, "Max 1000 charcters are allowed")
    .optional(),
  tags: z.string().array().optional(),
  category: z.number().min(1, "Category is required"),
  payment: z.number().min(1, "Payment method is required"),
});

export type ExpenseInput = z.infer<typeof expenseSchema>;
export type ExpenseOutput = z.output<typeof expenseSchema>;
