# Insurance Management - Web Application in ASP.NET Core

A web application for managing insured persons and their insurance policies, created as part of my studies in the Web Application Programmer C# .NET retraining course.

## 🖼️ Application Preview

<p align="center">
 <img width="50%" alt="Screenshot_2" src="https://github.com/user-attachments/assets/55d3995e-8a44-417a-9784-719eafc21481" />
<img width="45%" alt="Screenshot_3" src="https://github.com/user-attachments/assets/39a6fa03-b986-4eed-a4b8-38ce2c7842ba" />
<img width="30.5%" alt="Screenshot_7" src="https://github.com/user-attachments/assets/d009d378-516c-4df0-966f-ceb90f2b48ba" />
<img width="30%" alt="Screenshot_9"  src="https://github.com/user-attachments/assets/084c5443-cf10-43f8-8999-af01b31092a8" />
</p>

## About the Project

The goal of the project was to create a fully functional information system for an insurance broker. The application allows managing a database of clients and their insurance policies. Emphasis was placed on clean code, responsive design, and the implementation of user roles for data security.

The database is created and populated with data **automatically** upon the first run of the application.

### Key Features

- **Complete client management** (CRUD - Create, Read, Update, Delete).
- **Complete insurance policy management** (CRUD) with a direct link to a specific client.
- **User registration and login.**
- **User role system** (Administrator vs. Regular User).
  - **Administrator** has full access to all client and policy data. Can manage everything.
  - **Regular user** can only see their own details and policies after logging in.
- **Responsive design** for display on desktop and mobile devices.

### Technologies Used

- **Backend:** C#, ASP.NET Core MVC, Entity Framework Core
- **Frontend:** HTML, CSS, Bootstrap
- **Database:** MS-SQL (via SQL Server LocalDB)
- **Authentication & Authorization:** ASP.NET Core Identity
- **Other Tools:** AutoMapper, Git, Visual Studio

## 🚀 Running the Project

1. Clone the repository: `git clone https://github.com/KarinVac/PojisteniWebApp.git`
2. Open the project in Visual Studio.
3. Run the application by pressing **F5**.
4. On the first run, the database will be created and populated with the demo data below.

## 🔐 Test Accounts

To test the application's functionality, you can use the following pre-created accounts:

| Role | Email | Password | Description |
| :--- | :--- | :--- | :--- |
| **Administrator** | `admin@admin.com` | `Heslo.123` | Sees everything, can edit and delete all data. |
| **User** | `harry@potter.cz` | `Password.123` | Sees only their own profile and policies. |
| **User** | `ron@weasley.cz` | `Password.123` | Sees only their own profile and policies. |
| **User** | `hermiona@granger.cz`| `Password.123` | Sees only their own profile and policies. |

Of course, you can also create your own user by registering, who will automatically be assigned the "User" role.
