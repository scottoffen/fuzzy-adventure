# RabbitMQ Tutorial

Start by running the official [RabbitMQ Docker container](https://registry.hub.docker.com/_/rabbitmq/) locally.

One of the important things to note about RabbitMQ is that it stores data based on what it calls the "Node Name", which defaults to the hostname. What this means for usage in Docker is that we should specify `-h`/`--hostname` explicitly for each daemon so that we don't get a random hostname and can keep track of our data:

```cmd
$ docker run -d --hostname my-rabbit --name some-rabbit -p 15672:15672 -p 5672:5672 rabbitmq:3-management
```

This will start a RabbitMQ container listening on the default port of 5672, and the management console will be available at http://localhost:15672/#/ using a username/password of `guest`.