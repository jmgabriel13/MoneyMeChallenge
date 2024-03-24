export interface CustomerLoanDto {
    title: string,
    firstName: string,
    lastName: string,
    dateOfBirth: Date,
    mobileNumber: string,
    email: string,
    term: number,
    termInMonths: number,
    amountRequired: number,
    product: string
}

export interface CustomerLoanRate {
    title: string,
    firstName: string,
    lastName: string,
    dateOfBirth: Date,
    mobileNumber: string,
    email: string,
    term: string,
    amountRequired: string
}