# Training Management System Project Description

The Training Management System (TMS) is a comprehensive platform designed to streamline and enhance the administration and delivery of training programs. It caters to educational institutions and corporate training environments, providing a range of features to manage classes, track student progress, analyze performance, and handle administrative tasks efficiently.

## Key Features

### Class Management

- **Creation**: Facilitates the easy creation of classes, allowing trainers or administrators to add details such as class name, description, and training plan.
- **Progress Tracking**: Enables tracking of class progress and provides analytics on class performance. Trainers or administrators can view student progress through dashboards and reports.

### Student Management

- **Progress Tracking**: Tracks student progress through each training plan and displays it on dashboards and reports. Automated notifications can be sent when a training plan is completed or if a student is falling behind.
- **Performance Analytics**: Provides detailed analytics on student performance, including individual metrics and comparative metrics against the class average. Performance data can be exported for further analysis.
- **Profile Customization**: Allows customization of student profiles by adding custom fields and managing personal information.
- **Communication (Optional)**: Facilitates notifications to students about upcoming classes, training plans, and deadlines. It also supports messaging between students and trainers/administrators.

### Class/Student Management

- **Enrollment Management**: Simplifies the enrollment process for students into classes and supports retrieving lists of enrolled students.
- **Absence Reporting**: Manages class attendance, tracks absences, and provides summaries. Automated notifications can be sent for student absences.
- **Absence Approval**: Enables students to report absences in advance, providing reasons and expected return dates. Trainers or administrators can approve or reject absences and mark attendance.

### Score Management

- **Tracking & Recording**: Tracks student scores, provides analytics on performance, and allows trainers or administrators to view and update score records. Supports various types of scores such as assignments, homework, tests, and bonuses.
- **Calculation & Report**: Calculates final scores based on weighted averages, supports different weighting methods, and generates score reports with filtering and sorting options.

### Trainer Management

- **Registration**: Allows trainers to register by providing basic information and authenticates their identity and qualifications.
- **Assignment**: Restricts trainer access to resources based on their qualifications or areas of expertise.
- **Evaluation**: Evaluates trainer performance based on student feedback and assessment results, allowing administrators to provide feedback or coaching.

## Additional Features

- **CQRS**: The project follows the CQRS (Command Query Responsibility Segregation) pattern to ensure clean and maintainable code, separating the logic for reading data from the logic for writing data.
- **Email Notifications**: Sends automated emails for various actions such as absences and enrollment approvals.
- **File Uploads**: Supports uploading and downloading training materials and assignments.
- **Scheduled Jobs**: Uses Quartz for scheduled tasks like sending notifications or generating reports.

## Implementation and Setup

- The database schema can be managed with Entity Framework Core migrations.
- Key features are implemented with endpoints for CRUD operations, validations, and error handling.
- Comprehensive testing ensures the reliability and robustness of the system.

## How to run

1. Clone the repository
2. Build the project
3. Migration the database
   1. `dotnet ef database update`
4. Run the project `dotnet run`
5. (Optional) Execute the seeding api `api/seedData`

## Summary

The Training Management System is a robust solution designed to facilitate efficient training program management. By automating administrative tasks, providing detailed analytics, and ensuring effective communication, the TMS enhances the overall training experience for students, trainers, and administrators.
