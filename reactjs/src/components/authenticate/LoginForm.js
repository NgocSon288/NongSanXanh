import React, { useContext, useEffect, useState } from 'react';
import { Link, history } from 'react-router-dom';

import { AuthContext } from './../../contexts/member/AuthContext';
import * as AUTH_TYPE from './../../reducers/member/authType';

import './LoginForm.css';

export default function LoginForm() {
	const [user, setUser] = useState({
		username: '',
		password: '',
	});
	const { authState, dispatch } = useContext(AuthContext);

	const onChange = (e) => {
		setUser({ ...user, [e.target.name]: e.target.value });
	};

	const onSubmit = (e) => {
		e.preventDefault();

		dispatch({
			type: AUTH_TYPE.LOGIN,
			payload: { user },
		});
	};

	return (
		<>
			<Link to="/">Đi đến trang chủ</Link>
			<form action="" method="" onSubmit={(e) => onSubmit(e)}>
				<p>Tài khoản</p>
				<input
					type="text"
					name="username"
					value={user.username}
					onChange={(e) => onChange(e)}
				/>
				<p className="mt-5">Mật khẩu</p>
				<input
					type="password"
					name="password"
					value={user.password}
					onChange={(e) => onChange(e)}
				/>
				<label className="anim">
					<input type="checkbox" />
					<span> Nhớ tài khoản?</span>
				</label>
				<input
					type="submit"
					className="btn-login w-100"
					value="Đăng nhập"
				></input>
			</form>
		</>
	);
}
