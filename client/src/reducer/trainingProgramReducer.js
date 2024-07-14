import {
    TRAINING_PROGRAM_LOADED_SUCCESS,
    TRAINING_PROGRAM_LOADED_FAIL,
    ADD_TRAINING_PROGRAM,
    DELETE_TRAINING_PROGRAM,
    UPDATE_TRAINING_PROGRAM,
    FIND_TRAINING_PROGRAM,
    TRAINING_PROGRAM_GET_SUCCESS,
    SET_STATE
} from "../context/Constants";


export const trainingProgramReducer = (state, action) => {
    const { type, payload } = action;
    switch (type) {
        case TRAINING_PROGRAM_LOADED_SUCCESS:
            return {
                ...state,
                trainingProgramList: payload.items,
                trainingProgramLoading: false,
                pageIndex: payload.pageIndex,
                pageSize: payload.pageSize,
                totalPagesCount: payload.totalPagesCount,
                totalItemsCount: payload.totalItemsCount
            };

        case TRAINING_PROGRAM_LOADED_FAIL:
            return {
                ...state,
                trainingProgramList: [],
                trainingProgramLoading: false,
            };

        case TRAINING_PROGRAM_GET_SUCCESS:
            return {
                ...state,
                trainingProgram: payload,
                trainingProgramLoading: false,
            };
        case SET_STATE:
            return {
                ...state,
                trainingProgramLoading: true,
            };
        // case ADD_TRAINING_PROGRAM:
        //     return {
        //         ...state,
        //         syllabuses: [...state.syllabuses, payload],
        //         syllabusLoading: false,
        //     };
        // case DELETE_TRAINING_PROGRAM:
        //     return {
        //         ...state,
        //         syllabuses: state.syllabuses.filter((syllabus) => syllabus._id !== payload),
        //     };
        // case UPDATE_TRAINING_PROGRAM:
        //     const newSyllabus = state.syllabuses.map((syllabus) =>
        //         syllabus._id === payload._id ? payload : syllabus
        //     );
        //     return {
        //         ...state,
        //         syllabuses: newSyllabus,
        //     };
        // case FIND_TRAINING_PROGRAM:
        //     return {
        //         ...state,
        //         syllabuses: payload,
        //     };
    }
}
