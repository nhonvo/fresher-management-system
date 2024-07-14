import {
  TRAINING_CLASS_LOADED_SUCCESS,
  TRAINING_CLASS_LOADED_FAIL,
  TRAINING_CLASS_GET_SUCCESS,
  TRAINING_CLASS_GET_STUDENTGPA_SUCCESS,
  ADD_TRAINING_CLASS,
  DELETE_TRAINING_CLASS,
  UPDATE_TRAINING_CLASS,
  FIND_TRAINING_CLASS,
  SET_STATE,
} from "../context/Constants";

export const trainingClassReducer = (state, action) => {
  const { type, payload } = action;
  switch (type) {
    case TRAINING_CLASS_LOADED_SUCCESS:
      return {
        ...state,
        trainingClassList: payload.items,
        trainingClassLoading: false,
        pageIndex: payload.pageIndex,
        pageSize: payload.pageSize,
        totalPagesCount: payload.totalPagesCount,
        totalItemsCount: payload.totalItemsCount,
      };

    case TRAINING_CLASS_LOADED_FAIL:
      return {
        ...state,
        trainingClass: [],
        trainingClassLoading: false,
      };

    case TRAINING_CLASS_GET_SUCCESS:
      return {
        ...state,
        trainingClass: payload,
        trainingClassLoading: false,
      };
    case TRAINING_CLASS_GET_STUDENTGPA_SUCCESS:
      return {
        ...state,
        trainingClassStudentGPA: payload.data.items,
        trainingClassLoading: false,
        pageIndex: payload.data.pageIndex,
        pageSize: payload.data.pageSize,
        totalPagesCount: payload.data.totalPagesCount,
        totalItemsCount: payload.data.totalItemsCount,
      };
    case SET_STATE:
      return {
        ...state,
        trainingClassLoading: true,
      };

    // case ADD_TRAINING_CLASS:
    //     return {
    //         ...state,
    //         syllabuses: [...state.syllabuses, payload],
    //         syllabusLoading: false,
    //     };
    // case DELETE_TRAINING_CLASS:
    //     return {
    //         ...state,
    //         syllabuses: state.syllabuses.filter((syllabus) => syllabus._id !== payload),
    //     };
    case UPDATE_TRAINING_CLASS:
        return {
          ...state,
          trainingClassLoading: false,
          trainingClass: payload,
        };
    // case FIND_TRAINING_CLASS:
    //     return {
    //         ...state,
    //         syllabuses: payload,
    //     };
  }
};
