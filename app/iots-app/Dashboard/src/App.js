import React from 'react';
import CommandButton from './Components/CommandButton.js';
import Display from './Components/Display';
import DisplayAdvanced from './Components/DisplayAdvanced';
import {no2commandurl,cocommandurl, getcodataurl,getno2dataurl,cointerval,no2interval,no2treshold} from './urls.js';
import Box from '@material-ui/core/Box';
import Typography from '@material-ui/core/Typography';
import Notification from './Components/Notification.js';
import SetValue from './Components/SetValue.js';


function App() {
  return (
    <div className="App">
      
      <Box padding={5}>
          <Box display="flex" justifyContent="center" flexWrap="wrap" marginBottom={5}>
            <Typography variant='h2'>Air Quality Dashboard</Typography>
          </Box>

          <Box display="flex" flexDirection="row" justifyContent="center" flexWrap="wrap">
            <Box margin={2.5}>
              <Display key="CO" width={600} height={250} lineName="CO" displayCount={10} url={getcodataurl} color="#FF0000"/>
            </Box>
            <Box margin={2.5}>
              <Display key="NO2" width={600} height={250} lineName="NO2" displayCount={10} url={getno2dataurl} color="#00FF00"/>
            </Box>

            <Box margin={2.5}>
              <DisplayAdvanced key="CO" width={600} height={250} lineName="CO" displayCount={10} url={getcodataurl} color="#FF0000"/>
            </Box>
            <Box margin={2.5}>
              <DisplayAdvanced key="NO2" width={600} height={250} lineName="NO2" displayCount={10} url={getno2dataurl} color="#00FF00"/>
            </Box>

          </Box>

          <Box display="flex" justifyContent="center">
            <Typography variant='h4'>Commands</Typography>
          </Box>

          <Box padding={0.5} display="flex" flexDirection="row" borderRadius={15} flexWrap="wrap" justifyContent="center">
            <CommandButton buttonText="Turn on NO2 alarm" url={no2commandurl.concat("/Turn on NO2 Alarm.")} />
            <CommandButton buttonText="Turn on CO alarm" url={cocommandurl.concat("/Turn on CO Alarm.")} />
            <SetValue url={cointerval} buttonText="Set CO interval" fieldId="interval-co" labelText="CO send interval" type="number" helperText="Error!" successNotifText="Succesfuly set CO interval!" failNotifText="Error on setting CO interval!" />
            <SetValue url={no2interval} buttonText="Set NO2 interval" fieldId="interval-no2" labelText="NO2 send interval" type="number" helperText="Error!" successNotifText="Succesfuly set NO2 interval!" failNotifText="Error on setting NO2 interval!" />
            <SetValue url={no2treshold} buttonText="Set NO2 treshold" fieldId="treshold-no2" labelText="NO2 treshold" type="number" helperText="Error!" successNotifText="Succesfuly set NO2 treshold!" failNotifText="Error on setting NO2 treshold!" />
          </Box>

        
          <Notification />
        
      </Box>
    </div>
  );
}

export default App;
