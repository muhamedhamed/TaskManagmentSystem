import { Component, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ActivatedRoute } from '@angular/router';
import { TaskService } from '../../services/task/task.service';
import { Task } from '../../interfaces/task';
import { RouterModule } from '@angular/router';
import { FormControl, FormGroup, ReactiveFormsModule } from '@angular/forms';

@Component({
  selector: 'app-task-detail',
  standalone: true,
  imports: [CommonModule, RouterModule, ReactiveFormsModule],
  templateUrl: './task-detail.component.html',
  styleUrl: './task-detail.component.css',
})
export class TaskDetailComponent {
  route: ActivatedRoute = inject(ActivatedRoute);
  taskService = inject(TaskService);
  task: Task | undefined;

  constructor() {
    const taskId = this.route.snapshot.params['taskId'];
    this.taskService.getTaskById(taskId).then(task =>{
      this.task =task;
    });
  }
}
