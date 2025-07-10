import { API_BASE_URL } from '../config';
import type { TaskType } from '../interfaces/TaskType';

export async function getTaskTypes(): Promise<TaskType[]> {
  const response = await fetch(`${API_BASE_URL}/tasktype`);
  if (!response.ok) throw new Error('Failed to fetch task types');
  return response.json();
} 