import React from 'react';
import { Link } from 'react-router-dom';

const Error401 = () => {
	return (
		<>
			<h1>Bạn không có quyền truy cập trang này</h1>
			<Link to="/">Quay lại trang chủ</Link>
		</>
	);
};

export default Error401;
