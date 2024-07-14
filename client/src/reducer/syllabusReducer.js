import {
    SYLLABUS_LOADED_SUCCESS,
    SYLLABUS_LOADED_FAIL,
    ADD_SYLLABUS,
    DELETE_SYLLABUS,
    UPDATE_SYLLABUS,
    FIND_SYLLABUS,
    SET_STATE,
    FIND_SYLLABUS_DETAIL,
    ADD_SYLLABUS_UNIT,
    UPDATE_SYLLABUS_UNIT,
    DELETE_SYLLABUS_UNIT,
    ADD_SYLLABUS_LESSON,
    UPDATE_SYLLABUS_LESSON,
    DELETE_SYLLABUS_LESSON,
    ADD_SYLLABUS_MATERIAL,
    UPDATE_SYLLABUS_MATERIAL,
    DELETE_SYLLABUS_MATERIAL,
    IMPORT_SYLLABUS_FILE
} from "../context/Constants";

export const syllabusReducer = (state, action) => {
    const { type, payload } = action;
    switch (type) {
        case SYLLABUS_LOADED_SUCCESS:
            return {
                ...state,
                syllabuses: payload.items,
                syllabusLoading: false,
                pageIndex: payload.pageIndex,
                pageSize: payload.pageSize,
                totalPagesCount: payload.totalPagesCount,
                totalItemsCount: payload.totalItemsCount
            };

        case SET_STATE:
            return {
                ...state,
                syllabusLoading: true,
            };

        case FIND_SYLLABUS: {
            return {

                ...state,
                syllabus: payload,
                syllabusLoading: false,
            }
        }


        case SYLLABUS_LOADED_FAIL:
            return {
                ...state,
                syllabuses: [],
                syllabusLoading: false,
            };

        case ADD_SYLLABUS:
            return {
                ...state,
                //syllabuses: [...state.syllabuses, payload],
                syllabusLoading: false,
            };
        case DELETE_SYLLABUS:
            return {
                ...state,
            };
        case UPDATE_SYLLABUS:
            return {
                ...state,
                syllabusLoading: false,
                syllabus: payload,
                syllabusDetail: payload
            };
        case FIND_SYLLABUS_DETAIL:

            return {
                ...state,
                syllabusDetail: payload,
            }

        case ADD_SYLLABUS_UNIT:
            return {
                ...state,
                syllabusLoading: false,

            }
        case UPDATE_SYLLABUS_UNIT:
            return {
                ...state,
                syllabusLoading: false,

            }
        case DELETE_SYLLABUS_UNIT:
            return {
                ...state,
                syllabusLoading: false,

            }

        case ADD_SYLLABUS_LESSON:
            return {
                ...state,
                syllabusLoading: false,

            }
        case UPDATE_SYLLABUS_LESSON:
            return {
                ...state,
                syllabusLoading: false,

            }
        case DELETE_SYLLABUS_LESSON:
            return {
                ...state,
                syllabusLoading: false,

            }
        case ADD_SYLLABUS_MATERIAL:
            return {
                ...state,
                syllabusLoading: false,

            }
        case UPDATE_SYLLABUS_MATERIAL:
            return {
                ...state,
                syllabusLoading: false,

            }
        case DELETE_SYLLABUS_MATERIAL:
            return {
                ...state,
                syllabusLoading: false,
            }
        case IMPORT_SYLLABUS_FILE:
            return {
                ...state,
            }
        // case FIND_SYLLABUS:
        //     return {
        //         ...state,
        //         syllabuses: payload,
        //     };
    }
}