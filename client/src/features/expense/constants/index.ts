const CategoryEnum = {
  HOME_CONTRIBUTION: 1,
  FOOD: 2,
  TRANSPORT: 3,
  SHOPPING: 4,
  BILLS: 5,
  ENTERTAINMENT: 6,
  HEALTHCARE: 7,
  LENT: 8,
  BAD_HABBITS: 9,
  INVESTMENTS: 10,
  LEARNING: 11,
  MISC: 99,
} as const;

export const CATEGORY_OPTIONS = [
  {
    value: CategoryEnum.HOME_CONTRIBUTION,
    label: "Home Contribution",
    className: "bg-blue-100 text-blue-700 border-blue-200",
  },
  {
    value: CategoryEnum.FOOD,
    label: "Food",
    className: "bg-orange-100 text-orange-700 border-orange-200",
  },
  {
    value: CategoryEnum.TRANSPORT,
    label: "Transport",
    className: "bg-indigo-100 text-indigo-700 border-indigo-200",
  },
  {
    value: CategoryEnum.SHOPPING,
    label: "Shopping",
    className: "bg-pink-100 text-pink-700 border-pink-200",
  },
  {
    value: CategoryEnum.BILLS,
    label: "Bills",
    className: "bg-red-100 text-red-700 border-red-200",
  },
  {
    value: CategoryEnum.ENTERTAINMENT,
    label: "Entertainment",
    className: "bg-green-100 text-green-700 border-green-200",
  },
  {
    value: CategoryEnum.HEALTHCARE,
    label: "Healthcare",
    className: "bg-teal-100 text-teal-700 border-teal-200",
  },
  {
    value: CategoryEnum.LENT,
    label: "Lent",
    className: "bg-amber-100 text-amber-700 border-amber-200",
  },
  {
    value: CategoryEnum.BAD_HABBITS,
    label: "Bad Habbits",
    className: "bg-gray-100 text-gray-700 border-gray-200",
  },
  {
    value: CategoryEnum.INVESTMENTS,
    label: "Investments",
    className: "bg-purple-100 text-purple-700 border-purple-200",
  },
  {
    value: CategoryEnum.LEARNING,
    label: "Learning",
    className: "bg-blue-100 text-blue-700 border-blue-200",
  },
  {
    value: CategoryEnum.MISC,
    label: "Misc",
    className: "bg-gray-100 text-gray-700 border-gray-200",
  },
];

export const DATE_FILTER = {
  Today: 1,
  ThisWeek: 2,
  ThisMonth: 3,
  Custom: 4,
  LastMonth: 5,
} as const;

export const DATE_FILTER_OPTIONS = [
  { label: "Today", value: DATE_FILTER.Today },
  { label: "This Week", value: DATE_FILTER.ThisWeek },
  { label: "This Month", value: DATE_FILTER.ThisMonth },
  { label: "Custom", value: DATE_FILTER.Custom },
  { label: "Last Month", value: DATE_FILTER.LastMonth },
];

export const PaymentModeEnum = {
  Paytm: 1,
  Gpay: 2,
  CreditCard: 3,
  Other: 4,
} as const;

export const PAYMENT_MODE_OPTIONS = [
  { label: "Paytm", value: PaymentModeEnum.Paytm },
  { label: "Gpay", value: PaymentModeEnum.Gpay },
  { label: "Credit Card", value: PaymentModeEnum.CreditCard },
  { label: "Other", value: PaymentModeEnum.Other },
];
