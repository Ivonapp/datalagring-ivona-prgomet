import { useEffect, useState } from "react";
import "./Teachers.css"; // Samma CSS som Teachers för design

export default function Participants() {
  const [participants, setParticipants] = useState([]);
  const [form, setForm] = useState({
    firstName: "",
    lastName: "",
    email: "",
    phoneNumber: "",
  });
  const [editingId, setEditingId] = useState(null);

  const apiUrl = "https://localhost:7240/api/participants";

  function loadParticipants() {
    fetch(apiUrl)
      .then((r) => r.json())
      .then(setParticipants);
  }

  useEffect(loadParticipants, []);

  function createParticipant(e) {
    e.preventDefault();
    fetch(apiUrl, {
      method: "POST",
      headers: { "Content-Type": "application/json" },
      body: JSON.stringify(form),
    }).then(() => {
      setForm({ firstName: "", lastName: "", email: "", phoneNumber: "" });
      loadParticipants();
    });
  }

  function startEdit(p) {
    setEditingId(p.id);
    setForm({
      firstName: p.firstName,
      lastName: p.lastName,
      email: p.email,
      phoneNumber: p.phoneNumber,
    });
  }

  function updateParticipant(e) {
    e.preventDefault();
    fetch(`${apiUrl}/${editingId}`, {
      method: "PUT",
      headers: { "Content-Type": "application/json" },
      body: JSON.stringify(form),
    }).then(() => {
      setEditingId(null);
      setForm({ firstName: "", lastName: "", email: "", phoneNumber: "" });
      loadParticipants();
    });
  }

  function deleteParticipant(id) {
    fetch(`${apiUrl}/${id}`, { method: "DELETE" }).then(loadParticipants);
  }

  return (
    <div className="page">
      <h1>Participants</h1>

      {/* Form for Create / Update */}
      <form onSubmit={editingId ? updateParticipant : createParticipant} className="form">
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
        <button>{editingId ? "Update" : "Create"}</button>
      </form>

      {/* Table headers */}
      <div className="Tabell">
        <div>Participant</div>
        <div>Email</div>
        <div>Phone</div>
        <div>Actions</div>
      </div>

      {/* Participants List */}
      <div className="list">
        {participants.map((p) => (
          <div key={p.id} className="item">
            <div>{p.firstName} {p.lastName}</div>
            <div>{p.email}</div>
            <div>{p.phoneNumber}</div>
            <div>
              <button className="edit-btn" onClick={() => startEdit(p)}>Edit</button>
              <button className="delete-btn" onClick={() => deleteParticipant(p.id)}>Delete</button>
            </div>
          </div>
        ))}
      </div>
    </div>
  );
}
