import React from 'react';
import { Link } from 'react-router-dom';

const Dashboard = () => {
	return (
		<div className="admin-dashboard">
			<h1>Trang này cần quyền provider</h1>

			<Link to="/">Trở về trang chủ chung</Link>
		</div>
	);
};

export default Dashboard;
