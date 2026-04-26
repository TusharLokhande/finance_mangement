CREATE OR REPLACE FUNCTION get_expense_stats_v2(
    p_start_date TIMESTAMPTZ DEFAULT NULL,
    p_end_date   TIMESTAMPTZ DEFAULT NULL,
    p_category   NUMERIC     DEFAULT NULL
)
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
        COALESCE(p_start_date, '1970-01-01'::timestamptz) AS start_date,
        COALESCE(p_end_date, now()) AS end_date
),
final_bounds AS (
    SELECT
        start_date,
        end_date,
        (start_date - (end_date - start_date)) AS prev_start_date,
        start_date AS prev_end_date,
        (end_date - interval '7 days') AS start_of_week
    FROM bounds
),
filtered_expenses AS (
    SELECT e.*
    FROM "Expenses" e
    WHERE e."IsActive" = true
      AND (p_category IS NULL OR e."Category" = p_category)
),
category_totals AS (
    SELECT 
        e."Category",
        SUM(e."Amount") AS total
    FROM filtered_expenses e, final_bounds b
    WHERE e."Date" >= b.start_date 
      AND e."Date" < b.end_date
    GROUP BY e."Category"
    ORDER BY total DESC
    LIMIT 1
)
SELECT
    COALESCE(SUM(e."Amount") FILTER (
        WHERE e."Date" >= b.start_date 
          AND e."Date" < b.end_date
    ), 0),

    COALESCE(SUM(e."Amount") FILTER (
        WHERE e."Date" >= b.prev_start_date 
          AND e."Date" < b.prev_end_date
    ), 0),

    COALESCE(SUM(e."Amount") FILTER (
        WHERE e."Date" >= b.start_of_week
          AND e."Date" < b.end_date
    ), 0),

    COALESCE(AVG(e."Amount") FILTER (
        WHERE e."Date" >= b.start_date 
          AND e."Date" < b.end_date
    ), 0),

    COALESCE(
        (SELECT "Category" FROM category_totals),
        0
    )

FROM filtered_expenses e, final_bounds b;
$$;