import React from "react";
import { Collapse } from "antd";
import TableCell, { tableCellClasses } from "@mui/material/TableCell";
import TableRow from "@mui/material/TableRow";

const { Panel } = Collapse;
const text = `
  A dog is a type of domesticated animal.
  Known for its loyalty and faithfulness,
  it can be found as a welcome guest in many households across the world.
`;
const ClassTrainingProgram = ({ data }) => {
  console.log(data);
  const onChange = (key) => {
  };
  return (
    <>
      <h1>{data.name}</h1>
      <h5></h5>
      <Collapse defaultActiveKey={["1"]} onChange={onChange}>
        {data.programSyllabus.map((syllabusList) => (
          <>
            <Panel
              header={
                <>
                  <h2>Id: {syllabusList?.syllabus?.id} - Syllabus Name: {syllabusList?.syllabus?.name}</h2>
                  <p>on {syllabusList?.syllabus?.creationDate.split("T")[0]}</p>
                </>
              }
              key={syllabusList?.syllabusId}
            >
              {syllabusList?.syllabus?.units?.map((unit) => (
                <>
                  <TableRow>
                    <TableCell>{unit.name}</TableCell>
                  </TableRow>
                </>
              ))}
            </Panel>
          </>
        ))}
      </Collapse>
    </>
  );
};
export default ClassTrainingProgram;
