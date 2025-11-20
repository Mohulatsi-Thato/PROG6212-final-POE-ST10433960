Claims Management System
📖 Overview
Github link: https://github.com/Mohulatsi-Thato/PROG6212-final-POE-ST10433960.git
The Contract Monthly Claims Management System is a web application built with ASP.NET Core MVC.
It helps lecturers submit their monthly claims online and makes it easier for Programme Coordinators, Academic Managers, and HR to review, approve, and process those claims.

The system replaces the old manual, paper-based process with a faster and more transparent digital workflow.
***********************************
Main Features:

Lecturer Features

Submit claims online using a simple form.

Enter hours worked and hourly rate.

Automatic calculation of total payment.

Upload supporting documents (PDF, DOCX, XLS).

Track claim status: Pending, Verified, Approved, Rejected.



Coordinator & Manager Features

View all pending lecturer claims.

Check details and supporting documents.

Approve or reject claims with one click.

Status updates happen instantly.



HR Features

Access all approved claims.

Generate payment reports or simple invoices.

Manage lecturer data (basic profile info).

Export payment summaries.
******************************************
Technologies Used:

ASP.NET Core MVC

Entity Framework Core

SQL Server / Azure SQL

ASP.NET Identity (User authentication & roles)

JavaScript + jQuery (client-side validation & automation)

Bootstrap (UI styling)

GitHub (version control)
*****************************************************
Database Structure
Main Tables

Lecturers : lecturer information

Claims : claim entries (hours, rate, total, status)

Documents : file uploads linked to claims

AspNetUsers / AspNetRoles : Identity tables for access control

Key Relationships

One Lecturer : Many Claims

One Claim : Many Supporting Documents
*****************************************************
Unit Testing

The following areas are covered in unit tests:

Claim submission validation

Document upload validation

Claim status updates

Approval workflow logic

Error handling and exception cases
*******************************************************
Version Control

This project includes multiple Git commits showing:

UI design

Database setup

Controller development

Document upload functionality

Approval workflow

Final touches and bug fixes
*************************************
Updating uplaod function