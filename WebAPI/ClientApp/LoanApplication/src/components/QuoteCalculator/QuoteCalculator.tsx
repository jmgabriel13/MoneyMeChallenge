import { useNavigate, useSearchParams } from "react-router-dom"
import customerApi from "../../api/customerApi";
import { CustomerLoanDto } from "../../models/customerLoanDto";
import { Box, Button, Grid, TextField, Typography, Container, MenuItem, Stack, Slider, Paper, FormControl, FormHelperText, Autocomplete } from "@mui/material";
import Information from "../Information/Information";
import { DatePicker } from "@mui/x-date-pickers";
import { Controller, SubmitHandler, useForm } from "react-hook-form";
import dayjs from 'dayjs';
import { useEffect, useState } from "react";
import productApi from "../../api/productApi";
import { ProductDto } from "../../models/productDto";
import { monthsToYears } from "../../utility/monthsToYears";
import { amounts } from "./amounts";
import { terms } from "./terms";

const title = [
    {
      label: 'Mr.',
      value: 'Mr.',
    },
    {
      label: 'Ms.',
      value: 'Ms.',
    },
];

function InitializeCustomerLoanDto(): CustomerLoanDto {
    return {
        title: 'Mr.',
        firstName: '',
        lastName: '',
        dateOfBirth: new Date(),
        mobile: '',
        email: '',
        term: 0,
        termInMonths: 6,
        amountRequired: 5000,
        product: ''
    }
}

export default function QuoteCalculator() {
    const navigate = useNavigate();
    const [searchParams] = useSearchParams();
    const customerId = searchParams.get('customerId')
    const [disabled, setDisabled] = useState(false)
    const [products, setProducts] = useState<ProductDto[]>([])
    const [product, setProduct] = useState<ProductDto | null>({
        id: '',
        name: '',
        perAnnumInterestRate: 0,
        minimumDuration: 0,
        monthsOfFreeInterest: 0,
        establishmentFee: 0
    })

    // Get values from api
    const { control, handleSubmit } = useForm({
        defaultValues: async () => 
                customerId ? await customerApi.getCustomerLoanById(customerId!).then((customerLoanData) => {return customerLoanData!}) : InitializeCustomerLoanDto()
    })

    const onSubmit: SubmitHandler<CustomerLoanDto> = (data) => {
        if (customerId) {
            navigate(`/quote?customerId=${customerId}&productId=${data.product}&term=${data.termInMonths}&amountRequired=${data.amountRequired}`)
        } else {
            const request = {
                title: data.title!,
                firstName: data.firstName!,
                lastName: data.lastName!,
                dateOfBirth: new Date(data.dateOfBirth!)!,
                mobile: data.mobile!,
                email: data.email!,
                term: monthsToYears(data.termInMonths!).toString(),
                amountRequired: data.amountRequired.toString()
            }

            // If no id appended in parameter, just register the customer 
            // then append the response to URL then navigate.
            customerApi.getCustomerLoanRate(request).then((response) => {
                // to be updated
                navigate(response.value.replace("https://localhost:7089/", ""))
            });
        }
    }

    function valueText(value: number) {
        return `${value}`;
    }

    useEffect(() => {
        customerId ? setDisabled(true) : setDisabled(false)
        productApi.getAllProducts().then((productData) => setProducts(productData!))
    }, [customerId])

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
                <Paper elevation={3} sx={{ width: "90%", padding: "60px" }} >
                    <Typography variant="h4" fontWeight="bold">
                        Quote calculator
                    </Typography>
                    <Box component="form" noValidate autoComplete="off" onSubmit={handleSubmit(onSubmit)} sx={{ mt: 3, flexGrow: 1 }}>
                        <Grid container spacing={3}>
                            <Grid item xs={12} display="flex" justifyContent="center">
                                <Grid item xs={6}>
                                    <Controller
                                        name="product"
                                        rules={{
                                            required: "Please choose products"
                                        }}
                                        control={control}
                                        render={({ field: { onChange, onBlur, value, ref }, fieldState: { error } }) => 
                                            <FormControl fullWidth error={!!error}>
                                                <Autocomplete
                                                    id="product"
                                                    options={products}
                                                    value={value ? products.find((product) => {return value === product.id}) ?? null : null}
                                                    getOptionLabel={(product) => {
                                                        return product.name
                                                    }}
                                                    onChange={(event: unknown, newValue) => {
                                                        console.log(event)
                                                        setProduct(newValue)
                                                        onChange(newValue ? newValue.id : null)
                                                    }}
                                                    onBlur={onBlur}
                                                    renderInput={(params) => (
                                                        <TextField 
                                                            {...params}
                                                            label="Products"
                                                            inputRef={ref}
                                                        />
                                                    )}
                                                />
                                                {error?.message ? <FormHelperText>{error?.message}</FormHelperText> : null }
                                            </FormControl>
                                        }
                                    />
                                </Grid>
                            </Grid>
                            <Grid item xs={12}>
                                <Controller
                                    name="amountRequired"
                                    control={control}
                                    render={({ field: { onChange, onBlur, value } }) => 
                                        <FormControl fullWidth>
                                            <Typography component="p">
                                                Amount
                                            </Typography>
                                            <Slider
                                                name="amountRequired"
                                                aria-label="Amount"
                                                defaultValue={5000}
                                                onChange={onChange}
                                                onBlur={onBlur}
                                                value={value ?? 5000}
                                                getAriaValueText={valueText}
                                                step={100}
                                                valueLabelDisplay="on"
                                                marks={amounts}
                                                min={2100}
                                                max={15000}
                                            />
                                        </FormControl>
                                    }
                                />
                            </Grid>
                            <Grid item xs={12}>
                                <Controller
                                    name="termInMonths"
                                    control={control}
                                    render={({ field: { onChange, onBlur, value } }) => 
                                        <FormControl fullWidth>
                                            <Typography component="p">
                                                Term
                                            </Typography>
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
                                                min={product?.minimumDuration === 1 ? 0 : product?.minimumDuration}
                                                max={36}
                                            />
                                        </FormControl>
                                    }
                                />
                            </Grid>
                            <Grid item xs={12}>
                                <Grid container spacing={2}>
                                    <Grid item xs={5} sm={2}>
                                        <Controller
                                            name="title"
                                            rules={{
                                                required: "Title is required"
                                            }}
                                            control={control}
                                            render={({ field: { onChange, onBlur, value, ref }, fieldState: { error } }) => 
                                                <FormControl fullWidth error={!!error}>
                                                    <TextField
                                                        id="title"
                                                        select
                                                        label="Title"
                                                        defaultValue="Mr."
                                                        onChange={onChange}
                                                        onBlur={onBlur}
                                                        value={value ?? "Mr."}
                                                        inputRef={ref}
                                                        disabled={disabled}
                                                    >
                                                        {title.map((option) => (
                                                            <MenuItem key={option.value} value={option.value}>
                                                                {option.label}
                                                            </MenuItem>
                                                        ))}
                                                    </TextField>
                                                    {error?.message ? <FormHelperText>{error?.message}</FormHelperText> : null }
                                                </FormControl>
                                            }
                                        />
                                    </Grid>
                                    <Grid item xs={7} sm={5}>
                                        <Controller
                                            name="firstName"
                                            rules={{
                                                required: "First Name is required"
                                            }}
                                            control={control}
                                            render={({ field: { onChange, onBlur, value, ref }, fieldState: { error } }) => 
                                                <FormControl fullWidth error={!!error}>
                                                    <TextField
                                                        name="firstName"
                                                        required
                                                        fullWidth
                                                        id="firstName"
                                                        label="First name"
                                                        autoFocus
                                                        onChange={onChange}
                                                        onBlur={onBlur}
                                                        value={value ?? ""}
                                                        inputRef={ref}
                                                        disabled={disabled}
                                                    />
                                                    {error?.message ? <FormHelperText>{error?.message}</FormHelperText> : null }
                                                </FormControl>
                                            }
                                        />
                                    </Grid>
                                    <Grid item xs={12} sm={5}>
                                        <Controller
                                            name="lastName"
                                            rules={{
                                                required: "Last Name is required"
                                            }}
                                            control={control}
                                            render={({ field: { onChange, onBlur, value, ref }, fieldState: { error } }) => 
                                                <FormControl fullWidth error={!!error}>
                                                    <TextField
                                                        required
                                                        fullWidth
                                                        id="lastName"
                                                        label="Last name"
                                                        name="lastName"
                                                        onChange={onChange}
                                                        onBlur={onBlur}
                                                        value={value ?? ""}
                                                        inputRef={ref}
                                                        disabled={disabled}
                                                    />
                                                    {error?.message ? <FormHelperText>{error?.message}</FormHelperText> : null }
                                                </FormControl>
                                            }
                                        />
                                    </Grid>
                                </Grid>
                            </Grid>
                            <Grid item xs={12}>
                                <Controller
                                    name="email"
                                    rules={{
                                        required: "Email is required"
                                    }}
                                    control={control}
                                    render={({ field: { onChange, onBlur, value, ref }, fieldState: { error } }) => 
                                        <FormControl fullWidth error={!!error}>
                                            <TextField
                                                required
                                                fullWidth
                                                id="email"
                                                label="Your email"
                                                name="email"
                                                autoComplete="email"
                                                onChange={onChange}
                                                onBlur={onBlur}
                                                value={value ?? ""}
                                                inputRef={ref}
                                                disabled={disabled}
                                            />
                                            {error?.message ? <FormHelperText>{error?.message}</FormHelperText> : null }
                                        </FormControl>
                                    }
                                />
                            </Grid>
                            <Grid item xs={12}>
                                <Grid container spacing={2} justifyContent="space-between">
                                    <Grid item xs={12} sm={7}>
                                        <Controller
                                            name="mobile"
                                            rules={{
                                                required: "Mobile Number is required"
                                            }}
                                            control={control}
                                            render={({ field: { onChange, onBlur, value, ref }, fieldState: { error } }) => 
                                                <FormControl fullWidth error={!!error}>
                                                    <TextField
                                                        required
                                                        fullWidth
                                                        id="mobile"
                                                        label="Mobile number"
                                                        name="mobile"
                                                        onChange={onChange}
                                                        onBlur={onBlur}
                                                        value={value ?? ""}
                                                        inputRef={ref}
                                                        disabled={disabled}
                                                    />
                                                    {error?.message ? <FormHelperText>{error?.message}</FormHelperText> : null }
                                                </FormControl>
                                            }
                                        />
                                    </Grid>
                                    <Grid item>
                                        <Controller
                                            name="dateOfBirth"
                                            rules={{
                                                required: "Date of Birth is required"
                                            }}
                                            control={control}
                                            render={({ field: { onChange, value, ref }, fieldState: { error } }) => 
                                                <FormControl fullWidth error={!!error}>
                                                    <DatePicker 
                                                        label="Date of Birth"
                                                        name="dateOfBirth"
                                                        onChange={onChange}
                                                        value={dayjs(value) ?? ""}
                                                        inputRef={ref}
                                                        disabled={disabled}
                                                    />
                                                    {error?.message ? <FormHelperText>{error?.message}</FormHelperText> : null }
                                                </FormControl>
                                            }
                                        />
                                    </Grid>
                                </Grid>
                            </Grid>
                            <Grid item xs={12}>
                                <Stack display="flex" justifyContent="center" alignItems="center" >
                                    <Box width="70%" >
                                        <Button
                                            type="submit"
                                            fullWidth
                                            variant="contained"
                                            sx={{ mt: 3, mb: 2, color: "white", padding: "15px" }}
                                        >
                                            {customerId ? "Calculate quote" : "Register"}
                                        </Button>
                                    </Box>
                                </Stack>
                            </Grid>
                        </Grid>
                    </Box>
                    <Information props={{ mt: 5 }} info={'Quote does not affect your credit score'} />
                </Paper>
            </Box>
            
        </Container>
    )
}