import { ResponsiveLine } from "@nivo/line";
import { useTheme } from "@mui/material";
import { tokens } from "../theme";


const LineChart = ({ adatok=[], isCustomLineColors = false, isDashboard = false, id=[0] }) =>
{
  const theme = useTheme();
  const colors = tokens(theme.palette.mode);
  let tempadatok=[];
  if(adatok.length>0)
  {
    id.forEach(i=>tempadatok.push(adatok[i]))
  }
  return (
    <ResponsiveLine
      data={tempadatok}
      theme={{
        axis: {
          domain: {
            line: {
              stroke: colors.grey[100],
            },
          },
          legend: {
            text: {
              fill: colors.grey[100],
            },
          },
          ticks: {
            line: {
              stroke: colors.grey[100],
              strokeWidth: 1,
            },
            text: {
              fill: colors.grey[100],
            },
          },
        },
        legends: {
          text: {
            fill: colors.grey[100],
          },
        },
        tooltip: {
          container: {
            color: colors.primary[500],
          },
        },
      }}
      colors={isDashboard ? { datum: "color" } : { scheme: "nivo" }} // added
      margin={{ top: 50, right: 50, bottom: 50, left: 50 }}
      xScale={{ type: "point" }}
      yScale={{
        type: "linear",
        min: "auto",
        max: "auto",
        stacked: false,
        reverse: false,
      }}
      yFormat=" >-.2f"
      curve="linear"
      axisTop={null}
      axisRight={null}
      axisBottom={{
        orient: "bottom",
        tickValues: 1,
        tickSize: 1,
        tickPadding: 1,
        tickRotation: 90,
        legend: isDashboard ? undefined : "transportation", // added
        legendOffset: 1,
        legendPosition: "middle",
      }}
      axisLeft={{
        orient: "left",
        tickValues: 5, // added
        tickSize: 1,
        tickPadding: 1,
        tickRotation: 0,
        legend: isDashboard ? undefined : "count", // added
        legendOffset: -40,
        legendPosition: "middle",
      }}
      enableGridX={false}
      enableGridY={false}
      pointSize={3}
      pointColor={{ theme: "background" }}
      pointBorderWidth={2}
      pointBorderColor={{ from: "serieColor" }}
      pointLabelYOffset={-12}
      useMesh={true}
    />
  );
};

export default LineChart;
