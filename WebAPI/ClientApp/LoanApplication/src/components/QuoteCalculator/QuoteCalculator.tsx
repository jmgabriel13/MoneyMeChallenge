import { useEffect, useState } from "react"
import { useSearchParams } from "react-router-dom"
import customerApi from "../../api/customerApi";
import { CustomerLoanDto } from "../../models/customerLoanDto";

export default function QuoteCalculator() {
    const [searchParams] = useSearchParams();
    const id = searchParams.get('customerId')
    const [customerLoan, setCustomerLoan] = useState<CustomerLoanDto>({
        title: '',
        firstName: '',
        lastName: '',
        dateOfBirth: new Date(),
        mobileNumber: '',
        email: '',
        term: 0,
        amountRequired: 0
    })

    useEffect(() => {
        if (id) {
            customerApi.getCustomerLoanById(id).then(customerLoandata => setCustomerLoan(customerLoandata!));
        }
    }, [id])

    return (
        <>
            <h1>quote calculator</h1>
            <h2>{customerLoan.title}</h2>
        </>
    )
}