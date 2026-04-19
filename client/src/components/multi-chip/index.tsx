"use client";

import { Badge } from "@/components/ui/badge";
import {
  Popover,
  PopoverContent,
  PopoverTrigger,
} from "@/components/ui/popover";
import { useEffect, useRef, useState } from "react";
import { Tooltip, TooltipContent, TooltipTrigger } from "../ui/tooltip";

export function CustomTooltip({
  children,
  content,
  showTooltip = false,
}: {
  children: React.ReactNode;
  content: string;
  showTooltip?: boolean;
}) {
  return showTooltip ? (
    <Tooltip>
      <TooltipTrigger asChild>{children}</TooltipTrigger>
      <TooltipContent side="bottom" sideOffset={2} className="max-w-75">
        <p>{content}</p>
      </TooltipContent>
    </Tooltip>
  ) : (
    <>{children}</>
  );
}

type Props = {
  items: string[];
  maxVisible?: number;
  className?: string;
};

export function MultiChipOverflow({ items, maxVisible = 2, className }: Props) {
  const [open, setOpen] = useState(false);

  const visible = items.slice(0, maxVisible);
  const hidden = items.slice(maxVisible);

  return (
    <div className={`flex flex-wrap gap-2 ${className || ""}`}>
      {visible.map((item, i) => (
        <TruncatedBadge key={i} text={item} />
      ))}

      {hidden.length > 0 && (
        <Popover open={open} onOpenChange={setOpen}>
          <PopoverTrigger asChild>
            <Badge
              variant="outline"
              className="cursor-pointer hover:bg-gray-400"
            >
              +{hidden.length} More
            </Badge>
          </PopoverTrigger>

          <PopoverContent className="w-64 p-2">
            <div className="space-y-1 max-h-60 overflow-y-auto">
              {items.map((item, i) => (
                <div
                  key={i}
                  className="text-sm px-2 py-1 rounded hover:bg-muted"
                >
                  {item}
                </div>
              ))}
            </div>
          </PopoverContent>
        </Popover>
      )}
    </div>
  );
}

export function TruncatedBadge({ text }: { text: string }) {
  const ref = useRef<HTMLSpanElement>(null);
  const [isTruncated, setIsTruncated] = useState(false);

  useEffect(() => {
    const el = ref.current;
    if (el) {
      setIsTruncated(el.scrollWidth > el.clientWidth);
    }
  }, [text]);

  return (
    <CustomTooltip content={text} showTooltip={isTruncated}>
      <Badge variant="secondary" className="max-w-[160px] truncate">
        <span
          ref={ref}
          className="block overflow-hidden text-ellipsis whitespace-nowrap"
        >
          {text}
        </span>
      </Badge>
    </CustomTooltip>
  );
}
