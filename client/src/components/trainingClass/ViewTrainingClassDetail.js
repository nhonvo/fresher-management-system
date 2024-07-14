import { useParams } from "react-router-dom";
import { TrainingClassContext } from "../../context/TrainingClassContext";
import { Card } from "antd";
import { Descriptions } from "antd";
import { useNavigate } from "react-router-dom";
import { Container } from "@mui/material";
import React, { useContext, useState, useEffect } from "react";
import TableCell, { tableCellClasses } from "@mui/material/TableCell";
import { styled } from "@mui/material/styles";
import TableRow from "@mui/material/TableRow";
import { Collapse } from "antd";
import { Tag } from "antd";
import { Col, Row, Spin } from "antd";
import Score from "./Score";
import ChartGPA from "./ChartPie";
import ClassTrainingProgram from "./ClassTrainingProgram";
const { Panel } = Collapse;
const text = `
  A dog is a type of domesticated animal.
  Known for its loyalty and faithfulness,
  it can be found as a welcome guest in many households across the world.
`;

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

const tabListNoTitle = [
  {
    key: "TrainingProgram",
    tab: "Training Program",
  },
  {
    key: "Score",
    tab: "Score",
  },
  {
    key: "project",
    tab: "project",
  },
];

function ViewTrainingClassDetail() {
  const [activeTabKey2, setActiveTabKey2] = useState("app");
  const onTab2Change = (key) => {
    setActiveTabKey2(key);
  };
  const navigate = useNavigate();

  const { id } = useParams();
  const {
    TrainingClassState: {
      trainingClass,
      trainingClassLoading,
      trainingClassStudentGPA,
      pageIndex,
      pageSize,
      totalPagesCount,
    },
    getTrainingClass,
    getTrainingClassStudentGPA,
  } = useContext(TrainingClassContext);

  const contentListNoTitle = {
    TrainingProgram: (
      <ClassTrainingProgram data={trainingClass?.trainingProgram} />
    ),
    Score: <Score id={id} pageIndex={pageIndex} pageSize={pageSize}></Score>
  };
  useEffect(() => getTrainingClass(id), []);
  const onChange = (key) => {
    console.log(key);
  };
  console.log("TotalPagesCount: ", totalPagesCount);
  console.log(trainingClassStudentGPA);
  return (
    <>
      <Container sx={{ marginTop: "30px", minHeight: 360 }} fixed>
        {/* <div style={{ padding: 24, minHeight: 360, background: "red" }}>content</div>
                <div>{trainingClass?.id}</div>
                <div>{trainingClass?.status}</div>
                <div>{trainingClass?.creationDate}</div>
                <div>{trainingClass?.createdBy}</div> */}
        <Spin spinning={trainingClassLoading} delay={0}>

          <Row>
            <Col className="gutter-row">
              <Descriptions title="Class">
                <Descriptions.Item span={3}>
                  <h1>
                    {trainingClass?.name}
                    {"  "}
                    {
                      <Tag
                        sx={{ marginLeft: "30px" }}
                        color={
                          trainingClass?.status == 0
                            ? "yellow"
                            : (trainingClass?.status == 0) == 1
                              ? "green"
                              : "red"
                        }
                        key={trainingClass?.attendeeType}
                      >
                        {trainingClass?.status == 0
                          ? "Planning"
                          : (trainingClass?.status == 0) == 1
                            ? "Opening"
                            : "Closed"}
                      </Tag>
                    }
                  </h1>
                </Descriptions.Item>
                <Descriptions.Item label={<h3>Code</h3>}>
                  <h2>{trainingClass?.code}</h2>
                </Descriptions.Item>
              </Descriptions>
            </Col>
          </Row>
          <Row>
            <Col className="gutter-row">
              <Collapse defaultActiveKey={["1"]} onChange={onChange}>
                <Panel header="General" key={"1"}>
                  <Descriptions bordered>
                    <Descriptions.Item span={1} label={<b>Class time</b>}>
                      Cloud Database
                    </Descriptions.Item>
                    <Descriptions.Item span={1} label={<b>Location</b>}>
                      {trainingClass?.location}
                    </Descriptions.Item>
                    <Descriptions.Item span={1} label={<b>Trainer</b>}>
                      {trainingClass?.classTrainers?.map((classTrainer) => {
                        return <p>{classTrainer?.trainer?.name}</p>;
                      })}
                    </Descriptions.Item>
                    <Descriptions.Item span={1} label={<b>Admin</b>}>
                      {trainingClass?.classAdmins?.map((classAdmin) => {
                        return <p>{classAdmin?.admin?.name}</p>;
                      })}
                    </Descriptions.Item>
                    <Descriptions.Item span={1} label={<b>Created</b>}>
                      {trainingClass?.reviewOn.split("T")[0]}
                      <br />
                      by <a>{trainingClass?.createBy?.name}</a>
                    </Descriptions.Item>
                    <Descriptions.Item span={1} label={<b>Review</b>}>
                      {trainingClass?.reviewOn?.split("T")[0]}
                      <br />
                      by <a>{trainingClass?.reviewBy?.name}</a>
                    </Descriptions.Item>
                    <Descriptions.Item span={1} label={<b>Approve</b>}>
                      {trainingClass?.approveOn?.split("T")[0]}
                      <br />
                      by <a>{trainingClass?.approveBy?.name}</a>
                    </Descriptions.Item>
                  </Descriptions>
                </Panel>
                <Panel header="Attendee" key="2">
                  <Row>
                    <Col span={8}>
                      <Card size="small" title="Planned">
                        <h1>{trainingClass?.numberAttendeePlanned}</h1>
                      </Card>
                    </Col>
                    <Col span={8}>
                      <Card size="small" title="Accepted">
                        <h1>{trainingClass?.numberAttendeeAccepted}</h1>
                      </Card>
                    </Col>
                    <Col span={8}>
                      <Card size="small" title="Actual">
                        <h1>{trainingClass?.numberAttendeeActual}</h1>
                      </Card>
                    </Col>
                  </Row>
                </Panel>
              </Collapse>
            </Col>
          </Row>
          <Row gutter={16}>

            <Col className="gutter-row" span={32}>
              <Card
                style={{
                  width: "100%",
                }}
                tabList={tabListNoTitle}
                activeTabKey={activeTabKey2}
                onTabChange={onTab2Change}
              >
                {contentListNoTitle[activeTabKey2]}
              </Card>
            </Col>
          </Row>

        </Spin>
      </Container>
    </>
  );
}

export default ViewTrainingClassDetail;
