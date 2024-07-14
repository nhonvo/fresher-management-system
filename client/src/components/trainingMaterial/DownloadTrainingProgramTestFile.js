import { TrainingClassContext } from "../../context/TrainingClassContext";
import React, { useContext, useState, useEffect } from "react";
import { useParams } from "react-router-dom";
import { Container } from '@mui/material'


const DownloadTrainingProgramTestFile = () => {
    const {
      getTrainingClassStudentGPA,
      getTrainingProgramTestFile
    } = useContext(TrainingClassContext);
    const { id } = useParams();
    useEffect(
      () => getTrainingProgramTestFile(id)
    );
    return (
      <Container sx={{ marginTop: "30px", minHeight: 360 }} fixed>

        <p>Download Success</p>
      </Container>
    )
};

export default DownloadTrainingProgramTestFile;