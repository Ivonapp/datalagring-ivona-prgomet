import React from 'react'
import { NavLink } from 'react-router-dom'

const Header = () => {
  return (
    <header>
        <nav>
            <ul className="nav-links">
                <li className="link"><NavLink to="/Home">Home</NavLink></li>
                <li className="link"><NavLink to="/Teachers">Teachers</NavLink></li>
                <li className="link"><NavLink to="/ApplyForCourses">ApplyForCourses</NavLink></li>
            </ul>
        </nav>
    </header>
  )
}

export default Header