export interface CustomerLoanDto {
    amountRequired: number,
    term: number,
    title: string,
    firstName: string,
    lastName: string,
    dateOfBirth: Date,
    mobile: string,
    email: string,
    termInMonths: number,
    product: string
}

export interface CustomerLoanRate {
    amountRequired: string
    term: string,
    title: string,
    firstName: string,
    lastName: string,
    dateOfBirth: Date,
    mobile: string,
    email: string,
}

export interface CalculateCustomerQuoteRequest {
    customerId: string,
    productId: string,
    termInMonths: number,
    amountRequired: number
}

export interface CalculateCustomerQuoteResponse {
    firstName: string,
    lastName: string,
    mobile: string,
    email: string,
    principalAmount: number,
    termInMonths: number,
    repayment: number,
    repaymentFrequency: string,
    perAnnumInterestRate: number,
    monthlyInterestRate: number,
    totalRepayments: number,
    establishmentFee: number,
    totalInterest: number
}

export interface CustomerLoanApplicationRequest {
    customerId: string,
    repaymentFrequency: string,
    repayment: number,
    totalRepayments: number,
    interestRate: number,
    interest: number
}