// hooks/use-confirm.tsx
import { ConfirmDialog } from "@/components/confirm-dialog/confirm-dialog";
import { useState } from "react";

type Variant = "default" | "destructive" | "warning" | "success";

type ConfirmOptions = {
  title?: string;
  description?: string;
  confirmText?: string;
  cancelText?: string;
  variant?: Variant;
};

export function useConfirm() {
  const [open, setOpen] = useState(false);
  const [options, setOptions] = useState<ConfirmOptions>({});
  const [resolvePromise, setResolvePromise] =
    useState<(value: boolean) => void>();

  const confirm = (opts: ConfirmOptions = {}) => {
    setOptions(opts);
    setOpen(true);

    return new Promise<boolean>((resolve) => {
      setResolvePromise(() => resolve);
    });
  };

  const handleConfirm = () => {
    setOpen(false);
    resolvePromise?.(true);
  };

  const handleCancel = () => {
    setOpen(false);
    resolvePromise?.(false);
  };

  const ConfirmComponent = () => (
    <ConfirmDialog
      open={open}
      {...options}
      onConfirm={handleConfirm}
      onCancel={handleCancel}
      variant={options.variant || "default"}
    />
  );

  return { confirm, ConfirmComponent };
}
