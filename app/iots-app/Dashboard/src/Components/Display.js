import {
    React,
    useEffect,
    useState,
    useReducer
} from 'react';
import axios from 'axios';
import  {LineChart,Line,XAxis,YAxis,Tooltip,CartesianGrid,Legend} from 'recharts';
import Typography from '@material-ui/core/Typography';
import Box from '@material-ui/core/Box';

const Display = (props) => {

    const {url,lineName,width,height,displayCount,color} = props;
    const [renderCount,setRenderCount] = useReducer(x => x + 1, 0);
    const [data,setData] = useState([]);

    
    useEffect(() => {
        const fetchData = async () => {
            
            try {
                const response = await axios.get(url);
                setData(response.data);
                setRenderCount();
            }
            catch {
                
            }
        };
        fetchData(); //initial fetch

        const interval = setInterval(() => fetchData(), 5000);
        return () => {
            clearInterval(interval);
        };

    }, []);
    
    
    return ( 
        <div>
                <Box display="flex" justifyContent="center" flexWrap="wrap" marginBottom={1}>
                    <Typography variant='h5'>{lineName}</Typography>
                </Box>
            
                <LineChart
                width={width}
                height={height}
                data={data}
                key={renderCount}
                >
                    <CartesianGrid strokeDasharray="3 3" />
                    <XAxis dataKey="key" />
                    <YAxis />
                    <Tooltip />
                    <Legend />
                    <Line type="monotone" isAnimationActive={false} dataKey="value" name={lineName} stroke={color} />
                </LineChart>
                
        </div>
    )
}

export default Display