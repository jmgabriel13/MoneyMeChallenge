import { useSearchParams } from "react-router-dom"
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

const amounts = [
    {
        label: '2,100',
        value: 2100,
    },
    {
        label: '15,000',
        value: 15000,
    },
];

const terms = [
    {
        label: '1mo',
        value: 0,
    },
    {
        label: '6mos',
        value: 6,
    },
    {
        label: '36mos',
        value: 36,
    },
];

function InitializeCustomerLoanDto(): Partial<CustomerLoanDto> {
    return {
        firstName: '',
        lastName: '',
        mobileNumber: '',
        email: ''
    }
}

function monthsToYears(months: number): number {
    if (months < 0) {
        return 1 / 12;
    }

    return months / 12;
}

export default function QuoteCalculator() {
    const [searchParams] = useSearchParams();
    const id = searchParams.get('customerId')
    const [products, setProducts] = useState<ProductDto[]>([])
    const [product, setProduct] = useState<ProductDto | null>({
        id: '',
        name: '',
        perAnnumInterestRate: 0,
        minimumDuration: 0,
        monthsOfFreeInterest: 0
    })

    // Get values from api
    const { control, handleSubmit } = useForm({
        defaultValues: async () => 
                id ? await customerApi.getCustomerLoanById(id!).then((customerLoanData) => {return customerLoanData!}) : InitializeCustomerLoanDto()
    })

    const onSubmit: SubmitHandler<Partial<CustomerLoanDto>> = (data) => {
        console.log(data)

        if (id) {
            // Should call the api that calcualte quote using PMT Function

        } else {
            const request = {
                title: data.title!,
                firstName: data.firstName!,
                lastName: data.lastName!,
                dateOfBirth: new Date(data.dateOfBirth!)!,
                mobileNumber: data.mobileNumber!,
                email: data.email!,
                term: monthsToYears(data.termInMonths!).toString(),
                amountRequired: data.amountRequired!.toString()
            }
    
            console.log(request)
    
            customerApi.getCustomerRate(request).then((response) => console.log(response));
        }
    }

    function valueText(value: number) {
        return `${value}`;
    }

    useEffect(() => {
        productApi.getAllProducts().then((productData) => setProducts(productData!))
    }, [])

    return (
        <Container component="main" maxWidth="md">
            <Box
                sx={{
                    marginTop: 8,
                    display: 'flex',
                    flexDirection: 'column',
                    alignItems: 'center',
                }}
                >
                <Paper elevation={3} sx={{ padding: "60px" }} >
                    <Typography component="h1" variant="h3" fontWeight="bold">
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
                                                        console.log(newValue)
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
                                            name="mobileNumber"
                                            rules={{
                                                required: "Mobile Number is required"
                                            }}
                                            control={control}
                                            render={({ field: { onChange, onBlur, value, ref }, fieldState: { error } }) => 
                                                <FormControl fullWidth error={!!error}>
                                                    <TextField
                                                        required
                                                        fullWidth
                                                        id="mobileNumber"
                                                        label="Mobile number"
                                                        name="mobileNumber"
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
                                            Calculate quote
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