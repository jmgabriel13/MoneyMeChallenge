import { RouteObject, createBrowserRouter } from "react-router-dom";
import App from "../App";
import QuoteCalculator from "../components/QuoteCalculator/QuoteCalculator";
import Quote from "../components/QuoteCalculator/Quote";
import SuccessPage from "../components/SuccessPage/SuccessPage";

export const routes: RouteObject[] = [
    {
        path: '/',
        element: <App />,
        children: [
            { path: 'quote-calculator', element: <QuoteCalculator key='quote-calculator' /> },
            { path: 'quote', element: <Quote key='quote' /> },
            { path: 'success', element: <SuccessPage key='success' /> },
            { path: '*', element: <QuoteCalculator /> }
        ]
    }
]

export const router = createBrowserRouter(routes)