// src/api/taskService.ts

import type { ManagedTaskDto } from '../schemas/ManagedTaskDto';
import { API_BASE_URL } from '../config';

export async function getTasksForUser(userId: number): Promise<ManagedTaskDto[]> {
  const response = await fetch(`${API_BASE_URL}/task/user/${userId}`);
  if (!response.ok) {
    throw new Error('Failed to fetch tasks');
  }
  return response.json();
}

export async function createTask(task: ManagedTaskDto): Promise<ManagedTaskDto> {
  const response = await fetch(`${API_BASE_URL}/task`, {
    method: 'POST',
    headers: {
      'Content-Type': 'application/json',
    },
    body: JSON.stringify(task),
  });
  if (!response.ok) {
    throw new Error('Failed to create task');
  }
  return response.json();
}