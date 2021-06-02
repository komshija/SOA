import React, {useState} from 'react'
import Box from '@material-ui/core/Box';
import TextField from '@material-ui/core/TextField';
import axios from 'axios';
import { useSnackbar } from 'notistack';
import Button from '@material-ui/core/Button';

const SetValue = (props) => {
    const {url, fieldId, labelText, type, helperText, buttonText, successNotifText, failNotifText} = props;
    const [text, setText] = useState(5);
    const { enqueueSnackbar, closeSnackbar } = useSnackbar();

    const sendPost = async () => {
        try {
            // console.log(text);
            const response = await axios.post(url.concat(`/${text}`));
            console.log(response);
            if(response.status == 200) {
                enqueueSnackbar(successNotifText, {
                    anchorOrigin: {
                        horizontal: 'center',
                        vertical: 'bottom'
                    },
                    variant: 'success'    
                });
            }
            else
            {
                throw "error";
            }
        }
        catch {
            enqueueSnackbar(failNotifText, {
                anchorOrigin: {
                    horizontal: 'center',
                    vertical: 'bottom'
                },
                variant: 'error'    
            });
        }
    };


    return (
        <Box margin={2} display="flex">

                <TextField 
                value={text}
                onChange={event => setText(event.target.value)}
                error={RegExp("[^0-9]").test(text) || parseInt(text) <= 0}
                id={fieldId}
                label={labelText}
                variant="outlined"
                type={type}
                />

                <Box margin={1}>
                    <Button variant="contained" color="primary" size="large" onClick={sendPost}>{buttonText}</Button>
                </Box>

        </Box>
    )
}

export default SetValue
