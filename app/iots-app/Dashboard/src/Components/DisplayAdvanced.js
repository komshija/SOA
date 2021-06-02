import React, {useState} from 'react'
import Display from './Display.js';
import MenuItem from '@material-ui/core/MenuItem';
import Select from '@material-ui/core/Select';
import InputLabel from '@material-ui/core/InputLabel';
import Box from '@material-ui/core/Box';
import TextField from '@material-ui/core/TextField';

const DisplayAdvanced = (props) => {
    const {url,lineName,width,height,displayCount,color} = props;
    const [displayType, setDisplayType] = useState('less');
    const [value, setValue] = useState(0);

    const handleChange = (event) => {
        setDisplayType(event.target.value);
    };


    return (
        <Box>
            <Display url={url} lineName={lineName} width={width} height={height} displayCount={displayCount} color={color}/>
            <Box display="flex" justifyContent="center" flexDirection="column" flexBasis={width}>

                <InputLabel id="graterlesslabel">Select {lineName} type</InputLabel>
                <Select
                labelId="graterlesslabel"
                id="graterlessselect"
                value={displayType}
                onChange={handleChange}
                >
                <MenuItem value="less" selected={true}>Less</MenuItem>
                <MenuItem value="grater">Grater</MenuItem>
                </Select>

                <TextField 
                value={value}
                onChange={event => setValue(event.target.value)}
                label="Less or Grater then value"
                type="number"
                />

            </Box>
        </Box>
    )
}

export default DisplayAdvanced
