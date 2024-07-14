import { Container } from "@mui/material";
import React, { useContext, useState, useEffect } from "react";
import { TrainingClassContext } from "../../context/TrainingClassContext";
import TableCell, { tableCellClasses } from "@mui/material/TableCell";
import Table from "@mui/material/Table";
import TableContainer from "@mui/material/TableContainer";
import { styled } from "@mui/material/styles";
import TableRow from "@mui/material/TableRow";
import { useTheme } from "@mui/material/styles";
import Box from "@mui/material/Box";
import IconButton from "@mui/material/IconButton";
import LastPageIcon from "@mui/icons-material/LastPage";
import FirstPageIcon from "@mui/icons-material/FirstPage";
import KeyboardArrowLeft from "@mui/icons-material/KeyboardArrowLeft";
import KeyboardArrowRight from "@mui/icons-material/KeyboardArrowRight";
import PropTypes from "prop-types";
import { Pagination } from "@mui/material";
import { Skeleton, Spin } from "antd";
import Paper from "@mui/material/Paper";
import TableHead from "@mui/material/TableHead";
import TableBody from "@mui/material/TableBody";
import ActionBtn from "../../components/trainingClass/ActionBtn";
import { Tag } from "antd";

const StyledTableCell = styled(TableCell)(({ theme }) => ({
  [`&.${tableCellClasses.head}`]: {
    backgroundColor: theme.palette.common.black,
    color: theme.palette.common.white,
  },
  [`&.${tableCellClasses.body}`]: {
    fontSize: 14,
  },
}));

const StyledTableRow = styled(TableRow)(({ theme }) => ({
  "&:nth-of-type(odd)": {
    backgroundColor: theme.palette.action.hover,
  },
  // hide last border
  "&:last-child td, &:last-child th": {
    border: 0,
  },
}));

function TablePaginationActions(props) {
  const theme = useTheme();
  const { count, page, rowsPerPage, onPageChange } = props;

  const handleFirstPageButtonClick = (event) => {
    onPageChange(event, 0);
  };

  const handleBackButtonClick = (event) => {
    onPageChange(event, page - 1);
  };

  const handleNextButtonClick = (event) => {
    onPageChange(event, page + 1);
  };

  const handleLastPageButtonClick = (event) => {
    onPageChange(event, Math.max(0, Math.ceil(count / rowsPerPage) - 1));
  };

  return (
    <Box sx={{ flexShrink: 0, ml: 2.5 }}>
      <IconButton
        onClick={handleFirstPageButtonClick}
        disabled={page === 0}
        aria-label="first page"
      >
        {theme.direction === "rtl" ? <LastPageIcon /> : <FirstPageIcon />}
      </IconButton>
      <IconButton
        onClick={handleBackButtonClick}
        disabled={page === 0}
        aria-label="previous page"
      >
        {theme.direction === "rtl" ? (
          <KeyboardArrowRight />
        ) : (
          <KeyboardArrowLeft />
        )}
      </IconButton>
      <IconButton
        onClick={handleNextButtonClick}
        disabled={page >= Math.ceil(count / rowsPerPage) - 1}
        aria-label="next page"
      >
        {theme.direction === "rtl" ? (
          <KeyboardArrowLeft />
        ) : (
          <KeyboardArrowRight />
        )}
      </IconButton>
      <IconButton
        onClick={handleLastPageButtonClick}
        disabled={page >= Math.ceil(count / rowsPerPage) - 1}
        aria-label="last page"
      >
        {theme.direction === "rtl" ? <FirstPageIcon /> : <LastPageIcon />}
      </IconButton>
    </Box>
  );
}
TablePaginationActions.propTypes = {
  count: PropTypes.number.isRequired,
  onPageChange: PropTypes.func.isRequired,
  page: PropTypes.number.isRequired,
  rowsPerPage: PropTypes.number.isRequired,
};

function TrainingClassView() {
  const {
    TrainingClassState: {
      trainingClass,
      trainingClassList,
      trainingClassLoading,
      pageIndex,
      pageSize,
      totalPagesCount
    },
    setStateTrainingClass,
    getTrainingClassList,
  } = useContext(TrainingClassContext);

  const [pageState, setPageState] = useState({
    page: 0,
    pageSize: 10,
  });
  console.log(pageIndex);

  const handleOnChangePage = (event, pageIndex) => {
    setStateTrainingClass();
    setPageState((pre) => {
      console.log(pageIndex);
      return {
        ...pre,
        page: pageIndex - 1,
      };
    });
  };
  useEffect(
    () => getTrainingClassList(pageState.page, pageState.pageSize),
    [pageState]
  );
  console.log("TotalPagesCount: ", totalPagesCount);
  return (
    <>
      <Container sx={{ marginTop: "30px" }} fixed>
        <Spin spinning={trainingClassLoading} delay={0}>
          <TableContainer component={Paper} sx={{ borderRadius: "20px" }}>
            <Table sx={{ minWidth: 700 }} aria-label="customized table">
              <TableHead>
                <TableRow>
                  <StyledTableCell align="right">Id</StyledTableCell>
                  <StyledTableCell align="right">Class Name</StyledTableCell>
                  <StyledTableCell align="right">Class Code</StyledTableCell>
                  <StyledTableCell align="right">
                    Class Time Start
                  </StyledTableCell>
                  <StyledTableCell align="right">
                    Class Time End
                  </StyledTableCell>
                  <StyledTableCell align="right">Attendee Type</StyledTableCell>
                  <StyledTableCell align="right">Location</StyledTableCell>
                  <StyledTableCell align="right">Action</StyledTableCell>
                </TableRow>
              </TableHead>
              <TableBody>
                {trainingClassList.map((trainingClass) => (
                  <StyledTableRow key={trainingClass?.id}>
                    <StyledTableCell component="th" scope="row">
                      {trainingClass?.id}
                    </StyledTableCell>
                    <StyledTableCell align="right">
                      {trainingClass?.name}
                    </StyledTableCell>
                    <StyledTableCell align="right">
                      {trainingClass?.code}
                    </StyledTableCell>
                    <StyledTableCell align="right">
                      {trainingClass?.timeStart.split("T")[0]}
                    </StyledTableCell>
                    <StyledTableCell align="right">
                      {trainingClass?.timeEnd.split("T")[0]}
                    </StyledTableCell>
                    <StyledTableCell align="right">
                      {
                        <Tag
                          color={
                            trainingClass?.attendeeType == "Intern"
                              ? "grey"
                              : trainingClass?.attendeeType == "Intern"
                              ? "blue"
                              : trainingClass?.attendeeType == "OnlineFeeFresher"
                              ? "green"
                              : "orange"
                          }
                          key={trainingClass?.attendeeType}
                        >
                          {trainingClass?.attendeeType == "Intern"
                            ? "Intern"
                            : trainingClass?.attendeeType == "Intern"
                            ? "Fresher"
                            : trainingClass?.attendeeType == "OnlineFeeFresher"
                            ? "Online Fee-Fresher"
                            : "Offline Fee-Fresher"}
                        </Tag>
                      }
                    </StyledTableCell>
                    <StyledTableCell align="right">
                      {trainingClass?.classLocation == "HCM"
                        ? "Ho Chi Minh"
                        : trainingClass?.classLocation == "DN"
                        ? "Da Nang"
                        : "Ha Noi"}
                    </StyledTableCell>
                    <StyledTableCell align="right">
                      <ActionBtn id={trainingClass?.id} />
                    </StyledTableCell>
                  </StyledTableRow>
                ))}
              </TableBody>
            </Table>
          </TableContainer>
        </Spin>
        <Box justifyContent={"center"} alignItems={"center"} display={"flex"} sx={{
                marginTop: "20px",
                marginBottom: "100px"
            }}>
                <Pagination
                    count={totalPagesCount + 1}
                    onChange={handleOnChangePage}
                />
            </Box>
      </Container>
    </>
  );
}

export default TrainingClassView;
