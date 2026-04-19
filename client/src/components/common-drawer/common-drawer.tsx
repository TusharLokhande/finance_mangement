import { Button } from "@/components/ui/button";
import {
  Drawer,
  DrawerClose,
  DrawerContent,
  DrawerDescription,
  DrawerFooter,
  DrawerHeader,
  DrawerTitle,
} from "@/components/ui/drawer";
import { X } from "lucide-react";
import type { FC, ReactNode } from "react";

interface CommonDrawerProps {
  open: boolean;
  onOpenChange: (open: boolean) => void;
  title: string;
  description?: string;
  children: ReactNode;
  footer?: ReactNode;
  direction?: "left" | "right" | "top" | "bottom";
}

export const CommonDrawer: FC<CommonDrawerProps> = ({
  open,
  onOpenChange,
  title,
  description,
  children,
  footer,
  direction = "right",
}) => {
  return (
    <Drawer open={open} onOpenChange={onOpenChange} direction={direction}>
      <DrawerContent className="p-4">
        <DrawerHeader>
          <div className="flex justify-between">
            <DrawerTitle className="text-2xl">{title}</DrawerTitle>
            <DrawerClose asChild>
              <Button variant="outline">
                <X />
              </Button>
            </DrawerClose>
          </div>
          {description && <DrawerDescription>{description}</DrawerDescription>}
        </DrawerHeader>

        <div className="no-scrollbar overflow-y-auto px-4">{children}</div>
        <DrawerFooter>
          {footer ? (
            footer
          ) : (
            <DrawerClose asChild>
              <Button variant="outline">Close</Button>
            </DrawerClose>
          )}
        </DrawerFooter>
      </DrawerContent>
    </Drawer>
  );
};
