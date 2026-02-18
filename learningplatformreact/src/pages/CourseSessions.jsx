import React, { useEffect, useState } from "react";

const BASE = "https://localhost:7240";

export default function CourseSessions() {
  const [sessions, setSessions] = useState([]);
  const [courses, setCourses] = useState([]);
  const [form, setForm] = useState({
    courseId: "",
    startDate: "",
    endDate: ""
  });

  async function fetchData() {
    const [sRes, cRes] = await Promise.all([
      fetch(`${BASE}/api/coursesessions`),
      fetch(`${BASE}/api/courses`)
    ]);

    setSessions(await sRes.json());
    setCourses(await cRes.json());
  }

  useEffect(() => { fetchData(); }, []);

  const onChange = e =>
    setForm({ ...form, [e.target.name]: e.target.value });

  async function submit(e) {
    e.preventDefault();

    const payload = {
      courseId: parseInt(form.courseId),
      startDate: new Date(form.startDate).toISOString(),
      endDate: new Date(form.endDate).toISOString()
    };

    await fetch(`${BASE}/api/coursesessions`, {
      method: "POST",
      headers: { "Content-Type": "application/json" },
      body: JSON.stringify(payload)
    });

    setForm({ courseId: "", startDate: "", endDate: "" });
    fetchData();
  }

  async function remove(id) {
    await fetch(`${BASE}/api/coursesessions/${id}`, { method: "DELETE" });
    fetchData();
  }

  return (
    <div>
      <h1>KURSTILLFÄLLEN</h1>

      <form onSubmit={submit}>
        <select name="courseId" value={form.courseId} onChange={onChange} required>
          <option value="">Välj kurs</option>
          {courses.map(c => (
            <option key={c.id} value={c.id}>
              {c.title}
            </option>
          ))}
        </select>

        <input type="date" name="startDate" value={form.startDate} onChange={onChange} required />
        <input type="date" name="endDate" value={form.endDate} onChange={onChange} required />
        <button type="submit">Skapa</button>
      </form>

      <hr />

      {sessions.map(s => (
        <div key={s.id}>
          CourseId: {s.courseId} | 
          {new Date(s.startDate).toLocaleDateString()} - 
          {new Date(s.endDate).toLocaleDateString()}
          <button onClick={() => remove(s.id)}>Radera</button>
        </div>
      ))}
    </div>
  );
}
