import { useSearchParams } from "react-router-dom"
import customerApi from "../../api/customerApi";
import { CustomerLoanDto } from "../../models/customerLoanDto";
import { Box, Button, Grid, TextField, Typography, Container, MenuItem, Stack, Slider, Paper, FormControl, FormHelperText } from "@mui/material";
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
        value: 1,
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

export default function QuoteCalculator() {
    const [searchParams] = useSearchParams();
    const id = searchParams.get('customerId')
    const [products, setProducts] = useState<ProductDto[]>([])

    // Get values from api
    const { control, handleSubmit } = useForm({
        defaultValues: async () => await customerApi.getCustomerLoanById(id!).then((customerLoanData) => {
            console.log(customerLoanData)
            return customerLoanData!
        })
    })

    const onSubmit: SubmitHandler<CustomerLoanDto> = (data) => {
        console.log(data)
        console.log(new Date(data.dateOfBirth))
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
                        <Grid container spacing={2}>
                            <Grid item xs={12}>
                                <Grid item xs={5} sm={2}>
                                    <Controller
                                        name="product"
                                        control={control}
                                        render={({ field: { onChange, onBlur, value } }) => 
                                            <FormControl fullWidth>
                                                <TextField
                                                    id="product"
                                                    select
                                                    label="Product"
                                                    onChange={(newValue) => {
                                                        console.log(newValue)
                                                        onChange(newValue ? newValue : null)
                                                    }}
                                                    onBlur={onBlur}
                                                    value={value ? products.find((product) => { return value === product.id }) ?? "" : ""}
                                                >
                                                    {products.map((option) => (
                                                        <MenuItem key={option.id} value={option.id}>
                                                            {option.name}
                                                        </MenuItem>
                                                    ))}
                                                </TextField>
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
                                    name="term"
                                    control={control}
                                    render={({ field: { onChange, onBlur, value } }) => 
                                        <FormControl fullWidth>
                                            <Typography component="p">
                                                Term
                                            </Typography>
                                            <Slider
                                                name="term"
                                                aria-label="Term"
                                                defaultValue={0}
                                                onChange={onChange}
                                                onBlur={onBlur}
                                                value={value ?? 0}
                                                getAriaValueText={valueText}
                                                step={6}
                                                valueLabelDisplay="on"
                                                marks={terms}
                                                min={1}
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
                                            control={control}
                                            render={({ field: { onChange, onBlur, value } }) => 
                                                <FormControl fullWidth>
                                                    <TextField
                                                        id="title"
                                                        select
                                                        label="Title"
                                                        defaultValue="Mr."
                                                        onChange={onChange}
                                                        onBlur={onBlur}
                                                        value={value ?? "Mr."}
                                                    >
                                                        {title.map((option) => (
                                                            <MenuItem key={option.value} value={option.value}>
                                                                {option.label}
                                                            </MenuItem>
                                                        ))}
                                                    </TextField>
                                                </FormControl>
                                            }
                                        />
                                    </Grid>
                                    <Grid item xs={7} sm={5}>
                                        <Controller
                                            name="firstName"
                                            control={control}
                                            render={({ field: { onChange, onBlur, value }, fieldState: { error } }) => 
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
                                                    />
                                                    {error?.message ? <FormHelperText>error?.message</FormHelperText> : null }
                                                </FormControl>
                                            }
                                        />
                                    </Grid>
                                    <Grid item xs={12} sm={5}>
                                        <Controller
                                            name="lastName"
                                            control={control}
                                            render={({ field: { onChange, onBlur, value } }) => 
                                                <FormControl fullWidth>
                                                    <TextField
                                                        required
                                                        fullWidth
                                                        id="lastName"
                                                        label="Last name"
                                                        name="lastName"
                                                        onChange={onChange}
                                                        onBlur={onBlur}
                                                        value={value ?? ""}
                                                    />
                                                </FormControl>
                                            }
                                        />
                                    </Grid>
                                </Grid>
                            </Grid>
                            <Grid item xs={12}>
                                <Controller
                                    name="email"
                                    control={control}
                                    render={({ field: { onChange, onBlur, value } }) => 
                                        <FormControl fullWidth>
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
                                            />
                                        </FormControl>
                                    }
                                />
                            </Grid>
                            <Grid item xs={12}>
                                <Grid container spacing={2} justifyContent="space-between">
                                    <Grid item xs={12} sm={7}>
                                        <Controller
                                            name="mobileNumber"
                                            control={control}
                                            render={({ field: { onChange, onBlur, value } }) => 
                                                <FormControl fullWidth>
                                                    <TextField
                                                        required
                                                        fullWidth
                                                        id="mobileNumber"
                                                        label="Mobile number"
                                                        name="mobileNumber"
                                                        onChange={onChange}
                                                        onBlur={onBlur}
                                                        value={value ?? ""}
                                                    />
                                                </FormControl>
                                            }
                                        />
                                    </Grid>
                                    <Grid item>
                                        <Controller
                                            name="dateOfBirth"
                                            control={control}
                                            render={({ field: { onChange, value } }) => 
                                                <FormControl fullWidth>
                                                    <DatePicker 
                                                        label="Date of Birth"
                                                        name="dateOfBirth"
                                                        onChange={onChange}
                                                        value={dayjs(value) ?? ""}
                                                    />
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