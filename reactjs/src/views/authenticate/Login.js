import React, { useContext, useEffect } from 'react';
import { Redirect } from 'react-router-dom';
import { Spinner } from 'reactstrap';

import LoginForm from './../../components/authenticate/LoginForm';
import { AuthContext } from './../../contexts/member/AuthContext';
import * as AUTH_TYPE from './../../reducers/member/authType';

export default function LoginAccount({ title }) {
	const { authState, dispatch } = useContext(AuthContext);

	useEffect(() => {
		document.title = title;

		dispatch({
			type: AUTH_TYPE.SET_AUTH,
			payload: null,
		});
	}, []);

	if (authState.authLoading) {
		return (
			<div
				className="d-flex justify-content-center mt-2"
				style={{ height: '100%' }}
			>
				<Spinner color="primary" />
			</div>
		);
	}

	if (authState.isAuthenticated) {
		return <Redirect to="/" />;
	}
	return (
		<>
			<h1>Trang đăng nhập</h1>
			<ul>
				{authState.errors &&
					authState.errors.map((item, i) => (
						<b key={i}>
							{item} <br />
						</b>
					))}
			</ul>

			<LoginForm />
		</>
	);
}
