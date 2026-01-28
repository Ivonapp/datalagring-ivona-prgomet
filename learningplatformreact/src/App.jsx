
import { BrowserRouter, Routes, Route } from 'react-router-dom'
import './App.css'
import Home from './pages/Home'
import Teachers from './pages/Teachers'
import ApplyForCourses from './pages/ApplyForCourses'
import NotFoundPage from './pages/NotFoundPage'
import Header from './components/Header'


function App() {

  return (
    <>
    <BrowserRouter>
    <Header />
    <main>


    <Routes>
      <Route path="/Home" element={<Home />} />
      <Route path="/Teachers" element={<Teachers />} />
      <Route path="/ApplyForCourses" element={<ApplyForCourses />} />

      <Route path="*" element={<NotFoundPage />} />
    </Routes>
    </main>

    
    </BrowserRouter>
    </>
  )
}

export default App
