const express = require('express');
const cors = require('cors');
const app = express();
const axious = require('axios');
const bodyParser = require('body-parser');

app.use(bodyParser.urlencoded({ extended: false }));
app.use(bodyParser.json())
app.use(cors());

app.post('/', async function (req, res ,next) {
  
    const faasdResponse = await axious.post("http://172.18.193.128:8080/function/qrcode-csharp", req.body.input);    

    res.send(JSON.stringify(faasdResponse.data));
});

var server = app.listen(3000, function () {
   var host = server.address().address
   var port = server.address().port
   
   console.log("Example app listening at http://%s:%s", host, port)
})