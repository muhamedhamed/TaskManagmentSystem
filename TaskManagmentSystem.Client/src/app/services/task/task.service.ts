import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Task } from '../../models/task/task.model';

@Injectable({
  providedIn: 'root'
})
export class TaskService {

  // private apiUrl : string;

  // constructor(private http: HttpClient) { 
  //   this.apiUrl= 'http://localhost:5193'; 
  // }

  // getTasks(): Observable<Task[]> {
  //   return this.http.get<Task[]>(`${this.apiUrl}/api/tasks`);
  // }
}

