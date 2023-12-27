import { Component, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { TaskListComponent } from '../../task-list/task-list.component';
import { Task } from '../../../interfaces/task';
import { TaskService } from '../../../services/task/task.service';
import { RouterModule } from '@angular/router';

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [CommonModule, TaskListComponent,RouterModule],
  templateUrl: './home.component.html',
  styleUrl: './home.component.css',
})
export class HomeComponent {
  taskList: Task[] = [];
  taskService: TaskService =inject(TaskService)
  filteredTaskList: Task[]=[];
  
  constructor() {
    this.taskService.getAllTasks().then((taskList: Task[] = [])=>{
      this.taskList =taskList;
      this.filteredTaskList=taskList;
    });
  }

  filterResults(text: string) {
    if (!text) {
      this.filteredTaskList = this.taskList;
      return;
    }
  
    this.filteredTaskList = this.taskList.filter(
      task => task?.description.toLowerCase().includes(text.toLowerCase())
    );
  }
}
