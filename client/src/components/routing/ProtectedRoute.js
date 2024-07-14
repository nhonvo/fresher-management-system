import React from "react";
import { Navigate, Outlet } from "react-router-dom";
import { useContext,  } from "react";
import { AuthContext } from "../../context/AuthContext";
import NavbarMenu from "../layout/NavbarMenu";
import SideBarMenu from "../layout/SideBarMenu";
import { Layout } from 'antd';
import { Block } from "@mui/icons-material";
import { useLocation,useNavigate  } from 'react-router-dom';
const { Header, Content, Footer, Sider } = Layout;
const ProtectedRoute = () => {
  const {
    authState: { isAuthenticated },
  } = useContext(AuthContext);
  const location = useLocation();
  const currentPath = location.pathname;
  console.log(currentPath);
  const navigate = useNavigate();
  return isAuthenticated ? (
    <>
      <SideBarMenu></SideBarMenu>
      <Outlet />
      <Footer style={{
        textAlign: 'center', position: "fixed",
        display: "block",
        bottom: "0px",
        width: "100%",
        height: "60px",
      }}>Ant Design ©2023 Created by Ant UED</Footer>
    </>
  ) : (
     <Navigate to="/Login" state={{ currentPath }}/>
  );
};

export default ProtectedRoute;
