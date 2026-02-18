import React, { useState, useEffect } from "react";

const Courses = () => {
  const [courses, setCourses] = useState([]);
  const [teachers, setTeachers] = useState([]);
  const [selectedCourse, setSelectedCourse] = useState(null);
  const [form, setForm] = useState({
    title: "",
    description: "",
    courseCode: "",
    teacherId: "",
  });

  const coursesApi = "https://localhost:7240/api/courses";
  const teachersApi = "https://localhost:7240/api/teachers";

  // Hämta kurser
  const fetchCourses = async () => {
    const res = await fetch(coursesApi);
    const data = await res.json();
    setCourses(data);
  };

  // Hämta lärare
  const fetchTeachers = async () => {
    const res = await fetch(teachersApi);
    const data = await res.json();
    setTeachers(data);
  };

  // Hämta kursdetalj
  const fetchCourseDetail = async (id) => {
    const res = await fetch(`${coursesApi}/${id}`);
    const data = await res.json();
    setSelectedCourse(data);
    setForm({
      title: data.title || "",
      description: data.description || "",
      courseCode: data.courseCode || "",
      teacherId: data.teacher?.id || "",
    });
  };

  // Skapa kurs
  const createCourse = async () => {
    if (!form.teacherId) {
      alert("Please select a teacher!");
      return;
    }

    await fetch(coursesApi, {
      method: "POST",
      headers: { "Content-Type": "application/json" },
      body: JSON.stringify({
        title: form.title,
        description: form.description,
        courseCode: form.courseCode,
        teacherId: Number(form.teacherId),
      }),
    });

    setForm({ title: "", description: "", courseCode: "", teacherId: "" });
    fetchCourses();
  };

  // Uppdatera kurs
  const updateCourse = async (id) => {
    if (!form.teacherId) {
      alert("Please select a teacher!");
      return;
    }

    await fetch(`${coursesApi}/${id}`, {
      method: "PUT",
      headers: { "Content-Type": "application/json" },
      body: JSON.stringify({
        title: form.title,
        description: form.description,
        courseCode: form.courseCode,
        teacherId: Number(form.teacherId),
      }),
    });

    setSelectedCourse(null);
    setForm({ title: "", description: "", courseCode: "", teacherId: "" });
    fetchCourses();
  };

  // Ta bort kurs
  const deleteCourse = async (id) => {
    await fetch(`${coursesApi}/${id}`, { method: "DELETE" });
    fetchCourses();
  };

  useEffect(() => {
    fetchCourses();
    fetchTeachers();
  }, []);

  return (
    <div>
      <h1>Courses</h1>

      {/* CREATE FORM */}
      <div>
        <h2>Create Course</h2>
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

        {/* Dropdown för att välja teacher */}
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

        <button onClick={createCourse}>Create</button>
      </div>

      {/* LIST COURSES */}
      <h2>All Courses</h2>
      <ul>
        {courses.map((c) => (
          <li key={c.id}>
            {c.title} ({c.courseCode}) — Teacher:{" "}
            {c.teacher ? `${c.teacher.firstName} ${c.teacher.lastName}` : "Unknown"}
            <button onClick={() => fetchCourseDetail(c.id)}>Details</button>
            <button onClick={() => deleteCourse(c.id)}>Delete</button>
          </li>
        ))}
      </ul>

      {/* DETAIL & UPDATE */}
      {selectedCourse && (
        <div>
          <h2>Course Details</h2>
          <p>ID: {selectedCourse.id}</p>
          <p>Title: {selectedCourse.title}</p>
          <p>Description: {selectedCourse.description}</p>
          <p>Course Code: {selectedCourse.courseCode}</p>
          <p>
            Teacher:{" "}
            {selectedCourse.teacher
              ? `${selectedCourse.teacher.firstName} ${selectedCourse.teacher.lastName}`
              : "Unknown"}
          </p>

          <h3>Course Sessions</h3>
          <ul>
            {selectedCourse.courseSessions?.map((cs) => (
              <li key={cs.id}>
                {new Date(cs.startDate).toLocaleDateString()} -{" "}
                {new Date(cs.endDate).toLocaleDateString()} | Enrollments:{" "}
                {cs.enrollments?.length || 0}
              </li>
            ))}
          </ul>

          <h3>Update Course</h3>
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

          <button onClick={() => updateCourse(selectedCourse.id)}>Update</button>
          <button onClick={() => setSelectedCourse(null)}>Close</button>
        </div>
      )}
    </div>
  );
};

export default Courses;
