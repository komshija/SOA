import React from 'react';
import CommandButton from './Components/CommandButton.js';
import Display from './Components/Display';
import {no2commandurl,cocommandurl, getcodataurl,getno2dataurl} from './urls.js';
import Box from '@material-ui/core/Box';
import Typography from '@material-ui/core/Typography';
import Notification from './Components/Notification.js';


function App() {
  return (
    <div className="App">
      
      <Box padding={5}>
          <Box display="flex" justifyContent="center" marginBottom={5}>
            <Typography variant='h1'>Air Quality Dashboard</Typography>
          </Box>

          <Box display="flex" flexDirection="row" justifyContent="center" flexWrap="wrap">
            <Box margin={2.5}>
              <Display key="CO" width={1000} height={500} lineName="CO" displayCount={10} url={getcodataurl} color="#FF0000"/>
            </Box>
            <Box margin={2.5}>
              <Display key="NO2" width={1000} height={500} lineName="NO2" displayCount={10} url={getno2dataurl} color="#00FF00"/>
            </Box>
          </Box>

          <Box display="flex" justifyContent="center">
            <Typography variant='h4'>Commands</Typography>
          </Box>

          <Box padding={0.5} display="flex" flexDirection="row" borderRadius={15} flexWrap="wrap" justifyContent="center">
            <CommandButton buttonText="Turn on NO2 alarm" url={no2commandurl.concat("Turn on NO2 Alarm.")} />
            <CommandButton buttonText="Turn on CO alarm" url={cocommandurl.concat("Turn on CO Alarm.")} />
          </Box>

        
          <Notification />
        
      </Box>
    </div>
  );
}

export default App;
