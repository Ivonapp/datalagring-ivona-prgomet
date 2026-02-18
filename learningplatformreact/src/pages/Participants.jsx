import { useEffect, useState } from "react";

export default function Participants() {
  const [participants, setParticipants] = useState([]);
  const [form, setForm] = useState({
    firstName: "",
    lastName: "",
    email: "",
    phoneNumber: ""
  });

  const [editingId, setEditingId] = useState(null);

  function load() {
    fetch("https://localhost:7240/api/participants")
      .then(r => r.json())
      .then(setParticipants);
  }

  useEffect(load, []);

  function createParticipant(e) {
    e.preventDefault();

    fetch("https://localhost:7240/api/participants", {
      method: "POST",
      headers: { "Content-Type": "application/json" },
      body: JSON.stringify(form)
    }).then(() => {
      setForm({ firstName: "", lastName: "", email: "", phoneNumber: "" });
      load();
    });
  }

  function startEdit(p) {
    setEditingId(p.id);
    setForm({
      firstName: p.firstName,
      lastName: p.lastName,
      email: p.email,
      phoneNumber: p.phoneNumber
    });
  }

  function updateParticipant(e) {
    e.preventDefault();

    fetch(`https://localhost:7240/api/participants/${editingId}`, {
      method: "PUT",
      headers: { "Content-Type": "application/json" },
      body: JSON.stringify(form)
    }).then(() => {
      setEditingId(null);
      setForm({ firstName: "", lastName: "", email: "", phoneNumber: "" });
      load();
    });
  }

  function deleteParticipant(id) {
    fetch(`https://localhost:7240/api/participants/${id}`, {
      method: "DELETE"
    }).then(load);
  }

  return (
    <div className="page">
      <h1>Participants</h1>

      <form onSubmit={editingId ? updateParticipant : createParticipant} className="form">
        <input
          placeholder="First name"
          value={form.firstName}
          onChange={e => setForm({ ...form, firstName: e.target.value })}
        />
        <input
          placeholder="Last name"
          value={form.lastName}
          onChange={e => setForm({ ...form, lastName: e.target.value })}
        />
        <input
          placeholder="Email"
          value={form.email}
          onChange={e => setForm({ ...form, email: e.target.value })}
        />
        <input
          placeholder="Phone"
          value={form.phoneNumber}
          onChange={e => setForm({ ...form, phoneNumber: e.target.value })}
        />

        <button>{editingId ? "Update" : "Create"}</button>
      </form>

      <ul className="list">
        {participants.map(p => (
          <li key={p.id} className="item">
            {p.firstName} {p.lastName} — {p.email}
            <button onClick={() => startEdit(p)}>Edit</button>
            <button onClick={() => deleteParticipant(p.id)}>Delete</button>
          </li>
        ))}
      </ul>
    </div>
  );
}
