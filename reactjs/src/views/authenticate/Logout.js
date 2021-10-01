import React, { useContext, useEffect } from 'react';
import { Redirect } from 'react-router-dom';
import { Spinner } from 'reactstrap';

import LoginForm from './../../components/authenticate/LoginForm';
import { AuthContext } from './../../contexts/member/AuthContext';
import * as AUTH_TYPE from './../../reducers/member/authType';

export default function LogoutAccount({ title }) {
	const { authState, dispatch } = useContext(AuthContext);

	useEffect(() => {
		dispatch({
			type: AUTH_TYPE.LOGOUT,
			payload: null,
		});
	}, []);

	return <Redirect to="/dang-nhap" />;
}
