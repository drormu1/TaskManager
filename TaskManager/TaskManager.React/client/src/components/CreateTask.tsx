import React, { useState, useEffect } from 'react';
import { getUsers } from '../api/userService';
import { getTaskTypes } from '../api/taskTypeService';
import type { User } from '../interfaces/User';
import type { TaskType } from '../interfaces/TaskType';

export const CreateTask: React.FC<{ onCreate: (task: any) => void; onCancel?: () => void; }> = ({ onCreate, onCancel }) => {
  const [users, setUsers] = useState<User[]>([]);
  const [taskTypes, setTaskTypes] = useState<TaskType[]>([]);

  const [form, setForm] = useState({
    updatedById: '',
    title: '',
    description: '',
    taskTypeId: '',
    assignedUserId: '',
    taskDataJson: '{}'
  });

  useEffect(() => {
    getUsers().then(data => {
      console.log('users:', data);
      setUsers(data);
    });
    getTaskTypes().then(data => {
      console.log('taskTypes:', data);
      setTaskTypes(data);
    });
  }, []);

  const handleChange = (e: React.ChangeEvent<HTMLInputElement | HTMLSelectElement | HTMLTextAreaElement>) => {
    const { name, value } = e.target;
    setForm(prev => ({ ...prev, [name]: value }));
  };

  const handleSubmit = (e: React.FormEvent) => {
    e.preventDefault();
    if (!form.title || !form.description || !form.taskTypeId || !form.assignedUserId || !form.updatedById) {
      alert('Please fill all required fields');
      return;
    }
    onCreate(form);
    setForm({
      updatedById: '',
      title: '',
      description: '',
      taskTypeId: '',
      assignedUserId: '',
      taskDataJson: '{}'
    });
  };

  return (
    <form onSubmit={handleSubmit} style={{ maxWidth: 700, margin: '0 auto', background: '#fff', padding: 24, borderRadius: 8, boxShadow: '0 2px 8px #0001' }}>
      <h1 style={{ textAlign: 'left', marginBottom: 24 }}>Create New Task</h1>
      <div style={{ marginBottom: 16 }}>
        <label>
          Select task creator<br />
          <select
            name="updatedById"
            value={form.updatedById}
            onChange={handleChange}
            required
            style={{ width: '100%', padding: 8, fontSize: 16 }}
          >
            <option value="">Select...</option>
            {users.map((user: any) => (
              <option key={user.id} value={user.id}>{user.name}</option>
            ))}
          </select>
        </label>
      </div>
      <div style={{ marginBottom: 16 }}>
        <label>
          Title<br />
          <input
            type="text"
            name="title"
            value={form.title}
            onChange={handleChange}
            required
            style={{ width: '100%', padding: 8, fontSize: 16 }}
          />
        </label>
      </div>
      <div style={{ marginBottom: 16 }}>
        <label>
          Description<br />
          <textarea
            name="description"
            value={form.description}
            onChange={handleChange}
            required
            rows={3}
            style={{ width: '100%', padding: 8, fontSize: 16 }}
          />
        </label>
      </div>
      <div style={{ display: 'flex', gap: 16, marginBottom: 16 }}>
        <div style={{ flex: 1 }}>
          <label>
            Task Type<br />
            <select
              name="taskTypeId"
              value={form.taskTypeId}
              onChange={handleChange}
              required
              style={{ width: '100%', padding: 8, fontSize: 16 }}
            >
              <option value="">Select...</option>
              {taskTypes.map((type: any) => (
                <option key={type.id} value={type.id}>{type.name}</option>
              ))}
            </select>
          </label>
        </div>
        <div style={{ flex: 1 }}>
          <label>
            Assigned User<br />
            <select
              name="assignedUserId"
              value={form.assignedUserId}
              onChange={handleChange}
              required
              style={{ width: '100%', padding: 8, fontSize: 16 }}
            >
              <option value="">Select...</option>
              {users.map((user: any) => (
                <option key={user.id} value={user.id}>{user.name}</option>
              ))}
            </select>
          </label>
        </div>
      </div>
      <div style={{ marginBottom: 16 }}>
        <label>
          Task Data (JSON)<br />
          <textarea
            name="taskDataJson"
            value={form.taskDataJson}
            onChange={handleChange}
            rows={4}
            style={{ width: '100%', padding: 8, fontSize: 16 }}
          />
        </label>
      </div>
      <div style={{ display: 'flex', gap: 12 }}>
        <button type="submit" style={{ padding: '8px 32px', fontWeight: 'bold', borderRadius: 4, background: '#1976d2', color: '#fff', border: 'none', fontSize: 16 }}>
          Create
        </button>
        {onCancel && (
          <button type="button" onClick={onCancel} style={{ padding: '8px 32px', fontWeight: 'bold', borderRadius: 4, background: '#888', color: '#fff', border: 'none', fontSize: 16 }}>
            Cancel
          </button>
        )}
      </div>
    </form>
  );
};