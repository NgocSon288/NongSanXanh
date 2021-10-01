import axios from 'axios';
import { urlStr } from '../common/constants';

const userService = {
	authenticate: async (id) => {
		const res = await axios.get(`${urlStr}/accounts/authenticate`);
		return res.data;
	},
	login: async (account) => {
		const res = await axios.post(`${urlStr}/accounts/login`, account);
		return res.data;
	},
	register: async (account) => {
		const fd = new FormData();
		for (const [name, value] of Object.entries(account)) {
			fd.append(name, value);
		}

		const res = await axios.post(`${urlStr}/accounts/register`, fd);
		return res.data;
	},
};

export default userService;
