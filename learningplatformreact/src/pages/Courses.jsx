import { useEffect, useState } from "react";
import "./Courses.css";

export default function Courses() {
  const [courses, setCourses] = useState([]);
  const [teachers, setTeachers] = useState([]);
  const [form, setForm] = useState({
    title: "",
    description: "",
    courseCode: "",
    teacherId: "",
  });
  const [editingId, setEditingId] = useState(null);

  const coursesApi = "https://localhost:7240/api/courses";
  const teachersApi = "https://localhost:7240/api/teachers";

  function loadCourses() {
    fetch(coursesApi)
      .then((r) => r.json())
      .then(setCourses);
  }

  function loadTeachers() {
    fetch(teachersApi)
      .then((r) => r.json())
      .then(setTeachers);
  }

  useEffect(() => {
    loadCourses();
    loadTeachers();
  }, []);

  function createCourse(e) {
    e.preventDefault();
    if (!form.teacherId) return alert("Please select a teacher!");

    fetch(coursesApi, {
      method: "POST",
      headers: { "Content-Type": "application/json" },
      body: JSON.stringify({
        ...form,
        teacherId: Number(form.teacherId),
      }),
    }).then(() => {
      setForm({ title: "", description: "", courseCode: "", teacherId: "" });
      loadCourses();
    });
  }

  function startEdit(course) {
    setEditingId(course.id);
    setForm({
      title: course.title,
      description: course.description,
      courseCode: course.courseCode,
      teacherId: course.teacherId || "",
    });
  }

  function updateCourse(e) {
    e.preventDefault();
    if (!form.teacherId) return alert("Please select a teacher!");

    fetch(`${coursesApi}/${editingId}`, {
      method: "PUT",
      headers: { "Content-Type": "application/json" },
      body: JSON.stringify({
        ...form,
        teacherId: Number(form.teacherId),
      }),
    }).then(() => {
      setEditingId(null);
      setForm({ title: "", description: "", courseCode: "", teacherId: "" });
      loadCourses();
    });
  }

  function deleteCourse(id) {
    fetch(`${coursesApi}/${id}`, { method: "DELETE" }).then(loadCourses);
  }

  function getTeacherName(id) {
    const teacher = teachers.find((t) => t.id === id);
    return teacher ? `${teacher.firstName} ${teacher.lastName}` : "Unknown";
  }

  return (
    <div className="page">
      <h1>Courses</h1>

      {/* Form for Create / Update */}
      <form onSubmit={editingId ? updateCourse : createCourse} className="form">
        <input
          placeholder="Title"
          value={form.title}
          onChange={(e) => setForm({ ...form, title: e.target.value })}
        />
        <input
          placeholder="Description"
          value={form.description}
          onChange={(e) => setForm({ ...form, description: e.target.value })}
        />
        <input
          placeholder="Course Code"
          value={form.courseCode}
          onChange={(e) => setForm({ ...form, courseCode: e.target.value })}
        />
        <select
          value={form.teacherId}
          onChange={(e) => setForm({ ...form, teacherId: e.target.value })}
        >
          <option value="">Select Teacher</option>
          {teachers.map((t) => (
            <option key={t.id} value={t.id}>
              {t.firstName} {t.lastName}
            </option>
          ))}
        </select>
        <button>{editingId ? "Update" : "Create"}</button>
      </form>

      {/* Table headers */}
      <div className="Tabell">
        <div>Title</div>
        <div>Course Code</div>
        <div>Teacher</div>
        <div>Actions</div>
      </div>

      {/* Courses List */}
      <div className="list">
        {courses.map((c) => (
          <div key={c.id} className="item">
            <div>{c.title}</div>
            <div>{c.courseCode}</div>
            <div>{getTeacherName(c.teacherId)}</div>
            <div>
              <button className="edit-btn" onClick={() => startEdit(c)}>Edit</button>
              <button className="delete-btn" onClick={() => deleteCourse(c.id)}>Delete</button>
            </div>
          </div>
        ))}
      </div>
    </div>
  );
}
