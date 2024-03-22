import axios from "axios";
import { ProductDto } from "../models/productDto";
import { API_BASE_URL } from "../../config";

const productApi = {
    getAllProducts: async (): Promise<ProductDto[] | undefined> => {
        try {
            const response = await axios.get<ProductDto[]>(`${API_BASE_URL}/api/product`)
            
            return response.data;
        } catch (error) {
            console.log(error);
            throw error;
        }
    }
}

export default productApi;