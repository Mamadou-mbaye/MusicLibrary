export const formatDate = (date: string): string => {
  const parsedDate = new Date(date);
  const pad = (value: number) => String(value).padStart(2, '0');

  return `${parsedDate.getFullYear()}-${pad(parsedDate.getMonth() + 1)}-${pad(parsedDate.getDate())} ` +
    `${pad(parsedDate.getHours())}:${pad(parsedDate.getMinutes())}`;
};