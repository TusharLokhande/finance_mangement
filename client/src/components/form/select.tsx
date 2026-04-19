import {
  Controller,
  type Control,
  type ControllerRenderProps,
  type FieldValues,
  type Path,
} from "react-hook-form";
import { Dropdown, type DropdownProps } from "../select/Dropdown";

type Option = {
  label: string;
  value: string | number;
  color?: string;
};

interface RHFDropdownProps<T extends FieldValues> extends Omit<
  DropdownProps,
  "value" | "onChange" | "error" | "onCreateOption"
> {
  name: Path<T>;
  control: Control<T>;
  onCreateOption?: (inputValue: string) => void;
}

export function FormDropdown<T extends FieldValues>({
  name,
  control,
  isMulti,
  onCreateOption,
  ...dropdownProps
}: RHFDropdownProps<T>) {
  const handleOnCreateOption = (
    field: ControllerRenderProps<T, Path<T>>,
    inputValue: string,
  ) => {
    if (onCreateOption) {
      onCreateOption(inputValue);
      const value = isMulti ? [...(field.value || []), inputValue] : inputValue;
      field.onChange(value);
    }
  };
  return (
    <Controller
      name={name}
      control={control}
      render={({ field, fieldState }) => {
        const { options = [] } = dropdownProps;

        // Normalize to number before comparing to handle string/number mismatch
        const value = isMulti
          ? options.filter((o) =>
              (field.value as (string | number)[])
                ?.map((v) => v)
                .includes(o.value),
            )
          : (options.find((o) => o.value === field.value) ?? null);
        return (
          <Dropdown
            {...dropdownProps}
            value={value}
            isMulti={isMulti}
            error={fieldState.error?.message}
            onCreateOption={(inputValue) =>
              handleOnCreateOption(field, inputValue)
            }
            onChange={(selected) => {
              handleOnChange(field, selected);
            }}
          />
        );
      }}
    />
  );

  function handleOnChange(
    field: ControllerRenderProps<T, Path<T>>,
    selected: any,
  ) {
    if (isMulti) {
      field.onChange(
        Array.isArray(selected) ? selected.map((o) => o.value) : [],
      );
    } else {
      field.onChange((selected as Option)?.value ?? null);
    }
  }
}
