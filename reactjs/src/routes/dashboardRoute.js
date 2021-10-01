// Admin
import AdminDashboard from './../views/dashboard/admin/Dashboard';
import ProviderDashboard from './../views/dashboard/provider/Dashboard';

// Roles
import { ROLES } from '../common/constants';

// Member

//  Layouts
import AdminLayout from '../layouts/AdminLayout';
import ProviderLayout from '../layouts/ProviderLayout';

// ContextProvider

const dashboardRoute = [
	// Admin Dashboard route
	// Admin Dashboard route
	{
		path: '/admin',
		title: 'Trang chủ nhà quản trị',
		icon: 'design_app',
		roles: [ROLES.admin],
		component: AdminDashboard,
		layout: AdminLayout,
		wrapContextProvider: null,
	},
	//-----------------------------------------------------------------------------------------
	// Provider Dashboard route
	// Provider Dashboard route
	{
		path: '/provider',
		title: 'Trang chủ nhà cung cấp',
		icon: 'design_app',
		roles: [ROLES.provider],
		component: ProviderDashboard,
		layout: ProviderLayout,
		wrapContextProvider: null,
	},
];

export default dashboardRoute;
