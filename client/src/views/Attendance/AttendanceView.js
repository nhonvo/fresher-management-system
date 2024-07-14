import { Container } from '@mui/material'
import React, { useContext, useEffect, useState } from 'react'
import { UserContext } from '../../context/UserContext';
import { Space, Table, Tag, Spin, Button } from 'antd';
import Box from '@mui/material/Box';
import { Pagination } from '@mui/material';
import { Modal } from 'antd';



function AttendanceView() {
    const {
        userState: { users, usersLoading, pageIndex, pageSize, totalPagesCount, },
        setUserState,
        getAttendances,
        approveAttendance
    } = useContext(UserContext)
    const [pageState, setPageState] = useState({
        page: 0,
        pageSize: 10,
    });
    const [editingRow, setEditingRow] = useState(null);
    const [modalVisible, setModalVisible] = useState(false);
    const [modalData, setModalData] = useState({});

    const columns = [
        {
            title: 'Admin',
            dataIndex: 'adminName',
            key: 'adminName',
            render: (text) => <a>{text}</a>,
        },
        {
            title: 'Approve Status',
            dataIndex: 'approveStatus',
            key: 'approveStatus',
            render: (approveStatus) => {
                if (approveStatus?.toUpperCase() === "APPROVED") {
                    return (
                        <Tag color="blue" key={approveStatus}>
                            {approveStatus}
                        </Tag>
                    );
                } else if (approveStatus?.toUpperCase() === 'DECLINED') {
                    return (
                        <Tag color="red" key={approveStatus}>
                            {approveStatus}
                        </Tag>
                    );
                } else if (approveStatus?.toUpperCase() === 'PENDING') {
                    return (
                        <Tag color="green" key={approveStatus}>
                            {approveStatus}
                        </Tag>
                    );
                }
            }
        },
        {
            title: 'Attendee',
            dataIndex: 'classStudent',
            key: 'classStudent',
            render: (text) => <a>{text?.student?.name}</a>,
        },
        {
            title: 'Reason',
            dataIndex: 'reason',
            key: 'reason',
            render: (text) => <a>{text}</a>,
        },
        {
            title: 'Attendance Status',
            key: 'attendanceStatus',
            dataIndex: 'attendanceStatus',
            render: (attendanceStatus) => {
                if (attendanceStatus?.toUpperCase() === "PRESENT") {
                    return (
                        <Tag color="green" key={attendanceStatus}>
                            {attendanceStatus}
                        </Tag>
                    );
                } else if (attendanceStatus?.toUpperCase() === 'ABSENT') {
                    return (
                        <Tag color="blue" key={attendanceStatus}>
                            {attendanceStatus}
                        </Tag>
                    );
                } else if (attendanceStatus?.toUpperCase() === 'NOEXCUSE') {
                    return (
                        <Tag color="orange" key={attendanceStatus}>
                            {attendanceStatus}
                        </Tag>
                    );
                } else if (attendanceStatus?.toUpperCase() === 'OUTEARLY') {
                    return (
                        <Tag color="purple" key={attendanceStatus}>
                            {attendanceStatus}
                        </Tag>
                    );
                } else if (attendanceStatus?.toUpperCase() === 'INLATE') {
                    return (
                        <Tag color="purple" key={attendanceStatus}>
                            {attendanceStatus}
                        </Tag>
                    );
                }
            }

            // <>
            // {if(role.toUpperCase() === SUPERADMIN)
            //   (
            //     <Tag color={"green"} key={code}>
            //     {code.toUpperCase()}
            //   </Tag>
            //   )
            // }
            //   <Tag color={"green"} key={code}>
            //     {code.toUpperCase()}
            //   </Tag>
            // </>
            ,
        },
        {
            title: 'Day',
            dataIndex: 'day',
            key: 'day',
            render: () => (
                new Date().toLocaleDateString()
            )
        },
        {
            title: 'Actions',
            key: 'actions',
            render: (text, record) => (
                <>
                    {record?.approveStatus == "Pending" ?
                        <Space>
                            <Button type="primary" onClick={() => handleApprove(record?.id, "Approved")}>
                                Approve
                            </Button>
                            <Button type="primary" danger onClick={() => handleApprove(record?.id, "Declined")}>
                                Decline
                            </Button>
                        </Space>
                        : <>
                            <Button type="primary" style={{ backgroundColor: 'green', borderColor: 'green' }} onClick={() => handleApprove(record?.id, "Pending")}>
                                Undo
                            </Button>
                        </>
                    }

                </>
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
    const handleApprove = async (id, status) => {
        const response = await approveAttendance(id, status);
        const fakeEvent = {
            target: {
                value: "your desired value",
            },
        };
        handleOnChangePage(fakeEvent, 1);
    }
    const handleEdit = (record) => {
        console.log(record);
        setEditingRow(record);
        setModalData(record);
        setModalVisible(true);
    };

    const handleSaveModal = (data) => {
        // Xử lý logic để lưu các thay đổi dữ liệu từ modal
        console.log('Save:', data);
        setModalVisible(false); // Ẩn modal sau khi lưu
    };
    useEffect(() =>
        getAttendances(pageState.page, pageState.pageSize),

        [pageState]);
    console.log(users);
    return (
        <Container sx={{ marginTop: "30px" }} fixed>
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
                title="Edit User"
                visible={modalVisible}
                onOk={() => handleSaveModal(modalData)}
                onCancel={() => setModalVisible(false)}
            >
                {/* Hiển thị các trường chỉnh sửa, ví dụ: */}
                <div className="form-group">
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
                </div>
                <div className="form-group">
                    <label>Role:</label>
                    <select
                        className="form-control text-dark"
                        value={modalData.role}
                        onChange={(e) =>
                            setModalData({ ...modalData, role: e.target.value })
                        }
                    >
                        <option value="Male">Male</option>
                        <option value="Female">Female</option>
                        <option value="Other">Other</option>
                    </select>
                </div>
                <div className="form-group">
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
                </div>

                {/* Các trường chỉnh sửa khác */}
            </Modal>
        </Container>
    )
}

export default AttendanceView