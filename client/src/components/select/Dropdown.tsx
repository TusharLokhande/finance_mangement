import Select, { type StylesConfig } from "react-select";
import AsyncSelect from "react-select/async";
import AsyncCreatableSelect from "react-select/async-creatable";
import CreatableSelect from "react-select/creatable";

type Option = {
  label: string;
  value: string | number;
};

type DropdownType = "default" | "async" | "creatable" | "async-creatable";

export interface DropdownProps {
  label?: string;
  error?: string;
  options?: Option[]; // optional now (async won't need it)
  value?: Option | Option[] | null;
  onChange?: (value: any) => void;
  isMulti?: boolean;
  type?: DropdownType;
  loadOptions?: (inputValue: string) => Promise<Option[]>;
  formatOptionLabel?: (option: Option) => React.ReactNode;
  onCreateOption?: (inputValue: string) => void;
  placeholder?: string;
}

export function Dropdown({
  label,
  error,
  options = [],
  value,
  onChange,
  isMulti = false,
  type = "default",
  loadOptions,
  formatOptionLabel,
  onCreateOption,
  placeholder,
}: DropdownProps) {
  const styles: StylesConfig<Option, boolean> = {
    control: (base, state) => ({
      ...base,
      backgroundColor: "var(--background)",
      borderColor: error
        ? "var(--destructive)"
        : state.isFocused
          ? "var(--foreground)"
          : "var(--border)",
      borderWidth: "0.5px",
      borderRadius: "var(--radius-md)",
      boxShadow: state.isFocused
        ? "0 0 0 3px color-mix(in oklch, var(--foreground) 8%, transparent)"
        : "none",
      minHeight: "36px",
      transition: "border-color 120ms ease, box-shadow 120ms ease",
      "&:hover": {
        borderColor: state.isFocused ? "var(--foreground)" : "var(--ring)",
      },
    }),

    placeholder: (base) => ({
      ...base,
      color: "var(--muted-foreground)",
      fontSize: "0.9375rem",
      fontFamily: "var(--font-sans)",
    }),

    input: (base) => ({
      ...base,
      color: "var(--foreground)",
      fontFamily: "var(--font-sans)",
      fontSize: "0.9375rem",
      margin: "0",
      padding: "0",
    }),

    singleValue: (base) => ({
      ...base,
      color: "var(--foreground)",
      fontFamily: "var(--font-sans)",
      fontSize: "0.9375rem",
    }),

    valueContainer: (base) => ({
      ...base,
      padding: "0 10px",
      gap: "4px",
      flexWrap: "nowrap",
      overflow: "hidden",
    }),

    indicatorSeparator: () => ({
      display: "none",
    }),

    dropdownIndicator: (base, state) => ({
      ...base,
      color: "var(--muted-foreground)",
      padding: "0 10px",
      transition: "transform 150ms ease",
      transform: state.selectProps.menuIsOpen ? "rotate(180deg)" : "none",
      "&:hover": {
        color: "var(--foreground)",
      },
    }),

    clearIndicator: (base) => ({
      ...base,
      color: "var(--muted-foreground)",
      padding: "0 4px",
      "&:hover": {
        color: "var(--foreground)",
      },
    }),

    menu: (base) => ({
      ...base,
      backgroundColor: "var(--popover)",
      border: "0.5px solid var(--border)",
      borderRadius: "var(--radius-lg)",
      boxShadow:
        "0 4px 6px -1px oklch(0 0 0 / 0.07), 0 2px 4px -1px oklch(0 0 0 / 0.05)",
      overflow: "hidden",
      zIndex: 50,
      marginTop: "4px",
    }),

    menuList: (base) => ({
      ...base,
      padding: "4px",
    }),

    option: (base, state) => ({
      ...base,
      backgroundColor: state.isSelected
        ? "var(--primary)"
        : state.isFocused
          ? "var(--secondary)"
          : "transparent",
      color: state.isSelected
        ? "var(--primary-foreground)"
        : "var(--popover-foreground)",
      fontFamily: "var(--font-sans)",
      fontSize: "0.9375rem",
      borderRadius: "var(--radius-sm)",
      padding: "7px 10px",
      cursor: "pointer",
      transition: "background-color 80ms ease",
      "&:active": {
        backgroundColor: "var(--secondary)",
      },
    }),

    multiValue: (base) => ({
      ...base,
      backgroundColor: "var(--secondary)",
      border: "0.5px solid var(--border)",
      borderRadius: "var(--radius-sm)",
      margin: "2px",
    }),

    multiValueLabel: (base) => ({
      ...base,
      color: "var(--foreground)",
      fontFamily: "var(--font-sans)",
      fontSize: "0.8125rem",
      padding: "1px 4px",
    }),

    multiValueRemove: (base) => ({
      ...base,
      color: "var(--muted-foreground)",
      borderRadius: "0 var(--radius-sm) var(--radius-sm) 0",
      paddingLeft: "3px",
      paddingRight: "5px",
      "&:hover": {
        backgroundColor: "var(--surface-raised)",
        color: "var(--foreground)",
      },
    }),

    noOptionsMessage: (base) => ({
      ...base,
      color: "var(--muted-foreground)",
      fontFamily: "var(--font-sans)",
      fontSize: "0.875rem",
      padding: "8px 10px",
    }),
  };

  const commonProps = {
    value,
    onChange,
    isMulti,
    styles,
    placeholder,
    unstyled: false,
    isClearable: true,
  };

  const renderSelect = () => {
    switch (type) {
      case "async":
        if (!loadOptions) {
          throw new Error("loadOptions is required for async select");
        }
        return (
          <AsyncSelect
            {...commonProps}
            loadOptions={loadOptions}
            defaultOptions
            placeholder={placeholder}
            isClearable
          />
        );

      case "creatable":
        return (
          <CreatableSelect
            {...commonProps}
            options={options}
            onCreateOption={onCreateOption}
          />
        );

      case "async-creatable":
        if (!loadOptions) {
          throw new Error("loadOptions is required for async-creatable select");
        }
        return (
          <AsyncCreatableSelect
            {...commonProps}
            loadOptions={loadOptions}
            defaultOptions
          />
        );

      default:
        return <Select {...commonProps} options={options} />;
    }
  };

  return (
    <div className="w-full space-y-1.5">
      {label && (
        <label className="text-foreground text-sm font-medium">{label}</label>
      )}

      {renderSelect()}

      {error && <p className="text-destructive text-sm">{error}</p>}
    </div>
  );
}
