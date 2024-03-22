import React from 'react'
import ReactDOM from 'react-dom/client'
import './App.css'
import { RouterProvider } from 'react-router-dom'
import { router } from './router/routes.tsx'
import { CssBaseline, ThemeProvider, createTheme } from '@mui/material'
import { LocalizationProvider } from '@mui/x-date-pickers'
import { AdapterDayjs } from '@mui/x-date-pickers/AdapterDayjs'

const theme = createTheme({
	palette: {
		primary: {
			main: '#4cd133',
		},
	},
	typography: {
		fontFamily: [
			'Preto Sans',
			'Mediator'
		].join(','),
		button: {
			textTransform: 'none'
		},
	}
});

ReactDOM.createRoot(document.getElementById('root')!).render(
	<React.Fragment>
		<ThemeProvider theme={theme}>
			<LocalizationProvider dateAdapter={AdapterDayjs}>
				<CssBaseline />
				<RouterProvider router={router}/>
			</LocalizationProvider>
		</ThemeProvider>
	</React.Fragment>,
)
