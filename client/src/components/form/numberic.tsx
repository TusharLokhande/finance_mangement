import {
  Controller,
  type Control,
  type FieldValues,
  type Path,
} from "react-hook-form";
import { NumericFormat } from "react-number-format";
import { Input } from "../input/Input";

interface RHFCurrencyInputProps<T extends FieldValues> {
  name: Path<T>;
  control: Control<T>;
  label?: string;
  placeholder?: string;
}

export function FormCurrencyInput<T extends FieldValues>({
  name,
  control,
  label,
  placeholder,
}: RHFCurrencyInputProps<T>) {
  return (
    <Controller
      name={name}
      control={control}
      render={({ field, fieldState }) => {
        return (
          <NumericFormat
            value={field.value ?? ""}
            thousandSeparator=","
            decimalScale={2}
            prefix="₹"
            customInput={Input}
            label={label}
            placeholder={placeholder}
            error={fieldState.error?.message}
            onValueChange={(values) => {
              field.onChange(values.floatValue);
            }}
            onBlur={field.onBlur}
          />
        );
      }}
    />
  );
}
