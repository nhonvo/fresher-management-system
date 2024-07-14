import * as React from 'react';
import Box from '@mui/material/Box';
import { DataGridPro, GridRow, GridColumnHeaders } from '@mui/x-data-grid-pro';
import { useDemoData } from '@mui/x-data-grid-generator';
import CreateSyllabusForm from './CreateSyllabusForm';
import { Container } from '@mui/material'
const MemoizedRow = React.memo(GridRow);

const MemoizedColumnHeaders = React.memo(GridColumnHeaders);

export default function CreateSyllabus() {
  const { data } = useDemoData({
    dataSet: 'Commodity',
    rowLength: 100000,
    editable: true,
  });

  return (
    <Container sx={{ marginTop: "30px", minHeight: 360 }} fixed>
      <CreateSyllabusForm/>
    </Container>
  );
}