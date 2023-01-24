import { Typography, Box, useTheme } from "@mui/material";
import { tokens } from "../theme";
import LineChart from "./LineChart";

const LineChartBox = ({ adatok, id, title }) => {
  const theme = useTheme();
  const colors = tokens(theme.palette.mode);
  return (
    <Box
          gridColumn="span 12"
          gridRow="span 2"
          backgroundColor={colors.primary[400]}
        >
          <Box
            mt="10px"
            p="0 30px"
            display="flex "
            justifyContent="space-between"
            alignItems="center"
          >
            <Box>
              <Typography
                variant="h3"
                fontWeight="bold"
                color={colors.greenAccent[500]}
              >
                {title}
              </Typography>
            </Box>
            
          </Box>
          <Box height="300px" m="-10px 0 0 0">
            <LineChart isDashboard={true} adatok={adatok} id={id}/>
          </Box>
        </Box>
  );
};

export default LineChartBox;
