import { createContext, useReducer, useEffect } from "react";
import axios from "axios";
import { authReducer } from "../reducer/authReducer";
import { apiUrl, LOCAL_STORAGE_TOKEN_NAME } from "./Constants";
import setAuthToken from "../utils/setAuthToken";

export const AuthContext = createContext();

const AuthContextProvider = ({ children }) => {
  const [authState, dispatch] = useReducer(authReducer, {
    authLoading: true,
    isAuthenticated: false,
    user: null,
    registerLoading: false
  });

  //Authenticate user
  const loadUser = async () => {
    if (localStorage[LOCAL_STORAGE_TOKEN_NAME]) {
      setAuthToken(localStorage[LOCAL_STORAGE_TOKEN_NAME]);
    }
    try {
      const response = await axios.get(`${apiUrl}Account/GetUserByToken`);
      if (response.status == 200) {
        dispatch({
          type: "SET_AUTH",
          payload: { isAuthenticated: true, user: response.data},
        });
      }
    } catch (error) {
      localStorage.removeItem(LOCAL_STORAGE_TOKEN_NAME);
      setAuthToken(null);
      dispatch({
        type: "SET_AUTH",
        payload: { isAuthenticated: false, user: null },
      });
    }
  };

  useEffect(() => loadUser(), []);

  //Login http://localhost:5000/api/auth/login
  const LoginUser = async (userForm) => {
    try {
      const response = await axios.post(`${apiUrl}Account/Login`, userForm);
      console.log("login", response);
      if (response.status == 200) {
        setAuthToken(response.data.token);
        dispatch({
          type: "SET_AUTH",
          payload: response.data,
        });
        localStorage.setItem(
          LOCAL_STORAGE_TOKEN_NAME,
          response.data.token
        );
        // await loadUser();
        return response.data;
      }
    } catch (error) {
      if (error.response.data) return error.response.data;
      else return { success: false, message: error.message };
    }
  };

  //Register http://localhost:5000/api/auth/register
  const registerUser = async (registerForm) => {
    try {
      const response = await axios.post(`${apiUrl}Account/Register`, registerForm);
      console.log("register",response);

      if (response.status == 200) {
        // localStorage.setItem(
        //   LOCAL_STORAGE_TOKEN_NAME,
        //   response.data.accessToken
        // );
        // await loadUser();
        dispatch({
          type: "REGISTER",
          payload: { registerLoading: true },
        });
        return response;
      }
    } catch (error) {
      if (error.response.data) return error.response.data;
      else return { success: false, message: error.message };
    }
  };
  //Logout
  const logoutUser = () => {
    localStorage.removeItem(LOCAL_STORAGE_TOKEN_NAME);
    setAuthToken(null);
    dispatch({
      type: "SET_AUTH_LOGOUT",
    });
  };

  const setStateRegister = ()=>{
    dispatch({
      type: "SET_STATE_REGISTER"
    })
  }

  //context data
  const authContextData = {
    LoginUser,
    authState,
    registerUser,
    logoutUser,
    setStateRegister
  };

  return (
    <AuthContext.Provider value={authContextData}>
      {children}
    </AuthContext.Provider>
  );
};

export default AuthContextProvider;
