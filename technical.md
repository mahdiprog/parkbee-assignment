
## Technical questions

Please put answers to the following questions in the [technical.md](technical.md) file before adding it to the final zip file:

1. What architectures or patterns are you using currently or have worked on recently?
> Clean architecture, microservices and n-layered
> Repository, Singleton, Factory and Unit of Work patterns
2. What do you think of them and would you want to implement it again?
> Sure, In case that architecture and patterns are match with the project needs
3. What version control system do you use or prefer?
> Git
4. What is your favorite language feature and can you give a short snippet on how you use it?
> I love all OOP aspets of C# but if you meant recently added featues (that I presume), I think interface default implementation is quite usefull
 > I've used that to create an interface that can handle mapping in AutoMapper properly
 
```c#
public interface IMapFrom<T>
where T : class
{
    protected void Mapping(Profile profile) => profile.CreateMap(typeof(T), GetType());
}
```
5. What future or current technology do you look forward to the most or want to use and why?
> I love Kubernetes becuse it simplifies all the aspects of infrastructure and can be done by developers (in case of simple needs for sure)
> and Elasticsearch as it's architectured properly and have very useful features in stack
6. How would you find a production bug/performance issue? Have you done this before?
> I would use application monitoring systems e.g. Azure AppInsights and Elasticsearch APM. and yes I've used them for a long time and implemented one as well.
7. How would you improve the sample API (bug fixes, security, performance, etc.)?
> Implement a proper authentication and user identity systm