import React, { useState, useEffect } from "react";

const Teachers = () => {
  const [teachers, setTeachers] = useState([]);
  const [selectedTeacher, setSelectedTeacher] = useState(null);
  const [form, setForm] = useState({
    firstName: "",
    lastName: "",
    email: "",
    phoneNumber: "",
    major: "",
  });

  const apiUrl = "https://localhost:7240/api/teachers";

  const fetchTeachers = async () => {
    const res = await fetch(apiUrl);
    const data = await res.json();
    setTeachers(data);
  };

  const fetchTeacherDetail = async (id) => {
    const res = await fetch(`${apiUrl}/${id}`);
    const data = await res.json();
    setSelectedTeacher(data);
    setForm({
      firstName: data.firstName || "",
      lastName: data.lastName || "",
      email: data.email || "",
      phoneNumber: data.phoneNumber || "",
      major: data.major || "",
    });
  };

  const createTeacher = async () => {
    await fetch(apiUrl, {
      method: "POST",
      headers: { "Content-Type": "application/json" },
      body: JSON.stringify(form),
    });
    setForm({ firstName: "", lastName: "", email: "", phoneNumber: "", major: "" });
    fetchTeachers();
  };

  const updateTeacher = async (id) => {
    await fetch(`${apiUrl}/${id}`, {
      method: "PUT",
      headers: { "Content-Type": "application/json" },
      body: JSON.stringify(form),
    });
    setSelectedTeacher(null);
    fetchTeachers();
  };

  const deleteTeacher = async (id) => {
    await fetch(`${apiUrl}/${id}`, { method: "DELETE" });
    fetchTeachers();
  };

  useEffect(() => {
    fetchTeachers();
  }, []);

  return (
    <div>
      <h1>Teachers</h1>

      {/* CREATE FORM */}
      <div>
        <h2>Create Teacher</h2>
        <input placeholder="First Name" value={form.firstName} onChange={e => setForm({ ...form, firstName: e.target.value })}/>
        <input placeholder="Last Name" value={form.lastName} onChange={e => setForm({ ...form, lastName: e.target.value })}/>
        <input placeholder="Email" value={form.email} onChange={e => setForm({ ...form, email: e.target.value })}/>
        <input placeholder="Phone" value={form.phoneNumber} onChange={e => setForm({ ...form, phoneNumber: e.target.value })}/>
        <input placeholder="Major" value={form.major} onChange={e => setForm({ ...form, major: e.target.value })}/>
        <button onClick={createTeacher}>Create</button>
      </div>

      {/* LIST TEACHERS */}
      <h2>All Teachers</h2>
      <ul>
        {teachers.map(t => (
          <li key={t.id}>
            {t.firstName} {t.lastName} - {t.email}
            <button onClick={() => fetchTeacherDetail(t.id)}>Details</button>
            <button onClick={() => deleteTeacher(t.id)}>Delete</button>
          </li>
        ))}
      </ul>

      {/* SELECTED TEACHER DETAIL */}
      {selectedTeacher && (
        <div>
          <h2>Teacher Details</h2>
          <p>ID: {selectedTeacher.id}</p>
          <p>Email: {selectedTeacher.email}</p>
          <p>Phone: {selectedTeacher.phoneNumber}</p>
          <p>Major: {selectedTeacher.major}</p>

          {/* Courses */}
          <h3>Courses</h3>
          <ul>
            {selectedTeacher.courses?.map(c => (
              <li key={c.id}>{c.title} ({c.courseCode}) - Enrollments: {c.courseSessions?.reduce((acc, cs) => acc + (cs.enrollments?.length || 0), 0)}</li>
            ))}
          </ul>

          {/* Course Sessions */}
          <h3>Course Sessions</h3>
          <ul>
            {selectedTeacher.courseSessions?.map(cs => (
              <li key={cs.id}>
                Course ID: {cs.courseId} | {new Date(cs.startDate).toLocaleDateString()} - {new Date(cs.endDate).toLocaleDateString()}
              </li>
            ))}
          </ul>

          {/* Update Form */}
          <h3>Update Teacher</h3>
          <input placeholder="First Name" value={form.firstName} onChange={e => setForm({ ...form, firstName: e.target.value })}/>
          <input placeholder="Last Name" value={form.lastName} onChange={e => setForm({ ...form, lastName: e.target.value })}/>
          <input placeholder="Email" value={form.email} onChange={e => setForm({ ...form, email: e.target.value })}/>
          <input placeholder="Phone" value={form.phoneNumber} onChange={e => setForm({ ...form, phoneNumber: e.target.value })}/>
          <input placeholder="Major" value={form.major} onChange={e => setForm({ ...form, major: e.target.value })}/>
          <button onClick={() => updateTeacher(selectedTeacher.id)}>Update</button>
          <button onClick={() => setSelectedTeacher(null)}>Close</button>
        </div>
      )}
    </div>
  );
};

export default Teachers;
