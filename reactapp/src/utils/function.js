//function to check if the text is a number
export function matchIsNumeric(text) {
  const isNumber = typeof text === "number";
  const isString = matchIsString(text);
  return (isNumber || (isString && text !== "")) && !isNaN(Number(text));
}

//function to check if the text is a string
export function matchIsString(text) {
  return typeof text === "string" || text instanceof String;
}

//random number
export function getRandomNumber(min, max) {
  return Math.floor(Math.random() * (max - min + 1)) + min;
}
