import { Component, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { TaskService } from '../../services/task/task.service';
import { Task } from '../../interfaces/task';
import {
  FormControl,
  FormGroup,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';

@Component({
  selector: 'app-task-form',
  standalone: true,
  imports: [ReactiveFormsModule],
  templateUrl: './task-form.component.html',
  styleUrl: './task-form.component.css',
})
export class TaskFormComponent {
  taskService = inject(TaskService);
  task: Task | undefined;

  addTask = new FormGroup({
    title: new FormControl(''),
    description: new FormControl(''),
  });

  newTask: Task = {
    taskId: '', // generate one or handle it server-side
    title: '',
    description: '',
    dueDate: new Date(),
    createdAt: new Date(),
    updatedAt: new Date(),
    isActive: true,
    taskOwnerId: 'dbb63dbb-51a3-4d3e-93a4-2e8f04a8d3b4',
    // user: {}, // Assuming you have a User model
  };

  addTaskForm = new FormGroup({
    title: new FormControl(this.newTask.title, Validators.required),
    description: new FormControl(this.newTask.description, Validators.required),
    // Add other form controls with their default values and validators
  });

  submitTask() {
    // Check if the form is valid before submitting
    if (this.addTaskForm.valid) {
      const titleValue = this.addTaskForm.value.title as string;
      const descriptionValue = this.addTaskForm.value.description as string;

      if (titleValue !== undefined && descriptionValue !== undefined) {
        this.newTask.title = titleValue;
        this.newTask.description = descriptionValue;

        // Call the addTask method from the service
        this.taskService.addNewTask(this.newTask).then((addedTask) => {
          if (addedTask) {
            console.log('New task added:', addedTask);
            // Optionally, you can reset the form or perform other actions
            this.addTaskForm.reset(); // Reset the form
          } else {
            console.error('Failed to add a new task.');
            // Handle the failure appropriately, e.g., show an error message
          }
        });
      } else {
        console.error('Form is invalid. Please provide valid inputs.');
        // Optionally, you can show a user-friendly error message
      }
    }
  }
}
