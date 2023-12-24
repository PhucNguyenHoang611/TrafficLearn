// import * as React from "react";
import Button from "@mui/material/Button";
import ButtonGroup from "@mui/material/ButtonGroup";
import { useNavigate } from "react-router-dom";
import { ThemeProvider } from "@mui/material/styles";
import { theme } from "../../themes/CustomeTheme";

export default function BasicButtonGroup() {
  const navigate = useNavigate();
  return (
    <ThemeProvider theme={theme}>
      <ButtonGroup
        variant="contained"
        aria-label="outlined primary button group"
      >
        <Button
          onClick={() => {
            navigate("/fine/motorbike");
          }}
        >
          Xe máy
        </Button>
        <Button
          onClick={() => {
            navigate("/fine/car");
          }}
        >
          Xe ô tô
        </Button>
        <Button
          onClick={() => {
            navigate("/fine/other");
          }}
        >
          Khác
        </Button>
      </ButtonGroup>
    </ThemeProvider>
  );
}
