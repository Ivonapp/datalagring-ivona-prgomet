import { useEffect, useState } from "react";

export default function Enrollments() {
  const [enrollments, setEnrollments] = useState([]);
  const [participants, setParticipants] = useState([]);
  const [sessions, setSessions] = useState([]);
  const [form, setForm] = useState({ participantId: "", courseSessionId: "" });

  function load() {
    fetch("https://localhost:7240/api/enrollments").then(r => r.json()).then(setEnrollments);
    fetch("https://localhost:7240/api/participants").then(r => r.json()).then(setParticipants);
    fetch("https://localhost:7240/api/coursesessions").then(r => r.json()).then(setSessions);
  }

  useEffect(load, []);

  function createEnrollment(e) {
    e.preventDefault();
    fetch("https://localhost:7240/api/enrollments", {
      method: "POST",
      headers: { "Content-Type": "application/json" },
      body: JSON.stringify(form)
    }).then(load);
  }

  return (
    <div className="page">
      <h1>Enrollments</h1>

      <form onSubmit={createEnrollment} className="form">
        <select onChange={e => setForm({ ...form, participantId: Number(e.target.value) })}>
          <option value="">Select participant</option>
          {participants.map(p => (
            <option key={p.id} value={p.id}>
              {p.firstName} {p.lastName}
            </option>
          ))}
        </select>

        <select onChange={e => setForm({ ...form, courseSessionId: Number(e.target.value) })}>
          <option value="">Select session</option>
          {sessions.map(s => (
            <option key={s.id} value={s.id}>
              Session {s.id} (Course {s.courseId})
            </option>
          ))}
        </select>

        <button>Create</button>
      </form>

      <ul className="list">
        {enrollments.map(e => (
          <li key={e.id} className="item">
            Participant {e.participantId} → Session {e.courseSessionId}
          </li>
        ))}
      </ul>
    </div>
  );
}
