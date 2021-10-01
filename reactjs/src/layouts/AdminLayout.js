import React, { useContext, useEffect, useState } from 'react';
import { Link, Redirect } from 'react-router-dom';

import { checkRoleIsValid } from './../utils/checkRoleIsValid';

import AdminHeader from './../components/partials/admin/AdminHeader';
import AdminFooter from '../components/partials/admin/AdminFooter';

import { AuthContext } from './../contexts/member/AuthContext';
import * as AUTH_TYPE from './../reducers/member/authType';

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
			<AdminHeader />
			<hr />
			<div>{children}</div>
			<Link to="/">Trở lại trang chủ</Link>
			<AdminFooter />
		</React.Fragment>
	);
}
