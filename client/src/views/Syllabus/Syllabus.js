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
import { SyllabusContext } from '../../context/SyllabusContext';
import { useContext, useEffect, useState } from 'react';
import Container from '@mui/material/Container';
import { LicenseInfo } from '@mui/x-data-grid-pro';
import { DataGrid } from '@mui/x-data-grid';
import TablePagination from '@mui/material/TablePagination';
import CreateSyllabus from '../../components/syllabus/CreateSyllabus';
LicenseInfo.setLicenseKey(
  'x0jTPl0USVkVZV0SsMjM1kDNyADM5cjM2ETPZJVSQhVRsIDN0YTM6IVREJ1T0b9586ef25c9853decfa7709eee27a1e',
);
const columns = [
  { field: 'code', headerName: 'code', width: 90 },
  {
    field: 'version',
    headerName: 'version',
    width: 150,
  },
  {
    field: 'name',
    headerName: 'name',
    width: 150,
  },
  {
    field: 'lastModifiedOn',
    headerName: 'lastModifiedOn',
    width: 110,
  },
  {
    field: 'lastModifiedBy',
    headerName: 'lastModifiedBy',
    width: 110,

  },
  {
    field: 'level',
    headerName: 'level',
    width: 110,

  },
  {
    field: 'attendeeNumber',
    headerName: 'attendeeNumber',
    width: 110,

  },
  {
    field: 'courseObjectives',
    headerName: 'courseObjectives',
    width: 110,

  },
  {
    field: 'trainingDeliveryPrinciple',
    headerName: 'trainingDeliveryPrinciple',
    width: 110,

  },
  {
    field: 'quizCriteria',
    headerName: 'quizCriteria',
    width: 110,

  },
  {
    field: 'assignmentCriteria',
    headerName: 'assignmentCriteria',
    width: 110,

  },
  {
    field: 'finalCriteria',
    headerName: 'finalCriteria',
    width: 110,

  },
  {
    field: 'finalTheoryCriteria',
    headerName: 'finalTheoryCriteria',
    width: 110,

  },
  {
    field: 'finalPracticalCriteria',
    headerName: 'finalPracticalCriteria',
    width: 110,

  },
  {
    field: 'passingGPA',
    headerName: 'passingGPA',
    width: 110,

  },
  {
    field: 'isActive',
    headerName: 'isActive',
    width: 110,

  },
  {
    field: 'duration',
    headerName: 'duration',
    width: 110,
  }
];
// const rows = [
//   { id: 1, lastName: 'Snow', firstName: 'Jon', age: 35 },
//   { id: 2, lastName: 'Lannister', firstName: 'Cersei', age: 42 },
//   { id: 3, lastName: 'Lannister', firstName: 'Jaime', age: 45 },
//   { id: 4, lastName: 'Stark', firstName: 'Arya', age: 16 },
//   { id: 5, lastName: 'Targaryen', firstName: 'Daenerys', age: null },
//   { id: 6, lastName: 'Melisandre', firstName: null, age: 150 },
//   { id: 7, lastName: 'Clifford', firstName: 'Ferrara', age: 44 },
//   { id: 8, lastName: 'Frances', firstName: 'Rossini', age: 36 },
//   { id: 9, lastName: 'Roxie', firstName: 'Harvey', age: 65 },
// ];


const Syllabus = () => {
  // context
  const {
    syllabusState: { syllabus, syllabuses, syllabusLoading, pageIndex, pageSize, totalPagesCount, totalItemsCount },
    getSyllabuses
  } = useContext(SyllabusContext);
  console.log(syllabuses);
  const [pageState, setPageState] = useState({
    page: 1,
    pageSize: 10,
  });

  const [paginationModel, setPaginationModel] = React.useState({
    page: 0,
    pageSize: 10,
  });

  useEffect(() => getSyllabuses(pageState.page, pageState.pageSize), [pageState.page]);

  const handlePageChange = ()=>{
    setPaginationModel(previousState => {
      return { ...previousState, page: previousState.page + 1}
    });
  }
  // const { data } = useDemoData({
  //   dataSet: 'Employee',
  //   rowLength: 100000,
  //   editable: true,
  // });
  if (syllabuses === undefined) {
    return (
      <>
        <div>hello</div>
      </>
    )
  }
  else {
    return (
      <Container sx={{ marginTop: "30px" }} fixed>
          <CreateSyllabus/>
      </Container>
    );
  }

}

export default Syllabus;





