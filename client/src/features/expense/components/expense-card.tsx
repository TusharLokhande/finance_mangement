import { Card, CardContent, CardHeader, CardTitle } from "@/components/ui/card";
import { BarChart3, Calendar, Tag, Wallet } from "lucide-react";
import * as React from "react";
import { useMemo } from "react";
import { getCategoryName } from "../helper";
import { useGetExpenseStats } from "../hooks/queries/use-expense-hook";

const ExpenseSummaryCards: React.FC = () => {
  const { data: summary, isLoading, isError } = useGetExpenseStats();

  console.log("ExpenseSummaryCards - summary data:", summary);

  const topCategory = useMemo(() => {
    if (!summary) return null;
    return getCategoryName(summary.topCategory);
  }, [summary?.topCategory]);

  if (isLoading) return <div>Loading...</div>;

  if (isError) return <div>Error occurred while fetching expense stats.</div>;

  return (
    <div className="grid gap-4 md:grid-cols-2 lg:grid-cols-4 mb-6">
      <Card>
        <CardHeader className="flex flex-row items-center justify-between pb-2">
          <CardTitle className="text-sm font-medium">Total (Month)</CardTitle>
          <Wallet className="h-4 w-4 text-muted-foreground" />
        </CardHeader>
        <CardContent>
          <div className="text-2xl font-bold">
            ₹{summary?.totalThisMonth || 0}
          </div>
          <p
            className={`text-xs ${
              (summary?.percentChange || 0) >= 0
                ? "text-green-500"
                : "text-red-500"
            }`}
          >
            {(summary?.percentChange || 0) >= 0 ? "+0.0" : "0.0"}
            {summary?.percentChange?.toFixed(1)}% vs last month
          </p>
        </CardContent>
      </Card>

      {/* Weekly */}
      <Card>
        <CardHeader className="flex flex-row items-center justify-between pb-2">
          <CardTitle className="text-sm font-medium">Weekly Spend</CardTitle>
          <Calendar className="h-4 w-4 text-muted-foreground" />
        </CardHeader>
        <CardContent>
          <div className="text-2xl font-bold">₹{summary?.weekly ?? 0}</div>
          <p className="text-xs text-muted-foreground">Last 7 days</p>
        </CardContent>
      </Card>

      {/* Top Category */}
      <Card>
        <CardHeader className="flex flex-row items-center justify-between pb-2">
          <CardTitle className="text-sm font-medium">Top Category</CardTitle>
          <Tag className="h-4 w-4 text-muted-foreground" />
        </CardHeader>
        <CardContent>
          <div className="text-2xl font-bold">{topCategory}</div>
          <p className="text-xs text-muted-foreground">Most spending</p>
        </CardContent>
      </Card>

      {/* Avg per Expense */}
      <Card>
        <CardHeader className="flex flex-row items-center justify-between pb-2">
          <CardTitle className="text-sm font-medium">Avg Expense</CardTitle>
          <BarChart3 className="h-4 w-4 text-muted-foreground" />
        </CardHeader>
        <CardContent>
          <div className="text-2xl font-bold">₹{summary?.avg || 0}</div>
          <p className="text-xs text-muted-foreground">Per entry</p>
        </CardContent>
      </Card>
    </div>
  );
};

export default ExpenseSummaryCards;
