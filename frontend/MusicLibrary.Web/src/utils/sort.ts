export type SortDirection = 'asc' | 'desc';

export type SortState<T extends object> = {
  field: keyof T | null;
  direction: SortDirection;
};

const createSortState = <T extends object>(initialField?: keyof T) => ({
  field: initialField ?? null,
  direction: 'asc' as SortDirection,
});

const toggleSort = <T extends object>(current: SortState<T>, field: keyof T): SortState<T> => {
  if (current.field === field) {
    return {
      ...current,
      direction: current.direction === 'asc' ? 'desc' : 'asc',
    };
  }

  return {
    field,
    direction: 'asc',
  };
};

const orderByField = <T extends object>(
  records: T[],
  field: keyof T,
  direction: SortDirection = 'asc',
) => {
  return [...records].sort((a, b) => {
    const left = a[field];
    const right = b[field];

    const areNumbers = typeof left === 'number' && typeof right === 'number';

    const result = areNumbers
      ? Number(left) - Number(right)
      : String(left ?? '').localeCompare(String(right ?? ''));

    return direction === 'asc' ? result : -result;
  });
};

export { createSortState, toggleSort, orderByField };
