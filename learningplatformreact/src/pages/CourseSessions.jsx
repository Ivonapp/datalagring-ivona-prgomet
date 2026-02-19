import { useEffect, useState } from "react";

export default function CourseSessions() {
  const [sessions, setSessions] = useState([]);
  const [courses, setCourses] = useState([]);
  const [form, setForm] = useState({
    courseId: "",
    startDate: "",
    endDate: ""
  });

  // Hämta sessions och courses
  function load() {
    fetch("https://localhost:7240/api/coursesessions")
      .then(r => r.json())
      .then(setSessions);

    fetch("https://localhost:7240/api/courses")
      .then(r => r.json())
      .then(setCourses);
  }

  useEffect(load, []);

  // Hantera inputändringar
  function handleChange(e) {
    setForm({ ...form, [e.target.name]: e.target.value });
  }

  // Skapa session
  function createSession(e) {
    e.preventDefault();

    const payload = {
      courseId: Number(form.courseId),
      startDate: form.startDate,
      endDate: form.endDate
    };

    fetch("https://localhost:7240/api/coursesessions", {
      method: "POST",
      headers: { "Content-Type": "application/json" },
      body: JSON.stringify(payload)
    }).then(() => {
      setForm({ courseId: "", startDate: "", endDate: "" });
      load();
    });
  }

  // Ta bort session
  function removeSession(id) {
    fetch(`https://localhost:7240/api/coursesessions/${id}`, {
      method: "DELETE"
    }).then(load);
  }

  return (
    <div className="page">
      <h1>Course Sessions</h1>

      <form onSubmit={createSession} className="form">
        <select
          name="courseId"
          value={form.courseId}
          onChange={handleChange}
          required
        >
          <option value="">Select course</option>
          {courses.map(c => (
            <option key={c.id} value={c.id}>
              {c.title}
            </option>
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

        <button>Create</button>
      </form>

      <h2>All Sessions</h2>
      <ul className="list">
        {sessions.map(s => {
          const course = courses.find(c => c.id === s.courseId);
          return (
            <li key={s.id} className="item">
              Course: {course ? course.title : `Course ${s.courseId}`} <br />
              {new Date(s.startDate).toLocaleDateString()} – {new Date(s.endDate).toLocaleDateString()} <br />
              <button onClick={() => removeSession(s.id)}>Delete</button>
            </li>
          );
        })}
      </ul>
    </div>
  );
}
