// Admin

// Roles
import { ROLES } from '../common/constants';

// Member
import MemberProfile from '../views/profile/member/Profile';

//  Layouts
import MemberLayout from '../layouts/MemberLayout';

// ContextProvider

const profileRoute = [
	// Admin Profile route

	//-----------------------------------------------------------------------------------------
	// Member Profile route
	// Member Profile route
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

export default profileRoute;
