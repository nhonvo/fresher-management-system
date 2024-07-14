import "./App.css";
// import { BrowserRouter as Router,Route, Switch } from "react-router-dom";
//  ----------------------v5

import { BrowserRouter as Router, Route, Routes } from "react-router-dom"; //---v6
import Landing from "./components/layout/Landing";
import Auth from "./views/Auth";
import AuthContextProvider from "./context/AuthContext";
import Dashboard from "./views/Dashboard";
import ProtectedRoute from "./components/routing/ProtectedRoute";
import About from "./views/About";
import PostContextProvider from "./context/PostContext";
import Syllabus from "./views/Syllabus/Syllabus";
import SyllabusContextProvider from "./context/SyllabusContext";
import SyllabusVew from "./views/Syllabus/SyllabusVew";
import TrainingProgramView from "./views/TrainningProgram/TrainingProgramView";
import TrainingProgram from "./views/TrainningProgram/TrainingProgram";
import TrainingProgramProvider from "./context/TrainingProgramContext";
import ViewTrainingProgramDetail from "./components/trainingProgram/ViewTrainingProgramDetail";
import ViewSyllabusDetail from "./components/syllabus/ViewSyllabusDetail";
import TrainingClassProvider from "./context/TrainingClassContext";
import TrainingClassView from "./views/TrainingClass/TrainingClassView";
import TrainingClass from "./views/TrainingClass/TrainingClass";
import ViewTrainingClassDetail from "./components/trainingClass/ViewTrainingClassDetail";
import DownloadTrainingProgramTestFile from "./components/trainingMaterial/DownloadTrainingProgramTestFile";
import UserView from "./views/User/UserView";
import UserProfileView from "./views/User/UserProfileView";
import AttendanceView from "./views/Attendance/AttendanceView";
import UserProvider from "./context/UserContext";

function App() {
  return (
    <AuthContextProvider>
      <PostContextProvider>
        <SyllabusContextProvider>
          <TrainingProgramProvider>
            <TrainingClassProvider>
              <UserProvider>
                <Router>
                  <Routes>
                    <Route path="/" element={<Landing />} />
                    <Route path="/login" element={<Auth />} />
                    <Route path="/Register" element={<Auth />} />

                    <Route element={<ProtectedRoute />}>
                      <Route path="/Dashboard" element={<Dashboard />} />
                      <Route path="/About" element={<About />} />
                      <Route path="/Syllabus" element={<Syllabus />} />

                      <Route path="/ViewSyllabus" element={<SyllabusVew />} />
                      <Route path="/ViewSyllabus/:id" element={<ViewSyllabusDetail />} />

                      <Route path="/TrainingProgram" element={<TrainingProgramView />} />
                      <Route path="/TrainingProgram/:id" animate={true} element={<ViewTrainingProgramDetail />} />
                      <Route path="/CreateTrainingProgram" element={<TrainingProgram />} />

                      <Route path="/TrainingClass" element={<TrainingClassView />} />
                      <Route path="/TrainingClass/:id" animate={true} element={<ViewTrainingClassDetail />} />
                      <Route path="/CreateTrainingClass" element={<TrainingClass />} />
                      
                      <Route path="/User" element={<UserView />} />
                      <Route path="/User/Profile" element={<UserProfileView />} />

                      <Route path="/TrainingMaterial/:id" element={<DownloadTrainingProgramTestFile />} />

                      <Route path="/Attendance" element={<AttendanceView />} />
                    </Route>
                    <Route>

                    </Route>
                  </Routes>
                </Router>
              </UserProvider>
            </TrainingClassProvider>
          </TrainingProgramProvider>
        </SyllabusContextProvider>
      </PostContextProvider>
    </AuthContextProvider>
  );
}

export default App;
