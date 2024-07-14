import { Container } from '@mui/material'
import React, { useContext, useEffect, useState } from 'react'
import { UserContext } from '../../context/UserContext';
import { TrainingClassContext } from '../../context/TrainingClassContext';
import { Space, Table, Tag, Spin, Button } from 'antd';
import Box from '@mui/material/Box';
import { Pagination } from '@mui/material';
import { Modal } from 'antd';
import axios from 'axios';



function UserView() {
  const {
    userState: { users, usersLoading, totalPagesCount, },
    setUserState,
    getUsers,
    setRole
  } = useContext(UserContext)
  const {
    getExcelFile } = useContext(TrainingClassContext);

  const [pageState, setPageState] = useState({
    page: 0,
    pageSize: 10,
  });
  const [editingRow, setEditingRow] = useState(null);
  const [modalVisible, setModalVisible] = useState(false);
  const [modalData, setModalData] = useState({});

  const columns = [
    {
      title: 'Email',
      dataIndex: 'email',
      key: 'email',
      render: (text) => <a>{text}</a>,
    },

    {
      title: 'Gender',
      dataIndex: 'gender',
      key: 'gender',
      render: (gender) => (
        <>
          {gender === "Female" ? (
            <Tag color={"#1890ff"} key={gender}>
              {gender}
            </Tag>
          ) : (
            <Tag color={"#ff4d4f"} key={gender}>
              {gender}
            </Tag>
          )}
        </>
      )
    },
    {
      title: 'Full Name',
      dataIndex: 'name',
      key: 'name',
      render: (text) => <a>{text}</a>,
    },
    {
      title: 'Phone Number',
      dataIndex: 'phone',
      key: 'phone',
    },
    {
      title: 'Rode',
      key: 'role',
      dataIndex: 'role',
      render: (role) => {
        if (role?.toUpperCase() === 'SUPERADMIN') {
          return (
            <Tag color="green" key={role}>
              {role}
            </Tag>
          );
        } else if (role?.toUpperCase() === 'CLASSADMIN') {
          return (
            <Tag color="blue" key={role}>
              {role}
            </Tag>
          );
        } else if (role?.toUpperCase() === 'TRAINER') {
          return (
            <Tag color="orange" key={role}>
              {role}
            </Tag>
          );
        } else if (role?.toUpperCase() === 'TRAINEE') {
          return (
            <Tag color="purple" key={role}>
              {role}
            </Tag>
          );
        }
      }

    },
    // {
    //   title: 'Status',
    //   dataIndex: 'status',
    //   key: 'status',
    //   render: (status) => (
    //     <>
    //       {status == 0 ? (<Tag color={"green"} key={status}>
    //         Active
    //       </Tag>) : (<Tag color={"red"} key={status}>
    //         Inactive
    //       </Tag>)}
    //     </>
    //   ),
    // },
    {
      title: 'Date Of Birth',
      dataIndex: 'dateOfBirth',
      key: 'dateOfBirth',
      render: (dob) => (
        dob?.split('T')[0]
      )


    },
    {
      title: 'Actions',
      key: 'actions',
      render: (text, record) => (
        <Button type="primary" onClick={() => handleEdit(record)}>
          Edit Role
        </Button>
      ),
    },
  ];

  const handleOnChangePage = (event, pageIndex) => {
    setUserState()
    setPageState(pre => {
      return {
        ...pre, page: pageIndex - 1
      }
    })

  }
  const handleEdit = (record) => {
    setEditingRow(record);
    setModalData(record);
    setModalVisible(true);
  };

  const handleSaveModal = (data) => {
    // Xử lý logic để lưu các thay đổi dữ liệu từ modal
    console.log('Save:', data);
    const value = {
      userId: data.id,
      role: data.role
    }
    setUserState()
    setRole(value)
    getUsers(pageState.page, pageState.pageSize)
    setModalVisible(false); // Ẩn modal sau khi lưu
  };
  useEffect(() =>
    getUsers(pageState.page, pageState.pageSize),

    [pageState, usersLoading, setModalVisible]);
  console.log(users);
  const handleClickOpenDownload = () => {
    axios({
      method: 'GET',
      url: 'https://trainingmanagementsystem.azurewebsites.net/api/User/export-users-csv',
      responseType: 'blob', // Kiểu dữ liệu phản hồi là file blob
    })
      .then(response => {
        // Xử lý phản hồi từ API
        const url = window.URL.createObjectURL(new Blob([response.data]));
        const link = document.createElement('a');
        link.href = url;
        link.setAttribute('download', 'User.csv'); // Thiết lập tên file tải xuống
        document.body.appendChild(link);
        link.click();
      })
      .catch(error => {
        console.log('Lỗi khi tải xuống file:', error);
      });
  };
  return (
    <Container sx={{ marginTop: "30px" }} fixed>
      <Button type="primary" onClick={handleClickOpenDownload}>
        Export
      </Button>
      <Spin spinning={usersLoading} delay={0}>
        <Table
          pagination={false}
          columns={columns}
          dataSource={users ?? []}
        // onRow={(record) => ({
        //     onClick: () => {
        //         setSyllabusState();
        //         navigate(`/ViewSyllabus/${record.id}`)
        //     }
        // })}

        />
        <Box justifyContent={"center"} alignItems={"center"} display={"flex"} sx={{
          marginTop: "20px",
          marginBottom: "100px"
        }}>
          <Pagination
            count={totalPagesCount + 1}
            onChange={handleOnChangePage}
          />
        </Box>

      </Spin>
      <Modal
        title="Edit User Role"
        visible={modalVisible}
        onOk={() => handleSaveModal(modalData)}
        onCancel={() => setModalVisible(false)}
      >
        {/* Hiển thị các trường chỉnh sửa, ví dụ: */}
        {/* <div className="form-group">
          <label>Name:</label>
          <input
            type="text"
            className="form-control text-dark"
            value={modalData.name}
            onChange={(e) =>
              setModalData({ ...modalData, name: e.target.value })
            }
          />
        </div>
        <div className="form-group">
          <label>Email:</label>
          <input
            type="email"
            className="form-control text-dark"
            value={modalData.email}
            onChange={(e) =>
              setModalData({ ...modalData, email: e.target.value })
            }
          />
        </div>
        <div className="form-group">
          <label>Gender:</label>
          <select
            className="form-control text-dark"
            value={modalData.gender}
            onChange={(e) =>
              setModalData({ ...modalData, gender: e.target.value })
            }
          >
            <option value="SuperAdmin">SuperAdmin</option>
            <option value="Trainer">Trainer</option>
            <option value="ClassAdmin">ClassAdmin</option>
            <option value="Trainee">Trainee</option>

          </select>
        </div>
        <div className="form-group">
          <label>Phone:</label>
          <input
            type="email"
            className="form-control text-dark"
            value={modalData.phone}
            onChange={(e) =>
              setModalData({ ...modalData, phone: e.target.value })
            }
          />
        </div> */}
        <div className="form-group">
          <label>Role:</label>
          <select
            className="form-control text-dark"
            value={modalData.role}
            onChange={(e) =>
              setModalData({ ...modalData, role: e.target.value })
            }
          >
            <option value="SuperAdmin">SuperAdmin</option>
            <option value="Trainer">Trainer</option>
            <option value="ClassAdmin">ClassAdmin</option>
            <option value="Trainee">Trainee</option>
          </select>
        </div>
        {/* <div className="form-group">
          <label>Status:</label>
          <select
            className="form-control text-dark"
            value={modalData.status}
            onChange={(e) =>
              setModalData({ ...modalData, status: e.target.value })
            }
          >
            <option value="Active">Active</option>
            <option value="InActive">InActive</option>


          </select>
        </div> */}

        {/* Các trường chỉnh sửa khác */}
      </Modal>
    </Container>
  )
}

export default UserView