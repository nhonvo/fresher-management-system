import React, { useContext, useState, useEffect } from "react";
import { Container } from "@mui/material";
import { Skeleton, Spin } from "antd";
import Table from "@mui/material/Table";
import TableContainer from "@mui/material/TableContainer";
import Paper from "@mui/material/Paper";
import TableHead from "@mui/material/TableHead";
import TableBody from "@mui/material/TableBody";
import TableCell, { tableCellClasses } from "@mui/material/TableCell";
import { styled } from "@mui/material/styles";
import TableRow from "@mui/material/TableRow";
import Box from "@mui/material/Box";
import { Pagination } from "@mui/material";
import { UserContext } from "../../context/UserContext";
import { Button, Upload } from "antd";
import ScoreActionBtn from "../../components/trainingClass/ScoreActionBtn";
import { Tag, notification } from "antd";
import ChartPie from "../trainingClass/ChartPie";
import ChartColumn from "../trainingClass/ChartColumn";


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

const Score = ({ id }) => {
  const [expandedRow, setExpandedRow] = useState(null);
  const [pageState, setPageState] = useState({
    page: 0,
    pageSize: 10,
  });
  const {
    userState: {
      studentClassGPA,
      totalPagesCount,
      usersLoading
    },
    getStudentClassGPA,
    setUserState
  } = useContext(UserContext);

  const handleOnChangePage = (event, pageIndex) => {
    setUserState()
    setPageState((pre) => {
      console.log(pageIndex);
      return {
        ...pre,
        page: pageIndex - 1,
      };
    });
  };

  const handleTestDelete = () => {
    const fakeEvent = {
      target: {
        value: "your desired value",
      },
    };
    handleOnChangePage(fakeEvent, 1);
  };

  useEffect(
    () => getStudentClassGPA(id, pageState.page, pageState.pageSize),
    [pageState]
  );
  const handleRowClick = (rowId) => {
    if (expandedRow === rowId) {
      setExpandedRow(null);
      console.log("Closed", rowId);
    } else {
      setExpandedRow(rowId);
      console.log("Active", rowId);
    }
  };
  const props = {
    action: "https://www.mocky.io/v2/5cc8019d300000980a055e76",
    // onChange: handleChange,
    multiple: true,
  };
  return (
    <>
      <Container sx={{ marginTop: "30px" }} fixed>
        <Spin spinning={usersLoading} delay={0}>
          <TableContainer component={Paper} sx={{ borderRadius: "20px" }}>
            <Table
              sx={{
                minWidth: 700,
                background: "#79E0EE"
              }}
              aria-label="customized table"
            // onRow={(record) => ({
            //   onClick: () => handleRowClick(record.id),
            // })}
            >
              <TableHead>
                <TableRow>
                  <StyledTableCell align="left">Class Id</StyledTableCell>
                  <StyledTableCell align="left">Class Name</StyledTableCell>
                  <StyledTableCell align="left">GPA</StyledTableCell>
                  <StyledTableCell align="center">
                    Different from student average <br />
                    (Student Average : {studentClassGPA[0]?.studentAverage})
                  </StyledTableCell>
                </TableRow>
              </TableHead>
              <TableBody>
                {studentClassGPA.map((trainingClass) => (
                  <React.Fragment key={trainingClass?.classId}>
                    <StyledTableRow
                      key={trainingClass?.classId}
                      onClick={() => handleRowClick(trainingClass?.classId)}
                    >
                      <StyledTableCell component="th" scope="row" align="left">
                        {trainingClass?.classId}
                      </StyledTableCell>
                      <StyledTableCell align="left">
                        {trainingClass?.className}
                      </StyledTableCell>
                      <StyledTableCell align="left">
                        {trainingClass?.gpa}
                      </StyledTableCell>
                      <StyledTableCell align="center">
                        {trainingClass?.diffFromStudentAverage > 0 ? (
                          <Tag color="green">
                            +{trainingClass?.diffFromStudentAverage}
                          </Tag>
                        ) : trainingClass?.diffFromStudentAverage == 0 ? (
                          <Tag color="yellow">
                            {trainingClass?.diffFromStudentAverage}
                          </Tag>
                        ) : (
                          <Tag color="red">
                            {trainingClass?.diffFromStudentAverage}
                          </Tag>
                        )}
                      </StyledTableCell>
                    </StyledTableRow>
                    {expandedRow === trainingClass?.classId && (
                      <TableRow key={trainingClass?.classId}>
                        <TableCell colSpan={4}>
                          {/* <Collapse
                              in={expandedRow === trainingClass?.attendeeId}
                              timeout="auto"
                              unmountOnExit
                            > */}
                          <Table sx={{
                            background: "#98EECC"
                          }}>
                            {/* <TableHead>
                                  <TableRow
                                    onClick={() =>
                                      handleRowClick(trainingClass?.id)
                                    }
                                  >
                                    <StyledTableCell align="right">
                                      Syllabus Id
                                    </StyledTableCell>
                                    <StyledTableCell align="right">
                                      Final Syllabus Score
                                    </StyledTableCell>
                                  </TableRow>
                                </TableHead> */}
                            <TableBody>
                              {trainingClass?.listSyllabus?.map((syllabus) => (
                                <React.Fragment>
                                  <StyledTableRow
                                    key={trainingClass?.classId}
                                  >
                                    <StyledTableCell
                                      component="th"
                                      scope="row"
                                      align="right"
                                    >
                                      Syllabus Id: {syllabus?.syllabusId}
                                    </StyledTableCell>
                                    <StyledTableCell
                                      component="th"
                                      scope="row"
                                      align="right"
                                    >
                                      Syllabus Name: {syllabus?.syllabusName}
                                      <span>
                                        {" "}
                                        (Pass Score: {syllabus?.finalScheme})
                                      </span>
                                    </StyledTableCell>
                                    <StyledTableCell
                                      component="th"
                                      scope="row"
                                      align="right"
                                    >
                                      Final Syllabus Score:{" "}
                                      {syllabus?.finalSyllabusScore}
                                    </StyledTableCell>
                                    <StyledTableCell
                                      component="th"
                                      scope="row"
                                      align="right"
                                    >
                                      {
                                        <Tag
                                          color={
                                            syllabus?.finalSyllabusScore >=
                                              syllabus?.finalScheme
                                              ? "green"
                                              : "red"
                                          }
                                          key={trainingClass?.attendeeType}
                                        >
                                          {syllabus?.finalSyllabusScore >=
                                            syllabus?.finalScheme
                                            ? "Pass"
                                            : "Not Pass"}
                                        </Tag>
                                      }
                                    </StyledTableCell>
                                  </StyledTableRow>
                                  {expandedRow === trainingClass?.classId && (
                                    <TableRow>
                                      <TableCell colSpan={4}>
                                        {/* <Collapse
                                                in={
                                                  expandedRow ===
                                                  trainingClass?.classId
                                                }
                                                timeout="auto"
                                                unmountOnExit
                                              > */}
                                        <Table
                                          sx={{
                                            background: "White"
                                          }}
                                          key={trainingClass?.classId}>
                                          {/* <TableHead>
                                            <TableRow
                                              key={trainingClass?.classId}
                                              onClick={() =>
                                                handleRowClick(trainingClass?.id)
                                              }
                                            >
                                              <StyledTableCell align="right">
                                                Assessment Type
                                              </StyledTableCell>
                                              <StyledTableCell align="right">
                                                Assessment Scheme
                                              </StyledTableCell>
                                              <StyledTableCell align="right">
                                                Average Score
                                              </StyledTableCell>
                                            </TableRow>
                                          </TableHead> */}
                                          <TableBody>
                                            {syllabus?.listAssessment.map(
                                              (assessment) => (
                                                <>
                                                  <StyledTableRow
                                                    key={
                                                      assessment.testAssessmentType
                                                    }
                                                  >
                                                    <StyledTableCell
                                                      component="th"
                                                      scope="row"
                                                      align="right"
                                                      sx={{
                                                        background: "#F5F0BB"
                                                      }}
                                                    >
                                                      Assessment Type:{" "}
                                                      {assessment.testAssessmentType
                                                        .replace(
                                                          /[A-Z]/g,
                                                          " $&"
                                                        )
                                                        .trim()}
                                                    </StyledTableCell>
                                                    <StyledTableCell
                                                      component="th"
                                                      scope="row"
                                                      align="right"
                                                      sx={{
                                                        background: "#F5F0BB"
                                                      }}
                                                    >
                                                      Assessment Scheme:{" "}
                                                      {`${Math.round(
                                                        assessment.syllabusScheme *
                                                        10000
                                                      ) / 100
                                                        } %`}
                                                    </StyledTableCell>
                                                    <StyledTableCell
                                                      component="th"
                                                      scope="row"
                                                      align="right"
                                                      sx={{
                                                        background: "#F5F0BB"
                                                      }}
                                                    >
                                                      Average Score:{" "}
                                                      {assessment.averageScore}
                                                    </StyledTableCell>
                                                  </StyledTableRow>
                                                  <StyledTableRow>
                                                    <StyledTableCell
                                                      colSpan={3}
                                                    >
                                                      <Table
                                                        key={
                                                          trainingClass?.attendeeId
                                                        }
                                                      >
                                                        <TableBody>
                                                          {assessment.assessmentList.map(
                                                            (test) => (
                                                              // {
                                                              //   console.log(test);
                                                              //   return(
                                                              //     <div>
                                                              //       hello
                                                              //     </div>
                                                              //   )
                                                              // }
                                                              <TableRow
                                                                key={
                                                                  trainingClass?.attendeeId
                                                                }
                                                              >
                                                                <StyledTableCell align="right">
                                                                  <p>
                                                                    Test Number:{" "}
                                                                    {test.id}
                                                                  </p>
                                                                </StyledTableCell>
                                                                <StyledTableCell align="right">
                                                                  Score:{" "}
                                                                  {test.score}
                                                                </StyledTableCell>
                                                                <StyledTableCell align="right">
                                                                  <Upload
                                                                    {...props}
                                                                    // onClick={({ uid }) => HandleOnClickFile(uid)}
                                                                    fileList={test.trainingMaterials.map(
                                                                      (
                                                                        fileTest
                                                                      ) => {
                                                                        return {
                                                                          uid: fileTest.id,
                                                                          name: fileTest.fileName,
                                                                          status:
                                                                            "done",
                                                                          url:
                                                                            "/TrainingMaterial/" +
                                                                            fileTest.id,
                                                                        };
                                                                      }
                                                                    )}
                                                                  ></Upload>
                                                                </StyledTableCell>
                                                                <StyledTableCell align="right">
                                                                  <ScoreActionBtn
                                                                    onDelete={
                                                                      handleTestDelete
                                                                    }
                                                                    id={test.id}
                                                                  />
                                                                </StyledTableCell>
                                                              </TableRow>
                                                            )
                                                          )}
                                                        </TableBody>
                                                      </Table>
                                                    </StyledTableCell>
                                                  </StyledTableRow>
                                                </>
                                              )
                                            )}
                                          </TableBody>
                                        </Table>
                                        {/* </Collapse> */}
                                      </TableCell>
                                    </TableRow>
                                  )}
                                </React.Fragment>
                              ))}
                            </TableBody>
                          </Table>
                          {/* </Collapse> */}
                        </TableCell>
                      </TableRow>
                    )}
                  </React.Fragment>
                ))}
              </TableBody>
            </Table>
          </TableContainer>
          <Box
            justifyContent={"center"}
            alignItems={"center"}
            display={"flex"}
            sx={{
              marginTop: "20px",
              marginBottom: "100px",
            }}
          >
            <Pagination
              count={totalPagesCount + 1}
              onChange={handleOnChangePage}
            />
          </Box>

          <ChartColumn data={studentClassGPA.map((attendee) => ({
            year: attendee.className,
            value: attendee.gpa,
            type: "Name"
          }))}></ChartColumn>
          <ChartColumn data={studentClassGPA.map((attendee) => ({
            year: attendee.className,
            value: attendee.diffFromStudentAverage,
            type: attendee.diffFromStudentAverage < 0 ? "under" : "above"
          }))}></ChartColumn>
          <ChartPie data={studentClassGPA.map((attendee) => ({
            type: attendee.className,
            value: attendee.gpa
          }))}></ChartPie>
        </Spin>

      </Container>
    </>
  );
};

export default Score;
