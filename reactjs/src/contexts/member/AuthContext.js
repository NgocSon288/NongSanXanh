import React, { createContext, useEffect } from 'react';

import { authReducer } from '../../reducers/member/authReducer';
import useAsyncReducer from '../../reducers/useAsyncReducer';
import * as AUTH_TYPE from '../../reducers/member/authType';

export const AuthContext = createContext();

export default function AuthContextProvider({ children }) {
	const [authState, dispatch] = useAsyncReducer(authReducer, {
		authLoading: true,
		isAuthenticated: false,
		permission: [],
		user: null,
		errors: [],
	});

	// useEffect(() => {
	// 	dispatch({
	// 		type: AUTH_TYPE.SET_AUTH,
	// 		payload: null,
	// 	});
	// }, []);

	const authContextData = {
		authState,
		dispatch,
	};

	return (
		<AuthContext.Provider value={authContextData}>
			{children}
		</AuthContext.Provider>
	);
}
