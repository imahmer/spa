Securing SPA and implementing Authorization Code Grant using IdentityServer4 with PKCE

Identity Server 4 (.NET 5)

SPA (Angular 11)

# SPA
This project was generated using [ngX-Rocket](https://github.com/ngx-rocket/generator-ngx-rocket).

Benefits

- Quickstart a project in seconds and focus on features, not on frameworks or tools

- Industrial-grade tools, ready for usage in a continuous integration environment and DevOps

- Scalable architecture with base app template including example components, services and tests

## Code scaffolding
Run `ng generate component component-name` to generate a new component. You can also use `ng generate directive|pipe|service|class|guard|interface|enum|module`.

## Get Started

First check app.setting.json

uncomment below line from Program.cs if you want to run migration first.

//CreateHostBuilder(args).Build().MigrateDatabase().Run();

uncomment below line from startup.cs if you want to seed data first.

//EnsureSeedData(services);


- SPA
```
https://localhost:4200
```

- Identity Server running on
```
https://localhost:5005
```

## References
[Authentication flows and application scenarios] (https://docs.microsoft.com/en-us/azure/active-directory/develop/authentication-flows-app-scenarios)
