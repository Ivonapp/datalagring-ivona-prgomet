import React from 'react'
import { NavLink } from 'react-router-dom'

const Header = () => {
  return (
    <header>
        <nav>
            <ul className="nav-links">
                <li className="link"><NavLink to="/Teachers">Teachers</NavLink></li>
                <li className="link"><NavLink to="/Participants">Participants</NavLink></li>
                <li className="link"><NavLink to="/Courses">Courses</NavLink></li>
                <li className="link"><NavLink to="/Enrollments">Enrollments</NavLink></li>
                <li className="link"><NavLink to="/CourseSessions">CourseSessions</NavLink></li>

            </ul>
        </nav>
    </header>
  )
}

export default Header