# FullCalendar Demo App README

This README provides an overview and explanation of the FullCalendar demo app. The app showcases a simple event management system, where users can create, edit, and delete events on a calendar.

## Table of Contents

- [Introduction](#introduction)
- [Prerequisites](#prerequisites)
- [Installation](#installation)
- [Usage](#usage)
- [Features](#features)
- [Technologies Used](#technologies-used)
- [Contributing](#contributing)
- [License](#license)

## Introduction

The FullCalendar demo app is a web-based application that allows users to manage events using the FullCalendar library, jQuery, and ASP.NET Core. The app provides a responsive and intuitive calendar interface where users can create, view, update, and delete events. The events are stored in a database, and the app uses AJAX calls to interact with the server for data manipulation.

## Prerequisites

Before running the FullCalendar demo app, ensure you have the following prerequisites installed on your system:

1. [.NET Core SDK](https://dotnet.microsoft.com/download) - To run the ASP.NET Core application.
2. [Microsoft SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads) - To set up the database for event storage.

## Installation

To set up the FullCalendar demo app on your system, follow these steps:

1. Clone the repository from GitHub:

```
git clone <repository_url>
```

2. Navigate to the `FullCalendarApp` directory:

```
cd FullCalendarApp
```

3. Run the application using the .NET CLI:

```
dotnet run
```

4. Open your web browser and go to `http://localhost:5000` to access the FullCalendar demo app.

## Usage

Once you've launched the app in your web browser, you'll see the calendar interface with a month view by default. You can navigate between months using the navigation buttons on the top-left corner. The header toolbar allows you to switch between day, week, and month views.

### Creating Events

To create a new event, click on a specific date on the calendar. A modal will appear, allowing you to enter the event details, including the event title, start date, end date (if applicable), description, and color. You can also choose to make the event a full-day event by checking the "Full day" checkbox.

### Editing Events

To edit an existing event, click on the event on the calendar. A modal will display the event details. Click the "Edit" button to modify the event properties. You can change the event title, start date, end date, description, and color. You can also update the "Full day" checkbox to change the event's duration.

### Deleting Events

To delete an event, click on the event on the calendar, and then click the "Delete" button in the event details modal. A confirmation message will appear, asking you to confirm the deletion. Click "OK" to proceed with the deletion.

## Features

The FullCalendar demo app offers the following features:

- Create new events on the calendar.
- Edit existing events to update their properties.
- Delete events from the calendar.
- View event details, including title, description, start date, and end date.
- Set event color for visual distinction.
- Toggle between day, week, and month views on the calendar.
- Automatic update of the calendar when events are added, edited, or deleted.

## Technologies Used

The FullCalendar demo app is built using the following technologies:

- ASP.NET Core - A cross-platform, high-performance framework for building web applications.
- Entity Framework Core - An ORM (Object-Relational Mapping) tool for working with databases.
- FullCalendar - A popular JavaScript library for displaying and managing events on a calendar.
- jQuery - A fast and feature-rich JavaScript library for DOM manipulation and AJAX calls.
- Bootstrap - A front-end CSS framework for creating responsive and visually appealing web interfaces.
- Moment.js - A library for parsing, validating, manipulating, and formatting dates and times.

## Contributing

We welcome contributions to improve the FullCalendar demo app. If you have any suggestions, bug reports, or feature requests, please open an issue on the GitHub repository. If you want to contribute code, feel free to create a pull request with your changes.

## License

The FullCalendar demo app is open-source and released under the [MIT License](https://opensource.org/licenses/MIT). You are free to use, modify, and distribute the code for personal and commercial purposes. Attribution to the original authors is appreciated but not required.

---

This README provides an overview of the FullCalendar demo app and instructions on how to set it up and use its features. Feel free to update and customize this README to suit your project's specific requirements. Happy coding!
