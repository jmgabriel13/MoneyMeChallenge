import customerApi from "../../api/customerApi";
import { useSearchParams } from "react-router-dom";
import { Box, Button, Container, Divider, FormControl, FormHelperText, Grid, Paper, Stack, TextField, Typography, colors } from "@mui/material";
import { Controller, SubmitHandler, useForm } from "react-hook-form";
import { useEffect, useState } from "react";
import { CalculateCustomerQuoteResponse } from "../../models/customerLoanDto";
import { Error } from "../../models/resultResponse";
import { Label, LabelImportant } from "@mui/icons-material";
import Information from "../Information/Information";

interface CustomerInfo {
    name: string,
    mobile: string,
    email: string,
}

interface FinanceDetails {
    principalAmount: number,
    repayment: number,
    repaymentFrequency: string,
}

function InitializeCustomerInfo(data?: CustomerInfo): CustomerInfo {
    return {
        name: data?.name ?? '',
        mobile: data?.mobile ??  '',
        email: data?.email ?? ''
    }
}

function InitializeFinanceDetails(data?: FinanceDetails): FinanceDetails {
    return {
        principalAmount: data?.principalAmount ?? 0,
        repayment: data?.repayment ?? 0,
        repaymentFrequency: data?.repaymentFrequency ?? ''
    }
}

const roundTo = function(num: number, places: number) {
    const factor = 10 ** places;

    // format the number
    const USDollar = new Intl.NumberFormat('en-US', {
        style: 'currency',
        currency: 'USD',
    });

    return USDollar.format(Math.round(num * factor) / factor);
};

export default function Quote() {
    const [searchParams] = useSearchParams();
    const customerId = searchParams.get('customerId')
    const productId = searchParams.get('productId')
    const term = searchParams.get('term')
    const amountRequired = searchParams.get('amountRequired')
    const [infoEdit, setInfoEdit] = useState(false)
    const [financeDetailsEdit, setFinanceDetailsEdit] = useState(false)
    const [quoteState, setQuoteState] = useState<CalculateCustomerQuoteResponse>({})
    const [error, setError] = useState<Error>({
        type: "",
        title: "",
        status: 0,
        detail: "",
        errors: {}
    })

    // Customer Information form
    const { control: customerInforControl,
        handleSubmit: handleSubmitCustomerInfo
    } = useForm({
        defaultValues: async () => { 
            if (customerId && productId && term && amountRequired) {
                const response = await customerApi.calculateQuote({ 
                    customerId: customerId,
                    productId: productId, 
                    termInMonths: parseFloat(term),
                    amountRequired: parseInt(amountRequired)
                }).then((response) => {
                    return response
                });

                return InitializeCustomerInfo({
                    name: `${response.value.firstName} ${response.value.lastName}`,
                    mobile: response.value.mobile,
                    email: response.value.email
                })
            } else {
                InitializeCustomerInfo()
            }
        }
    })

    // Financial Details form
    const { 
        control: financialDetailsControl,
        handleSubmit: handleSubmitFinancialDetails 
    } = useForm({
        defaultValues: 
        async () => {
            if (customerId && productId && term && amountRequired) {
                const response = await customerApi.calculateQuote({ 
                    customerId: customerId,
                    productId: productId, 
                    termInMonths: parseFloat(term),
                    amountRequired: parseInt(amountRequired)
                 }).then((response) => {
                    return response
                });

                return InitializeFinanceDetails({
                    principalAmount: response.value.principalAmount,
                    repayment: response.value.repayment,
                    repaymentFrequency: response.value.repaymentFrequency
                })
            } else {
                InitializeFinanceDetails()
            }
        }
    })

    const onSubmitCustomerInfo: SubmitHandler<Partial<CustomerInfo>> = (data) => {

        console.log(data)
    }

    const onSubmitFinancialDetails: SubmitHandler<Partial<FinanceDetails>> = (data) => {

        console.log(data)
    }

    const handleLoanApplication = async() => {
        console.log("Test")
        await customerApi.customerLoanApplication({
            customerId: customerId!,
            repaymentFrequency: quoteState.repaymentFrequency,
            repayment: quoteState.repayment,
            totalRepayments: quoteState.totalRepayments,
            interestRate: quoteState.monthlyInterestRate,
            interest: quoteState.totalInterest
        }).then((res) => {
            console.log(res)
            if (res.status != 200) {
                setError(res)
            }
        })
    }

    useEffect(() => {
        // not best approach, to be refactor
        async function fetch() {
            if (customerId && productId && term && amountRequired) {
                const response = await customerApi.calculateQuote({ 
                    customerId: customerId,
                    productId: productId, 
                    termInMonths: parseFloat(term),
                    amountRequired: parseInt(amountRequired)
                 }).then((response) => {
                    return response
                });
                setQuoteState(response.value)
            }
        }
        fetch()
    }, [customerId, productId, term, amountRequired])

    return (
        <Container component="main" maxWidth="md">
            <Box
                sx={{
                    marginTop: 8,
                    display: 'flex',
                    flexDirection: 'column',
                    alignItems: 'center',
                    width: "100%"
                }}
                >
                <Paper elevation={3} sx={{ width: "60%", padding: "60px" }} >
                    <Typography variant="h4" fontWeight="bold">
                        Your quote
                    </Typography>
                    <Box key={1} component="form" noValidate autoComplete="off" onSubmit={handleSubmitCustomerInfo(onSubmitCustomerInfo)} sx={{ mt: 3, flexGrow: 1 }}>
                        <Stack spacing={1}>
                            <Grid container display="flex" alignItems="center">
                                <Grid item xs={5} sm={6} md={6} display="flex">
                                    <Typography
										variant='caption'
										sx={{
                                            fontSize: "1.2em",
                                            fontWeight: "bold"
										}}
									>
										Your Information
									</Typography>
                                </Grid>
                                <Grid item xs={7} sm={6} md={6} display="flex" justifyContent="flex-end">
                                    <Button 
                                        onClick={() => infoEdit ? 
                                                setInfoEdit(false) 
                                                : 
                                                setInfoEdit(true)
                                        }
                                        size="medium"
                                        sx={{ color: "#08d1cf", fontWeight: "bold" }}
                                    >
                                        Edit
                                    </Button>
                                </Grid>
                            </Grid>
                            <Grid container display="flex" alignItems="center">
                                <Grid item xs={5} sm={6} md={6} display="flex">
                                    <Typography
										variant='caption'
										sx={{
											color: colors.grey[500],
                                            fontSize: "1em"
										}}
									>
										Name
									</Typography>
                                </Grid>
                                <Grid item xs={7} sm={6} md={6}>
                                    <Controller
                                        name="name"
                                        rules={{
                                            required: "Name is required"
                                        }}
                                        control={customerInforControl}
                                        render={({ field: { onChange, onBlur, value, ref }, fieldState: { error } }) => 
                                            <FormControl fullWidth error={!!error}>
                                                {infoEdit ?
                                                    <TextField
                                                        id="name"
                                                        label="Name"
                                                        onChange={onChange}
                                                        onBlur={onBlur}
                                                        value={value ?? ""}
                                                        inputRef={ref}
                                                    />
                                                    : 
                                                    <Typography
                                                        variant='caption'
                                                        sx={{
                                                            color: colors.grey[500],
                                                            fontSize: "1em",
                                                            fontWeight: "bold"
                                                        }}
                                                        align="right"
                                                    >
                                                        {value}
                                                    </Typography>
                                                }
                                                {error?.message ? <FormHelperText>{error?.message}</FormHelperText> : null }
                                            </FormControl>
                                        }
                                    />
                                </Grid>
                            </Grid>
                            <Grid container display="flex" alignItems="center">
                                <Grid item xs={5} sm={6} md={6} display="flex">
                                    <Typography
                                        variant='caption'
                                        sx={{
                                            color: colors.grey[500],
                                            fontSize: "1em"
                                        }}
                                    >
                                        Mobile
                                    </Typography>
                                </Grid>
                                <Grid item xs={7} sm={6} md={6}>
                                    <Controller
                                        name="mobile"
                                        rules={{
                                            required: "Mobile is required"
                                        }}
                                        control={customerInforControl}
                                        render={({ field: { onChange, onBlur, value, ref }, fieldState: { error } }) => 
                                            <FormControl fullWidth error={!!error}>
                                                {infoEdit ?
                                                    <TextField
                                                        id="mobile"
                                                        label="Mobile"
                                                        onChange={onChange}
                                                        onBlur={onBlur}
                                                        value={value ?? ""}
                                                        inputRef={ref}
                                                    />
                                                    : 
                                                    <Typography
                                                        variant='caption'
                                                        sx={{
                                                            color: colors.grey[500],
                                                            fontSize: "1em",
                                                            fontWeight: "bold"
                                                        }}
                                                        align="right"
                                                    >
                                                        {value}
                                                    </Typography>
                                                }
                                                {error?.message ? <FormHelperText>{error?.message}</FormHelperText> : null }
                                            </FormControl>
                                        }
                                    />
                                </Grid>
                            </Grid>
                            <Grid container display="flex" alignItems="center">
                                <Grid item xs={5} sm={6} md={6} display="flex">
                                    <Typography
                                        variant='caption'
                                        sx={{
                                            color: colors.grey[500],
                                            fontSize: "1em"
                                        }}
                                    >
                                        Email
                                    </Typography>
                                </Grid>
                                <Grid item xs={7} sm={6} md={6}>
                                    <Controller
                                        name="email"
                                        rules={{
                                            required: "Email is required"
                                        }}
                                        control={customerInforControl}
                                        render={({ field: { onChange, onBlur, value, ref }, fieldState: { error } }) => 
                                            <FormControl fullWidth error={!!error}>
                                                {infoEdit ?
                                                    <TextField
                                                        id="email"
                                                        label="Your email"
                                                        onChange={onChange}
                                                        onBlur={onBlur}
                                                        value={value ?? ""}
                                                        inputRef={ref}
                                                    />
                                                    : 
                                                    <Typography
                                                        variant='caption'
                                                        sx={{
                                                            color: colors.grey[500],
                                                            fontSize: "1em",
                                                            fontWeight: "bold"
                                                        }}
                                                        align="right"
                                                    >
                                                        {value}
                                                    </Typography>
                                                }
                                                {error?.message ? <FormHelperText>{error?.message}</FormHelperText> : null }
                                            </FormControl>
                                        }
                                    />
                                </Grid>
                            </Grid>
                        </Stack>
                    </Box>
                    <Box key={2} component="form" noValidate autoComplete="off" onSubmit={handleSubmitFinancialDetails(onSubmitFinancialDetails)} sx={{ mt: 3, flexGrow: 1 }}>
                        <Stack spacing={1}>
                            <Grid container display="flex" alignItems="center">
                                <Grid item xs={5} sm={6} md={6} display="flex">
                                    <Typography
										variant='caption'
										sx={{
                                            fontSize: "1.2em",
                                            fontWeight: "bold"
										}}
									>
										Finance details
									</Typography>
                                </Grid>
                                <Grid item xs={7} sm={6} md={6} display="flex" justifyContent="flex-end">
                                    <Button 
                                        onClick={() => financeDetailsEdit ? 
                                                setFinanceDetailsEdit(false) 
                                                : 
                                                setFinanceDetailsEdit(true)
                                        }
                                        size="medium"
                                        sx={{ color: "#08d1cf", fontWeight: "bold" }}
                                    >
                                        Edit
                                    </Button>
                                </Grid>
                            </Grid>
                            <Grid container display="flex" alignItems="center">
                                <Grid item xs={5} sm={6} md={6} display="flex">
                                    <Typography
                                        variant='caption'
                                        sx={{
                                            color: colors.grey[500],
                                            fontSize: "1.1em"
                                        }}
                                    >
                                        Finance Amount
                                    </Typography>
                                </Grid>
                                <Grid item xs={7} sm={6} md={6}>
                                    <Controller
                                        name="principalAmount"
                                        rules={{
                                            required: "Principal Amount is required"
                                        }}
                                        control={financialDetailsControl}
                                        render={({ field: { onChange, onBlur, value, ref }, fieldState: { error } }) => 
                                            <FormControl fullWidth error={!!error}>
                                                {financeDetailsEdit ?
                                                    <TextField
                                                        id="principalAmount"
                                                        label="Principal Amount"
                                                        onChange={onChange}
                                                        onBlur={onBlur}
                                                        value={value ?? 0}
                                                        inputRef={ref}
                                                    />
                                                    : 
                                                    <Typography
                                                        variant='caption'
                                                        sx={{
                                                            color: colors.grey[500],
                                                            fontSize: "1em",
                                                            fontWeight: "bold"
                                                        }}
                                                        align="right"
                                                    >
                                                        {roundTo(value, 2)}
                                                    </Typography>
                                                }
                                                {error?.message ? <FormHelperText>{error?.message}</FormHelperText> : null }
                                            </FormControl>
                                        }
                                    />
                                </Grid>
                                
                                <Box flexGrow={1}>
                                    <Divider sx={{ borderStyle: "dotted", borderRadius: 5 }} textAlign="right">over {quoteState.termInMonths} months</Divider>
                                </Box>
                            </Grid>
                            <Grid container display="flex" alignItems="center">
                                <Grid item xs={5} sm={6} md={6} display="flex">
                                    <Typography
                                        variant='caption'
                                        sx={{
                                            color: colors.grey[500],
                                            fontSize: "1.1em"
                                        }}
                                    >
                                        Repayments From
                                    </Typography>
                                </Grid>
                                <Grid item xs={7} sm={6} md={6}>
                                    <Controller
                                        name="repayment"
                                        rules={{
                                            required: "Repayment is required"
                                        }}
                                        control={financialDetailsControl}
                                        render={({ field: { onChange, onBlur, value, ref }, fieldState: { error } }) => 
                                            <FormControl fullWidth error={!!error}>
                                                {financeDetailsEdit ?
                                                    <TextField
                                                        id="repayment"
                                                        label="Repayment"
                                                        onChange={onChange}
                                                        onBlur={onBlur}
                                                        value={value ?? 0}
                                                        inputRef={ref}
                                                    />
                                                    : 
                                                    <Typography
                                                        variant='caption'
                                                        sx={{
                                                            color: colors.grey[500],
                                                            fontSize: "1em",
                                                            fontWeight: "bold"
                                                        }}
                                                        align="right"
                                                    >
                                                        {roundTo(value, 2)}
                                                    </Typography>
                                                }
                                                {error?.message ? <FormHelperText>{error?.message}</FormHelperText> : null }
                                            </FormControl>
                                        }
                                    />
                                </Grid>

                                <Box flexGrow={1}>
                                    <Divider sx={{ borderStyle: "dotted", borderRadius: 5 }} textAlign="right">{quoteState.repaymentFrequency}</Divider>
                                </Box>
                            </Grid>
                        </Stack>
                    </Box>
                    <Grid item xs={12}>
                        {error?.detail ? <FormHelperText>{error?.detail}</FormHelperText> : null }
                        <Stack display="flex" justifyContent="center" alignItems="center" >
                            <Box width="70%" >
                                <Button
                                    onClick={handleLoanApplication}
                                    fullWidth
                                    variant="contained"
                                    sx={{ mt: 3, mb: 2, color: "white", padding: "15px" }}
                                >
                                    Apply Now
                                </Button>
                            </Box>
                        </Stack>
                    </Grid>
                    <Information props={{ mt: 5, color: colors.grey[500] }} info={
                        `Total repayments ${roundTo(quoteState.totalRepayments, 2)}, made up of an establishment fee of ${roundTo(quoteState.establishmentFee, 2)},
                        interest of ${roundTo(quoteState.totalInterest, 2)}. The repayment amount is based on the variables selected, 
                        is subject to our assessment and suitability, and other important terms and conditions apply.`
                        } />
                </Paper>
            </Box>
        </Container>
    )
}