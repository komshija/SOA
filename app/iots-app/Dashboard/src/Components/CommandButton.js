import React from 'react';
import axios from 'axios';
import Button from '@material-ui/core/Button';
import Box from '@material-ui/core/Box';
import { useSnackbar } from 'notistack';

const CommandButton = (props) => {
    
    const url = props.url;
    const buttonText = props.buttonText;
    const { enqueueSnackbar, closeSnackbar } = useSnackbar();


    const sendPost = async () => {
        try {

            const response = await axios.post(url);
            console.log(response);
            if(response.status == 200) {
                enqueueSnackbar("Command activated succesfuly", {
                    anchorOrigin: {
                        horizontal: 'center',
                        vertical: 'bottom'
                    },
                    variant: 'success'    
                });
            }
        }
        catch {
            enqueueSnackbar("Error sending command", {
                anchorOrigin: {
                    horizontal: 'center',
                    vertical: 'bottom'
                },
                variant: 'error'    
            });
        }
    };
   
    return (
        <Box margin={2.5}>
            <Button variant="contained" color="primary" size="large" onClick={sendPost}>{buttonText}</Button>
        </Box>
    )
}

export default CommandButton
