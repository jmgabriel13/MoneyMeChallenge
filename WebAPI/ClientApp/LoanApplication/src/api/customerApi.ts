import axios from "axios";
import { CustomerLoanDto } from "../models/customerLoanDto";
import { API_BASE_URL } from './../../config';

const customerApi = {
    getCustomerLoanById: async (customerId: string): Promise<CustomerLoanDto | undefined> => {
        try {
            const response = await axios.get<CustomerLoanDto>(`${API_BASE_URL}/api/customer/loan?customerId=${customerId}`)
            
            return response.data;
        } catch (error) {
            console.log(error);
            throw error;
        }
    }
}

export default customerApi;