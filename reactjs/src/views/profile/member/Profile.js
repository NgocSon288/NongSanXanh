import React, { useContext } from 'react';
import { Link } from 'react-router-dom';

import { AuthContext } from '../../../contexts/member/AuthContext';

const Profile = () => {
	const { authState } = useContext(AuthContext);

	return (
		<div className="profile">
			<h4>Trang profile: {authState.user}</h4>
			<Link to="/provider">Đi đến trang Nhà cùng cấp của tôi</Link>
		</div>
	);
};

export default Profile;
