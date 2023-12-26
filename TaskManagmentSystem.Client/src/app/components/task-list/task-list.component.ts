import { Component, OnInit, ViewEncapsulation } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';
import { TaskDetailComponent } from '../task-detail/task-detail.component';

@Component({
  selector: 'app-task-list',
  encapsulation: ViewEncapsulation.None,
  standalone: true,
  imports: [HttpClientModule,
    TaskDetailComponent],
  templateUrl: './task-list.component.html',
  styleUrls: ['./task-list.component.css']
})
export class TaskListComponent {
  
//   tasks: Task[] = [];
  
//   constructor(private taskService: TaskService) {}
 
//   ngOnInit(): void {
//     this.taskService.getTasks().subscribe(tasks => {
//       this.tasks = tasks;
//     });
//   }
}
