import {
  Controller,
  type Control,
  type FieldValues,
  type Path,
} from "react-hook-form";
import { Input } from "../input/Input";

interface RHFInputProps<T extends FieldValues> {
  name: Path<T>;
  control: Control<T>;
  label?: string;
  placeholder?: string;
  type?: string;
}

export function FormInput<T extends FieldValues>({
  name,
  control,
  label,
  type,

  ...props
}: RHFInputProps<T>) {
  return (
    <Controller
      name={name}
      control={control}
      render={({ field, fieldState }) => {
        return (
          <Input
            {...field}
            label={label}
            error={fieldState.error?.message}
            inputMode={type === "number" ? "numeric" : undefined}
            onChange={(e) => {
              let value = e.target.value as any;
              if (type === "number") {
                value = Number(value.replace(/[^0-9]/g, ""));
              }
              field.onChange(value);
            }}
            {...props}
          />
        );
      }}
    />
  );
}
