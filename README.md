# Fundoo App - Google Keep Clone

## Table of Contents

- [Introduction](#introduction)
- [Features](#features)
- [System Architecture](#system-architecture)
- [Technologies Used](#technologies-used)
- [Getting Started](#getting-started)
    - [Prerequisites](#prerequisites)
    - [Installation](#installation)
    - [Configuration](#configuration)
- [Usage](#usage)
- [API Documentation](#api-documentation)
- [Contributing](#contributing)
- [License](#license)

## Introduction

Fundoo App is a web application that serves as a clone of Google Keep. It allows users to create, edit, organize, and manage notes. This application is built using .NET Core Web API 3.1 and follows a three-tier architecture to ensure a structured and maintainable codebase.

## Features

- Create, edit, and delete notes.
- Organize notes with labels.
- Archive and unarchive notes.
- Set reminders for notes.
- Collaborate with other users on shared notes.
- Search notes.
- Secure user authentication and authorization.

## System Architecture

The application follows a three-tier architecture, consisting of the following layers:

1. **Presentation Layer**: This layer handles the user interface and interactions. It is implemented using HTML, CSS, and JavaScript. The frontend is built using a modern framework like Angular, React, or Vue.js.

2. **Application Layer**: The application layer contains the business logic and acts as an intermediary between the presentation and data layers. It is implemented using .NET Core Web API 3.1.

3. **Data Layer**: This layer is responsible for interacting with the database. It uses a relational database management system (RDBMS) like SQL Server or MySQL. Entity Framework Core is used for data access.

## Technologies Used

- .NET Core Web API 3.1
- Entity Framework Core
- SQL Server or MySQL
- HTML, CSS, JavaScript (for the frontend)
- Authentication and Authorization (e.g., JWT)
  Msmq,rabbitmq

## Getting Started

### Prerequisites

Before you begin, ensure you have met the following requirements:

- [.NET Core 3.1 SDK](https://dotnet.microsoft.com/download/dotnet/3.1)
- [SQL Server or MySQL](https://www.microsoft.com/en-us/sql-server/sql-server-downloads) (or other RDBMS)
- [Node.js](https://nodejs.org/) and [npm](https://www.npmjs.com/) (for frontend)

### Installation

1. Clone the repository:

   ```bash
   git clone https://github.com/bhoromkar/FundooApplication.git
   ```

2. Navigate to the project folder:

   ```bash
   cd fundoo-app
   ```

3. Install the required dependencies for the backend:

   ```bash
   cd FundooApp.Api
   dotnet restore
   ```

4. Install the required dependencies for the frontend (Angular example):

   ```bash
   cd FundooApplication.Client/angular
   npm install
   ```

### Configuration

1. Configure the database connection in `appsettings.json` in the `FundooApp.Api` project.

2. Set up authentication FundooApplication and authorization (e.g., JWT) according to your needs.

3. Configure the frontend to connect to the backend API by updating API endpoint URLs.

## Usage

1. Build and run the backend API:

   ```bash
   cd FundooApplication.Api
   dotnet build
   dotnet run
   ```

2. Build and run the frontend (Angular example):

   ```bash
   cd FundooApplication.Client/angular
   ng serve
   ```

3. Access the application in your web browser at `http://localhost:4200` (Angular default).

## API Documentation

You can find detailed API documentation in the `/docs` folder in this repository.

## Contributing

If you'd like to contribute to this project, please follow our [contribution guidelines](CONTRIBUTING.md).

## License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.
