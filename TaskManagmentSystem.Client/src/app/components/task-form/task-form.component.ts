import { Component, inject } from '@angular/core';
import { TaskService } from '../../services/task/task.service';
import { Task } from '../../interfaces/task';
import { RouterModule } from '@angular/router';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatInputModule } from '@angular/material/input';
import {FormControl,FormGroup,ReactiveFormsModule} from '@angular/forms';


@Component({
  selector: 'app-task-form',
  standalone: true,
  imports: [
    ReactiveFormsModule,
    RouterModule,
    MatDatepickerModule,
    MatInputModule,
  ],
  templateUrl: './task-form.component.html',
  styleUrl: './task-form.component.css',
})
export class TaskFormComponent {
  taskService = inject(TaskService);

  addTaskForm = new FormGroup({
    title: new FormControl(),
    description: new FormControl(),
  });

  submitTask() {
    debugger;
    // Check if the form is valid before submitting
    if (this.addTaskForm.valid) {
      const now = new Date();
      // const id =uuidv4();

      const newTask: Task = {
        taskId: 'dbb63dbb-51a3-4d3e-93a4-2e8f04a8d3b5',
        title: this.addTaskForm.value.title,
        description: this.addTaskForm.value.description,
        dueDate: now,
        createdAt: now,
        updatedAt: now,
        isActive: true,
        taskOwnerId: 'dbb63dbb-51a3-4d3e-93a4-2e8f04a8d3b4',
      };
      
      this.taskService.addNewTask(newTask).subscribe(
        (addedTask) => {
          if (addedTask) {
            console.log('New task added:', addedTask);
            this.addTaskForm.reset(); // Reset the form
          } else {
            console.error('Failed to add a new task.');
          }
        },
        (error) => {
          console.error('Error adding a new task:', error);
        }
      );
    } else {
      console.error('Form is invalid. Please provide valid inputs.');
    }
  }

  updateTask() {
    // Implement logic for updating a task
  }

  deleteTask() {
    // Implement logic for deleting a task
  }
}
function uuidv4(): string {
  throw new Error('Function not implemented.');
}

