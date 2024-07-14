import { Delete, Edit, Preview } from '@mui/icons-material';
import { Box, IconButton, Tooltip } from '@mui/material';
import React from 'react'
import { Link } from 'react-router-dom';

const ActionBtn = ({id})=> {

    const handClickViewDetail =(event)=>{
            console.log(id);
    }
  return (
    <>
    <Box>
      <Tooltip title="View Detail">
            <IconButton  to={`/TrainingProgram/${id}`} as={Link}>
                <Preview/>
            </IconButton>
      </Tooltip>
      <Tooltip title="Edit Detail">
            <IconButton>
                <Edit/>
            </IconButton>
      </Tooltip>
      <Tooltip title="Delete Detail">
            <IconButton>
                <Delete/>
            </IconButton>
      </Tooltip>
    </Box>
    </>
  )
}

export default ActionBtn