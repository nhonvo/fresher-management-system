import React, { createContext, useReducer } from 'react'
import { trainingProgramReducer } from '../reducer/trainingProgramReducer';
import axios from "axios";
import { apiUrl, TRAINING_PROGRAM_LOADED_SUCCESS, TRAINING_PROGRAM_GET_SUCCESS, TRAINING_PROGRAM_LOADED_FAIL, SET_STATE } from "./Constants";


export const TrainingProgramContext = createContext();
function TrainingProgramProvider({ children }) {
    const [TrainingProgramState, dispatch] = useReducer(trainingProgramReducer, {
        trainingProgram: null,
        trainingProgramList: [],
        trainingProgramLoading: true,
        pageIndex: 0,
        pageSize: 10,
        totalPagesCount: 0,
        totalItemsCount: 0,
    })


    const getTrainingProgramList = async (pageIndex, pageSize) => {
        try {
            const response = await axios.get(`${apiUrl}TrainingProgram?pageIndex=${pageIndex}&pageSize=${pageSize}`)
            console.log(response);
            if (response.status == 200) {
                dispatch({
                    type: TRAINING_PROGRAM_LOADED_SUCCESS,
                    payload: response.data,
                })
            }
        } catch (error) {
            dispatch({ TRAINING_PROGRAM_LOADED_FAIL })

        }
    }

    const getTrainingProgram = async (id) => {
        try {
            const response = await axios.get(`${apiUrl}TrainingProgram/Detail/${id}`)
            console.log(response);
            if (response.status == 200) {
                dispatch({
                    type: TRAINING_PROGRAM_GET_SUCCESS,
                    payload: response.data,
                })
            }
        } catch (error) {
            dispatch({ TRAINING_PROGRAM_LOADED_FAIL })
        }
    }
    const setStateTrainingProgram = () => {
        dispatch({
            type: SET_STATE
        })
    }


    const trainingProgramContextData = {
        getTrainingProgramList,
        TrainingProgramState,
        getTrainingProgram,
        setStateTrainingProgram
    }
    return (
        <TrainingProgramContext.Provider value={trainingProgramContextData}>
            {children}
        </TrainingProgramContext.Provider>
    );
}

export default TrainingProgramProvider