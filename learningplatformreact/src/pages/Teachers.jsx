import { useEffect, useState } from "react";
import "./Teachers.css";

export default function Teachers() {
  const [teachers, setTeachers] = useState([]);
  const [form, setForm] = useState({
    firstName: "",
    lastName: "",
    email: "",
    phoneNumber: "",
    major: "",
  });
  const [editingId, setEditingId] = useState(null);

  const apiUrl = "https://localhost:7240/api/teachers";

  function loadTeachers() {
    fetch(apiUrl)
      .then((r) => r.json())
      .then(setTeachers);
  }

  useEffect(loadTeachers, []);

  function createTeacher(e) {
    e.preventDefault();
    fetch(apiUrl, {
      method: "POST",
      headers: { "Content-Type": "application/json" },
      body: JSON.stringify(form),
    }).then(() => {
      setForm({ firstName: "", lastName: "", email: "", phoneNumber: "", major: "" });
      loadTeachers();
    });
  }

  function startEdit(teacher) {
    setEditingId(teacher.id);
    setForm({
      firstName: teacher.firstName,
      lastName: teacher.lastName,
      email: teacher.email,
      phoneNumber: teacher.phoneNumber,
      major: teacher.major,
    });
  }

  function updateTeacher(e) {
    e.preventDefault();
    fetch(`${apiUrl}/${editingId}`, {
      method: "PUT",
      headers: { "Content-Type": "application/json" },
      body: JSON.stringify(form),
    }).then(() => {
      setEditingId(null);
      setForm({ firstName: "", lastName: "", email: "", phoneNumber: "", major: "" });
      loadTeachers();
    });
  }

  function deleteTeacher(id) {
    fetch(`${apiUrl}/${id}`, { method: "DELETE" }).then(loadTeachers);
  }

  return (
    <div className="page">
      <h1>Teachers</h1>

      {/* Form for Create / Update */}
      <form onSubmit={editingId ? updateTeacher : createTeacher} className="form">
        <input
          placeholder="First name"
          value={form.firstName}
          onChange={(e) => setForm({ ...form, firstName: e.target.value })}
        />
        <input
          placeholder="Last name"
          value={form.lastName}
          onChange={(e) => setForm({ ...form, lastName: e.target.value })}
        />
        <input
          placeholder="Email"
          value={form.email}
          onChange={(e) => setForm({ ...form, email: e.target.value })}
        />
        <input
          placeholder="Phone"
          value={form.phoneNumber}
          onChange={(e) => setForm({ ...form, phoneNumber: e.target.value })}
        />
        <input
          placeholder="Major"
          value={form.major}
          onChange={(e) => setForm({ ...form, major: e.target.value })}
        />
        <button>{editingId ? "Update" : "Create"}</button>
      </form>

      {/* Table headers */}
      <div className="Tabell">
        <div>Teacher</div>
        <div>Email</div>
        <div>Major</div>
        <div>Actions</div>
      </div>

      {/* Teachers List */}
      <div className="list">
        {teachers.map((t) => (
          <div key={t.id} className="item">
            <div>{t.firstName} {t.lastName}</div>
            <div>{t.email}</div>
            <div>{t.major}</div>
            <div>
              <button className="edit-btn" onClick={() => startEdit(t)}>Edit</button>
              <button className="delete-btn" onClick={() => deleteTeacher(t.id)}>Delete</button>
            </div>
          </div>
        ))}
      </div>
    </div>
  );
}
