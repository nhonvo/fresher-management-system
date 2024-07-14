import { createContext, useReducer } from "react";
import { userReducer } from "../reducer/userReducer";
import { USER_LOADED_SUCCESS, USER_LOADED_FAIL, GET_USER_PROFILE, STUDENT_GET_CLASSGPA_SUCCESS, UPDATE_ATTENDANCE, apiUrl, SET_STATE, SET_ROLE } from "./Constants";
import axios from "axios";

export const UserContext = createContext();
function UserProvider({ children }) {
    const [userState, dispatch] = useReducer(userReducer,
        {
            users: [],
            user: null,
            usersLoading: true,
            studentClassGPA: [],
            pageIndex: 0,
            pageSize: 10,
            totalPagesCount: 0,
            totalItemsCount: 0,
        })
    const getUsers = async (pageIndex, pageSize) => {
        try {
            const response = await axios.get(`${apiUrl}User?pageIndex=${pageIndex}&pageSize=${pageSize}`)
            console.log(response);
            if (response.status == 200) {
                dispatch({
                    type: USER_LOADED_SUCCESS,
                    payload: response.data,
                })
            }
        } catch (error) {
            dispatch({ USER_LOADED_FAIL })

        }
    }

    const getAttendances = async (pageIndex, pageSize) => {
        try {
            const response = await axios.get(`${apiUrl}Attendance/Filter?pageIndex=${pageIndex}&pageSize=${pageSize}`)
            console.log(response);
            if (response.status == 200) {
                dispatch({
                    type: USER_LOADED_SUCCESS,
                    payload: response.data,
                })
            }
        } catch (error) {
            dispatch({ USER_LOADED_FAIL })

        }
    }

    const getUserProfile = async () => {
        try {
            const response = await axios.get(`${apiUrl}User/Profile`);
            console.log(response);
            if (response.status == 200) {
                dispatch({
                    type: GET_USER_PROFILE,
                    payload: response.data,
                });
            }
        } catch (error) {
            dispatch({ USER_LOADED_FAIL });
        }
    };

    const getStudentClassGPA = async (id, pageIndex, pageSize) => {
        try {
            const response = await axios.get(
                `${apiUrl}TestAssessment/student/${id}/classGPA?pageIndex=${pageIndex}&pageSize=${pageSize}`
            );
            console.log(response);
            if (response.status == 200) {
                dispatch({
                    type: STUDENT_GET_CLASSGPA_SUCCESS,
                    payload: response.data,
                });
            }
        } catch (error) {
            dispatch({ USER_LOADED_FAIL });
        }
    };

    const approveAttendance = async (attendanceId, status) => {
        try {
            console.log(attendanceId, status);
            const response = await axios.put(`${apiUrl}Attendance/ApproveAbsent`, {
                id: attendanceId,
                attendanceStatus: status
            });
            console.log(response);
            if (response.data.success)
                dispatch({
                    type: UPDATE_ATTENDANCE,
                    payload: response.data,
                })


            return response;

        } catch (error) {
            return error.response.data
                ? error.response.data
                : { success: false, message: "server error" };
        }
    }

    const setUserState = () => {
        dispatch({
            type: SET_STATE,
        })
    }

    const setRole = async (value) => {
        try {
            const response = await axios.post(`${apiUrl}Account/add-role`, value)
            console.log(response);
            if (response.status == 200) {
                dispatch({
                    type: SET_ROLE,
                    payload: response.data,
                })
            }
        } catch (error) {
            dispatch({ USER_LOADED_FAIL })
        }
    }

    const sendAbsenceRequest = async (value) => {
        try {
            const response = await axios.post(`${apiUrl}Attendance`, value)
            console.log(response);
            if (response.status == 200) {
                dispatch({
                    type: SET_ROLE,
                    payload: response.data,
                })
            }
        } catch (error) {
            dispatch({ USER_LOADED_FAIL })
        }
    }

    const userContextData = {
        getUsers,
        setUserState,
        getUserProfile,
        getStudentClassGPA,
        getAttendances,
        approveAttendance,
        sendAbsenceRequest,
        userState,
        setRole
    }
    return (
        <UserContext.Provider value={userContextData}>
            {children}
        </UserContext.Provider>
    )
}
export default UserProvider