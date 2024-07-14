export const authReducer = (state, action) => {
  const {
    type,
    payload
  } = action;

  switch (type) {
    case "SET_AUTH":
      return {
        ...state,
        authLoading: false,
        isAuthenticated: true,
        user: payload,
      };

    case "REGISTER":
      return {
        ...state,
        registerLoading: false
      }
    case "SET_STATE_REGISTER":
      return {
        ...state,
        registerLoading: true
      }
    case "SET_AUTH_LOGOUT":
      return {
        ...state,
        isAuthenticated: false,
        user: null
      }

    default:
      return state;
  }
};
