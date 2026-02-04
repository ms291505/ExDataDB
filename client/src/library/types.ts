export type Exercise = {
  id: number,
  name: string,
  force?: string,
  mechanic?: string,
  primaryMuscle: string,
  secondaryMuscle: string,
  equipment?: string,
  category: string
};

export interface PagedResponse<T> {
  items: T[];
  page: number;
  pageSize: number;
  totalCount: number;
}
export interface CursorResponse<T> {
  items: T[];
  hasMore: boolean;
  pageSize: number;
  totalCount: number;
}
