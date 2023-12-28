import { Component, OnInit, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { TaskListComponent } from '../../task-list/task-list.component';
import { Task } from '../../../interfaces/task';
import { TaskService } from '../../../services/task/task.service';
import { RouterModule } from '@angular/router';

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [CommonModule, TaskListComponent, RouterModule],
  templateUrl: './home.component.html',
  styleUrl: './home.component.css',
})
export class HomeComponent implements OnInit {
  taskList: Task[] = [];
  taskService: TaskService = inject(TaskService);
  filteredTaskList: Task[] = [];

  constructor() {}

  ngOnInit() {
    this.fetchTasks();
  }

  private fetchTasks() {
    this.taskService.getAllTasks().subscribe(
      (taskList) => {
        this.taskList = taskList;
        this.filteredTaskList = taskList;
      },
      (error) => {
        console.error('Error fetching tasks:', error);
      }
    );
  }

  filterResults(text: string) {
    if (!text) {
      this.filteredTaskList = this.taskList;
      return;
    }

    this.filteredTaskList = this.taskList.filter((task) =>
      task?.description.toLowerCase().includes(text.toLowerCase())
    );
  }

  addNewTask(){}
}
