import { Injectable } from '@angular/core';
import { Task } from '../../interfaces/task';

@Injectable({
  providedIn: 'root',
})
export class TaskService {
  private apiUrl: string;

  constructor() {
    this.apiUrl = 'http://localhost:5193';
  }

  protected taskList: Task[] = [];

  async getAllTasks(): Promise<Task[]> {
    try {
      const data = await fetch(`${this.apiUrl}/api/tasks`, {
        redirect: 'follow',
      });

      if (!data.ok) {
        throw new Error(`Failed to fetch tasks. Status: ${data.status}`);
      }

      return (await data.json()) ?? [];
    } catch (error) {
      console.error('Error fetching tasks:', error);
      return [];
    }
  }

  async getTaskById(taskId: string): Promise<Task | undefined> {
    try {
      const data = await fetch(`${this.apiUrl}/api/tasks/${taskId}`);

      if (!data.ok) {
        console.error(
          `Failed to fetch task with Id: ${taskId}. Status: ${data.status}`
        );
        // Handle the error appropriately, e.g., return undefined or throw a custom error.
        return undefined;
      }
      const jsonData = await data.json();

      // Ensure jsonData is an object before returning
      if (typeof jsonData === 'object' && jsonData !== null) {
        return jsonData as Task;
      } else {
        console.error(`Invalid JSON data for task with Id: ${taskId}`);
        // Handle the error appropriately, e.g., return undefined or throw a custom error.
        return undefined;
      }
    } catch (error) {
      console.error('Error fetching task:', error);
      // Handle the error appropriately, e.g., return undefined or throw a custom error.
      return undefined;
    }
  }

  submitTask(title: string, description: string) {
    console.log(
      `Task Owner information: firstName: ${title}, lastName: ${description}`
    );
  }

  async addNewTask(newTask: Task): Promise<Task | undefined> {
    try {
      const response = await fetch(`${this.apiUrl}/api/tasks`, {
        method: 'POST',
        headers: {
          'Content-Type': 'application/json',
        },
        body: JSON.stringify(newTask),
      });

      if (!response.ok) {
        console.error(`Failed to add a new task. Status: ${response.status}`);
        // Handle the error appropriately, e.g., return undefined or throw a custom error.
        return undefined;
      }

      const addedTask = await response.json();

      // Ensure addedTask is an object before returning
      if (typeof addedTask === 'object' && addedTask !== null) {
        return addedTask as Task;
      } else {
        console.error('Invalid JSON data for the added task');
        // Handle the error appropriately, e.g., return undefined or throw a custom error.
        return undefined;
      }
    } catch (error) {
      console.error('Error adding a new task:', error);
      // Handle the error appropriately, e.g., return undefined or throw a custom error.
      return undefined;
    }
  }
}
