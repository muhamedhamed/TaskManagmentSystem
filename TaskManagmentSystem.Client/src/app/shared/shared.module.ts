import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { DatePipe } from '@angular/common';


@NgModule({
  declarations: [DatePipe],
  imports: [
    CommonModule
  ],
  exports: [DatePipe],
})
export class SharedModule { }
