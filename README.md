<kbd>[<img title="Italian" alt="Italian" src="https://upload.wikimedia.org/wikipedia/en/thumb/0/03/Flag_of_Italy.svg/1500px-Flag_of_Italy.svg.png" width="22">](translations/README.ita.md)</kbd>
<kbd>[<img title="Spanish" alt="Spanish" src="https://cdn11.bigcommerce.com/s-hhbbk/images/stencil/1280x1280/products/854/42341/SPAN0001__19783.1580480313.png?c=2" width="22">](translations/README.spa.md)</kbd>

<p align="center">
    <img src="https://img.shields.io/badge/React-20232A?style=for-the-badge&logo=react&logoColor=61DAFB" />
    <img src="https://img.shields.io/badge/TypeScript-007ACC?style=for-the-badge&logo=typescript&logoColor=white" />
    <img src="https://img.shields.io/badge/Vite-B73BFE?style=for-the-badge&logo=vite&logoColor=FFD62E" />
    <img src="https://img.shields.io/badge/Tailwind_CSS-38B2AC?style=for-the-badge&logo=tailwind-css&logoColor=white" />
    <img src="https://img.shields.io/badge/npm-CB3837?style=for-the-badge&logo=npm&logoColor=white" />
    <img src="https://img.shields.io/badge/.NET-512BD4?style=for-the-badge&logo=dotnet&logoColor=white" />
    <img src="https://img.shields.io/badge/NuGet-004880?style=for-the-badge&logo=nuget&logoColor=white" />
    <img src="https://img.shields.io/badge/Docker-2CA5E0?style=for-the-badge&logo=docker&logoColor=white" />
</p>

![screencapture](https://github.com/user-attachments/assets/9627f4e1-9199-4246-a994-ac289fb8e11c)

### Overview

This .NET and React-based application is designed to help users manage and track their expenses and incomes effectively. The goal is to provide a comprehensive solution for organizing financial transactions, generating reports, and offering additional premium features for an enhanced user experience.

## Postman API documentation
<a href="https://documenter.getpostman.com/view/21619259/2s9YsRd9TF#757dd6bd-9a08-40fd-b5f9-7b19dfaf9b81" target="_blank">Click here</a>

## Swagger API documentation
[applicationURL]/swagger

## Default Administrator user

### Credentials:
Email address: admin@gmail.com
<br />
Password: password

## User Interface - UI

1. **Dashboard**
   - Last week summary - Chart
   - Last 5 expense changes + graphical change in relation to the highest expense cost amount
   - Last 5 income changes + graphical change in relation to the highest income cost amount
   - Menu to navigate to expense and income group list pages
   - Customized plans for your financial journey
   - Testimonials section
   - Frequently asked questions(FAQ) section
   - Newsletter section
   - Footer with related links

2. **Income/Expense List Page**
   - Table
     - Id
     - Name(description)
     - Amount
     - Group
     - Edit and delete buttons
   - Checkbox button to select all available incomes/expenses
   - Delete icon button to delete all available incomes/expenses
   - Sorting arrows to sort all available incomes/expenses
   - Button to add income/expense through popup
   - Search input field for searching incomes/expenses
   - Filter dropdown to filter incomes/expenses
   - Available filter options: Min amount, max amount and filter by income/expense group(name)
   - Button to export stats to email
   - Pagination options to paginate incomes/expenses
   - "Rows per page" feature to show certain amount of incomes/expenses

3. **Income/Expense Edit Popup**
   - Editable name field
   - Editable description field
   - Editable amount field
   - Editable income group field
   - Save button
   - Cancel button

4. **Income/Expense Delete Popup**
   - Save button
   - Cancel button

5. **Income/Expense Group List Page**
   - Table
     - Id
     - Name
     - Description
     - Edit and delete buttons
   - Checkbox button to select all available income/expense groups
   - Delete icon button to delete all available income/expense groups
   - Sorting arrows to sort all available income/expense groups
   - Button to add income/expense group through popup
   - Pagination options to paginate income/expense groups
   - "Rows per page" feature to show certain amount of income/expense groups
   - Delete modal with confirmation and cancel option
   - Clickable income/expense group name which goes to details page

6. **Income/Expense Group Edit Popup**
   - Editable name field
   - Editable description field
   - Save button
   - Cancel button

7. **Income/Expense Group Delete Popup**
   - Save button
   - Cancel button

8. **Income/Expense Group Details Page**
   - Name
   - Description
   - Last 5 account changes for that group
   - Breadcrumb for easy navigation

9. **Reminders (Premium Feature inside Profile Settings)**
   - Button to create reminder through a popup
   - Details of currently set reminder on the dashboard
   - Breadcrumb for easy navigation

10. **Reminder Delete Popup**
   - Save button
   - Cancel button

11. **Blogs (Premium Feature)**
   - Button to create blog through a popup
   - Cards with all available blogs
   - Breadcrumb for easy navigation

12. **Blog Delete Popup**
   - Save button
   - Cancel button

13. **Blog Details Page**
   - Title
   - Description
   - Author
   - Created at date
   - Body
   - Breadcrumb for easy navigation

## Additional integrations

### Pipeline status emails
**Backend Pipeline:**
Enable tests in the .NET project.
<br/>
<br/>
**Frontend Pipeline:**
Build the project.

**Email Notification:**

Send pipeline status emails to a predefined list of recipients, focusing only on failed pipelines.

### Mailchimp integration
Automatically register users who sign up for the newsletter to the connected Mailchimp audience.

<hr/>

All rights reserved.
<br/>
Copyright &copy; 2025
