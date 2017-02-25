##### Using Mad Kristensen's Angular 2 .Net Core Template
https://blogs.msdn.microsoft.com/webdev/2017/02/14/building-single-page-applications-on-asp-net-core-with-javascriptservices/



## To Create Code First Database Migrations
##### Open Tools -> Nuget Package Manager -> Package Manager Console and run the following lines (May need to increment the migration count)
* Add-Migration -Project Angular2CoreBase.Data -Context CoreBaseContext -Name CoreBase000
* Update-Database  -Project Angular2CoreBase.Data