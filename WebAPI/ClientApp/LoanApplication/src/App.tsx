import { Outlet, useLocation } from 'react-router-dom'
import './App.css'
import QouteCalculator from './components/QuoteCalculator/QuoteCalculator'

function App() {
	const location = useLocation();

	return (
		<main>
			{location.pathname === '/' ? <QouteCalculator /> : (
				<div>
					<Outlet />
				</div>
			)}
		</main>
	)
}

export default App
