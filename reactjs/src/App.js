import { BrowserRouter as Router, Route, Switch } from 'react-router-dom';

import route from './routes/route';
import AuthContextProvider from './contexts/member/AuthContext';

const AppRoute = ({
	component: Component,
	layout: Layout,
	wrapContextProvider: WrapContextProvider,
	title,
	roles,
	...rest
}) => (
	<Route
		{...rest}
		render={(props) => (
			<AuthContextProvider>
				<Layout roles={roles} title={title}>
					{WrapContextProvider && (
						<WrapContextProvider>
							<Component {...props}></Component>
						</WrapContextProvider>
					)}
					{!WrapContextProvider && <Component {...props}></Component>}
				</Layout>
			</AuthContextProvider>
		)}
	></Route>
);

function App() {
	return (
		<Router>
			<Switch>
				{route.map(
					(
						{
							path,
							layout,
							component,
							title,
							wrapContextProvider,
							roles,
						},
						i
					) => (
						<AppRoute
							path={path}
							exact
							layout={layout}
							roles={roles}
							component={component}
							title={title}
							wrapContextProvider={wrapContextProvider}
							key={i}
						/>
					)
				)}
			</Switch>
		</Router>
	);
}

export default App;
