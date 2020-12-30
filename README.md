# SQ-TeamClack-TermProj
WPF Application that uses C# .NET and MySQL and is used as an ordering system that allows 3 levels of users to manage various orders within a database. The users may also experience a more modern and colourful UI/UX that was created using Materialize CSS.

![alt text](https://github.com/SemiDoge/SQ-TeamClack-TermProj/blob/main/Project%20Documents/TMSProject.png) 
## Getting Started
These instructions will get you a copy of the project up and running on your local machine for development and testing purposes.
### WPF Development & MySQL
This application requires [Visual Studio 2019](https://visualstudio.microsoft.com/downloads/) to build, test, and deploy. Ensure that you are installing the appropriate .NET desktop development package during installation. You must also have [MySQL](https://www.mysql.com/downloads/) in order to access and use the MySQL databases.
### Using the application
To use this application, clone the repository with
```bash
git clone https://github.com/SemiDoge/SQ-TeamClack-TermProj.git
```
You must also create a database called "omnicorp" and populate it accordingly using the provided SQL script.

Once you are finished installing everything and have populated the database, go into the project and open app.config and ensure the "AdminAcc" credentials match up with your all access user.

Now you can run the program through visual studio IDE and test out the program.
