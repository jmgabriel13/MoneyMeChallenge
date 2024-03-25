import axios from "axios";
import { CustomerLoanDto, CustomerLoanRate, CustomerLoanResponse } from "../models/customerLoanDto";
import { API_BASE_URL } from './../../config';

const customerApi = {
    getCustomerLoanById: async (customerId: string): Promise<CustomerLoanDto> => {
        try {
            const response = await axios.get<CustomerLoanResponse>(`${API_BASE_URL}/api/customers/loan/${customerId}`)
            console.log(response)
            return response.data.value;
        } catch (error) {
            console.log(error);
            throw error;
        }
    },

    getCustomerRate: async (customerLoan: CustomerLoanRate): Promise<string> => {
        try {
            const response = await axios.post<string>(`${API_BASE_URL}/api/customers/rate`, customerLoan);
            console.log(response)
            return response.data;
        } catch (error) {
            console.log(error)
            throw error;
        }
    },

}

export default customerApi;