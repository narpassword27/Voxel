var express = require('express');
var edge = require('edge-js');
var app = express();
var fs = require("fs");
var bodyParser = require('body-parser');

app.get('/', (req, res) => res.send('Hello World!'));

app.get('/test', (req, res) => {
	var helloWorld = edge.func(function () {/*
    async (input) => { 
        return ".NET Welcomes " + input.ToString(); 
    }
	*/});
	
	helloWorld(req.query['param1'], function (error, result) {
		if (error) throw error;
		//console.log(result);
		res.send(result);
	});	
});

app.get('/test2', (req, res) => {
	var helloWorld = edge.func({
		assemblyFile: 'nodeTest.dll',
		typeName: 'nodeTest.Class1',
		methodName: 'HelloWorld' // This must be Func<object,Task<object>>
	});
	
	helloWorld(req.query['param1'], function (error, result) {
		if (error) throw error;
		//console.log(result);
		res.send(result);
	});	
});



app.use(bodyParser.json());
app.use(bodyParser.urlencoded({ extended: false }));


app.post('/test3', function(req, res) {
	console.log(req.body);
    var user_id = req.body.id;
    var token = req.body.token;
	
	var jsonresult = {
	status : 'ok',
	message : user_id + ' ' + token
    };
    res.send(jsonresult);
	
	
	
});







// start stuff here
var server = app.listen(8081, function () {

  var host = server.address().address;
  var port = server.address().port;

  console.log("Example app listening at http://%s:%s", host, port);

});