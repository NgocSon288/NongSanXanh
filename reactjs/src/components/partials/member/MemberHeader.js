import React, { useContext, useEffect } from 'react';
import { Link } from 'react-router-dom';

import { AuthContext } from './../../../contexts/member/AuthContext';
import * as AUTH_TYPE from './../../../reducers/member/authType';

import './MemberHeader.css';

const MemberHeader = () => {
	const { authState, dispatch } = useContext(AuthContext);

	return (
		<div className="member-header">
			<h1 className="member-header__title">
				Xin chào <span>{authState && authState.user}</span>
			</h1>
			<ul className="member-header__navbar">
				<li>
					<Link to="/">Trang chủ</Link>
				</li>
				<li>
					<Link to="/lien-he">Trang liên hệ</Link>
				</li>
				<li>
					<Link to="/ca-nhan">Trang cá nhân</Link>
				</li>
				{authState.isAuthenticated && (
					<li>
						<Link to="/dang-xuat">Đăng xuất</Link>
					</li>
				)}
				{!authState.isAuthenticated && (
					<li>
						<Link to="/dang-nhap">Đăng nhập</Link>
					</li>
				)}
			</ul>
		</div>
	);
};

export default MemberHeader;
