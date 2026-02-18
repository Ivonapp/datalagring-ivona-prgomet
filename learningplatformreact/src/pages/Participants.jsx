import React, { useEffect, useState } from "react";

const API = "https://localhost:7240/api/participants";

export default function Participants() {
  const [participants, setParticipants] = useState([]);
  const [form, setForm] = useState({
    firstName: "",
    lastName: "",
    email: "",
    phoneNumber: ""
  });

  async function fetchParticipants() {
    const res = await fetch(API);
    setParticipants(await res.json());
  }

  useEffect(() => { fetchParticipants(); }, []);

  const onChange = e =>
    setForm({ ...form, [e.target.name]: e.target.value });

  async function submit(e) {
    e.preventDefault();

    await fetch(API, {
      method: "POST",
      headers: { "Content-Type": "application/json" },
      body: JSON.stringify(form)
    });

    setForm({ firstName: "", lastName: "", email: "", phoneNumber: "" });
    fetchParticipants();
  }

  async function remove(id) {
    await fetch(`${API}/${id}`, { method: "DELETE" });
    fetchParticipants();
  }

  return (
    <div>
      <h1>DELTAGARE</h1>

      <form onSubmit={submit}>
        <input name="firstName" placeholder="Förnamn" value={form.firstName} onChange={onChange} required />
        <input name="lastName" placeholder="Efternamn" value={form.lastName} onChange={onChange} required />
        <input name="email" placeholder="Email" value={form.email} onChange={onChange} required />
        <input name="phoneNumber" placeholder="Telefon" value={form.phoneNumber} onChange={onChange} required />
        <button type="submit">Skapa</button>
      </form>

      <hr />

      {participants.map(p => (
        <div key={p.id}>
          {p.firstName} {p.lastName}
          <button onClick={() => remove(p.id)}>Radera</button>
        </div>
      ))}
    </div>
  );
}
