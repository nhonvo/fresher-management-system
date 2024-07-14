import React from "react";
import Button from "react-bootstrap/Button";
import Form from "react-bootstrap/Form";
import { Link } from "react-router-dom";
import { useContext, useState } from "react";
import { AuthContext } from "../../context/AuthContext";
import AlertMessage from "../layout/AlertMessage";
import { message, notification, Spin } from 'antd';
import { useNavigate } from 'react-router-dom';
const RegisterFrom = () => {
  //context
  const {
    authState: { registerLoading },
    registerUser } = useContext(AuthContext);
  const navigate = useNavigate();

  //Route

  //local State
  const [registerForm, setRegisterForm] = useState({
    name: "",
    password: "",
    confirmPassword: "",
    gender: "",
    phone: "",
    dateOfBirth: "",
    emailL: ""
  });
  const [alert, setAlert] = useState(null);

  const { username, password, confirmPassword, gender, phone, dateOfBirth } = registerForm;
  const onChangeRegisterForm = (event) =>
    setRegisterForm({
      ...registerForm,
      [event.target.name]: event.target.value,
    });

  const register = async (event) => {

    event.preventDefault();

    if (password !== confirmPassword) {
      setAlert({ type: "danger", message: "Password do not match" });
      setTimeout(() => setAlert(null), 5000);
      return;
    }
    const date = new Date(registerForm.dateOfBirth);
    const isoString = date.toISOString();

    const registerValue = {
      name: registerForm.name,
      email: registerForm.email,
      gender: registerForm.gender,
      phone: registerForm.phone,
      password: registerForm.password,
      dateOfBirth: isoString,

    }

    try {
      const registerData = await registerUser(registerValue);


      console.log(registerData);
      if (registerData.status == 404) {
        setAlert({ type: "danger", message: registerData.detail });
        setTimeout(() => setAlert(null), 5000);
      }
      else if (registerData.status == 200) {
        notification.success({
          message: 'Register successfully',
          duration: 2, // Thời gian hiển thị (tính bằng giây)
          // onClose: () => setShowNotification(false), // Thiết lập biến showNotification thành false khi thông báo đóng
          style: {
            marginTop: '100px'
          },
        });
        navigate('/Login');
      }
    } catch (error) {
      console.log(error);
    }
  };
  return (
    <>
     
        <Form onSubmit={register}>
          <AlertMessage info={alert} />
          <Form.Group className="mb-3">
            <Form.Control
              type="email"
              placeholder="Email"
              name="email"
              required
              value={username}
              onChange={onChangeRegisterForm}
            />
          </Form.Group>
          <Form.Group className="mb-3">
            <Form.Control
              type="text"
              placeholder="Username"
              name="name"
              required
              value={username}
              onChange={onChangeRegisterForm}
            />
          </Form.Group>
          <Form.Group className="mb-3">
            <Form.Select
              name="gender"
              required
              value={gender}
              onChange={onChangeRegisterForm}
            >
              <option value="">Chọn giới tính</option>
              <option value="male">Male</option>
              <option value="female">Female</option>
            </Form.Select>
          </Form.Group>


          <Form.Group className="mb-3">
            <Form.Control
              type="password"
              placeholder="Password"
              name="password"
              required
              value={password}
              onChange={onChangeRegisterForm}
            />
          </Form.Group>

          <Form.Group className="mb-3">
            <Form.Control
              type="password"
              placeholder="Confirm Password"
              name="confirmPassword"
              required
              value={confirmPassword}
              onChange={onChangeRegisterForm}
            />
          </Form.Group>
          <Form.Group className="mb-3">
            <Form.Control
              type="tel"
              placeholder="Phone"
              name="phone"
              required
              value={phone}
              onChange={onChangeRegisterForm}
            />
          </Form.Group>


          <Form.Group className="mb-3">
            <Form.Control
              type="date"
              placeholder="Date of Birth"
              name="dateOfBirth"
              required
              value={dateOfBirth}
              onChange={onChangeRegisterForm}
            />
          </Form.Group>

          <Button variant="success" type="submit">
            Submit
          </Button>
        </Form>
        <p>
          Already have an account ?
          <Link to="/Login">
            <Button variant="info" size="sm" className="ml-2">
              Login
            </Button>
          </Link>
        </p>
    </>
  );
};

export default RegisterFrom;
