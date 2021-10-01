import userService from '../../services/userService';
import * as TYPE from './authType';
import setAuthToken from '../../utils/setAuthToken';
import { LOCAL_STORAGE_TOKEN_NAME } from '../../common/constants';
import { apiErrorToMessages } from './../../utils/converter';

const loadState = async (state, errs) => {
	setAuthToken(localStorage[LOCAL_STORAGE_TOKEN_NAME]);

	try {
		const res = await userService.authenticate();

		if (res.isSuccessed) {
			state = {
				authLoading: false,
				isAuthenticated: true,
				permission: res.result.roles,
				user: res.result.fullName,
				errors: [],
			};
		}
	} catch (error) {
		let { status } = error.response;
		let errors = null;

		if (errs) {
			errors = status === 401 ? [...errs] : ['không phải 401'];
		}

		localStorage.removeItem(LOCAL_STORAGE_TOKEN_NAME);
		setAuthToken(null);
		state = {
			authLoading: false,
			isAuthenticated: false,
			permission: '',
			user: null,
			errors: errors,
		};
	}

	return state;
};

export const authReducer = async (state, action) => {
	const { type, payload } = action;

	switch (type) {
		case TYPE.SET_AUTH: {
			return await loadState(state);
		}

		case TYPE.LOGIN: {
			const { user } = payload;
			try {
				const res = await userService.login(user);
				if (res.isSuccessed) {
					localStorage.setItem(LOCAL_STORAGE_TOKEN_NAME, res.result);
					return await loadState(state);
				} else {
					return await loadState(state, res);
				}
			} catch (error) {
				localStorage.removeItem(LOCAL_STORAGE_TOKEN_NAME);
				return await loadState(state, apiErrorToMessages(error));
			}
		}

		case TYPE.REGISTER: {
			const { user } = payload;
			try {
				const res = await userService.create(user);
				if (res.isSuccessed)
					localStorage.setItem(LOCAL_STORAGE_TOKEN_NAME, res.accessToken);

				return await loadState(state);
			} catch (error) {
				if (error.res.data) return error.res.data;
				else return { isSuccessed: false, message: error.message };
			}
		}

		case TYPE.LOGOUT: {
			localStorage.removeItem(LOCAL_STORAGE_TOKEN_NAME);
			state = {
				authLoading: false,
				isAuthenticated: false,
				permission: '',
				user: null,
			};

			return state;
		}

		default:
			return state;
	}
};
