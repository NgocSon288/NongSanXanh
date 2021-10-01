// Shared
import Error401 from './../views/shared/Error401';

// Layouts

const sharedRoute = [
	// Error 401 Unauthorized
	{
		path: '/error-401',
		title: 'Không có quyền',
		icon: 'design_app',
		roles: null,
		component: Error401,
		layout: ({ children }) => <>{children}</>,
		wrapContextProvider: null,
	},
];

export default sharedRoute;
