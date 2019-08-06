# dual-host

An example of an Console Application that start an instance of IHostedService together with a web server.

There are two examples:
1. Using Nancy SelfHost as a web server
2. Using Kestrel 

both implementation expose one single route: /health

Note: Nancy can also be used using Kestrel, but than we need to introduce Owin pipelines, which was something that I wanted to avoid since AspNet Core already offers us a pipeline that works great with Kestrel
