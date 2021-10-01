import React, { useContext, useEffect, useState } from 'react';
import { Redirect } from 'react-router-dom';

import { checkRoleIsValid } from '../utils/checkRoleIsValid';

import ProviderHeader from '../components/partials/provider/ProviderHeader';
import ProviderFooter from '../components/partials/provider/ProviderFooter';

import { AuthContext } from '../contexts/member/AuthContext';
import * as AUTH_TYPE from '../reducers/member/authType';

export default function AdminLayout({ children, roles, title }) {
	const { authState, dispatch } = useContext(AuthContext);

	document.title = title;

	useEffect(() => {
		dispatch({
			type: AUTH_TYPE.SET_AUTH,
			payload: null,
		});
	}, []);

	if (authState.authLoading) {
		return (
			<div className="d-flex justify-content-center align-items-center mt-2 my-loading">
				<div className="text-center">
					<h1>Loading...</h1>
				</div>
			</div>
		);
	}

	// if (!authState.isAuthenticated) return <Redirect to="/dang-nhap" />;

	if (!checkRoleIsValid(roles, authState.permission))
		return <Redirect to="/error-401" />;

	return (
		<React.Fragment>
			<ProviderHeader />
			<hr />
			<div>{children}</div>
			<ProviderFooter />
		</React.Fragment>
	);
}
