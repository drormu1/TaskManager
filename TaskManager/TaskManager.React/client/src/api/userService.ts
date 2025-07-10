import { API_BASE_URL } from '../config';
import type { User } from '../interfaces/User';

  export async function getUsers(): Promise<User[]> {
    const response = await fetch(`${API_BASE_URL}/user`);
    if (!response.ok) throw new Error('Failed to fetch users');
    return response.json();
  } 