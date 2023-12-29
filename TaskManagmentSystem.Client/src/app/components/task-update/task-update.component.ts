import { Component, OnInit, inject } from '@angular/core';
import { FormGroup, FormControl, ReactiveFormsModule } from '@angular/forms';
import { TaskService } from '../../services/task/task.service';
import { Task } from '../../interfaces/task';
import { ActivatedRoute, Router, RouterModule } from '@angular/router';

@Component({
  selector: 'app-task-update',
  standalone: true,
  imports: [ReactiveFormsModule, RouterModule],
  templateUrl: './task-update.component.html',
  styleUrl: './task-update.component.css',
})
export class TaskUpdateComponent implements OnInit {
  taskId: string;
  task: Task | undefined;

  constructor() {
    this.taskId = '';
  }
  route: ActivatedRoute = inject(ActivatedRoute);
  router: Router = inject(Router);
  taskService: TaskService = inject(TaskService);

  ngOnInit() {
    this.displayTaskDetails();
  }

  displayTaskDetails() {
    debugger;
    this.taskId = this.route.snapshot.params['taskId'];
    this.taskService.getTaskById(this.taskId).subscribe(
      (task) => {
        this.task = task;
        // Prepopulate the form with current task values
        this.addTaskForm.setValue({
          title: this.task?.title,
          description: this.task?.description,
        });
      },
      (error) => {
        console.error('Error fetching task:', error);
      }
    );
  }

  addTaskForm = new FormGroup({
    title: new FormControl(),
    description: new FormControl(),
  });

  updateTask(taskId: string) {
    debugger;
    if (this.addTaskForm.valid) {
      const now = new Date();

      const newUpdatedTask: Task = {
        taskId: taskId,
        title: this.addTaskForm.value.title,
        description: this.addTaskForm.value.description,
        dueDate: this.task?.dueDate!,
        createdAt: this.task?.createdAt!,
        updatedAt: now,
        isActive: true,
        taskOwnerId: this.task?.taskOwnerId!,
      };

      this.taskService.updateTask(this.taskId, newUpdatedTask).subscribe(
        (addedTask) => {
          if (addedTask) {
            console.log('Task updated:', addedTask);
            this.addTaskForm.reset(); // Reset the form
          } else {
            console.error('Failed to update task.');
          }
          this.router.navigate(['/']);
        },
        (error) => {
          console.error('Error updating the task:', error);
        }
      );

    } else {
      console.error('Form is invalid. Please provide valid inputs.');
    }
  }
}
