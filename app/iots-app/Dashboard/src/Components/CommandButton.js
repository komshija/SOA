import React, { useState, useEffect } from 'react';
import axios from 'axios';
import Button from '@material-ui/core/Button';
import Box from '@material-ui/core/Box';

const CommandButton = (props) => {
    
    const url = props.url;
    const buttonText = props.buttonText;

    const sendPost = async (e) => {
        const response = await axios.post(url);
    };
   
    return (
        <Box margin={2.5}>
            <Button variant="contained" color="primary" size="large" onClick={sendPost}>{buttonText}</Button>
        </Box>
    )
}

export default CommandButton
