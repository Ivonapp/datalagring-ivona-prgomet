import { useEffect, useState } from "react";

export default function Enrollments() {
  const [enrollments, setEnrollments] = useState([]);
  const [participants, setParticipants] = useState([]);
  const [sessions, setSessions] = useState([]);
  const [courses, setCourses] = useState([]);
  const [form, setForm] = useState({ participantId: "", courseSessionId: "" });

  // Hämta all data
  function load() {
    fetch("https://localhost:7240/api/enrollments")
      .then(r => r.json())
      .then(setEnrollments);

    fetch("https://localhost:7240/api/participants")
      .then(r => r.json())
      .then(setParticipants);

    fetch("https://localhost:7240/api/coursesessions")
      .then(r => r.json())
      .then(setSessions);

    fetch("https://localhost:7240/api/courses")
      .then(r => r.json())
      .then(setCourses);
  }

  useEffect(load, []);

  // Skapa ny enrollment
  function createEnrollment(e) {
    e.preventDefault();
    fetch("https://localhost:7240/api/enrollments", {
      method: "POST",
      headers: { "Content-Type": "application/json" },
      body: JSON.stringify(form),
    }).then(() => {
      setForm({ participantId: "", courseSessionId: "" });
      load();
    });
  }

  // Ta bort enrollment
  function removeEnrollment(id) {
    fetch(`https://localhost:7240/api/enrollments/${id}`, {
      method: "DELETE",
    }).then(load);
  }

  return (
    <div className="page">
      <h1>Enrollments</h1>

      <form onSubmit={createEnrollment} className="form">
        <select
          value={form.participantId}
          onChange={e => setForm({ ...form, participantId: Number(e.target.value) })}
          required
        >
          <option value="">Select participant</option>
          {participants.map(p => (
            <option key={p.id} value={p.id}>
              {p.firstName} {p.lastName}
            </option>
          ))}
        </select>

        <select
          value={form.courseSessionId}
          onChange={e => setForm({ ...form, courseSessionId: Number(e.target.value) })}
          required
        >
          <option value="">Select session</option>
          {sessions.map(s => {
            const course = courses.find(c => c.id === s.courseId);
            return (
              <option key={s.id} value={s.id}>
                {course ? course.title : `Course ${s.courseId}`} |{" "}
                {new Date(s.startDate).toLocaleDateString()} –{" "}
                {new Date(s.endDate).toLocaleDateString()}
              </option>
            );
          })}
        </select>

        <button>Create</button>
      </form>

      <h2>All Enrollments</h2>
      <ul className="list">
        {enrollments.map(e => {
          const participant = participants.find(p => p.id === e.participantId);
          const session = sessions.find(s => s.id === e.courseSessionId);
          const course = courses.find(c => c.id === session?.courseId);

          return (
            <li key={e.id} className="item">
              Participant: {participant ? `${participant.firstName} ${participant.lastName}` : "Unknown"} <br />
              Course: {course ? course.title : "Unknown"} <br />
              Session: {session ? `${new Date(session.startDate).toLocaleDateString()} – ${new Date(session.endDate).toLocaleDateString()}` : "Unknown"} <br />
              <button onClick={() => removeEnrollment(e.id)}>Delete</button>
            </li>
          );
        })}
      </ul>
    </div>
  );
}
