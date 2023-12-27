import { Component,Input} from '@angular/core';
import { CommonModule } from '@angular/common';
import { Task } from '../../interfaces/task';
import { RouterModule } from '@angular/router';

@Component({
  selector: 'app-task-list',
  standalone: true,
  imports: [CommonModule,RouterModule],
  templateUrl: './task-list.component.html',
  styleUrls: ['./task-list.component.css'],
})

export class TaskListComponent {

  @Input() task!: Task;
}
