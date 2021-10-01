// Admin

// Roles
import { ROLES } from './../common/constants';

// Member
import MemberHome from '../views/sites/member/Home';
import MemberContact from '../views/sites/member/Contact';
import MemberProfile from '../views/profile/member/Profile';

//  Layouts
import MemberLayout from './../layouts/MemberLayout';

// ContextProvider

const siteRoute = [
	// Admin Sites route

	//-----------------------------------------------------------------------------------------
	// Member Sites route
	// Member Home route
	{
		path: '/',
		title: 'Trang chủ',
		icon: 'design_app',
		roles: [ROLES.anonymous],
		component: MemberHome,
		layout: MemberLayout,
		wrapContextProvider: null,
	},
	// Member Contact route
	{
		path: '/lien-he',
		title: 'Trang liên hệ',
		icon: 'design_app',
		roles: [ROLES.anonymous],
		component: MemberContact,
		layout: MemberLayout,
		wrapContextProvider: null,
	},
	// Member Contact route
	{
		path: '/ca-nhan',
		title: 'Trang cá nhân',
		icon: 'design_app',
		roles: [ROLES.member],
		component: MemberProfile,
		layout: MemberLayout,
		wrapContextProvider: null,
	},
];

export default siteRoute;
