import { Box, Button, useTheme } from "@mui/material";
import { tokens } from "../../theme";
import Header from "../../components/Header";
import { useEffect, useState } from "react";
import axios from "axios";
import LineChartBox from "../../components/LineChartBox";

const SpeedDashboard = () => {
  const theme = useTheme();
  const colors = tokens(theme.palette.mode);
  const [adatok, setAdatok] = useState([]);
  const [motorHutes, setMotorHutes] = useState(false);
  const [akkuHutes, setAkkuHutes] = useState(false);

  
    
    useEffect(() =>
    {
      
        axios.get("http://localhost:8080/api/values")
        .then(result =>
        {
          let length=result.data.length;
          let converted=[
            {
              id:"AkkuFeszultseg",
              color: tokens("light").grey[600],
              data:[]
            },
            {
              id:"AkkuToltoaram",
              color: tokens("dark").blueAccent[300],
              data:[]
            },
            {
              id:"AkkuHomerseklet",
              color: tokens("dark").greenAccent[500],
              data:[]
            },
            {
              id:"MotorHomerseklet",
              color: tokens("dark").redAccent[600],
              data:[]
            },
            {
              id:"MotorAram",
              color: tokens("dark").blueAccent[100],
              data:[]
            },
            {
              id:"AkkuHutes",
              color: tokens("dark").greenAccent[400],
              data:[]
            },
            {
              id:"MotorHutes",
              color: tokens("dark").redAccent[400],
              data:[]
            },
            {
              id:"RelativSebesseg",
              color: tokens("dark").grey[400],
              data:[]
            },
            {
              id:"AbszolutSebesseg",
              color: tokens("dark").grey[400],
              data:[]
            },
            {
              id:"Fenyerosseg",
              color: tokens("dark").grey[400],
              data:[]
            },
            {
              id:"Base",
              color: tokens("dark").grey[400],
              data:[]
            },
          ];
          for(let i=0;i<length;i++)
          {
            converted[0].data.push({
              x:length-i,
              y: result.data[i]["AkkuFeszultseg"],
            });
            converted[1].data.push({
              x:length-i,
              y: result.data[i]["AkkuToltoaram"],
            });
            converted[2].data.push({
              x:length-i,
              y: result.data[i]["AkkuHomerseklet"],
            });
            converted[3].data.push({
              x: length-i,
              y: result.data[i]["MotorHomerseklet"],
            });
            converted[4].data.push({
              x: length-i,
              y: result.data[i]["MotorAram"],
            });
            converted[5].data.push({
              x: length-i,
              y: result.data[i]["AkkuHutes"]?9:1,
            });
            converted[6].data.push({
              x: length-i,
              y: result.data[i]["MotorHutes"]?10:0,
            });
            converted[7].data.push({
              x: length-i,
              y: result.data[i]["RelativSebesseg"],
            });
            converted[8].data.push({
              x: length-i,
              y: result.data[i]["AbszolutSebesseg"],
            });
            converted[9].data.push({
              x: length-i,
              y: result.data[i]["Fenyerosseg"],
            });
            converted[10].data.push({
              x: length-i,
              y: 0,
            });
          }
          setAdatok( converted)
          setMotorHutes(result.data[length-1]["MotorHutes"]);
          setAkkuHutes(result.data[length-1]["AkkuHutes"]);
        })
        .catch(error =>
        {
          console.log(error)
        });
        
    });

  const charts=[
    // {title:"Engine Current",      id:[4]},
    // {title:"Temperature",         id:[2,3]},
    // {title:"Cooling",             id:[5,6,7]},
    // {title:"Engine Temperature",  id:[3]},
    {title:"Relative Speed",      id:[7]},
    {title:"Absolute Speed",      id:[8]},
    {title:"Light",               id:[9]},
  ]

  return (
    <Box m="20px">
      {/* HEADER */}
      <Box display="flex" justifyContent="space-between" alignItems="center">
        <Header title="Speed / Light" subtitle="Welcome to the Speed/Light Dashboard" />

        <Box>
          {/* <Button
            sx={{
              backgroundColor: colors.blueAccent[700],
              color: colors.grey[100],
              fontSize: "14px",
              fontWeight: "bold",
              padding: "10px 20px",
            }}
            onClick={()=>{
              axios.put("http://localhost:8080/api/values",{"Tipus":"motor"})
            }}
          >
            {motorHutes?"Turn Engine Cooling Off":"Turn Engine Cooling On"}
          </Button>
             <span>&nbsp;&nbsp;&nbsp;</span> */}
          <Button
            sx={{
              backgroundColor: colors.blueAccent[700],
              color: colors.grey[100],
              fontSize: "14px",
              fontWeight: "bold",
              padding: "10px 20px",
            }}
            onClick={()=>{
              axios.put("http://localhost:8080/api/values",{"Tipus":"akku"})
            }}
          >
            {akkuHutes?"Turn Battery Cooling Off":"Turn Battery Cooling On"}
          </Button>
        </Box>
      </Box>

      {/* GRID & CHARTS */}
      <Box
        display="grid"
        gridTemplateColumns="repeat(12, 1fr)"
        gridAutoRows="140px"
        gap="20px"
      >
        {charts.map(c=>
          <LineChartBox
            adatok={adatok}
            id={c.id}
            title={c.title}
          />
        )}
        
      </Box>
    </Box>
  );
};

export default SpeedDashboard;
