import React from 'react'
import { SET_STATE, USER_GET_SUCCESS,UPDATE_ATTENDANCE, GET_USER_PROFILE, USER_LOADED_FAIL, USER_LOADED_SUCCESS , STUDENT_GET_CLASSGPA_SUCCESS, SET_ROLE} from '../context/Constants'

export const userReducer = (state, action) => {
    const { type, payload } = action
    switch (type) {
        case USER_LOADED_SUCCESS:
            return {
                ...state,
                usersLoading: false,
                users: payload.items,
                pageIndex: payload.pageIndex,
                pageSize: payload.pageSize,
                totalPagesCount: payload.totalPagesCount,
                totalItemsCount: payload.totalItemsCount
            }
        case USER_LOADED_FAIL:
            return {
                ...state,
                users: [],
                usersLoading: false,
            };
        case STUDENT_GET_CLASSGPA_SUCCESS:
            return {
                ...state,
                studentClassGPA: payload.data.items,
                usersLoading: false,
                pageIndex: payload.data.pageIndex,
                pageSize: payload.data.pageSize,
                totalPagesCount: payload.data.totalPagesCount,
                totalItemsCount: payload.data.totalItemsCount,
            };
        case GET_USER_PROFILE:
            return {
                ...state,
                user: payload,
                usersLoading: false,
            };
        case UPDATE_ATTENDANCE:
            return {
                ...state,
                usersLoading: false,
                user: payload,
            };
        case SET_STATE:
            return {
                ...state,
                usersLoading: true,
            }
        case SET_ROLE:{
            return{
                ...state,
                usersLoading: false
            }
        }
    }
}
