import { createTheme, ThemeProvider, styled } from "@mui/material/styles";

export const theme = createTheme({
  palette: {
    primary: {
      main: "#ff9c72",
    },
    secondary: {
      main: "#1b9a8c",
    },
    error: {
      main: "#f0217d",
    },
    warning: {
      main: "#fffd84",
    },
  },
  typography: {
    fontFamily: ["Roboto", "sans-serif"].join(","),
  },
});
