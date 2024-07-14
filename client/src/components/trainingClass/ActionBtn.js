import { Delete, Edit, Preview, SendOutlined } from '@mui/icons-material';
import { Box, IconButton, Tooltip } from '@mui/material';
import { Link } from 'react-router-dom';
import React, { useContext, useState, useEffect } from "react";
import { TrainingClassContext } from "../../context/TrainingClassContext";

const ActionBtn = ({ id }) => {
  const {
    TrainingClassState: {
      trainingClassLoading,
      trainingClassStudentGPA,
      totalPagesCount
    },
    postSendRequest,
    getTrainingClassStudentGPA,
  } = useContext(TrainingClassContext);

  const handleSendRequest = (id) => {
    console.log("send request", id);
    postSendRequest(id)
  }
  return (
    <>
      <Box>
        <Tooltip title="View Detail">
          <IconButton to={`/TrainingClass/${id}`} as={Link}>
            <Preview />
          </IconButton>
        </Tooltip>
        <Tooltip title="Edit Detail">
          <IconButton>
            <Edit />
          </IconButton>
        </Tooltip>
        <Tooltip title="Delete Detail">
          <IconButton>
            <Delete />
          </IconButton>
        </Tooltip>
        <Tooltip title="Send Request">
          <IconButton onClick={() => handleSendRequest(id)}>
            <SendOutlined />
          </IconButton>
        </Tooltip>
      </Box>
    </>
  )
}

export default ActionBtn