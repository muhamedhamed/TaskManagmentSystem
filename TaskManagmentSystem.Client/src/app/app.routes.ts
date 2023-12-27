import { Routes } from '@angular/router';
import { TaskListComponent } from './components/task-list/task-list.component';
import { TaskDetailComponent } from './components/task-detail/task-detail.component';
import { HomeComponent } from './components/home/home/home.component';
import { TaskFormComponent } from './components/task-form/task-form.component';

const routeConfig: Routes = [
    {
      path: '',
      component: HomeComponent,
      title: 'Task List'
    },
    {
      path: 'details/:taskId',
      component: TaskDetailComponent,
      title: 'Task Details'
    },
    {
        path: 'add',
        component: TaskFormComponent,
        title: 'Add Task'
      }
  ];
  
  export default routeConfig;
