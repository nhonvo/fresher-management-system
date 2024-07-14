import { Container } from "@mui/material";
import { UserContext } from "../../context/UserContext";
import React, { useContext, useState, useEffect } from "react";
import { EditOutlined, EllipsisOutlined, SettingOutlined, SendOutlined } from '@ant-design/icons';
import { Avatar, Card, Skeleton, Switch, Spin } from 'antd';
import { Modal } from 'antd';


import Score from "../../components/user/Score";

const { Meta } = Card;

function UserProfileView() {

  const {
    userState: { user, usersLoading, },
    setUserState,
    getUserProfile,
    sendAbsenceRequest

  } = useContext(UserContext);
  const [loading, setLoading] = useState(true);
  console.log(user);
  const [editingRow, setEditingRow] = useState(null);
  const [modalVisible, setModalVisible] = useState(false);
  const [modalData, setModalData] = useState({});

  useEffect(() => getUserProfile(), []);


  const onChange = (checked) => {
    setLoading(!checked);
  };

  const handleEdit = () => {
    setModalVisible(true);
  };
  const handleSaveModal = (data) => {
    // Xử lý logic để lưu các thay đổi dữ liệu từ modal
    console.log('Save:', data);
    const value = {
      reason: data.reason,
      expectedDates: new Date()
    }
    setUserState()
    // setRole(value)
    sendAbsenceRequest(value)
    setModalVisible(false); // Ẩn modal sau khi lưu
  };

  return (
    <Container sx={{ marginTop: "30px" }} fixed>
      <Spin spinning={usersLoading} delay={0}>
        <Card
          title="User Information"
          bordered={false}
          style={{
            width: 300,
          }}
          actions={[
            <SettingOutlined key="setting" />,
            <EditOutlined key="edit" />,
            <SendOutlined key="sendAttendance" onClick={() => handleEdit()} />,
          ]}
        >
          <Meta
            avatar={<Avatar src="https://xsgames.co/randomusers/avatar.php?g=pixel&key=2" />}
            title={user?.name}
            description={user?.email}
          />
          <br />
          <br />
          <div>
            <p><b>Id:</b> <span>{user?.id}</span></p>
            <p><b>Gender:</b> <span>{user?.gender}</span></p>
            <p><b>Phone:</b> <span>{user?.phone}</span></p>
            <p><b>Role:</b> <span> {user?.role?.replace(/[A-Z]/g, " $&").trim()}</span></p>
            <p><b>Status:</b> <span> {user?.status}</span></p>
          </div>
        </Card>
        {
          user?.id >= 10 || user?.id == null   ? <></>  : <Score id={user?.id}></Score>
        }
      </Spin>
      <Modal
        title="Send Leave of absence request"
        visible={modalVisible}
        onOk={() => handleSaveModal(modalData)}
        onCancel={() => setModalVisible(false)}
      >
        <div className="form-group">
          <label>Reason:</label>
          <textarea className="form-control text-dark" rows={4} onChange={(e) =>
              setModalData({ ...modalData, reason: e.target.value })
            } />
        </div>
      </Modal>
    </Container>
  );
}
export default UserProfileView;
