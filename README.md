# VideoServiceBackEnd
Video Service Back-End web app.

Delevoped with  .Net Core 2.2, Entity Framework Core.

For Authitification and Authorization was used Bearer JWT Token.

Included Docker support with Nginx as reverse-proxy server.

To run app need to use docker-copmose orchestrator and create SSL certificate.

To run docker with docker-compose use 2 commands: docker-compose build, docker-compose up.

Note: Require DB setup. Was used a code-first approach. DB was used without docker and volume setup!
