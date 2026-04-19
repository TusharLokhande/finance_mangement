
CREATE OR REPLACE FUNCTION get_expense_stats()
RETURNS TABLE (
    "TotalThisMonth" NUMERIC,
    "PrevMonthTotal" NUMERIC,
    "Weekly" NUMERIC,
    "Avg" NUMERIC,
    "TopCategory" NUMERIC
)
LANGUAGE sql
AS $$
WITH bounds AS (
    SELECT 
        date_trunc('month', now() AT TIME ZONE 'utc') AS start_of_month,
        date_trunc('month', now() AT TIME ZONE 'utc') + interval '1 month' AS start_of_next_month,
        date_trunc('month', now() AT TIME ZONE 'utc') - interval '1 month' AS start_of_prev_month,
        (now() AT TIME ZONE 'utc') - interval '7 days' AS start_of_week
),
category_totals AS (
    SELECT 
        e."Category",
        SUM(e."Amount") AS total
    FROM "Expenses" e, bounds b
    WHERE e."IsActive" = true
      AND e."Date" >= b.start_of_month 
      AND e."Date" < b.start_of_next_month
    GROUP BY e."Category"
    ORDER BY total DESC
    LIMIT 1
)
SELECT
    COALESCE(SUM(e."Amount") FILTER (
        WHERE e."Date" >= b.start_of_month 
          AND e."Date" < b.start_of_next_month
    ), 0),

    COALESCE(SUM(e."Amount") FILTER (
        WHERE e."Date" >= b.start_of_prev_month 
          AND e."Date" < b.start_of_month
    ), 0),

    COALESCE(SUM(e."Amount") FILTER (
        WHERE e."Date" >= b.start_of_week
    ), 0),

    COALESCE(AVG(e."Amount") FILTER (
        WHERE e."Date" >= b.start_of_month 
          AND e."Date" < b.start_of_next_month
    ), 0),

    COALESCE(
        (SELECT "Category" FROM category_totals),
        0
    )

FROM "Expenses" e, bounds b
WHERE e."IsActive" = true;
$$;