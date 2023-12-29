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
  task: Task | undefined;

  route: ActivatedRoute = inject(ActivatedRoute);
  router: Router = inject(Router);
  taskService: TaskService = inject(TaskService);

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

  deleteTask(taskId: string) {
    this.taskService.deleteTask(this.taskId).subscribe(
      () => {
        console.log(`Task with ID ${this.taskId} deleted successfully.`);
        this.router.navigate(['/']);
      },
      (error) => {
        console.error(`Error deleting task with ID ${taskId}:`, error);
      }
    );
  }
}
