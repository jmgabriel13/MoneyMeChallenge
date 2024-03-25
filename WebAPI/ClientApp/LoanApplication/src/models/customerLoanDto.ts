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

export interface CustomerLoanResponse {
    value: CustomerLoanDto,
    isSuccess: boolean,
    isFailure: boolean,
    error: {
        code: string,
        message: string
    }
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