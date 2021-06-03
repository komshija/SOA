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

    const {url,lineName,width,height,color} = props;
    const [renderCount,setRenderCount] = useReducer(x => x + 1, 0);
    const [data,setData] = useState([]);
    const [info, setInfo] = useState({
        min:0,
        max:0,
        avg:0
    });

    const getInfo = () => {
        const min = Math.min.apply(Math, data.map(d => d.value));
        const max = Math.max.apply(Math, data.map(d => d.value));
        const avg = data.reduce((sum, d) => sum += d.value, 0) / data.length;
        setInfo({
            min: min,
            max: max,
            avg: avg
        });
        return info;
    };

    
    useEffect(() => {
        const fetchData = async () => {
            try {
                const response = await axios.get(url);
                setData(response.data);
                getInfo();
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

                <Box display="flex" justifyContent="center">
                    <Box margin={1}>
                        <Typography variant="caption" key={renderCount}>
                        Min: {Math.min.apply(Math, data.map(d => d.value))}
                        </Typography>
                    </Box>
                    <Box margin={1}>
                        <Typography variant="caption" key={renderCount}>
                        Max: {Math.max.apply(Math, data.map(d => d.value))}
                        </Typography>
                    </Box>
                    <Box margin={1}>
                        <Typography variant="caption" key={renderCount}>
                        Avg: {data.reduce((sum, d) => sum += d.value, 0) / data.length}
                        </Typography>
                    </Box>
                </Box>
                
        </div>
    )
}

export default Display