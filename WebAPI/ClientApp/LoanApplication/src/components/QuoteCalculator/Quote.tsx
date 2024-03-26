import customerApi from "../../api/customerApi";
import { useNavigate, useSearchParams } from "react-router-dom";
import { Box, Button, Container, Divider, FormControl, FormHelperText, Grid, Paper, Slider, Stack, TextField, Typography, colors } from "@mui/material";
import { Controller, SubmitHandler, useForm } from "react-hook-form";
import { useEffect, useState } from "react";
import { CalculateCustomerQuoteResponse } from "../../models/customerLoanDto";
import Information from "../Information/Information";
import { amounts } from "./amounts";
import { terms } from "./terms";
import { Error } from "../../models/resultResponse";

interface CustomerInfo {
    name: string,
    firstName: string,
    lastName: string,
    mobile: string,
    email: string,
}

interface FinanceDetails {
    principalAmount: number,
    termInMonths: number,
    repayment: number,
}

function InitializeCustomerInfo(data?: CustomerInfo): CustomerInfo {
    return {
        name: data?.name ?? '',
        firstName: data?.firstName ?? '',
        lastName: data?.lastName ?? '',
        mobile: data?.mobile ??  '',
        email: data?.email ?? ''
    }
}

function InitializeFinanceDetails(data?: FinanceDetails): FinanceDetails {
    return {
        principalAmount: data?.principalAmount ?? 0,
        termInMonths: data?.termInMonths ?? 0,
        repayment: data?.repayment ?? 0
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
    const navigate = useNavigate();
    const [searchParams] = useSearchParams();
    const newQueryParameters : URLSearchParams = new URLSearchParams();
    const [currentQueryParameters, setSearchParams] = useSearchParams();
    const customerId = searchParams.get('customerId')
    const productId = searchParams.get('productId')
    const term = searchParams.get('term')
    const amountRequired = searchParams.get('amountRequired')
    const [infoEdit, setInfoEdit] = useState(false)
    const [financeDetailsEdit, setFinanceDetailsEdit] = useState(false)
    const [quoteState, setQuoteState] = useState<CalculateCustomerQuoteResponse>({
        firstName: "",
        lastName: "",
        mobile: "",
        email: "",
        principalAmount: 0,
        termInMonths: 0,
        repayment: 0,
        repaymentWithoutInterest: 0,
        repaymentFrequency: "",
        perAnnumInterestRate: 0,
        monthlyInterestRate: 0,
        totalRepayments: 0,
        establishmentFee: 0,
        totalInterest: 0,
        monthsOfFreeInterest: 0
    })
    const [error, setError] = useState<Error>({
        type: "",
        title: "",
        status: 0,
        detail: "",
        errors: {},
        isSuccess: false
    })

    // not best approach, to be refactor
    async function calculateQuote(customerId: string, productId: string, term: string, amountRequired: string) {
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
                    firstName: response.value.firstName,
                    lastName: response.value.lastName,
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

                return InitializeFinanceDetails({
                    principalAmount: response.value.principalAmount,
                    termInMonths: response.value.termInMonths,
                    repayment: response.value.repayment
                })
            } else {
                InitializeFinanceDetails()
            }
        }
    })

    const onSubmitCustomerInfo: SubmitHandler<Partial<CustomerInfo>> = (data) => {

        console.log(data)

        if (customerId && productId && term && amountRequired) {
            // Save updated data 
            customerApi.updateCustomerInfo(customerId, {
                firstName: data.firstName!,
                lastName: data.lastName!,
                mobile: data.mobile!,
                email: data.email!,
            }).then(() => {
                // Then call updates from api
                calculateQuote(customerId, productId, term, amountRequired)
                window.location.reload()
            })
        }
    }

    const onSubmitFinancialDetails: SubmitHandler<Partial<FinanceDetails>> = (data) => {

        console.log(data)

        if (customerId && productId && term && amountRequired) {
            
            // Save updated data 
            customerApi.updateFinanceDetails(customerId, {
                amountRequired: data.principalAmount!,
                termInMonths: data.termInMonths!
            }).then(() => {
                calculateQuote(customerId, productId, term, amountRequired).then(() => {
                    console.log("financedetails")
                    newQueryParameters.set("customerId", customerId)
                    newQueryParameters.set("productId", productId)
                    newQueryParameters.set("term", (data.termInMonths!).toString() ?? quoteState.principalAmount)
                    newQueryParameters.set("amountRequired", (data.principalAmount!).toString() ?? quoteState.principalAmount)
                    setSearchParams(newQueryParameters)
                    console.log(currentQueryParameters.get(""))
                    window.location.reload()
                })
            })
        }
    }

    const handleLoanApplication = async() => {
        await customerApi.customerLoanApplication({
            customerId: customerId!,
            repaymentFrequency: quoteState.repaymentFrequency,
            repayment: quoteState.repayment,
            totalRepayments: quoteState.totalRepayments,
            interestRate: quoteState.monthlyInterestRate,
            interest: quoteState.totalInterest
        }).then((res) => {
            console.log(res)
            if (res.status == 200 || res.isSuccess) {
                navigate("/success")
            } else {
                setError(res)
            }
        })
    }

    function valueText(value: number) {
        return `${value}`;
    }

    useEffect(() => {
        if (customerId && productId && term && amountRequired) {
            calculateQuote(customerId, productId, term, amountRequired)
        }
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
                                        type={infoEdit ? "button" : "submit"}
                                        onClick={() => infoEdit ? 
                                                setInfoEdit(false) 
                                                : 
                                                setInfoEdit(true)
                                        }
                                        size="medium"
                                        sx={{ color: "#08d1cf", fontWeight: "bold" }}
                                    >
                                        {infoEdit ? "Save" : "Edit"}
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
                                {infoEdit ? (
                                    <Grid container display="flex" alignItems="center">
                                        <Grid item xs={7} sm={6} md={6}>
                                            <Controller
                                                name="firstName"
                                                rules={{
                                                    required: "First Name is required"
                                                }}
                                                control={customerInforControl}
                                                render={({ field: { onChange, onBlur, value, ref }, fieldState: { error } }) => 
                                                    <FormControl fullWidth error={!!error}>
                                                        <TextField
                                                            id="firstName"
                                                            label="First Name"
                                                            onChange={onChange}
                                                            onBlur={onBlur}
                                                            value={value ?? ""}
                                                            inputRef={ref}
                                                        />
                                                        {error?.message ? <FormHelperText>{error?.message}</FormHelperText> : null }
                                                    </FormControl>
                                                }
                                            />
                                        </Grid>
                                        <Grid item xs={7} sm={6} md={6} display="flex" justifyContent="flex-end">
                                            <Controller
                                                name="lastName"
                                                rules={{
                                                    required: "Last Name is required"
                                                }}
                                                control={customerInforControl}
                                                render={({ field: { onChange, onBlur, value, ref }, fieldState: { error } }) => 
                                                    <FormControl fullWidth error={!!error}>
                                                        <TextField
                                                            id="lastName"
                                                            label="Last Name"
                                                            onChange={onChange}
                                                            onBlur={onBlur}
                                                            value={value ?? ""}
                                                            inputRef={ref}
                                                        />
                                                        {error?.message ? <FormHelperText>{error?.message}</FormHelperText> : null }
                                                    </FormControl>
                                                }
                                            />
                                        </Grid>
                                    </Grid>
                                    ) : (
                                    <Grid item xs={7} sm={6} md={6}>
                                        <Controller
                                            name="name"
                                            rules={{
                                                required: "Name is required"
                                            }}
                                            control={customerInforControl}
                                            render={({ field: { value }, fieldState: { error } }) => 
                                                <FormControl fullWidth error={!!error}>
                                                    <Typography
                                                        id="name"
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
                                                    {error?.message ? <FormHelperText>{error?.message}</FormHelperText> : null }
                                                </FormControl>
                                            }
                                        />
                                    </Grid>
                                    )
                                }
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
                                        type={financeDetailsEdit ? "button" : "submit"}
                                        onClick={() => financeDetailsEdit ? 
                                                setFinanceDetailsEdit(false) 
                                                : 
                                                setFinanceDetailsEdit(true)
                                        }
                                        size="medium"
                                        sx={{ color: "#08d1cf", fontWeight: "bold" }}
                                    >
                                        {financeDetailsEdit ? "Save" : "Edit"}
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
                                        render={({ field: { onChange, onBlur, value }, fieldState: { error } }) => 
                                            <FormControl fullWidth error={!!error}>
                                                {financeDetailsEdit ?
                                                    <Slider
                                                        name="principalAmount"
                                                        aria-label="Amount"
                                                        defaultValue={5000}
                                                        onChange={onChange}
                                                        onBlur={onBlur}
                                                        value={value ?? 5000}
                                                        getAriaValueText={valueText}
                                                        step={100}
                                                        valueLabelDisplay="auto"
                                                        marks={amounts}
                                                        min={2100}
                                                        max={15000}
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
                            </Grid>

                            <Grid container display="flex" alignItems="center">
                                {financeDetailsEdit ? (
                                    <>
                                        <Grid item xs={5} sm={6} md={6} display="flex">
                                            <Typography
                                                variant='caption'
                                                sx={{
                                                    color: colors.grey[500],
                                                    fontSize: "1.1em"
                                                }}
                                            >
                                                Term
                                            </Typography>
                                        </Grid>
                                        <Grid item xs={7} sm={6} md={6}>
                                            <Controller
                                                name="termInMonths"
                                                rules={{
                                                    required: "Term is required"
                                                }}
                                                control={financialDetailsControl}
                                                render={({ field: { onChange, onBlur, value }, fieldState: { error } }) => 
                                                    <FormControl fullWidth error={!!error}>
                                                        <Slider
                                                            name="termInMonths"
                                                            aria-label="Term"
                                                            defaultValue={0}
                                                            onChange={onChange}
                                                            onBlur={onBlur}
                                                            value={value ?? 6}
                                                            getAriaValueText={valueText}
                                                            step={6}
                                                            valueLabelDisplay="on"
                                                            marks={terms}
                                                            min={quoteState.termInMonths === 36 ? 6 : quoteState.termInMonths}
                                                            max={36}
                                                        />
                                                        {error?.message ? <FormHelperText>{error?.message}</FormHelperText> : null }
                                                    </FormControl>
                                                }
                                            />
                                        </Grid>
                                    </>
                                    ) : <></>
                                }
                                <Box flexGrow={1}>
                                    <Divider 
                                        sx={{ 
                                            color: colors.grey[700],
                                            fontSize: ".8em",
                                            fontWeight: "bold" 
                                        }}
                                        textAlign="right"
                                    >
                                        over {quoteState.termInMonths} months
                                    </Divider>
                                </Box>
                            </Grid>
                            {quoteState.repaymentWithoutInterest > 0 ? 
                                <Grid container display="flex" alignItems="center">
                                    <Grid item xs={5} sm={6} md={6} display="flex">
                                        <Typography
                                            variant='caption'
                                            sx={{
                                                color: colors.grey[500],
                                                fontSize: "1.1em"
                                            }}
                                        >
                                            
                                        </Typography>
                                    </Grid>
                                    <Grid item xs={7} sm={6} md={6}>
                                        <FormControl fullWidth error={!!error}>
                                            <Typography
                                                variant='caption'
                                                sx={{
                                                    color: colors.grey[500],
                                                    fontSize: "1em",
                                                    fontWeight: "bold"
                                                }}
                                                align="right"
                                            >
                                                {roundTo(quoteState.repaymentWithoutInterest, 2)}
                                            </Typography>
                                        </FormControl>
                                    </Grid>
                                    <Box flexGrow={1}>
                                        <Divider 
                                            sx={{ 
                                                color: colors.grey[700],
                                                fontSize: ".8em",
                                                fontWeight: "bold" 
                                            }}
                                            textAlign="right"
                                        >
                                            Repayment from first {quoteState.monthsOfFreeInterest} month(s)
                                        </Divider>
                                    </Box>
                                </Grid>
                                : <></>
                            }
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
                                        render={({ field: { value }}) => 
                                            <FormControl fullWidth error={!!error}>
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
                                            </FormControl>
                                        }
                                    />
                                </Grid>
                                <Box flexGrow={1}>
                                    <Divider 
                                        sx={{ 
                                            color: colors.grey[700],
                                            fontSize: ".8em",
                                            fontWeight: "bold" 
                                        }}
                                        textAlign="right"
                                    >
                                        {quoteState.repaymentFrequency}
                                    </Divider>
                                </Box>
                            </Grid>
                        </Stack>
                    </Box>
                    <Grid item xs={12}>
                        <Stack display="flex" justifyContent="center" alignItems="center" >
                        {error?.detail ? <FormHelperText sx={{ fontSize: "15px", color: colors.red[500], textAlign: "center" }}>{error?.detail}</FormHelperText> : null }
                            <Box width="70%" >
                                <Button
                                    disabled={infoEdit || financeDetailsEdit}
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
                        interest of ${roundTo(quoteState.totalInterest, 2)}. The repayment amount is based on the Product selected, 
                        is subject to our assessment and suitability, and other important terms and conditions apply.`
                        } />
                </Paper>
            </Box>
        </Container>
    )
}