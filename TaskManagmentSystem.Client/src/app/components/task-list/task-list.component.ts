import { Component,Input, inject} from '@angular/core';
import { CommonModule } from '@angular/common';
import { Task } from '../../interfaces/task';
import { RouterModule } from '@angular/router';
import { TaskService } from '../../services/task/task.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-task-list',
  standalone: true,
  imports: [CommonModule,RouterModule],
  templateUrl: './task-list.component.html',
  styleUrls: ['./task-list.component.css'],
})

export class TaskListComponent {
  router: Router = inject(Router);
  @Input() task!: Task;
  tasks: Task[] = [];

  taskService = inject(TaskService);

  updateTask(taskId:string) {

  }

  deleteTask(taskId: string) {
    debugger
    this.taskService.deleteTask(taskId).subscribe(
      () => {
        console.log(`Task with ID ${taskId} deleted successfully.`);
        //Need to check how the auto refresh should apply
        //Should be auto refresh
        this.getAllTasks();
        this.router.navigate(['/']);
      },
      (error) => {
        console.error(`Error deleting task with ID ${taskId}:`, error);
      }
    );
  }

  getAllTasks() {
    this.taskService.getAllTasks().subscribe(
      (tasks) => {
        this.tasks = tasks;
      },
      (error) => {
        console.error('Error fetching tasks:', error);
      }
    );
  }
}
