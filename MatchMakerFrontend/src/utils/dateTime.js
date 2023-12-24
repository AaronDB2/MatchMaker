// Gets current date
export function getDate() {
  const today = new Date();
  return `${today.getDate()}-${
    today.getMonth() + 1
  }-${today.getFullYear()} ${today.getHours()}:${today.getMinutes()}:${today.getSeconds()}`;
}

// Converts dataTime to date notation
export function dateTimeToDate(dateTime) {
  return dateTime.split("T")[0];
}
