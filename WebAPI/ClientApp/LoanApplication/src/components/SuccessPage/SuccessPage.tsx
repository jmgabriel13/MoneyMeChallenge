import { Box, Button, Container, Grid, Paper, Stack, Typography } from "@mui/material";
import { useNavigate } from "react-router-dom";

export default function SuccessPage() {
    const navigate = useNavigate();

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
                <Paper elevation={3} sx={{ width: "60%", height: "600px", padding: "60px" }} >
                    <Box
                        sx={{
                            textAlign: 'center',
                            borderRadius: "200px",
                            height: "200px",
                            width: "200px",
                            background: "#F8FAF5",
                            margin: "0 auto"
                        }}
                    >
                        <i className="checkmark">✓</i>
                        <Typography sx={{
                            color: "#88B04B",
                            fontFamily: ["Nunito Sans", "Helvetica Neue", "sans-serif"].join(','),
                            fontWeight: "900",
                            fontSize: "1.5em"
                        }}
                        >
                            Loan Application Success!
                        </Typography>
                    </Box>
                    <Box flexGrow={1} sx={{ marginTop: "150px" }}>
                        <Typography variant='body1' flexGrow={1}>
                            We received your application for a loan, we'll be in touch shortly!
                        </Typography>
                    </Box>

                    <Box flexGrow={1}>
                        <Grid item xs={12}>
                            <Stack display="flex" justifyContent="center" alignItems="center" >
                                <Box width="70%" >
                                    <Button
                                        onClick={() => navigate("/")}
                                        fullWidth
                                        variant="contained"
                                        sx={{ mt: 3, mb: 2, color: "white", padding: "15px" }}
                                    >
                                        Back to home
                                    </Button>
                                </Box>
                            </Stack>
                        </Grid>
                    </Box>
                </Paper>
            </Box>
        </Container>
    )
}