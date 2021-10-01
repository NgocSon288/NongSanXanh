# Introduction

Nông Sản Xanh, đồ án môn Nhập môn Công nghệ Phần mềm - UIT

## Technologies

-  ASP.NET Core 5.0
-  Entity Framework Core 5.0
-  ReactJs

## Nuget

-  Microsoft.EntityFrameworkCore
-  Microsoft.EntityFrameworkCore.Relational
-  Microsoft.EntityFrameworkCore.SqlServer
-  Microsoft.EntityFrameworkCore.Design
-  Microsoft.EntityFrameworkCore.Tools
-  Microsoft.AspNetCore.Identity.EntityFrameworkCore

-  Microsoft.Extensions.Configuration
-  Microsoft.Extensions.Configuration.Json

-  Microsoft.AspNetCore.Http.Features

-  FluentValidation
-  FluentValidation.AspNetCore

-  Microsoft.AspNetCore.Authentication.JwtBearer

-  MailKit

-  Microsoft.Extensions.Options

## Npm

-  axios
-  react-router-dom
-  bootstrap
-  reactstrap

## Install Tools

-  .NET Core SDK 5.0
-  Git client
-  Visual Studio 2019
-  SQL Server 2019
-  Nodejs

## How to configure and run

### Run Server

-  Clone code from Github: git clone git@github.com:NgocSon288/NongSanXanh.git
-  Open solution NongSan.sln in Visual Studio 2019
-  Set startup project is NongSan.Data
-  Change connection string in Appsetting.json in NongSan.Data project
-  Open Tools --> Nuget Package Manager --> Package Manager Console in Visual Studio
-  Run Update-database and Enter.
-  After migrate database successful, set Startup Project is NongSan.Api
-  Change database connection in appsettings.json in NongSan.Api project.
-  Set startup project is NongSan.Api
-  Choose profile to run or press F5

### Run Client

## How to contribute

-  Fork and create your branch
-  Create Pull request to us.
