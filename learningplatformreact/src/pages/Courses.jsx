import React, { useState, useEffect } from "react";

const Courses = () => {
  const [courses, setCourses] = useState([]);
  const [selectedCourse, setSelectedCourse] = useState(null);
  const [form, setForm] = useState({
    title: "",
    description: "",
    courseCode: "",
    teacherId: "",
  });

  const apiUrl = "https://localhost:7240/api/courses";

  const fetchCourses = async () => {
    const res = await fetch(apiUrl);
    const data = await res.json();
    setCourses(data);
  };

  const fetchCourseDetail = async (id) => {
    const res = await fetch(`${apiUrl}/${id}`);
    const data = await res.json();
    setSelectedCourse(data);
    setForm({
      title: data.title || "",
      description: data.description || "",
      courseCode: data.courseCode || "",
      teacherId: data.teacherId || "",
    });
  };

  const createCourse = async () => {
    await fetch(apiUrl, { method: "POST", headers: { "Content-Type": "application/json" }, body: JSON.stringify(form) });
    setForm({ title: "", description: "", courseCode: "", teacherId: "" });
    fetchCourses();
  };

  const updateCourse = async (id) => {
    await fetch(`${apiUrl}/${id}`, { method: "PUT", headers: { "Content-Type": "application/json" }, body: JSON.stringify(form) });
    setSelectedCourse(null);
    fetchCourses();
  };

  const deleteCourse = async (id) => {
    await fetch(`${apiUrl}/${id}`, { method: "DELETE" });
    fetchCourses();
  };

  useEffect(() => { fetchCourses(); }, []);

  return (
    <div>
      <h1>Courses</h1>

      {/* CREATE */}
      <div>
        <h2>Create Course</h2>
        <input placeholder="Title" value={form.title} onChange={e => setForm({ ...form, title: e.target.value })}/>
        <input placeholder="Description" value={form.description} onChange={e => setForm({ ...form, description: e.target.value })}/>
        <input placeholder="Course Code" value={form.courseCode} onChange={e => setForm({ ...form, courseCode: e.target.value })}/>
        <input placeholder="Teacher ID" value={form.teacherId} onChange={e => setForm({ ...form, teacherId: e.target.value })}/>
        <button onClick={createCourse}>Create</button>
      </div>

      {/* LIST */}
      <h2>All Courses</h2>
      <ul>
        {courses.map(c => (
          <li key={c.id}>
            {c.title} ({c.courseCode})
            <button onClick={() => fetchCourseDetail(c.id)}>Details</button>
            <button onClick={() => deleteCourse(c.id)}>Delete</button>
          </li>
        ))}
      </ul>

      {/* DETAIL */}
      {selectedCourse && (
        <div>
          <h2>Course Details</h2>
          <p>ID: {selectedCourse.id}</p>
          <p>Title: {selectedCourse.title}</p>
          <p>Description: {selectedCourse.description}</p>
          <p>Course Code: {selectedCourse.courseCode}</p>
          <p>Teacher ID: {selectedCourse.teacherId}</p>

          <h3>Course Sessions</h3>
          <ul>
            {selectedCourse.courseSessions?.map(cs => (
              <li key={cs.id}>
                {new Date(cs.startDate).toLocaleDateString()} - {new Date(cs.endDate).toLocaleDateString()} | Enrollments: {cs.enrollments?.length || 0}
              </li>
            ))}
          </ul>

          {/* UPDATE */}
          <h3>Update Course</h3>
          <input placeholder="Title" value={form.title} onChange={e => setForm({ ...form, title: e.target.value })}/>
          <input placeholder="Description" value={form.description} onChange={e => setForm({ ...form, description: e.target.value })}/>
          <input placeholder="Course Code" value={form.courseCode} onChange={e => setForm({ ...form, courseCode: e.target.value })}/>
          <input placeholder="Teacher ID" value={form.teacherId} onChange={e => setForm({ ...form, teacherId: e.target.value })}/>
          <button onClick={() => updateCourse(selectedCourse.id)}>Update</button>
          <button onClick={() => setSelectedCourse(null)}>Close</button>
        </div>
      )}
    </div>
  );
};

export default Courses;
