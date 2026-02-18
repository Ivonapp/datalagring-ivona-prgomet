import { BrowserRouter, Routes, Route } from 'react-router-dom'
import './App.css'
import Teachers from './pages/Teachers'
import Courses from './pages/Courses'
import NotFoundPage from './pages/NotFoundPage'
import Header from './components/Header'
import Participants from './pages/Participants'
import Enrollments from './pages/Enrollments'
import CourseSessions from './pages/CourseSessions'



function App() {

  return (
    <>
    <BrowserRouter>
    <Header />
    <main>


    <Routes>
      <Route path="/Teachers" element={<Teachers />} />
      <Route path="/Participants" element={<Participants />} />
      <Route path="/Courses" element={<Courses />} />
      <Route path="/Enrollments" element={<Enrollments />} />
      <Route path="/CourseSessions" element={<CourseSessions />} />
      <Route path="*" element={<NotFoundPage />} />
    </Routes>
    </main>

    
    </BrowserRouter>
    </>
  )
}

export default App
