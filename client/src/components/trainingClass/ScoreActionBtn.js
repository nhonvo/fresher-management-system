import { Delete, Edit, Preview } from "@mui/icons-material";
import { Box, IconButton, Tooltip } from "@mui/material";
import { Link } from "react-router-dom";
import { TrainingClassContext } from "../../context/TrainingClassContext";
import React, { useContext, useState, useEffect } from "react";

const ScoreActionBtn = ({ id, onDelete }) => {
  const { deleteTestAssessment } = useContext(TrainingClassContext);
  const [rerenderKey, setRerenderKey] = useState(0);
  const HandleDelete = async (id) => {
    try {
      // Call the deleteTestAssessment function with the file ID
      const response = await deleteTestAssessment(id);
      // Handle the response or perform other actions if needed
      onDelete();
      console.log(response);
      setRerenderKey((prevKey) => prevKey + 1);
    } catch (error) {
      console.log(error);
    }
  };
  const handClickViewDetail = (event) => {
    console.log(id);
  };
  return (
    <>
      <Box>
        <Tooltip title="View Detail">
          {/* <IconButton to={`/TrainingClass/${id}`} as={Link}> */}
          <IconButton>
            <Preview />
          </IconButton>
        </Tooltip>
        <Tooltip title="Edit Detail">
          <IconButton>
            <Edit />
          </IconButton>
        </Tooltip>
        <Tooltip title="Delete Detail">
          <IconButton onClick={() => HandleDelete(id)}>
            <Delete />
          </IconButton>
        </Tooltip>
      </Box>
    </>
  );
};

export default ScoreActionBtn;
