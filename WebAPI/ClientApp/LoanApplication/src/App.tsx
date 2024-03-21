import { Outlet, useLocation } from 'react-router-dom'
import './App.css'
import QouteCalculator from './components/QuoteCalculator/QuoteCalculator'

function App() {
	const location = useLocation();

	return (
		<>
			{location.pathname === '/' ? <QouteCalculator /> : (
				<div>
					<Outlet />
				</div>
			)}
		</>
	)
}

export default App
