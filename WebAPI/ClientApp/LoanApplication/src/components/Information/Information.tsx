import { Typography } from "@mui/material";
import { InformationParam } from "./informationParam";

export default function Information(props: InformationParam) {
    return (
        <Typography justifyContent="center" variant="body2" color="text.secondary" align="center" {...props.props}>
            {props.info}
        </Typography>
    );
}