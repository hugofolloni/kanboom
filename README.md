# kanboom

Kanboom is an open-source task management system designed to empower individuals and teams to organize their tasks efficiently using the Kanban methodology. It provides a reliable, customizable, and open-source solution for visualizing and managing work progress. Kanboom allows users to create boards, define custom stages, and collaborate seamlessly by inviting team members and assigning tasks.

**Why Kanboom?**

Kanboom aims to provide a straightforward and effective Kanban experience. It emphasizes simplicity and reliability, offering a robust platform for task management without unnecessary complexity. It's built for those who value open-source solutions and want a customizable tool to fit their specific workflow.

**Target Audience:**

* Individuals seeking personal task organization.
* Small to medium-sized teams requiring collaborative task management.
* Developers and project managers looking for a customizable Kanban system.

## Tech Stack

* **Backend:** .NET 8 (C#) - Chosen for its performance, robust ecosystem, and strong community support.
* **Frontend:** React with Redux and Next.js (latest version) - Selected for its component-based architecture, efficient state management, and server-side rendering capabilities.
* **Database:** PostgreSQL- Chosen for reliability, being open-source and familiarity.
* **Authentication:** JWT (JSON Web Tokens) - Implemented for secure and stateless authentication.
* **Containerization:** Docker (planned for local development and deployment)

## Features

* **Customizable Boards:** Create and manage multiple boards with personalized stages.
* **Task Management:** Create, assign, edit, and track tasks across various stages.
* **User Collaboration:** Invite users, assign tasks, and manage permissions within boards.
* **Board Ownership:** Transfer board ownership to other users.
* **Flexible Stages:** Customize stages within boards, including renaming and deleting stages.

* **Invite Links:** Generate invitation links for users to join boards.
* **User Scoped Data:** Users only access data within their assigned scope, ensuring privacy.

## Getting Started

### Prerequisites

* Node.js v18 or later
* .NET 8 SDK
* [**Add your database system here, e.g., PostgreSQL**]
* Git
* Docker (optional, for local development)

### Installation

1.  Clone the repository: `git clone [repository URL]`
2.  Navigate to the frontend directory: `cd frontend`
3.  Install frontend dependencies: `npm install`
4.  Navigate to the backend directory: `cd backend`
5.  Install backend dependencies: `dotnet restore`
6.  Set up your database and create the necessary tables (refer to the database schema).
7.  Configure environment variables (see below).

### Quick Start

1.  Start the database.
2.  Navigate to the backend directory and run: `dotnet run`
3.  Navigate to the frontend directory and run: `npm run dev`

## Architecture Overview

* **User Basic Info Architecture:**

![alt text](assets/userinfo.png)

* **Authentication Flow:** 

![alt text](assets/auth.png)

* **Database Schema:**
    * **Key Relationships:**
        * A Board belongs to a User (the owner).
        * A Board can have many Tasks.
        * A Task belongs to a Stage.
        * A Board has customizable Stages.
        * A Board can have multiple Users.
        * Tasks are assigned to Users.

![alt text](assets/database.png)


* **User Flowchart:**

![alt text](assets/userflowchart.png)

## API Documentation

### Authentication

* **POST /auth/login**
    * Description: Authenticates a user and returns a JWT token.
    * Request Body:
        ```json
        { "username": "string", "password": "string" }
        ```
    * Response Body:
        ```json
        { "token": "string" }
        ```
    * Status Codes:
        * 200 OK: Authentication successful
        * 400 Bad Request: Invalid username or password
        * 500 Internal Server Error: Server error during authentication
    * Authentication: None
* **POST /auth/persist**
    * [**Add detailed documentation for this endpoint.**]

### Boards

* **POST /board**
    * [**Add detailed documentation for this endpoint.**]
* **POST /board/create**
    * [**Add detailed documentation for this endpoint.**]
* **PATCH /board/changeOwner**
    * [**Add detailed documentation for this endpoint.**]
* **POST /board/addStage**
    * [**Add detailed documentation for this endpoint.**]
* **DELETE /board/deleteStage**
    * [**Add detailed documentation for this endpoint.**]
* **POST /board/renameStage**
    * [**Add detailed documentation for this endpoint.**]

### Board Users

* **POST /boardUser/invite**
    * [**Add detailed documentation for this endpoint.**]
* **DELETE /boardUser/leave**
    * [**Add detailed documentation for this endpoint.**]

### Tasks

* **POST /task/create**
    * [**Add detailed documentation for this endpoint.**]
* **PATCH /task/edit**
    * [**Add detailed documentation for this endpoint.**]
* **PATCH /task/changeVisibility**
    * [**Add detailed documentation for this endpoint.**]
* **PATCH /task/changeStage**
    * [**Add detailed documentation for this endpoint.**]
* **PATCH /task/changeAssignee**
    * [**Add detailed documentation for this endpoint.**]

### Users

* **POST /user**
    * [**Add detailed documentation for this endpoint.**]
* **POST /user/create**
    * [**Add detailed documentation for this endpoint.**]

## Security

* **JWT Authentication:** All requests to the API are protected with JWT tokens.
* **Password Hashing:** Passwords are currently hashed using MD5.
* **SQL Injection Protection:** The application uses parameterized queries to prevent SQL injection attacks. .NET automatically handles most SQL injection protection.

## Future Plans (Postponed)

* **[Feature] Group:** A group can own a board, and users can join multiple groups.
* **[Feature] Multiple Admins:** Allow boards to have additional admins beyond the owner.
* **[Feature] Board Abbreviations:** Users can assign custom abbreviations to boards.
* **[Feature] Task Tagging:** Automatically assign task tags based on the board abbreviation and task index.
* **[Architecture] Redis:** Create cache implementation.
* **[Architecture] RabbitMQ:** Send emails using RabbitMQ and some mailing libraty to notify users on task changes.
* **[Architecture] Kubernetes:** Docker + Kubernetes + Tilt implementation.

## Docker Setup

The app can be run locally with Docker using the provided `docker-compose.yml` file. This will set up both the frontend and backend services.

## CI/CD and Logging

CI/CD setup and logging features will be implemented in future versions of the application to automate deployments and ensure robust error tracking.

## Contributing

Kanboom is open-source, and contributions are welcome! Feel free to fork the repository, make changes, and create pull requests. Make sure to follow the coding standards and add tests where applicable.

## License

This project is licensed under the MIT License - see the `LICENSE` file for details.

## Contact

For any questions or suggestions, please feel free to reach out through hugofolloni@gmail.com.