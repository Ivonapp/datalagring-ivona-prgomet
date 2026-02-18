import React, { useEffect, useState } from "react";

const API = "https://localhost:7240/api/courses";

export default function Courses() {
  const [courses, setCourses] = useState([]);
  const [form, setForm] = useState({
    title: "",
    description: "",
    courseCode: ""
  });

  async function fetchCourses() {
    const res = await fetch(API);
    const data = await res.json();
    setCourses(data);
  }

  useEffect(() => { fetchCourses(); }, []);

  const onChange = e =>
    setForm({ ...form, [e.target.name]: e.target.value });

  async function submit(e) {
    e.preventDefault();

    const payload = {
      title: form.title,
      description: form.description,
      courseCode: parseInt(form.courseCode)
    };

    await fetch(API, {
      method: "POST",
      headers: { "Content-Type": "application/json" },
      body: JSON.stringify(payload)
    });

    setForm({ title: "", description: "", courseCode: "" });
    fetchCourses();
  }

  async function remove(id) {
    await fetch(`${API}/${id}`, { method: "DELETE" });
    fetchCourses();
  }

  return (
    <div>
      <h1>KURSER</h1>

      <form onSubmit={submit}>
        <input name="title" placeholder="Titel" value={form.title} onChange={onChange} required />
        <input name="description" placeholder="Beskrivning" value={form.description} onChange={onChange} />
        <input type="number" name="courseCode" placeholder="Kurskod" value={form.courseCode} onChange={onChange} required />
        <button type="submit">Skapa</button>
      </form>

      <hr />

      {courses.map(c => (
        <div key={c.id}>
          {c.title} (Kod: {c.courseCode})
          <button onClick={() => remove(c.id)}>Radera</button>
        </div>
      ))}
    </div>
  );
}
