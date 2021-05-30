import {
    React,
    useEffect,
    useState,
    useReducer,
    useRef 
} from 'react';
import ReactDOM from "react-dom";
import axios from 'axios';

import  {LineChart,Line,XAxis,YAxis,Tooltip,CartesianGrid,Legend} from 'recharts';
const Display = (props) => {

    const {url,lineName,width,height,displayCount,color} = props;
    const [renderCount,setRenderCount] = useReducer(x => x + 1, 0);
    const [data,setData] = useState([]);

    
    useEffect(() => {
        const fetchData = async () => {
            
            try {
                const response = await axios.get(url);
                setData(response.data.reverse().slice(Math.max(response.data.length - displayCount, 0)));
                setRenderCount();
            }
            catch {
                
            }
        };
        fetchData();

    }, [data])
    
    
    return ( 
        <div>
           
            
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
                    <Line type="monotone" isAnimationActive={false} dataKey="value.value" name={lineName} stroke={color} />
                </LineChart>
                
        </div>
    )
}

export default Display