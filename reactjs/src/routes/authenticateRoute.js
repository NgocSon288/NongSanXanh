// Roles
import { ROLES } from './../common/constants';

// Admin

// Provider

// Member
import Login from './../views/authenticate/Login';
import Logout from './../views/authenticate/Logout';

//  Layouts
// import MemberLayout from './../layouts/MemberLayout'

// ContextProvider
import AuthContextProvider from './../contexts/member/AuthContext';

const userRoute = [
	// Member Login route
	{
		path: '/dang-nhap',
		title: 'Đăng nhập',
		icon: 'design_app',
		roles: [ROLES.anonymous],
		component: Login,
		layout: ({ children }) => <>{children}</>,
		wrapContextProvider: null,
	},
	// Member Logout route
	{
		path: '/dang-xuat',
		title: 'Trang đăng nhập',
		icon: 'design_app',
		roles: [ROLES.anonymous],
		component: Logout,
		layout: ({ children }) => <>{children}</>,
		wrapContextProvider: null,
	},

	//-----------------------------------------------------------------------------------------
	// Admin User route

	//-----------------------------------------------------------------------------------------
	// Admin User route
];

export default userRoute;
