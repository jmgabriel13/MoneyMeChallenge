import { CalculateCustomerQuoteRequest, CalculateCustomerQuoteResponse, CustomerLoanApplicationRequest, CustomerLoanDto, CustomerLoanRate } from "../models/customerLoanDto";
import { ResultResponse } from "../models/resultResponse";
import { API_BASE_URL } from './../../config';
import axiosInstance from "./axiosInstance";

const customerApi = {
    getCustomerLoanRate: async (customerLoan: CustomerLoanRate): Promise<ResultResponse<string>> => {
        const response = await axiosInstance.post<ResultResponse<string>>(`${API_BASE_URL}/api/customers/loan/rate`, customerLoan);
            
        return response.data;
    },

    getCustomerLoanById: async (customerId: string): Promise<CustomerLoanDto> => {
        const response = await axiosInstance.get<ResultResponse<CustomerLoanDto>>(`${API_BASE_URL}/api/customers/loan/${customerId}`)
            
        return response.data.value;
    },

    calculateQuote: async (calculateCustomerQuoteRequest: CalculateCustomerQuoteRequest): Promise<ResultResponse<CalculateCustomerQuoteResponse>> => {
        const response = await axiosInstance.get<ResultResponse<CalculateCustomerQuoteResponse>>(`${API_BASE_URL}/api/customers/calculate/quote`, { params: calculateCustomerQuoteRequest });
        console.log("calculateQuote")
        console.log(response)
        return response.data;
    },

    customerLoanApplication: async (customerLoanApplicationRequest: CustomerLoanApplicationRequest): Promise<string> => {
        const response = await axiosInstance.post(`/api/customers/loan/application`, customerLoanApplicationRequest);
        console.log(response)
        return response.data;
    },

}

export default customerApi;