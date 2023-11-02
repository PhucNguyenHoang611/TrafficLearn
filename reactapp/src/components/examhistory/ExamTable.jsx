import * as React from "react";
import Paper from "@mui/material/Paper";
import Table from "@mui/material/Table";
import TableBody from "@mui/material/TableBody";
import TableCell from "@mui/material/TableCell";
import TableContainer from "@mui/material/TableContainer";
import TableHead from "@mui/material/TableHead";
import TablePagination from "@mui/material/TablePagination";
import TableRow from "@mui/material/TableRow";
import { Button } from "@mui/material";
import { theme } from "../../themes/CustomeTheme";

const columns = [
  { id: "license", label: "Loại\u00a0bằng", minWidth: 40 },
  {
    id: "date",
    label: "Ngày\u00a0thi",
    minWidth: 120,
    align: "right",
    format: (value) => value.toLocaleString("vn-VN"),
  },
  {
    id: "score",
    label: "Điểm\u00a0số",
    minWidth: 100,
    align: "right",
    format: (value) => value.toFixed(0),
  },
  //   {
  //     id: "detail",
  //     label: "Chi\u00a0tiết",
  //     minWidth: 100,
  //     align: "right",
  //     format: (value) => value.toFixed(2),
  //   },
];

function createData(license, date, score) {
  return { license, date, score };
}

const rows = [
  createData("B2", "22-11-2022", 45),
  createData("A2", "31-10-2002", 56),
  createData("A2", "01-01-2000", 67),
];

export default function StickyHeadTable() {
  const [page, setPage] = React.useState(0);
  const [rowsPerPage, setRowsPerPage] = React.useState(10);

  const handleChangePage = (event, newPage) => {
    setPage(newPage);
  };

  const handleChangeRowsPerPage = (event) => {
    setRowsPerPage(+event.target.value);
    setPage(0);
  };

  return (
    <Paper sx={{ width: "100%", overflow: "hidden" }} theme={theme}>
      <TableContainer sx={{ maxHeight: 440 }}>
        <Table stickyHeader aria-label="sticky table">
          <TableHead>
            <TableRow>
              {columns.map((column) => (
                <TableCell
                  key={column.id}
                  align={column.align}
                  style={{ minWidth: column.minWidth }}
                >
                  {column.label}
                </TableCell>
              ))}
            </TableRow>
          </TableHead>
          <TableBody>
            {rows
              .slice(page * rowsPerPage, page * rowsPerPage + rowsPerPage)
              .map((row) => {
                return (
                  <TableRow hover role="checkbox" tabIndex={-1} key={row.code}>
                    {columns.map((column) => {
                      const value = row[column.id];
                      return (
                        <TableCell key={column.id} align={column.align}>
                          {column.format && typeof value === "number"
                            ? column.format(value)
                            : value}
                        </TableCell>
                      );
                    })}
                    <TableCell align="right">
                      <Button variant="contained" color="secondary">
                        Chi tiết
                      </Button>
                    </TableCell>
                  </TableRow>
                );
              })}
          </TableBody>
        </Table>
      </TableContainer>
      <TablePagination
        rowsPerPageOptions={[10, 25, 100]}
        component="div"
        count={rows.length}
        rowsPerPage={rowsPerPage}
        page={page}
        onPageChange={handleChangePage}
        onRowsPerPageChange={handleChangeRowsPerPage}
      />
    </Paper>
  );
}
