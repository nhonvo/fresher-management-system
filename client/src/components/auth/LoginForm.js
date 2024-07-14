import React from "react";
import Button from "react-bootstrap/Button";
import Form from "react-bootstrap/Form";
import { Link } from "react-router-dom";
import { useState, useContext } from "react";
import { AuthContext } from "../../context/AuthContext";
import AlertMessage from "../layout/AlertMessage";
import { message, notification } from 'antd';
const LoginForm = () => {
  //context
  const {

    LoginUser,
    setStateRegister } = useContext(AuthContext);


  //local State
  const [loginForm, setLoginForm] = useState({
    email: "",
    password: "",
  });
  const [alert, setAlert] = useState(null);
  const { email, password } = loginForm;
  const onChangeLoginForm = (event) =>
    setLoginForm({
      ...loginForm,
      [event.target.name]: event.target.value,
    });


  const login = async (event) => {
    event.preventDefault();
    setStateRegister()
    try {
      const loginData = await LoginUser(loginForm);
      console.log(loginData);
      if (loginData.token) {
        notification.success({
          message: 'Đăng nhập thành công',
          duration: 2, // Thời gian hiển thị (tính bằng giây)
          // onClose: () => setShowNotification(false), // Thiết lập biến showNotification thành false khi thông báo đóng
          style: {
            marginTop: '100px'
          },
        });
      }
      else if (loginData.status == 400) {

        setAlert({ type: "danger", message: loginData.errors.Email ? loginData.errors.Email[0]  : loginData.errors.Password[0] });
        setTimeout(() => setAlert(null), 2000);
      }
      else if (loginData.status == 404) {
        setAlert({ type: "danger", message: loginData.detail });
        setTimeout(() => setAlert(null), 2000);
      }
    } catch (error) {
      console.log(error);
    }
  };

  return (
    <>
      <Form onSubmit={login}>
        <AlertMessage info={alert} />
        <Form.Group className="mb-3">
          <Form.Control
            type="text"
            placeholder="Email"
            name="email"
            required
            value={email}
            onChange={onChangeLoginForm}
          />
        </Form.Group>

        <Form.Group className="mb-3">
          <Form.Control
            type="password"
            placeholder="Password"
            name="password"
            required
            value={password}
            onChange={onChangeLoginForm}
          />
        </Form.Group>

        <Button variant="success" type="submit">
          Login
        </Button>
      </Form>
      <p>
        Dont have an account ?
        <Link to="/Register">
          <Button variant="info" size="sm" className="ml-2">
            Register
          </Button>
        </Link>
      </p>
    </>
  );
};

export default LoginForm;
