import React, {useState,useEffect,useReducer} from 'react'

import MenuItem from '@material-ui/core/MenuItem';
import Select from '@material-ui/core/Select';
import InputLabel from '@material-ui/core/InputLabel';
import Box from '@material-ui/core/Box';
import Button from '@material-ui/core/Button';
import TextField from '@material-ui/core/TextField';
import axios from 'axios';
import {DataGrid} from '@material-ui/data-grid';
import Typography from '@material-ui/core/Typography';

const DisplayAdvanced = (props) => {
   
    const [displayType, setDisplayType] = useState(0);
    const [value, setValue] = useState(0);
    const [type, setType] = useState(0);
    const [data,setData] = useState([]);
    const [displayTable, setDisplayTable]= useState(false);

    const handleDisplayTypeChange = (event) => {
        setDisplayType(event.target.value);
    };

    const handleTypeChange = (event) => {
        setType(event.target.value);
    };

    const handleValueChange = (event) => {
        setValue(event.target.value);
    };

    const getType = (type) => {
        switch (type) {
            case 0:
                return "co";
            case 1:
                return "no2";
            default:
                break;
        }
    };

    const constructUrl = () => {
        let url = "/api/";
        console.log(displayType);
        if(displayType === 0) {
            url = url.concat("get/");
            url = url.concat(getType(type));
            return url;
        }
        else if(displayType === 1)
            url = url.concat("less/");
        else{
            url = url.concat("greater/");

        }
        url = url.concat(getType(type));
        url = url.concat("/");
        url = url.concat(value);
        return url;
    };

    const fetchData = async () => {
        console.log(constructUrl());
        try {
            const response = await axios.get(constructUrl());
            const tempData = [];
            let i = 0;
            response.data.map(value => {
                tempData.push({...value, id: i++});
            });
            setData(tempData);
            setDisplayTable(true);
        }
        catch {
            setDisplayTable(false);
        }
    };

    const columns = [
        { field: 'date', headerName: 'Date', flex: 1},
        { field: 'value', headerName: 'Value', flex: 1 },
    ];


    return (
        <Box>

            <Box display="flex" justifyContent="center" flexWrap="wrap" marginBottom={1}>
                <Typography variant='h5'>Table display</Typography>
            </Box>
            
           
            <Box display="flex" flexDirection="column" width={800}>
                {
                    displayTable ? 
                    (<Button onClick={() => setDisplayTable(false)} width="100%" color="secondary">Close</Button>) 
                :(
                    <Box display="flex" flexDirection="column" width={800}>
                        <InputLabel id="graterlesslabel">Select display type</InputLabel>
                        <Select
                        labelId="graterlesslabel"
                        id="graterlessselect"
                        value={type}
                        onChange={handleTypeChange}
                        >
                            <MenuItem value={0} selected={true}>CO</MenuItem>
                            <MenuItem value={1}>NO2</MenuItem>
                        </Select>

                        <InputLabel id="graterlesslabel">Select display type</InputLabel>
                        <Select
                        labelId="graterlesslabel"
                        id="graterlessselect"
                        value={displayType}
                        onChange={handleDisplayTypeChange}
                        >
                            <MenuItem value={0} selected={true}>All</MenuItem>
                            <MenuItem value={1}>Less</MenuItem>
                            <MenuItem value={2}>Greater</MenuItem>
                        </Select>

                        <TextField 
                        value={value}
                        onChange={handleValueChange}
                        label="Less or Grater then value"
                        type="number"
                        disabled={displayType === 0}
                        />
                        <Button onClick={fetchData}>Display table</Button>
                    </Box>
                )
                }
            </Box>
            <Box>


            {
                displayTable ? 
                (
                <div style={{ height: 400, width: '100%' }}>
                    <DataGrid rows={data} columns={columns} pageSize={5} autoPageSize={true} isRowSelectable={false} />
                    
                </div>
                )
                :
                (<div></div>)
            }

            </Box>

        </Box>
    )
}

export default DisplayAdvanced
