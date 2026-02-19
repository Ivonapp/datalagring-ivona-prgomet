import { useEffect, useState } from "react";
import "./Teachers.css"; // Återanvänd samma CSS

export default function Enrollments() {
  const [enrollments, setEnrollments] = useState([]);
  const [participants, setParticipants] = useState([]);
  const [sessions, setSessions] = useState([]);
  const [courses, setCourses] = useState([]);
  const [form, setForm] = useState({ participantId: "", courseSessionId: "" });
  const [editingId, setEditingId] = useState(null);

  const apiUrl = "https://localhost:7240/api/enrollments";

  function load() {
    fetch(apiUrl).then((r) => r.json()).then(setEnrollments);
    fetch("https://localhost:7240/api/participants").then((r) => r.json()).then(setParticipants);
    fetch("https://localhost:7240/api/coursesessions").then((r) => r.json()).then(setSessions);
    fetch("https://localhost:7240/api/courses").then((r) => r.json()).then(setCourses);
  }

  useEffect(load, []);

  function startEdit(enrollment) {
    setEditingId(enrollment.id);
    setForm({
      participantId: enrollment.participantId,
      courseSessionId: enrollment.courseSessionId,
    });
  }

  function createEnrollment(e) {
    e.preventDefault();
    fetch(apiUrl, {
      method: "POST",
      headers: { "Content-Type": "application/json" },
      body: JSON.stringify(form),
    }).then(() => {
      setForm({ participantId: "", courseSessionId: "" });
      load();
    });
  }

  function updateEnrollment(e) {
    e.preventDefault();
    fetch(`${apiUrl}/${editingId}`, {
      method: "PUT",
      headers: { "Content-Type": "application/json" },
      body: JSON.stringify(form),
    }).then(() => {
      setEditingId(null);
      setForm({ participantId: "", courseSessionId: "" });
      load();
    });
  }

  function deleteEnrollment(id) {
    fetch(`${apiUrl}/${id}`, { method: "DELETE" }).then(load);
  }

  return (
    <div className="page">
      <h1>Enrollments</h1>

      {/* Form for Create / Update */}
      <form onSubmit={editingId ? updateEnrollment : createEnrollment} className="form">
        <select
          value={form.participantId}
          onChange={(e) => setForm({ ...form, participantId: Number(e.target.value) })}
          required
        >
          <option value="">Select Participant</option>
          {participants.map((p) => (
            <option key={p.id} value={p.id}>
              {p.firstName} {p.lastName}
            </option>
          ))}
        </select>

        <select
          value={form.courseSessionId}
          onChange={(e) => setForm({ ...form, courseSessionId: Number(e.target.value) })}
          required
        >
          <option value="">Select Session</option>
          {sessions.map((s) => {
            const course = courses.find((c) => c.id === s.courseId);
            return (
              <option key={s.id} value={s.id}>
                {course ? course.title : `Course ${s.courseId}`} |{" "}
                {new Date(s.startDate).toLocaleDateString()} – {new Date(s.endDate).toLocaleDateString()}
              </option>
            );
          })}
        </select>

        <button>{editingId ? "Update" : "Create"}</button>
      </form>

      {/* Table headers */}
      <div className="Tabell">
        <div>Participant</div>
        <div>Course</div>
        <div>Start Date – End Date</div>
        <div>Actions</div>
      </div>

      {/* Enrollments List */}
      <div className="list">
        {enrollments.map((e) => {
          const participant = participants.find((p) => p.id === e.participantId);
          const session = sessions.find((s) => s.id === e.courseSessionId);
          const course = courses.find((c) => c.id === session?.courseId);

          return (
            <div key={e.id} className="item">
              <div>{participant ? `${participant.firstName} ${participant.lastName}` : "Unknown"}</div>
              <div>{course ? course.title : "Unknown"}</div>
              <div>
                {session
                  ? `${new Date(session.startDate).toLocaleDateString()} – ${new Date(session.endDate).toLocaleDateString()}`
                  : "Unknown"}
              </div>
              <div>
                <button className="edit-btn" onClick={() => startEdit(e)}>Edit</button>
                <button className="delete-btn" onClick={() => deleteEnrollment(e.id)}>Delete</button>
              </div>
            </div>
          );
        })}
      </div>
    </div>
  );
}
