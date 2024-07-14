import React, { createContext, useReducer } from "react";
import { trainingClassReducer } from "../reducer/trainingClassReducer";
import axios from "axios";
import {
  apiUrl,
  TRAINING_CLASS_LOADED_SUCCESS,
  TRAINING_CLASS_LOADED_FAIL,
  TRAINING_CLASS_GET_SUCCESS,
  TRAINING_CLASS_GET_STUDENTGPA_SUCCESS,
  TRAINING_PROGRAM_TEST_FILE_GET_SUCCESS,
  DELETE_TESTASSESSMENT,
  SET_STATE,
  UPDATE_TRAINING_CLASS,
} from "./Constants";

export const TrainingClassContext = createContext();
function TrainingClassProvider({ children }) {
  const [TrainingClassState, dispatch] = useReducer(trainingClassReducer, {
    trainingClass: null,
    trainingClassStudentGPA: [],
    trainingClassList: [],
    trainingClassLoading: true,
    pageIndex: 0,
    pageSize: 10,
    totalPageCount: 0,
    totalItemsCount: 0,
  });

  const getTrainingClassList = async (pageIndex, pageSize) => {
    try {
      const response = await axios.get(
        `${apiUrl}Class?pageIndex=${pageIndex}&pageSize=${pageSize}`
      );
      console.log(response);
      if (response.status == 200) {
        dispatch({
          type: TRAINING_CLASS_LOADED_SUCCESS,
          payload: response.data,
        });
      }
    } catch (error) {
      dispatch({ TRAINING_CLASS_LOADED_FAIL });
    }
  };

  const getTrainingClass = async (id) => {
    try {
      const response = await axios.get(`${apiUrl}Class/${id}`);
      console.log(response);
      if (response.status == 200) {
        dispatch({
          type: TRAINING_CLASS_GET_SUCCESS,
          payload: response.data,
        });
      }
    } catch (error) {
      dispatch({ TRAINING_CLASS_LOADED_FAIL });
    }
  };

  const getTrainingClassStudentGPA = async (id, pageIndex, pageSize) => {
    try {
      const response = await axios.get(
        `${apiUrl}TestAssessment/class/${id}/studentGPA?pageIndex=${pageIndex}&pageSize=${pageSize}`
      );
      console.log(response);
      if (response.status == 200) {
        dispatch({
          type: TRAINING_CLASS_GET_STUDENTGPA_SUCCESS,
          payload: response.data,
        });
      }
    } catch (error) {
      dispatch({ TRAINING_CLASS_LOADED_FAIL });
    }
  };

  const postSendRequest = async (classId) => {
    try {
      console.log(classId);
      const response = await axios.post(`${apiUrl}ApproveRequest`, {
        classId: classId,
      });
      console.log(response);
      if (response.data.success)
        dispatch({
          type: UPDATE_TRAINING_CLASS,
          payload: response.data,
        })


      return response;

    } catch (error) {
      return error.response.data
        ? error.response.data
        : { success: false, message: "server error" };
    }
  }

  const getTrainingProgramTestFile = async (id) => {
    try {
      window.open(`${apiUrl}TrainingMaterial/${id}/download`);
      
      // if (response.status == 200) {
      //   dispatch({
      //     type: TRAINING_PROGRAM_TEST_FILE_GET_SUCCESS,
      //     payload: response.data,
      //   });
      // }
    } catch (error) {
      dispatch({ TRAINING_CLASS_LOADED_FAIL });
    }
  };

  const getExcelFile = async (string) => {
    try {
      var path = string == "User" ? `${apiUrl}User/export-users-csv`
        : `${apiUrl}Syllabuses/export-syllabuses-csv`;
      window.open(path);

      // if (response.status == 200) {
      //   dispatch({
      //     type: TRAINING_PROGRAM_TEST_FILE_GET_SUCCESS,
      //     payload: response.data,
      //   });
      // }
    } catch (error) {
      dispatch({ TRAINING_CLASS_LOADED_FAIL });
    }
  };

  const deleteTestAssessment = async (id) => {
    try {
      const response = await axios.delete(`${apiUrl}TestAssessment/${id}`);
      console.log(response);
      if (response.data.success) {
        dispatch({
          type: DELETE_TESTASSESSMENT,
          payload: response.data,
        });
      }
      return response;
    } catch (error) {
      return error.response.data
        ? error.response.data
        : { success: false, message: "server error" };
    }
  };

  const setStateTrainingClass = () => {
    dispatch({
      type: SET_STATE,
    });
  };

  const trainingClassContextData = {
    getTrainingClassList,
    getTrainingClass,
    getTrainingClassStudentGPA,
    setStateTrainingClass,
    getTrainingProgramTestFile,
    getExcelFile,
    deleteTestAssessment,
    postSendRequest,
    TrainingClassState,
  };
  return (
    <TrainingClassContext.Provider value={trainingClassContextData}>
      {children}
    </TrainingClassContext.Provider>
  );
}

export default TrainingClassProvider;
