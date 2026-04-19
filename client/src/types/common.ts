export interface CommonResponse<T> {
  status: ErrorStatus;
  data: T;
  message?: string;
}

export interface CommonDashboardResponse<T> {
  totalcount: number;
  data: T[];
  pageCount: number;
}

export const ErrorStatus = {
  NoError: -1,
  Duplicate: 1,
  NotFound: 2,
  InvalidCred: 3,
  UnAuthorized: 5,
  InternalServerError: 6,
  ValidationError: 7,
  TokenExpired: 8,
} as const;

export type ErrorStatus = (typeof ErrorStatus)[keyof typeof ErrorStatus];

export type PageSorting = {
  field: string;
  direction: "asc" | "desc";
};

export type PageRequest = {
  page: number;
  pageSize: number;
  sorting?: PageSorting;
};
