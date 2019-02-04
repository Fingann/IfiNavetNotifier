
This Project contains the Infrastructure, such as Dbcontext.

Most of your application's dependencies on external resources should be implemented in classes defined in the Infrastructure project.
These classes should implement interfaces defined in Core. If you have a very large project with many dependencies,
it may make sense to have multiple Infrastructure projects (e.g. Infrastructure.Data), but for most projects one Infrastructure project with folders works fine.