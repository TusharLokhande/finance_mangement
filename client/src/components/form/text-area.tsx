import {
  Controller,
  type Control,
  type FieldValues,
  type Path,
} from "react-hook-form";
import { Textarea } from "../input/TextArea";

interface RHFTextareaProps<T extends FieldValues> {
  name: Path<T>;
  control: Control<T>;
  label?: string;
  placeholder?: string;
  rows?: number;
}

export function FormTextarea<T extends FieldValues>({
  name,
  control,
  label,
  rows = 4,
  ...props
}: RHFTextareaProps<T>) {
  return (
    <Controller
      name={name}
      control={control}
      render={({ field, fieldState }) => (
        <Textarea
          {...field}
          rows={rows}
          label={label}
          error={fieldState.error?.message}
          {...props}
        />
      )}
    />
  );
}
