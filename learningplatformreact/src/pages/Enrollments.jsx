import React, { useEffect, useState } from "react";

const BASE = "https://localhost:7240";

export default function Enrollments() {
  const [enrollments, setEnrollments] = useState([]);
  const [participants, setParticipants] = useState([]);
  const [sessions, setSessions] = useState([]);
  const [form, setForm] = useState({
    participantId: "",
    courseSessionId: ""
  });

  async function fetchData() {
    const [e, p, s] = await Promise.all([
      fetch(`${BASE}/api/enrollments`),
      fetch(`${BASE}/api/participants`),
      fetch(`${BASE}/api/coursesessions`)
    ]);

    setEnrollments(await e.json());
    setParticipants(await p.json());
    setSessions(await s.json());
  }

  useEffect(() => { fetchData(); }, []);

  const onChange = e =>
    setForm({ ...form, [e.target.name]: e.target.value });

  async function submit(e) {
    e.preventDefault();

    const payload = {
      participantId: parseInt(form.participantId),
      courseSessionId: parseInt(form.courseSessionId),
      enrollmentDate: new Date().toISOString()
    };

    await fetch(`${BASE}/api/enrollments`, {
      method: "POST",
      headers: { "Content-Type": "application/json" },
      body: JSON.stringify(payload)
    });

    setForm({ participantId: "", courseSessionId: "" });
    fetchData();
  }

  async function remove(id) {
    await fetch(`${BASE}/api/enrollments/${id}`, { method: "DELETE" });
    fetchData();
  }

  return (
    <div>
      <h1>REGISTRERINGAR</h1>

      <form onSubmit={submit}>
        <select name="participantId" value={form.participantId} onChange={onChange} required>
          <option value="">Välj deltagare</option>
          {participants.map(p => (
            <option key={p.id} value={p.id}>
              {p.firstName} {p.lastName}
            </option>
          ))}
        </select>

        <select name="courseSessionId" value={form.courseSessionId} onChange={onChange} required>
          <option value="">Välj session</option>
          {sessions.map(s => (
            <option key={s.id} value={s.id}>
              Session #{s.id}
            </option>
          ))}
        </select>

        <button type="submit">Registrera</button>
      </form>

      <hr />

      {enrollments.map(e => (
        <div key={e.id}>
          ParticipantId: {e.participantId} | SessionId: {e.courseSessionId}
          <button onClick={() => remove(e.id)}>Radera</button>
        </div>
      ))}
    </div>
  );
}
