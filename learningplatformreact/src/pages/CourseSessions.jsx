import { useEffect, useState } from "react";
import "./Teachers.css"; // Återanvänd samma CSS

export default function CourseSessions() {
  const [sessions, setSessions] = useState([]);
  const [courses, setCourses] = useState([]);
  const [form, setForm] = useState({ courseId: "", startDate: "", endDate: "" });
  const [editingId, setEditingId] = useState(null);

  const apiUrl = "https://localhost:7240/api/coursesessions";

  // Hämta sessions och courses
  function load() {
    fetch(apiUrl).then(r => r.json()).then(setSessions);
    fetch("https://localhost:7240/api/courses").then(r => r.json()).then(setCourses);
  }

  useEffect(load, []);

  function handleChange(e) {
    setForm({ ...form, [e.target.name]: e.target.value });
  }

  function startEdit(session) {
    setEditingId(session.id);
    setForm({
      courseId: session.courseId,
      startDate: session.startDate.slice(0, 10),
      endDate: session.endDate.slice(0, 10),
    });
  }

  function createSession(e) {
    e.preventDefault();
    fetch(apiUrl, {
      method: "POST",
      headers: { "Content-Type": "application/json" },
      body: JSON.stringify({
        courseId: Number(form.courseId),
        startDate: form.startDate,
        endDate: form.endDate,
      }),
    }).then(() => {
      setForm({ courseId: "", startDate: "", endDate: "" });
      load();
    });
  }

  function updateSession(e) {
    e.preventDefault();
    fetch(`${apiUrl}/${editingId}`, {
      method: "PUT",
      headers: { "Content-Type": "application/json" },
      body: JSON.stringify({
        courseId: Number(form.courseId),
        startDate: form.startDate,
        endDate: form.endDate,
      }),
    }).then(() => {
      setEditingId(null);
      setForm({ courseId: "", startDate: "", endDate: "" });
      load();
    });
  }

  function removeSession(id) {
    fetch(`${apiUrl}/${id}`, { method: "DELETE" }).then(load);
  }

  return (
    <div className="page">
      <h1>Course Sessions</h1>

      {/* Form for Create / Update */}
      <form onSubmit={editingId ? updateSession : createSession} className="form">
        <select
          name="courseId"
          value={form.courseId}
          onChange={handleChange}
          required
        >
          <option value="">Select Course</option>
          {courses.map(c => (
            <option key={c.id} value={c.id}>{c.title}</option>
          ))}
        </select>

        <input
          type="date"
          name="startDate"
          value={form.startDate}
          onChange={handleChange}
          required
        />
        <input
          type="date"
          name="endDate"
          value={form.endDate}
          onChange={handleChange}
          required
        />

        <button>{editingId ? "Update" : "Create"}</button>
      </form>

      {/* Table headers */}
      <div className="Tabell">
        <div>Course</div>
        <div>Start Date</div>
        <div>End Date</div>
        <div>Actions</div>
      </div>

      {/* Sessions List */}
      <div className="list">
        {sessions.map(s => {
          const course = courses.find(c => c.id === s.courseId);
          return (
            <div key={s.id} className="item">
              <div>{course ? course.title : `Course ${s.courseId}`}</div>
              <div>{new Date(s.startDate).toLocaleDateString()}</div>
              <div>{new Date(s.endDate).toLocaleDateString()}</div>
              <div>
                <button className="edit-btn" onClick={() => startEdit(s)}>Edit</button>
                <button className="delete-btn" onClick={() => removeSession(s.id)}>Delete</button>
              </div>
            </div>
          );
        })}
      </div>
    </div>
  );
}
