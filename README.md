# Training management system

## Class

1. Creation
   1. Allow for easy creation of classes.
   2. Allow trainers or administrators to add details such as class name, description, and training plan.
   // include program table.
   // TODO: Add author only role trainer allow use this endpoint.
2. Progress Tracking
   1. Have the ability to track progress and provide analytics on class performance. MR. DO KU TO(assign)
   // score feature. getStudent of class sort by ???. ***.
   // include table test and syllabus get "he so" of test and caculate the score. (caculate by 5 fields in syllabus)
   2. Allow trainers or administrators to view the progress of each student in the class through a dashboard or report.
   /// optional. (đang ơ ngay bn, tong bn, list student và diem hien tai) 

## Student

1. Progress Tracking
   1. Have the ability to track student progress through each training plan. 
   GET /student/:id/progress (id of student) return student and list training plan of student.
   
   2. Allow trainers and administrators to view the progress of each student through a dashboard or report.
   3. Have the ability to send automated notifications to students, trainers, or administrators when a training plan is completed or when a student is falling behind.// optional hangfire.
2. Performance Analytics
   1. Provide detailed analytics on student performance, including individual performance metrics and comparative metrics against the class average.
   // {
         index: 1,
         pageSize: 5.
         totalCount: 5,
         "items": [
            {
               "classId": 1,
               "CurrentScore": 0,
               "AverageScore": 0,
            }
   ]}

   2. Allow for easy exporting of performance data for further analysis.
   // csv
3. Profile Customization
   1. Allow for custom fields to be added to student profiles to capture additional information that may be required.
   // add new table profile of student
   2. Allow students to customize their profiles and manage their personal information.
   // edit profile.
4. Communication (optional)
   1. Have the ability to send notifications to students regarding upcoming classes, training plans, and assessment deadlines.
   2. Have the ability to allow students to communicate with trainers or administrators through messaging or chat features.

## Class/Student

1. Enrollment Management
   1. Allow for easy enrollment of students into classes.
   // post: add student to class. approve by admin. Create new table relation with user and class has fields(
      "classId": 1,
      "studentId": 1,
      "createdAt": "2021-09-30T08:00:00.000Z",
      "approveAt": "2021-09-30T08:00:00.000Z",
      "approveBy": 1,
   )
   2. Support retrieving the list of enrolled students for each class.
   // get from table ClassUser.
2. Absence Reporting
   1. Have the ability to track and manage class attendance.
   2. Allow trainers or administrators to view the list of reported absences for each student, or view summary on class.
   3. Have the ability to send automated notifications to students, trainers, or administrators when a student is absent from a class (optional).
3. Absence Approval
   1. Allow students to report their absences in advance, providing reasons and expected dates of return.
   2. Allow trainers or administrators to approve or reject reported absences.
   3. Allow trainers or administrators to mark student attendance and view attendance records.

## Score

1. Tracking & Recording
   1. Have the ability to track student scores and provide analytics on student performance.
   2. Allow trainers or administrators to view the score records of each student and identify patterns of performance.
   3. Allow trainers or administrators to record and update student scores for each assessment.
   4. Support different types of scores, such as assignment, homework, final test, bonus.
2. Calculation & Report
   1. Have the ability to calculate final scores for each student based on the weighted average of their scores in each assessment.
   2. Support different weighting methods, such as equal weighting or custom weighting.
   3. Allow trainers or administrators to generate score reports for each class or for individual students.
   4. Support filtering and sorting options for the score reports, such as by assessment type or score range.

## Trainer

1. Registration
   1. Allow trainers to register themselves by providing their basic information such as name, contact information, and credentials.
   2. Have the ability to authenticate and verify the identity and qualifications of the trainer.
2. Assignment
   1. Have the ability to restrict trainer access to certain resources or assessments based on their qualifications or areas of expertise.
3. Evaluation
   1. Have the ability to evaluate the performance of trainers based on student feedback