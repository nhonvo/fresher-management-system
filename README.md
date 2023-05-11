# Training management system
// binh diem danh
// dat tinh diem
// do phan tich hoc sinh
// nhon nam seed data lay tinh nang

// TODO: check and add validation if not has
// TODO: check again error message

## Class _Nhon_

1. Creation
   1. Allow for easy creation of classes.
   2. Allow trainers or administrators to add details such as class name, description, and training plan.
   // TODO: Add author only role trainer allow use this endpoint.
2. Progress Tracking
   1. Have the ability to track progress and provide analytics on class performance.
   // score feature. getStudent of class sort by ???. ***.
   // include table test and syllabus get "he so" of test and calculate the score. (calculate by 5 fields in syllabus)
   2. Allow trainers or administrators to view the progress of each student in the class through a dashboard or report. (**)
   /// (đang ơ ngay bn, tong bn, list student và diem hien tai)

## Student

1. Progress Tracking
   1. Have the ability to track student progress through each training plan.
   GET /student/:id/progress (id of student) return student and list training plan of student.
  _MR. DO KU TO(assign)_
   // TODO: get date, score, ... of student in class. (include table classUser, score, test, syllabus)
   2. Allow trainers and administrators to view the progress of each student through a dashboard or report.
   3. Have the ability to send automated notifications to students, trainers, or administrators when a training plan is completed or when a student is falling behind.// optional hangfire.
2. Performance Analytics
   1. Provide detailed analytics on student performance, including individual performance metrics and comparative metrics against the class average. _MR. DO KU TO(assign)_
   // {
         index: 1,
         pageSize: 5.
         totalCount: 5,
         "items": [
            {
               "classId": 1,
               "CurrentGPA": 8,
               "AverageGPA": 0,
            }
         ]
      }

   2. Allow for easy exporting of performance data for further analysis.
   // csv
3. Profile Customization _Nhon_ done
   1. Allow for custom fields to be added to student profiles to capture additional information that may be required.
   // add new table profile of student
   2. Allow students to customize their profiles and manage their personal information. _Nhon_
   // edit profile.
4. Communication (optional)
   1. Have the ability to send notifications to students regarding upcoming classes, training plans, and assessment deadlines.
   2. Have the ability to allow students to communicate with trainers or administrators through messaging or chat features.

/// TODO: Add hangfire services
/// student enroll class has admin approve
/// attendance student 

/// TODO: Tracking score

## Class/Student

1. Enrollment Management
   1. Allow for easy enrollment of students into classes.
   // problem how to know what class class students belong ? we don't have relationships between them
   // DONE: endpoint: POST /class/:id/enroll (id of class) body: {studentId: 1} return class and student.
   // post: add student to class. approve by admin. Create new table relation with user and class has fields(
      "classId": 1,
      "studentId": 1,
      "createdAt": "2021-09-30T08:00:00.000Z",
      "approveAt": "2021-09-30T08:00:00.000Z",
      "approveBy": 1,
   )
   2. Support retrieving the list of enrolled students for each class.
   // TODO: endpoint: GET /class/:id/enroll (id of class) return list student of class.
2. Absence Reporting
   1. Have the ability to track and manage class attendance.
         //TODO: endpoint:  get duration of training program for all syllabus.
         // can not find duration property of training program.
   2. Allow trainers or administrators to view the list of reported absences for each student, or view summary on class.
         //DONE: endpoint: get list absence of student in class.
   3. Have the ability to send automated notifications to students, trainers, or administrators when a student is absent from a class (optional).
         //TODO: allow send mails to student, trainer, admin when student absence.
3. Absence Approval
   1. Allow students to report their absences in advance, providing reasons and expected dates of return.
   // DONE: CRUD _DONE_
   2. Allow trainers or administrators to approve or reject reported absences.
   // DONE: endpoint: POST /attendance/:id/approve (id of student) _
   3. Allow trainers or administrators to mark student attendance and view attendance records.
   // DONE: endpoint: POST /attendance/:id/mark (id of student)

## Score

1. Tracking & Recording
   1. Have the ability to track student scores and provide analytics on student performance.
   // TODO: endpoint: GET /score/:id (id of student) return list score of student.
   2. Allow trainers or administrators to view the score records of each student and identify patterns of performance.
   3. Allow trainers or administrators to record and update student scores for each assessment.
   // TODO: endpoint: POST /score/:id (id of student) body: {score: 10, testId: 1} return score of student.
   // TODO: endpoint: PUT /score/:id (id of score) body: {score: 10} return score of student.
   // TODO: endpoint: DELETE /score/:id (id of score) return score of student.
   4. Support different types of scores, such as assignment, homework, final test, bonus.
2. Calculation & Report
   1. Have the ability to calculate final scores for each student based on the weighted _average_ of their scores in each assessment.
   // TODO: endpoint: GET /score/:id (id of student) return score average of student.
   2. Support different weighting methods, such as equal weighting or custom weighting.
   3. Allow trainers or administrators to generate score reports for each class or for individual students.
   4. Support filtering and sorting options for the score reports, such as by assessment type or score range.
   5. CRUD TestAssignment _Nhon_ DONE

## Trainer

1. Registration
   1. Allow trainers to register themselves by providing their basic information such as name, contact information, and credentials.
   // done: endpoint: POST /trainer {id}/ Role: {add role}
   // done: endpoint: POST /trainer body: {name: "Nhon", email: "nhon@gmail", password: "123456"} return trainer.
   2. Have the ability to authenticate and verify the identity and qualifications of the trainer.
   // done: endpoint: POST /trainer/login body: {email: "nhon@gmail", password: "123456"} return token.
   // done: endpoint: GET /trainer/:id (id of trainer) return trainer.
2. Assignment
   1. Have the ability to restrict trainer access to certain resources or assessments based on their qualifications or areas of expertise.
3. Evaluation
   1. Have the ability to evaluate the performance of trainers based on student feedback.****
// TODO: endpoint: POST /student:trainerId/feedback
// create table feedback between student and syllabus.
// TODO: endpoint: GET /student:trainerId/feedback
