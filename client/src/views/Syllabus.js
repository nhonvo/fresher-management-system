// import React from 'react'
// import Box from '@mui/material/Box';

// import { DataGridPro, GridRow, GridColumnHeaders } from '@mui/x-data-grid-pro';
// import { useDemoData } from '@mui/x-data-grid-generator';
// import { DataGrid } from "@mui/x-data-grid";

// const MemoizedRow = React.memo(GridRow);

// const MemoizedColumnHeaders = React.memo(GridColumnHeaders);

// const Syllabus = () => {

//     const { data } = useDemoData({
//         dataSet: 'Commodity',
//         rowLength: 100000,
//         editable: true,
//       });
//   return (
//     <>
//      <Container sx={{ marginTop: "30px" }} fixed>
//         <Box sx={{ height: 520, width: '100%' }}>
//         <DataGridPro
//             {...data}
//             loading={data.rows.length === 0}
//             rowHeight={38}
//             checkboxSelection
//             disableRowSelectionOnClick
//             components={{
//             Row: MemoizedRow,
//             ColumnHeaders: MemoizedColumnHeaders,
//             }}
//         />
//         </Box>
//       </Container>
//     </>

//   )
// }

// export default Syllabus

import * as React from 'react';
import Box from '@mui/material/Box';
import { DataGridPro, GridRow, GridColumnHeaders } from '@mui/x-data-grid-pro';
import { useDemoData } from '@mui/x-data-grid-generator';
import { SyllabusContext } from '../context/SyllabusContext';
import { useContext, useEffect } from 'react';
import Container from '@mui/material/Container';
import { LicenseInfo } from '@mui/x-data-grid-pro';
import { DataGrid } from '@mui/x-data-grid';
LicenseInfo.setLicenseKey(
  'x0jTPl0USVkVZV0SsMjM1kDNyADM5cjM2ETPZJVSQhVRsIDN0YTM6IVREJ1T0b9586ef25c9853decfa7709eee27a1e',
);
const columns = [
  { field: 'id', headerName: 'ID', width: 90 },
  {
    field: 'firstName',
    headerName: 'First name',
    width: 150,
    editable: true,
  },
  {
    field: 'lastName',
    headerName: 'Last name',
    width: 150,
    editable: true,
  },
  {
    field: 'age',
    headerName: 'Age',
    type: 'money',
    width: 110,
    editable: true,
  },
  {
    field: 'fullName',
    headerName: 'Full name',
    description: 'This column has a value getter and is not sortable.',
    sortable: false,
    width: 160,
    valueGetter: (params) =>
      `${params.row.firstName || ''} ${params.row.lastName || ''}`,
  },
];
const rows = [
  { id: 1, lastName: 'Snow', firstName: 'Jon', age: 35 },
  { id: 2, lastName: 'Lannister', firstName: 'Cersei', age: 42 },
  { id: 3, lastName: 'Lannister', firstName: 'Jaime', age: 45 },
  { id: 4, lastName: 'Stark', firstName: 'Arya', age: 16 },
  { id: 5, lastName: 'Targaryen', firstName: 'Daenerys', age: null },
  { id: 6, lastName: 'Melisandre', firstName: null, age: 150 },
  { id: 7, lastName: 'Clifford', firstName: 'Ferrara', age: 44 },
  { id: 8, lastName: 'Frances', firstName: 'Rossini', age: 36 },
  { id: 9, lastName: 'Roxie', firstName: 'Harvey', age: 65 },
];

const MemoizedRow = React.memo(GridRow);

const MemoizedColumnHeaders = React.memo(GridColumnHeaders);

const Syllabus = () =>{
 // context
  const {
    syllabusState: { syllabus, syllabuses, syllabusLoading },
    getSyllabuses
  } = useContext(SyllabusContext);
  
  const [pagination, setPagination] = React.useState({pageIndex: 1 ,pageSize: 10 });
  useEffect(() => getSyllabuses(pagination.pageIndex, pagination.pageSize), []);


  const { data } = useDemoData({
    dataSet: 'Employee',
    rowLength: 100000,
    editable: true,
  });


  console.log(syllabuses);
  return (
    <Container sx={{ marginTop: "30px" }} fixed>
    <Box sx={{ height: 400, width: '100%' }}>
      <DataGrid
        rows={rows}
        columns={columns}
        initialState={{
          pagination: {
            paginationModel: {
              pageSize: 5,
            },
          },
        }}
        pageSizeOptions={[5]}
        checkboxSelection
        disableRowSelectionOnClick
      />
    </Box>
    </Container>
  );
}

export default Syllabus;





