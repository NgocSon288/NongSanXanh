import authenticateRoute from './authenticateRoute';
import dashboardRoute from './dashboardRoute';
import profileRoute from './profileRoute';
import sharedRoute from './sharedRoute';
import siteRoute from './siteRoute';

let route = [
	authenticateRoute,
	dashboardRoute,
	profileRoute,
	siteRoute,
	sharedRoute,
];

route = route.flat();

export default route;
