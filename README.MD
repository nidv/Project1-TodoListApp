📄 Project-1 Todo List

1. Overview

This project is a command-line Todo List application.
Its purpose is to let a user organize their todo tasks by creating, updating and saving them through a text-based interface.

The application supports loading, editing and persisting a task list so that a user can continue their work in later sessions.


2. Functional Features
2.1 Task Model

The application models a task with the following attributes:

Title - a short text label for the task
Due Date - a date specifying when the task is due
Status - whether a task is done or not done
Project - a text label representing the project/category the task belongs to


2.2 Task Collection Display

The application:

Display all tasks in a collection, sorted by either due date or by project, based on user preference


2.3 Task Manipulation

The application supports:

Adding a new task
Editing an existing task
Marking a task as done
Removing a task


2.4 User Interface (Command Line)

The application:

Use a text-based user interface via the command line
Wait for explicit user commands


2.5 Save and Load Functionality

The application:

Save the full task list to a file when the user chooses to quit
Load the previously saved task list when the application starts



2.6 Optional Creative Features

The application may include additional optional features if the developer chooses, but they must not change the required behavior listed above.
Examples of creative-but-allowed enhancements:

Colored command-line output


3. Non-Functional Constraints

All operations occur in a single-user environment.
No network connectivity is required.
The application must run in a simple terminal environment.
Data persistence must be on a local file only.


4. Installation & Running the Project

Follow these steps to set up and run the Todo List application:

1. Clone or download the repository
2. Ensure required runtime is installed
3. Compile the project (if applicable)
4. Run the application

The application will launch and automatically load any previously saved tasks.