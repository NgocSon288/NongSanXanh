import { ROLES } from './../common/constants.js';

export const checkRoleIsValid = (pageRoles, userRoles) => {
	if (!pageRoles || pageRoles.length <= 0) {
		return true;
	}

	if (pageRoles.indexOf(ROLES.anonymous) >= 0) {
		return true;
	}

	try {
		let roles = userRoles.filter((r) => pageRoles.indexOf(r) >= 0);
		if (roles && roles.length > 0) {
			return true;
		}

		return false;
	} catch (error) {
		return false;
	}
};
