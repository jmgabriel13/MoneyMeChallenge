
export interface ResultResponse<T> {
    value: T;
    isSuccess: boolean;
    isFailure: boolean;
    error: {
        code: string;
        message: string;
    };
}

export interface Error {
    type: string,
    title: string,
    status: number,
    detail: string,
    errors: object,
    isSuccess: boolean
}