import { createContext, useReducer, useEffect } from "react";
import { syllabusReducer } from "../reducer/syllabusReducer";
import axios from "axios";
import { apiUrl, SYLLABUS_LOADED_SUCCESS, SYLLABUS_LOADED_FAIL, SET_STATE, FIND_SYLLABUS, ADD_SYLLABUS, FIND_SYLLABUS_DETAIL, UPDATE_SYLLABUS, DELETE_SYLLABUS, ADD_SYLLABUS_UNIT, UPDATE_SYLLABUS_UNIT, DELETE_SYLLABUS_UNIT, ADD_SYLLABUS_LESSON, UPDATE_SYLLABUS_LESSON, DELETE_SYLLABUS_LESSON, ADD_SYLLABUS_MATERIAL, IMPORT_SYLLABUS_FILE } from "./Constants";


export const SyllabusContext = createContext();

//https://localhost:5001/api/Syllabuses

const SyllabusContextProvider = ({ children }) => {

    //syllabusState
    const [syllabusState, dispatch] = useReducer(syllabusReducer, {
        syllabus: null,
        syllabuses: [],
        syllabusLoading: true,
        pageIndex: 0,
        pageSize: 10,
        totalPagesCount: 0,
        syllabusDetail: [],


    });

    const getSyllabuses = async (pageIndex, pageSize) => {
        try {
            const response = await axios.get(`${apiUrl}Syllabuses?pageIndex=${pageIndex}&pageSize=${pageSize}`);
            console.log(response);
            if (response.data != null) {
                dispatch({
                    type: SYLLABUS_LOADED_SUCCESS,
                    payload: response.data,
                });



            }

        } catch (error) {
            dispatch({ type: SYLLABUS_LOADED_FAIL });
        }
    };
    const setSyllabusState = () => {
        dispatch({
            type: SET_STATE
        })
    }

    const createSyllabus = async (syllabusForm) => {
        try {
            const response = await axios.post(`${apiUrl}Syllabuses`, syllabusForm);
            if (response.data.success)
                dispatch({
                    type: ADD_SYLLABUS,
                    payload: response.data,
                })


            return response;

        } catch (error) {
            return error.response.data
                ? error.response.data
                : { success: false, message: "server error" };
        }
    }
    const updateSyllabus = async (syllabusFormUpdate) => {
        try {
            const response = await axios.put(`${apiUrl}Syllabuses`, syllabusFormUpdate);
            console.log(response);
            if (response.data.success)
                dispatch({
                    type: UPDATE_SYLLABUS,
                    payload: response.data,
                })


            return response;

        } catch (error) {
            return error.response.data
                ? error.response.data
                : { success: false, message: "server error" };
        }
    }
    const DeleteSyllabus = async (idSyllabus) => {
        try {
            const response = await axios.delete(`${apiUrl}Syllabuses/${idSyllabus}`);
            console.log(response);
            if (response.data.success) {
                dispatch({
                    type: DELETE_SYLLABUS,
                    payload: response.data
                });
            }
            return response;

        } catch (error) {
            return error.response.data
                ? error.response.data
                : { success: false, message: "server error" };
        }
    }




    const getSyllabus = async (id) => {
        try {
            console.log("context");
            const response = await axios.get(`${apiUrl}Syllabuses/${id}`)
            console.log(response);
            if (response.status == 200) {
                dispatch({
                    type: FIND_SYLLABUS,
                    payload: response.data,
                })
            }
        } catch (error) {
            dispatch({ SYLLABUS_LOADED_SUCCESS })
        }
    }
    const getSyllabusDetail = async (id) => {
        try {
            console.log("context");
            const response = await axios.get(`${apiUrl}Syllabuses/Detail/${id}`)
            console.log(response);
            if (response.status == 200) {
                dispatch({
                    type: FIND_SYLLABUS_DETAIL,
                    payload: response.data,
                })
            }
        } catch (error) {
            dispatch({ SYLLABUS_LOADED_SUCCESS })
        }
    }
    const AddSyllabusUnit = async (from, id) => {
        try {
            console.log("context");
            const response = await axios.post(`${apiUrl}Syllabuses/${id}/Unit/`, from)
            console.log(response);
            if (response.status == 200) {
                dispatch({
                    type: ADD_SYLLABUS_UNIT,
                    payload: response.data,
                })
            }
        } catch (error) {
            dispatch({ SYLLABUS_LOADED_SUCCESS })
        }
    }

    const UpdateSyllabusUnit = async (fromUpdate) => {
        try {
            const response = await axios.put(`${apiUrl}Unit`, fromUpdate)
            console.log(response);
            if (response.status == 200) {
                dispatch({
                    type: UPDATE_SYLLABUS_UNIT,
                    payload: response.data,
                })
            }
        } catch (error) {
            dispatch({ SYLLABUS_LOADED_SUCCESS })
        }
    }

    const DeleteSyllabusUnit = async (id) => {
        try {
            const response = await axios.delete(`${apiUrl}Unit/${id}`)
            console.log(response);
            if (response.status == 200) {
                dispatch({
                    type: DELETE_SYLLABUS_UNIT,
                    payload: response.data,
                })
            }
        } catch (error) {
            dispatch({ SYLLABUS_LOADED_FAIL })
        }
    }


    const AddSyllabusLesson = async (from, id) => {
        try {
            const response = await axios.post(`${apiUrl}Syllabuses/${id}/Lesson`, from)
            console.log(response);
            if (response.status == 200) {
                dispatch({
                    type: ADD_SYLLABUS_LESSON,
                    payload: response.data,
                })
            }
        } catch (error) {
            dispatch({ SYLLABUS_LOADED_SUCCESS })
        }
    }

    const UpdateSyllabusLesson = async (fromUpdate) => {
        try {
            const response = await axios.put(`${apiUrl}Lesson`, fromUpdate)
            console.log(response);
            if (response.status == 200) {
                dispatch({
                    type: UPDATE_SYLLABUS_LESSON,
                    payload: response.data,
                })
            }
        } catch (error) {
            dispatch({ SYLLABUS_LOADED_SUCCESS })
        }
    }



    const DeleteSyllabusLesson = async (id) => {
        try {
            const response = await axios.delete(`${apiUrl}Lesson/${id}`)
            console.log(response);
            if (response.status == 200) {
                dispatch({
                    type: DELETE_SYLLABUS_LESSON,
                    payload: response.data,
                })
            }
        } catch (error) {
            dispatch({ SYLLABUS_LOADED_FAIL })
        }
    }


    const AddSyllabusMaterial = async (files, id) => {
        try {
            const response = await axios.post(`${apiUrl}Lesson/${id}/training-materials`, files,
                {
                    headers: {
                        'Content-Type': 'multipart/form-data', // Đặt tiêu đề Content-Type
                    },
                })
            console.log(response);
            if (response.status == 200) {
                dispatch({
                    type: ADD_SYLLABUS_MATERIAL,
                    payload: response.data,
                })
            }
        } catch (error) {
            dispatch({ SYLLABUS_LOADED_SUCCESS })
        }
    }

    const importFileSyllabus = async (files) => {
        try {
            const response = await axios.post(`${apiUrl}Syllabuses/import-syllabuses-csv-v2`, files,
                {
                    headers: {
                        'Content-Type': 'multipart/form-data', // Đặt tiêu đề Content-Type
                    },
                    params: {
                        IsScanCode: '',
                        IsScanName: '',
                        DuplicateHandle: '',
                    },
                })
            console.log(response);
            if (response.status == 200) {
                dispatch({
                    type: IMPORT_SYLLABUS_FILE,
                    payload: response.data,
                })
            }
        } catch (error) {
            dispatch({ SYLLABUS_LOADED_SUCCESS })
        }
    }

    const SyllabusContextData = {
        getSyllabuses,
        syllabusState,
        setSyllabusState,
        getSyllabus,
        createSyllabus,
        getSyllabusDetail,
        updateSyllabus,
        DeleteSyllabus,
        AddSyllabusUnit,
        UpdateSyllabusUnit,
        DeleteSyllabusUnit,
        AddSyllabusLesson,
        UpdateSyllabusLesson,
        DeleteSyllabusLesson,
        AddSyllabusMaterial,
        importFileSyllabus
    };
    return (
        <SyllabusContext.Provider value={SyllabusContextData}>
            {children}
        </SyllabusContext.Provider>
    );
}
export default SyllabusContextProvider;
