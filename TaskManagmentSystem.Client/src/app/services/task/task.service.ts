import { Injectable } from '@angular/core';
import { Task } from '../../interfaces/task';
import { Observable, throwError } from 'rxjs';
import { catchError, map } from 'rxjs/operators';
import { HttpClient, HttpHeaders } from '@angular/common/http';

@Injectable({
  providedIn: 'root',
})
export class TaskService {
  constructor(private http: HttpClient) {}

  public apiUrl = 'http://localhost:5193';

  protected taskList: Task[] = [];

  getAllTasks(): Observable<Task[]> {
    const url = `${this.apiUrl}/api/tasks`;

    return this.http.get<Task[]>(url).pipe(
      catchError((error) => {
        console.error('Error fetching tasks:', error);
        return throwError('Failed to fetch tasks.');
      })
    );
  }

  getTaskById(taskId: string): Observable<Task | undefined> {
    const url = `${this.apiUrl}/api/tasks/${taskId}`;

    return this.http.get<Task>(url).pipe(
      catchError((error) => {
        console.error(
          `Failed to fetch task with Id: ${taskId}. Error: `,
          error
        );
        return throwError(`Failed to fetch task with Id: ${taskId}`);
      })
    );
  }

  addNewTask(newTask: any): Observable<Task | undefined> {
    debugger;
    const headers = new HttpHeaders({
      'Content-Type': 'application/json',
    });

    return this.http.post(`${this.apiUrl}/api/tasks`, newTask, { headers })
      .pipe(
        map((addedTask: any) => addedTask),
        catchError((error) => {
          console.error('Error adding a new task:', error);
          return throwError('Failed to add a new task.');
        })
      );
  }

  updateTask(taskId: string, updatedTask: any): Observable<Task | undefined> {
    const headers = new HttpHeaders({
      'Content-Type': 'application/json',
    });

    const url = `${this.apiUrl}/api/tasks/${taskId}`;

    return this.http.put(url, updatedTask, { headers }).pipe(
      map((editedTask: any) => editedTask),
      catchError((error) => {
        console.error(`Error updating task with ID ${taskId}:`, error);
        return throwError('Failed to update task.');
      })
    );
  }

  deleteTask(taskId: string): Observable<void> {
    debugger
    const url = `${this.apiUrl}/api/tasks/${taskId}`;

    return this.http.delete<void>(url).pipe(
      catchError((error) => {
        console.error(`Error deleting task with ID ${taskId}:`, error);
        return throwError('Failed to delete task.');
      })
    );
  }
}

