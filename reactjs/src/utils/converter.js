export const apiErrorToMessages = (error) => {
	let errs = null;

	if (error.response.data.errors) {
		errs = Object.entries(error.response.data.errors)
			.map(([key, val]) => val)
			.flat();
	} else {
		const { validationErrors, message } = error.response.data;
		if (validationErrors && validationErrors.lenght > 0) {
			errs = [...validationErrors];
		} else {
			errs = [message];
		}
	}

	return errs;
};
