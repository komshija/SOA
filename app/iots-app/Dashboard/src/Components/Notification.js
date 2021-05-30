import { React , useEffect, useState}from 'react'
import { HubConnectionBuilder } from '@microsoft/signalr';
import { signalrcommandurl } from '../urls.js';
import Alert from '@material-ui/lab/Alert';
import Snackbar from '@material-ui/core/Snackbar';
import { useSnackbar } from 'notistack';

const Notification = (props) => {
    const [displayNotification, setDisplayNotification] = useState(true);
    const [notificationMessage,setNotificationMessage] = useState("test");
    const { enqueueSnackbar, closeSnackbar } = useSnackbar();

    useEffect(() => {
        const connection = new HubConnectionBuilder()
        .withUrl(signalrcommandurl)
        .withAutomaticReconnect()
        .build();

        connection.start()
        .then(result => {
            console.log('Connected!');

            connection.on('Notification', message => {
                enqueueSnackbar(message, {
                    anchorOrigin: {
                        horizontal: 'right',
                        vertical: 'top'
                    },
                    variant: 'info'    
                });
            });
        })
        .catch(e => console.log('Connection failed: ', e));
  

    }, [])
    


    return (
        <div>

        </div>
    )
}

export default Notification
