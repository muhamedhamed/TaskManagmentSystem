import { Component, OnInit, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ActivatedRoute, Router } from '@angular/router';
import { TaskService } from '../../services/task/task.service';
import { Task } from '../../interfaces/task';
import { RouterModule } from '@angular/router';
import { ReactiveFormsModule } from '@angular/forms';

@Component({
  selector: 'app-task-detail',
  standalone: true,
  imports: [CommonModule, RouterModule, ReactiveFormsModule],
  providers: [TaskService],
  templateUrl: './task-detail.component.html',
  styleUrl: './task-detail.component.css',
})
export class TaskDetailComponent implements OnInit {
  taskId: string;
  route: ActivatedRoute = inject(ActivatedRoute);
  router: Router = inject(Router);
  taskService = inject(TaskService);
  task: Task | undefined;

  constructor() {
    this.taskId = '';
  }
  ngOnInit() {
    this.displayTaskDetails();
  }
  displayTaskDetails() {
    this.taskId = this.route.snapshot.params['taskId'];
    this.taskService.getTaskById(this.taskId).subscribe(
      (task) => {
        this.task = task;
      },
      (error) => {
        console.error('Error fetching tasks:', error);
      }
    );
  }

  updateTask(taskId: string) {}

  deleteTask(taskId: string) {
    this.taskService.deleteTask(this.taskId).subscribe(
      () => {
        console.log(`Task with ID ${this.taskId} deleted successfully.`);
        // I should Add refresh the list here
        // I shoud navigate back to the list task componant
        this.router.navigate(['/']);
      },
      (error) => {
        console.error(`Error deleting task with ID ${taskId}:`, error);
      }
    );
  }
}
