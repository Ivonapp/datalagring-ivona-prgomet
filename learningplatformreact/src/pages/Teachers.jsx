import React, { useState, useEffect } from 'react'
import './Teachers.css';


const Teachers = () => {
    const [teachers, setTeachers] = useState([]);
    const [formData, setFormData] = useState({
        firstName: '',
        lastName: '',
        email: '',
        phoneNumber: '',
        major: ''
    });

    const apiUrl = "https://localhost:7240/api/teachers";

    const getTeachers = async () => {
        try {
            const response = await fetch(apiUrl);
            const data = await response.json();
            setTeachers(data);
        } catch (error) {
            console.error("Fel vid hämtning:", error);
        }
    };

    useEffect(() => {
        getTeachers();
    }, []);

    const handleChange = (e) => {
        setFormData({ ...formData, [e.target.name]: e.target.value });
    };

    const handleSubmit = async (e) => {
        e.preventDefault();
        try {
            const response = await fetch(apiUrl, {
                method: 'POST',
                headers: { 'Content-Type': 'application/json' },
                body: JSON.stringify(formData)
            });

            if (response.ok) {
                setFormData({ firstName: '', lastName: '', email: '', phoneNumber: '', major: '' });
                getTeachers();
            }
        } catch (error) {
            console.error("Fel vid skapande:", error);
        }
    };

    return (
        <div className="page-container">
            <h1 className="page-title">LÄRARE</h1>

            <form onSubmit={handleSubmit} className="teacher-form">
                <input className="form-input" name="firstName" placeholder="Förnamn" value={formData.firstName} onChange={handleChange} required />
                <input className="form-input" name="lastName" placeholder="Efternamn" value={formData.lastName} onChange={handleChange} required />
                <input className="form-input" name="email" placeholder="E-post" value={formData.email} onChange={handleChange} required />
                <input className="form-input" name="phoneNumber" placeholder="Telefon" value={formData.phoneNumber} onChange={handleChange} />
                <input className="form-input" name="major" placeholder="Ämne/Kurs" value={formData.major} onChange={handleChange} />
                <button type="submit" className="submitbtn">Lägg till lärare</button>
            </form>

            <hr className="divider" />

            <div className="teacher-list">
                {teachers.length === 0 ? (
                    <p className="empty-msg">Inga lärare hittades.</p>
                ) : (
                    teachers.map(t => (
                        <div key={t.id} className="teacher-item">
                            <p className="teacher-info">
                                <span className="teacher-name">{t.firstName} {t.lastName}</span> - 
                                <span className="teacher-major"> {t.major}</span>
                            </p>
                        </div>
                    ))
                )}
            </div>
        </div>
    );
};

export default Teachers;